using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Domain
{
    public class ParticipationDomainService<T> : DomainServiceBase<T>, IParticipationDomainService<T>
        where T : IIdentifiable, IParticipate
    {
        public ParticipationDomainService(IDomainDependencies<T> domainDependencies)
            : base(domainDependencies)
        {
        }

        public async Task<IEnumerable<PersistenceWrapper<T>>> GetBySelectedIndex(Guid userId, int selectedIndex, IRequestDependencies<T> requestDependencies)
        {
            var dateBaseOnRelativeIndex = DateTimeOffset.UtcNow.AddMonths(selectedIndex);

            var startDate = new DateTime(dateBaseOnRelativeIndex.Year, dateBaseOnRelativeIndex.Month, 1, 0, 0, 0);
            var endDate = startDate.AddMonths(1);
            var queryable = _domainDependencies.PersistanceService.Query(requestDependencies.CompanyId).Where(i =>
            i.Model.SubmissionDate >= startDate &&
            i.Model.SubmissionDate < endDate &&
            i.Model.UserId == userId);
            return await queryable.ToListAsync(requestDependencies.CancellationToken);
        }
    }
}
