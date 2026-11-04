public class CashPaymentService
{
    private readonly AccountingDbContext _dbContext;

    public CashPaymentService(AccountingDbContext dbContext)
    {
        _dbContext = dbContext;
    }
public async Task<Guid> CreateCashPaymentAsync(
    decimal amount, 
    Guid debitAccountId, 
    Guid creditAccountId,
    string description,
    CancellationToken ct)
    
{
    // التحقق من صحة البيانات على مستوى الأعمال
    if (amount <= 0) throw new ArgumentException("المبلغ يجب أن يكون أكبر من 0");
    
    await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

    try
    {
        // 1. إنشاء المستند المصدر
        var cashPayment = new CashPayment
        {
            Id = Guid.CreateVersion7(),
            Amount = amount,
            Description = description,
            CreatedAt = DateTime.UtcNow
        };
        _dbContext.CashPayments.Add(cashPayment);

        // 2. إنشاء القيد المحاسبي (يجب أن يكون متوازناً)
        var journalEntry = new JournalEntry
        {
            Id = Guid.CreateVersion7(),
            DocumentType = DocumentType.CashPayment,
            DocumentId = cashPayment.Id,
            EntryDate = DateTime.UtcNow,
            Description = description
        };

        // إضافة سطري القيد المدين والدائن
        var lines = new List<JournalLine>
        {
            new() { Id = Guid.CreateVersion7(), JournalEntryId = journalEntry.Id, 
                    AccountId = debitAccountId, Debit = amount, Credit = 0 },
            new() { Id = Guid.CreateVersion7(), JournalEntryId = journalEntry.Id, 
                    AccountId = creditAccountId, Debit = 0, Credit = amount }
        };
        
        // التحقق من التوازن (في الذاكرة، للسماح بالاكتشاف المبكر)
        if (lines.Sum(l => l.Debit) != lines.Sum(l => l.Credit))
            throw new InvalidOperationException("القيد غير متوازن، تم إلغاء الإنشاء");

        journalEntry.Lines = lines;
        _dbContext.JournalEntries.Add(journalEntry);
        _dbContext.JournalLines.AddRange(lines);

        // 3. الحفظ والالتزام في نفس المعاملة
        await _dbContext.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return cashPayment.Id;
    }
    catch (DbUpdateException ex)
    {
        await transaction.RollbackAsync(ct);
        // هنا يمكنك تحليل InnerException لتحديد ما إذا كان انتهاك قيد فريد
        throw new InvalidOperationException("فشل إنشاء المستند، تم التراجع عن جميع العمليات", ex);
    }
    catch
    {
        await transaction.RollbackAsync(ct);
        throw;
    }
}
public async Task<bool> DeleteCashPaymentAsync(Guid cashPaymentId, CancellationToken ct)
{
    // أعلى موثوقية: استخدام معاملة صريحة، ووضع جميع العمليات في نفس وحدة العمل
    await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

    try
    {
        // 1. حذف JournalLine أولاً (التابع للـ JournalEntry)
        var journalEntry = await _dbContext.JournalEntries
            .FirstOrDefaultAsync(x => x.DocumentType == DocumentType.CashPayment 
                                   && x.DocumentId == cashPaymentId, ct);

        if (journalEntry != null)
        {
            // حذف الأسطر التابعة يدوياً (لأنه لا يوجد حذف متتالي من قاعدة البيانات)
            var lines = await _dbContext.JournalLines
                .Where(x => x.JournalEntryId == journalEntry.Id)
                .ToListAsync(ct);
            
            _dbContext.JournalLines.RemoveRange(lines);
            _dbContext.JournalEntries.Remove(journalEntry);
        }

        // 2. حذف CashPayment نفسه
        var cashPayment = await _dbContext.CashPayments.FindAsync([cashPaymentId], ct);
        if (cashPayment == null)
        {
            await transaction.RollbackAsync(ct);
            return false; // أو throw NotFoundException
        }
        
        _dbContext.CashPayments.Remove(cashPayment);

        // 3. الحفظ والالتزام (جميع العمليات في نفس المعاملة، إما تنجح جميعاً أو تفشل جميعاً)
        await _dbContext.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);

        return true;
    }
    catch (DbUpdateConcurrencyException ex)
    {
        // تم تعديل البيانات بشكل متزامن (PostgreSQL سيرفع استثناء التزامن)
        await transaction.RollbackAsync(ct);
        // يمكنك إعادة المحاولة مرة واحدة هنا، أو رفع استثناء إلى الطبقة العليا
        throw new InvalidOperationException("تم تعديل البيانات التي حاولت حذفها بواسطة عملية أخرى، يرجى إعادة المحاولة", ex);
    }
    catch
    {
        await transaction.RollbackAsync(ct);
        throw;
    }
}
public async Task<bool> UpdateCashPaymentAsync(
    Guid cashPaymentId,
    decimal newAmount,
    Guid newDebitAccountId,
    Guid newCreditAccountId,
    string newDescription,
    byte[] rowVersion, // للتحقق من التزامن (انظر أدناه)
    CancellationToken ct)
{
    if (newAmount <= 0) throw new ArgumentException("المبلغ يجب أن يكون أكبر من 0");

    await using var transaction = await _dbContext.Database.BeginTransactionAsync(ct);

    try
    {
        // 1. تحميل المستند المصدر
        var cashPayment = await _dbContext.CashPayments
            .FirstOrDefaultAsync(x => x.Id == cashPaymentId, ct);
        if (cashPayment == null) 
        {
            await transaction.RollbackAsync(ct);
            return false;
        }

        // التحقق من التزامن (إذا كنت قد أضفت xmin في PostgreSQL)
        // إذا لم يكن متطابقاً، فهذا يعني أن شخصاً آخر عدّل السجل
        // (يتطلب تكوين xmin كـ RowVersion، انظر الملاحظة أدناه)

        // 2. العثور على JournalEntry المرتبط عبر DocumentType + DocumentId
        var journalEntry = await _dbContext.JournalEntries
            .FirstOrDefaultAsync(x => x.DocumentType == DocumentType.CashPayment 
                                   && x.DocumentId == cashPaymentId, ct);
        if (journalEntry == null)
        {
            await transaction.RollbackAsync(ct);
            throw new InvalidOperationException("لم يتم العثور على القيد المحاسبي المرتبط، قد تكون البيانات غير متسقة");
        }

        // 3. تحميل JournalLine المرتبط
        var lines = await _dbContext.JournalLines
            .Where(x => x.JournalEntryId == journalEntry.Id)
            .ToListAsync(ct);
        if (lines.Count != 2)
        {
            await transaction.RollbackAsync(ct);
            throw new InvalidOperationException($"عدد أسطر القيد غير صحيح ({lines.Count})، لا يمكن التعديل تلقائياً");
        }

        // 4. تحديث الحقول الثلاثة
        // 4.1 تحديث CashPayment
        cashPayment.Amount = newAmount;
        cashPayment.Description = newDescription;
        cashPayment.UpdatedAt = DateTime.UtcNow;

        // 4.2 تحديث JournalEntry
        journalEntry.Description = newDescription;
        journalEntry.UpdatedAt = DateTime.UtcNow;

        // 4.3 تحديث JournalLine (سطر المدين وسطر الدائن)
        var debitLine = lines.First(l => l.Debit > 0);
        var creditLine = lines.First(l => l.Credit > 0);

        debitLine.AccountId = newDebitAccountId;
        debitLine.Debit = newAmount;
        debitLine.Credit = 0;

        creditLine.AccountId = newCreditAccountId;
        creditLine.Credit = newAmount;
        creditLine.Debit = 0;

        // 5. التحقق من التوازن بعد التعديل
        if (lines.Sum(l => l.Debit) != lines.Sum(l => l.Credit))
        {
            await transaction.RollbackAsync(ct);
            throw new InvalidOperationException("القيد غير متوازن بعد التعديل، تم التراجع");
        }

        // 6. الحفظ والالتزام
        await _dbContext.SaveChangesAsync(ct);
        await transaction.CommitAsync(ct);
        return true;
    }
    catch (DbUpdateConcurrencyException ex)
    {
        await transaction.RollbackAsync(ct);
        throw new InvalidOperationException("تم تعديل البيانات بواسطة عملية أخرى، يرجى إعادة المحاولة", ex);
    }
    catch
    {
        await transaction.RollbackAsync(ct);
        throw;
    }
}
}