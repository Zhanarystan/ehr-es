namespace API.Core;

public class Event
{
    public virtual long AggregateId { get; init; }
    public virtual string Type { get; init; }
    public virtual int Version { get; init; }
}

public class EventEntity
{   
    public long Id { get; set; }
    public long AggregateId { get; set; }
    public int Version { get; set; }
    public string Type { get; set; }
    public string Data { get; set; }
}


