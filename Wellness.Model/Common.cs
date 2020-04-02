using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wellness.Model
{
    public class Common
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public CommonOptions Options { get; set; }
        public Guid CompanyId { get; set; }
    }


    [Flags]
    public enum CommonOptions
    {
        None = 0
    }

    public interface IHaveCommon
    {
        Common Common { get; set; }
    }
}
