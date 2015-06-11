using DSLNG.PEAR.Data.Persistence;
using StructureMap;

namespace DSLNG.PEAR.Services.Implementations
{
    public class BaseService
    {
        private IContainer _container;

        public BaseService(IContainer container)
        {
            _container = container;
        }

        protected IDataContext DataContext
        {
            get { return _container.GetInstance<IDataContext>(); }
            //get { return ObjectFactory.GetInstance<IDataContext>(); }
            //get { return new DataContext(); }
        }
    }
}
