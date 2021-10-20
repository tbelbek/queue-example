using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportingWith_Queue
{
    public abstract class DataGenerator<T> : IGenerator<T> where T : IDataObject, new()
    {
        internal static readonly Random gen = new Random();

        public virtual T CreateSingleRandom()
        {
            return new T();
        }

        public virtual List<T> CreateBulkRandom()
        {
            ConcurrentBag<T> list = new ConcurrentBag<T>();
            Parallel.For(0, 500, toExclusive => list.Add(CreateSingleRandom()));
            return list.ToList();
        }
    }
}