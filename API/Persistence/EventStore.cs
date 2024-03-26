using System.Reflection;
using API.Core;
using API.Domain;
using SnowflakeGenerator;

namespace API.Persistence;

public class EventStore
{
    private readonly DataContext _dbContext;

    public EventStore(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public int SaveEvent(Event @event)
    {
        var snowflake = new Snowflake();
        var maxVersion = _dbContext.Events
            .Where(e => e.AggregateId == @event.AggregateId)
            .Max(e => (int?)e.Version) ?? 0;
            
        var eventEntity = new EventEntity
        {
            AggregateId = @event.AggregateId,
            Version = maxVersion + 1,
            Type = @event.Type,
            Data = Newtonsoft.Json.JsonConvert.SerializeObject(@event)
        };

        _dbContext.Events.Add(eventEntity);
        
        return _dbContext.SaveChanges();
    }

    public IEnumerable<Event> GetEventsForAggregate(long aggregateId)
    {
        return _dbContext.Events
            .Where(e => e.AggregateId == aggregateId)
            .OrderBy(e => e.Version)
            .Select(e => Newtonsoft.Json.JsonConvert.DeserializeObject(e.Data, ExtractType(e.Type)))
            .Cast<Event>()
            .ToList();
    }

    public static Type ExtractType(string type) =>
        type switch 
        {
            ElectronicHealthRecordCreated.TYPE => typeof(ElectronicHealthRecordCreated),
            DiagnosisAdded.TYPE => typeof(DiagnosisAdded),
            "" => null
        };
}