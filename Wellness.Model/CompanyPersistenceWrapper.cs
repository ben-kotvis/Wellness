namespace Wellness.Model
{
    public class CompanyPersistenceWrapper<T> where T : IIdentifiable
    {
        public string Id { get { return Model.Id.ToString(); } }

        public CompanyPersistenceWrapper(T model, Common common)
        {
            Model = model;
            Common = common;
        }

        public CompanyPersistenceWrapper()
        {

        }

        public T Model { get; set; }
        public CompanyCommon Common { get; set; }

    }

}
