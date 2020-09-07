using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface ICompanyModelQueryable<T> where T : IHaveCommon
    {

        Task<T> FirstOrDefaultAsync(CancellationToken cancellationToken);
        ICompanyModelQueryable<T> Where(Expression<Func<T, bool>> condition);
        Task<List<T>> ToListAsync(CancellationToken cancellationToken);
    }

}
