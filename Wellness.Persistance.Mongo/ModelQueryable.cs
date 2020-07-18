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
    public class ModelQueryable<T> : IModelQueryable<T>
    {
        private IMongoQueryable<T> _wrapped;
        public ModelQueryable(IMongoQueryable<T> wrapped)
        {
            _wrapped = wrapped;
        }

        public IModelQueryable<T> Where(Expression<Func<T, bool>> condition)
        {
            _wrapped = _wrapped.Where(condition);
            return this;
        }

        public async Task<List<T>> ToListAsync(CancellationToken cancellationToken)
        {
            return await _wrapped.ToListAsync(cancellationToken);
        }
    }
}
