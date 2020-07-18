using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IProfileService
    {
        Task<User> GetCurrent();
        Task<User> Get(Guid id);
        Task<IEnumerable<User>> Find(string searchText);
    }
}
