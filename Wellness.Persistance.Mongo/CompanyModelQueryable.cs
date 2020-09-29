using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Wellness.Model;

namespace Wellness.Persistance.Mongo
{
    public class CompanyModelQueryable<T> : ICompanyModelQueryable<T> where T : IHaveCommon
    {
        private IMongoQueryable<T> _wrapped;
        private readonly Guid _companyId;
        public CompanyModelQueryable(IMongoQueryable<T> wrapped)
        {
            _wrapped = wrapped;
        }

        public ICompanyModelQueryable<T> Where(Expression<Func<T, bool>> condition)
        {
            _wrapped = _wrapped.Where(condition);
            return this;
        }

        public async Task<List<T>> ToListAsync(CancellationToken cancellationToken)
        {
            return await _wrapped.Where(i => i.Common.CompanyId == _companyId).ToListAsync(cancellationToken);
        }

        public async Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken)
        {
            return await _wrapped.Where(i => i.Common.CompanyId == _companyId).FirstOrDefaultAsync(cancellationToken);
        }
    }
}
