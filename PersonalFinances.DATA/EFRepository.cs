using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PersonalFinances.DATA
{
    class EFRepository<T> : IRepository<T>
    {

        private string _ConnectionString = "";

        public void IRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public EFRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public int Add(T param)
        {
            throw new NotImplementedException();
        }

        public List<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetElementByID(string id)
        {
            throw new NotImplementedException();
        }

        public List<T> GetList(T param)
        {
            throw new NotImplementedException();
        }

        public int Remove(T param)
        {
            throw new NotImplementedException();
        }

        public int Update(T param)
        {
            throw new NotImplementedException();
        }
    }
}
