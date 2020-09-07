using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceService<T> : ICompanyPersistanceReaderService<T> where T : IIdentifiable
    {
        Task Create(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Update(PersistenceWrapper<T> wrapped, CancellationToken cancellationToken);
        Task Delete(Guid id, CancellationToken cancellationToken);
    }

    public interface ICompoundCompanyPersistanceService<T> where T : IIdentifiable
    {
        IPersistanceService<T> Transactions { get; }
        ICompanyPersistanceReaderService<T> Reader { get; }
    }

    public interface ICompoundPersistanceService<T> where T : IIdentifiable
    {
        IPersistanceService<T> Transactions { get; }
        IPersistanceReaderService<T> Reader { get; }
    }
}
