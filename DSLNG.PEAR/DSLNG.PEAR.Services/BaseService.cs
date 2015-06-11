using DSLNG.PEAR.Data.Persistence;
using StructureMap;

namespace DSLNG.PEAR.Services
{
    public class BaseService
    {
        private readonly IDataContext _dataContext;

        public BaseService(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }


        protected IDataContext DataContext
        {
            get { return _dataContext; }
        }
    }
}
