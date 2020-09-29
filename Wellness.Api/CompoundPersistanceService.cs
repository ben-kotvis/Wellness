using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Api
{
    public class CompoundPersistanceService<T> : ICompoundCompanyPersistanceService<T>
        where T : IIdentifiable
    {
        public CompoundPersistanceService(IPersistanceService<T> transactions, ICompanyPersistanceReaderService<T> reader)
        {
            Transactions = transactions;
            Reader = reader;
        }

        public IPersistanceService<T> Transactions { get; }

        public ICompanyPersistanceReaderService<T> Reader { get; }
    }
}
