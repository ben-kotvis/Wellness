using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Authorization.Api
{
    public class CompoundPersistanceService<T> : ICompoundPersistanceService<T>
        where T : IIdentifiable
    {
        public CompoundPersistanceService(IPersistanceService<T> transactions, IPersistanceReaderService<T> reader)
        {
            Transactions = transactions;
            Reader = reader;
        }

        public IPersistanceService<T> Transactions { get; }

        public IPersistanceReaderService<T> Reader { get; }
    }
}
