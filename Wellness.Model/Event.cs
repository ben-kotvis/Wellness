namespace Wellness.Model
{
    public class Event : NamedEntity, IIdentifiable
    {
        public decimal Points { get; set; }
        public decimal AnnualMaximum { get; set; }
        public bool Active { get; set; }
        public bool RequireAttachment { get; set; }
    }
}
