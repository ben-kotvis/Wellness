using System;

namespace Wellness.Model
{
    public class Common : CompanyCommon
    {
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
