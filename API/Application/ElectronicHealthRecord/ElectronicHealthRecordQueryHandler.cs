using API.Core;
using API.Domain;
using API.Interfaces;
using API.Persistence;
using static API.Domain.ElectronicHealthRecordQuery;

namespace API.Application;

public class ElectronicHealthRecordQueryHandler : IElectronicHealthRecordQueryHandler
{
    private readonly EventStore _store;

    public ElectronicHealthRecordQueryHandler(EventStore store)
    {
        _store = store;
    }

    public object Handle(Query query) =>
        query switch 
        {
            FindQuery q => Find(q)
        };

    public ElectronicHealthRecord Find(FindQuery query) =>
        _store
            .GetEventsForAggregate(query.Id)
            .Aggregate(
                ElectronicHealthRecord.Empty, 
                (ehr, e) => ElectronicHealthRecordAggregator.Aggregate(e)(ehr));
}