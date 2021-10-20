using System.Collections.Generic;

namespace ReportingWith_Queue
{
    public interface IGenerator<T> where T : IDataObject
    {
        T CreateSingleRandom();
        List<T> CreateBulkRandom();
    }
}