using API.Core;
namespace API.Domain;


public class ElectronicHealthRecordEvent<DATA> : Event
{
    public DATA Data { get; init; } = default(DATA)!;
}

