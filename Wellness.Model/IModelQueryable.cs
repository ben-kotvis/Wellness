using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IModelQueryable<T> 
    {
        IModelQueryable<T> Where(Expression<Func<T, bool>> condition);
        Task<List<T>> ToListAsync(CancellationToken cancellationToken);
    }

}
