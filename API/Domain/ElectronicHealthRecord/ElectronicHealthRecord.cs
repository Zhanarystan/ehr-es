namespace API.Domain;

public enum PatientGender 
{
    Undefined,
    Male,
    Female
};

public record ElectronicHealthRecord
{
    public static ElectronicHealthRecord Empty =>
        new ElectronicHealthRecord
        {
            Name = "",
            BirthDate = DateTime.MinValue,
            Gender = PatientGender.Undefined,
            Email = "",
            Diagnosis = new (),
            Medications = new ()
        };

    public string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public PatientGender Gender { get; set; }
    public string Email { get; set; }
    public List<Diagnosis> Diagnosis { get; set; }
}


public record ElectronicHealthRecordCreateDto(
    string Name, 
    DateTime BirthDate, 
    PatientGender Gender,
    string Email);