using API.Core;

namespace API.Domain;

public static class ElectronicHealthRecordCommand
{
    internal static Command Create(ElectronicHealthRecordCreateDto dto) =>
        new CreateCommand
        {
            Name = dto.Name,
            BirthDate = dto.BirthDate,
            Gender = dto.Gender,
            Email = dto.Email
        };

    internal static Command AddDiagnosis(long ehrId, Diagnosis input) =>
        new AddDiagnosisCommand
        {
            EhrId = ehrId,
            Diagnosis = input
        };


    public class CreateCommand : Command
    {
        public string Name { get; init; }
        public DateTime BirthDate { get; set; }
        public PatientGender Gender { get; set; }
        public string Email { get; set; }
    }

    public class AddDiagnosisCommand : Command
    {
        public long EhrId { get; init; }
        public Diagnosis Diagnosis { get; init; }
    }
}