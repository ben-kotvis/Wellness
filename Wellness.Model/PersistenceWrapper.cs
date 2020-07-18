namespace Wellness.Model
{
    public class PersistenceWrapper<T> : IHaveCommon
        where T : IIdentifiable
    {
        public string Id { get { return Model.Id.ToString(); } }

        public PersistenceWrapper(T model, Common common)
        {
            Model = model;
            Common = common;
        }

        public PersistenceWrapper()
        {

        }

        public T Model { get; set; }
        public Common Common { get; set; }
    }

}
