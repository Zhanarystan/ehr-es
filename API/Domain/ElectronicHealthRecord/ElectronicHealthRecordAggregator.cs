using API.Core;

namespace API.Domain;

public static class ElectronicHealthRecordAggregator
{
    public static Func<ElectronicHealthRecord, ElectronicHealthRecord> Aggregate(Event @event) => aggregator =>
        @event switch 
        {
            ElectronicHealthRecordCreated e => ApplyElectronicHealthRecordCreated(e)(aggregator),
            DiagnosisAdded e => ApplyDiagnosisAdded(e)(aggregator),
        };

    public static Func<ElectronicHealthRecord, ElectronicHealthRecord> ApplyElectronicHealthRecordCreated(ElectronicHealthRecordCreated e) => aggregator =>
        aggregator with 
        {
            Name = e.Data.Name,
            Gender = e.Data.Gender,
            BirthDate = e.Data.BirthDate,
            Email = e.Data.Email
        };
    
    public static Func<ElectronicHealthRecord, ElectronicHealthRecord> ApplyDiagnosisAdded(DiagnosisAdded e) => aggregator =>
        aggregator with 
        {
            Diagnosis = aggregator.Diagnosis.Append(e.Data.Diagnosis).ToList()
        };

}