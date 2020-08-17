using System;

namespace Wellness.Model
{
    public interface IParticipate
    {
        Guid UserId { get; set; }
        DateTime SubmissionDate { get; set; }
    }
}
