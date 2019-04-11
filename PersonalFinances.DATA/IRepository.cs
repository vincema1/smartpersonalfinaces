using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.DATA
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetElementByID(string id);
        List<T> GetList(T param);

        int Add(T param);
        int Remove(T param);
        int Update(T param);

        void IRepository(string ConnectionString);

    }
}
