using API.Core;
using API.Domain;

namespace API.Interfaces;

public interface IElectronicHealthRecordCommandHandler
{
    public ElectronicHealthRecord Handle(Command command);
}