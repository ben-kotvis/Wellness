using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IParticipationDomainService<T> : IDomainService<T> where T : IIdentifiable, IParticipate
    {
        Task<IEnumerable<PersistenceWrapper<T>>> GetBySelectedIndex(Guid userId, int selectedIndex, CancellationToken cancellationToken);
    }
}
