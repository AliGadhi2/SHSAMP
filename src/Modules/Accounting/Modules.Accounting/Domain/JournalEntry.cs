using FSH.Framework.Core.Domain;
 

namespace FSH.Modules.Accounting.Domain;
public sealed class JournalEntry:AggregateRoot<Guid>, ISoftDeletable
{
    public long EntryNumber { get;  set; } 
     public long RowNumber { get; private set; } 
     public Guid EntryParentId { get;  set; }
    public DateTime EntryDate { get; private set; }
    public string? Description { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
    public Guid DocumentType { get;  set; }
public bool ISDepanded { get; private set; }=true;
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedOnUtc { get; private set; }
    public string? DeletedBy { get; private set; }
    public TimeSpan? EnterTime { get; set; }
    public ICollection<JournalLine> JournalLines { get; private set; } = new List<JournalLine>();
     
    
    public uint RowVersion { get; set; }  

    private JournalEntry() { }

    public static JournalEntry Create( DateTime entryDate, string? description, Guid doctypeId, bool isDepanded,long rowNumber)
    {
      

        return new JournalEntry
        {
            Id = Guid.CreateVersion7(),
            RowNumber = rowNumber,
          
            EntryDate = entryDate,
            Description = description?.Trim(),
            DocumentType = doctypeId,
            ISDepanded = isDepanded,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void Update(  DateTime entryDate, string? description, bool isDepanded)
    {
       

        ISDepanded = isDepanded;
        EntryDate = entryDate;
        Description = description?.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (!IsDeleted) return;
        IsDeleted = false;
        DeletedOnUtc = null;
        DeletedBy = null;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}