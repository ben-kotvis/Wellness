using System;

namespace Wellness.Model
{
    public class CompanyCommon
    {
        public string CreatedBy { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTimeOffset UpdatedOn { get; set; }
        public CommonOptions Options { get; set; }
        public Version Version { get; set; }
    }
}
