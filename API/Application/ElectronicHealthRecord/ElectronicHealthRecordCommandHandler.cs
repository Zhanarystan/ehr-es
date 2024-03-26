using API.Core;
using API.Domain;
using API.Interfaces;
using API.Persistence;
using Microsoft.IdentityModel.Tokens;
using SnowflakeGenerator;
using static API.Domain.ElectronicHealthRecordCommand;

namespace API.Application;

public class ElectronicHealthRecordCommandHandler : IElectronicHealthRecordCommandHandler
{
    private readonly EventStore _store;

    public ElectronicHealthRecordCommandHandler(EventStore store) 
    {
        _store = store;
    }
    public ElectronicHealthRecord Handle(Command command) =>
        command switch 
        {
            CreateCommand c => HandleCreate(c.Name, c.BirthDate, c.Gender, c.Email),
            AddDiagnosisCommand c => HandleAddDiagnosis(c.EhrId, c.Diagnosis),
            default(Command) => null
        };

    public ElectronicHealthRecord HandleCreate(string name, DateTime birthDate, PatientGender gender, string email)
    {
        var snowflake = new Snowflake();
        long aggregateId = snowflake.NextID();
        var data = new ElectronicHealthRecordCreatedData(name, gender, birthDate, email);
        var newEvent = ElectronicHealthRecordCreated.Construct(aggregateId, data);
        if (_store.SaveEvent(newEvent) <= 0)
            return null;
        return ElectronicHealthRecordAggregator.Aggregate(newEvent)(ElectronicHealthRecord.Empty);
    }

    public ElectronicHealthRecord HandleAddDiagnosis(long ehrId, Diagnosis diagnosis) 
    {
        var events = _store.GetEventsForAggregate(ehrId);
        if (events.IsNullOrEmpty())
            return null;
        var data = new DiagnosisAddedData(diagnosis);
        var newEvent = DiagnosisAdded.Construct(ehrId, data);
        if (_store.SaveEvent(newEvent) <= 0)
            return null;
        events = events.Append(newEvent);
        var ehr = events.Aggregate(
                    ElectronicHealthRecord.Empty, 
                    (ehr, e) => ElectronicHealthRecordAggregator.Aggregate(e)(ehr));
        return ehr;
    }
}

