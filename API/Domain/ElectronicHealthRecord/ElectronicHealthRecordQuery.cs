using API.Core;
using API.Interfaces;
using API.Persistence;

namespace API.Domain;

public static class ElectronicHealthRecordQuery
{
    public static Query Find(long id) 
    {
        return new FindQuery
        {
            Id = id
        };
    }

    public class FindQuery : Query
    {
        public long Id { get; init; }
    };
}