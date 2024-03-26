using API.Core;

namespace API.Domain;

public record ElectronicHealthRecordCreatedData(
    string Name,
    PatientGender Gender,
    DateTime BirthDate,
    string Email);

public class ElectronicHealthRecordCreated : ElectronicHealthRecordEvent<ElectronicHealthRecordCreatedData>
{ 
    public const string TYPE = "electronic-health-record:created";

    public static ElectronicHealthRecordCreated Construct(long aggregateId, ElectronicHealthRecordCreatedData data) =>
        new ElectronicHealthRecordCreated   
        {
            AggregateId = aggregateId,
            Type = TYPE,
            Version = 1,
            Data = data
        };       
}

