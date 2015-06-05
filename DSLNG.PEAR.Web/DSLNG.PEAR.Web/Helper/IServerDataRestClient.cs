using DSLNG.PEAR.Web.Models;
using System.Collections.Generic;

namespace DSLNG.PEAR.Web.Helper
{
    public interface IServerDataRestClient
    {
        void Add(ServerDataModel serverData);
        void Delete(int id);
        IEnumerable<ServerDataModel> GetAll();
        ServerDataModel GetById(int id);
        ServerDataModel GetByIP(int ip);
        ServerDataModel GetByType(int type);
        void Update(ServerDataModel serverData);
    }
}
