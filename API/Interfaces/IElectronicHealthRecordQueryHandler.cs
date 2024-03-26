using API.Core;

namespace API.Interfaces;

public interface IElectronicHealthRecordQueryHandler
{
    public object Handle(Query query);
}