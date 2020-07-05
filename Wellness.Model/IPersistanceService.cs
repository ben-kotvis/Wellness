using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public interface IPersistanceService
    {
        Task Create<T>(PersistenceWrapper<T> wrapped) where T : IIdentifiable;
    }
}
