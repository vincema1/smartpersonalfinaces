using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

using POCO = PersonalFinances.DATA.POCO;


namespace PersonalFinances.DATA
{
    public class AdoRepository<T> : IRepository<T>
    {
        private SqlConnection con;

        private string _ConnectionString = "";

        public void IRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        public AdoRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
            con = new SqlConnection(ConnectionString);
        }

        public List<T> GetAll()
        {
            Type t = typeof(T);

            con.Open();

            SqlCommand cmd = new SqlCommand(string.Format("select * from {0}", t.Name), con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            return DataTableToObjet(dt);

        }

        public T GetElementByID(string id)
        {
            Type t = typeof(T);
            T tmp = (T)Activator.CreateInstance(typeof(T));

            con.Open();

            SqlCommand cmd = new SqlCommand(string.Format("select * from {0} where {0}Id=@{0}Id", t.Name), con);

            cmd.Parameters.AddWithValue("@" + t.Name + "_Id", id);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            con.Close();

            if (dt.Rows.Count != 1)
            {
                tmp = default(T);
                return tmp;
            }

            foreach (DataColumn column in dt.Columns)
            {
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    if (prop.Name == column.ColumnName)
                    {
                        object obj = (object)dt.Rows[0][column.ColumnName];
                        prop.SetValue(tmp, obj);
                    }
                }
            }

            return tmp;
        }


   

    
         public List<T> GetList(T param)
         {
            Type t = typeof(T);
            DataTable dt;

            SqlCommand cmd; // = new SqlCommand();

            string where = GetWhereFromFilter(param, out cmd);
       
            using (con)
            {
                con.ConnectionString = _ConnectionString;
                
                cmd.CommandText = string.Format("select * from {0} ", t.Name) + where;
                cmd.Connection = con;
                //SqlCommand cmd = new SqlCommand(string.Format("select * from {0}", t.Name), con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                dt = new DataTable();
                da.Fill(dt);
                con.Close();
            }
            return DataTableToObjet(dt);
        }

        

        public int Add(T param)
        {
            Type t = typeof(T);
            //string commandString = "insert into {0} ({1}) values ({2}))";

            StringBuilder fields = new StringBuilder();
            StringBuilder values = new StringBuilder();
            bool first = true;
            SqlCommand cmd = new SqlCommand();
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object value = GetPropertyValue(param, prop.Name);
                if (value == null||prop.Name==t.Name + "Id")
                    continue;

                string comma = (first) ? "" : ",";
                fields.Append(comma + prop.Name);
                values.Append(comma + "@" + prop.Name);

               
                cmd.Parameters.AddWithValue("@" + prop.Name, value);

                first = false;
            }

            cmd.CommandText = string.Format("insert into {0} ({1}) values ({2})", t.Name, fields.ToString(), values.ToString());
            
            return ExecuteNonQuery(cmd);


        }
      

        public int Remove(T param)
        {
            Type t = typeof(T);
            SqlCommand cmd; // = new SqlCommand();

            string where = GetWhereFromFilter(param, out cmd);
            cmd.CommandText = string.Format("delete from {0} ", t.Name) + where;

            return ExecuteNonQuery(cmd);
        }

        public int Update(T param)
        {
            Type t = typeof(T);
            SqlCommand cmd; // = new SqlCommand();

            string set = GetSetFromFilter(param, out cmd);
            string where = string.Format(" where {0}=@{0}", t.Name + "Id");
            object value = GetPropertyValue(param, t.Name + "Id");
            cmd.Parameters.AddWithValue(string.Format("@{0}", t.Name + "Id"), value);
            cmd.CommandText = string.Format("update {0} ", t.Name) + set + where;

            return ExecuteNonQuery(cmd);

        }


        public List<T> recordFullSearch(int dossierId,
                                        string beginDate,
                                        string endDate,
                                        int recordCategoryId,
                                        int recordSubcategoryId,
                                        string description,
                                        string comment)
        {

            List<T> list = new List<T>();

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_records_fullsearch]",con);
                cmd.Parameters.AddWithValue("@dossierId", dossierId);
                cmd.Parameters.AddWithValue("@beginDate", beginDate);
                cmd.Parameters.AddWithValue("@endDate", endDate);
                cmd.Parameters.AddWithValue("@recordCategoryId", recordCategoryId);
                cmd.Parameters.AddWithValue("@recordSubcategoryId", recordSubcategoryId);
                cmd.Parameters.AddWithValue("@descr", description);
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                list = DataTableToObjet(dt);

            }

            return list;

        }


        public List<T> YearlyExpensePerCategory(int dossierId,
                                                string cat1,
                                                string cat2,
                                                string cat3,
                                                string cat4,
                                                bool isExpense)
        {

            List<T> list = new List<T>();

            using (SqlConnection con = new SqlConnection(_ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[sp_YearlyExpensePerCategory]", con);
                cmd.Parameters.AddWithValue("@dossierId", dossierId);
                cmd.Parameters.AddWithValue("@cat1", cat1);
                cmd.Parameters.AddWithValue("@cat2", cat2);
                cmd.Parameters.AddWithValue("@cat3", cat3);
                cmd.Parameters.AddWithValue("@cat4", cat4);
                cmd.Parameters.AddWithValue("@isExpense", isExpense);

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                con.Close();
                list = DataTableToObjet(dt);

            }

            return list;

        }

        //********************PRIVATE METHODS***********************
        #region private methods 
        private int ExecuteNonQuery(SqlCommand cmd)
        {
            int ret = -1;

            using (con)
            {
                con.ConnectionString = _ConnectionString;
                con.Open();
                cmd.Connection = con;
                ret = cmd.ExecuteNonQuery();
                con.Close();
            }

            return ret;
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperties()
               .Single(pi => pi.Name == propertyName)
               .GetValue(obj, null);
        }

        private String GetWhereFromFilter(T filter, out SqlCommand cmd)
        {
            cmd = new SqlCommand();
            Type t = typeof(T);
            StringBuilder where = new StringBuilder("where 1=1 ");


            foreach (PropertyInfo prop in t.GetProperties())
            {
                object filterValue = GetPropertyValue(filter, prop.Name);
                if (filterValue != null)
                {
                    //If the primary key is 0 the check is bypassed
                    if (prop.Name == string.Format("{0}Id", t.Name) &&
                        filterValue.Equals(0))
                        continue;

                    where.Append(string.Format(" and {0}=@{0}", prop.Name));
                    cmd.Parameters.AddWithValue(string.Format("@{0}", prop.Name), filterValue);

                }
            }


            return where.ToString();
        }

        private String GetSetFromFilter(T filter, out SqlCommand cmd)
        {
            cmd = new SqlCommand();
            Type t = typeof(T);
            StringBuilder set = new StringBuilder(" set ");

            bool first = true;
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object filterValue = GetPropertyValue(filter, prop.Name);
                if (filterValue != null)
                {
                    //If the primary key is 0 the check is bypassed
                    if (prop.Name == string.Format("{0}Id", t.Name))
                        continue;

                    set.Append(string.Format(" {0}{1}=@{1}", ((first) ? "" : ","), prop.Name));
                    first = false;
                    cmd.Parameters.AddWithValue(string.Format("@{0}", prop.Name), filterValue);

                }
            }


            return set.ToString();
        }
        private List<T> DataTableToObjet(DataTable dt)
        {
            T tmp;
            List<T> _list = new List<T>();
            Type t = typeof(T);

            foreach (DataRow item in dt.Rows)
            {
                tmp = (T)Activator.CreateInstance(typeof(T));

                foreach (DataColumn column in dt.Columns)
                {
                    foreach (PropertyInfo prop in t.GetProperties())
                    {
                        if (prop.Name == column.ColumnName)
                        {
                            object obj = item[column.ColumnName] as object;
                            if (obj != null && !(obj is System.DBNull))
                                prop.SetValue(tmp, obj);
                        }
                    }
                }

                _list.Add(tmp);
            }
            return _list;
        }

        #endregion
    }

}
