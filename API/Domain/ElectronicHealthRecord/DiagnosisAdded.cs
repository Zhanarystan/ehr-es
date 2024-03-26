using API.Core;

namespace API.Domain;

public record DiagnosisAddedData(
    Diagnosis Diagnosis);

public class DiagnosisAdded : ElectronicHealthRecordEvent<DiagnosisAddedData>
{ 
    public const string TYPE = "diagnosis:added";

    public static DiagnosisAdded Construct(long aggregateId, DiagnosisAddedData data) =>
        new DiagnosisAdded   
        {
            AggregateId = aggregateId,
            Type = TYPE,
            Data = data
        };       
}

