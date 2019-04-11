//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Xml.Linq;
//using System.Runtime.Serialization;
//using System.Web.Script.Serialization;
//using Newtonsoft.Json.Linq;
//using PersonalFinances.DATA.DataModel;
//using Newtonsoft.Json;
//using System.Reflection;
//using System.Diagnostics;

//namespace LaboWiki.DATA
//{

//    public class FakeRepository<T> : IRepository<T>
//    {

//        private string _DBFakeJason="";
//        private string _ConnectionString="";

//        public void IRepository(string ConnectionString)
//        {
//            _ConnectionString= ConnectionString;
//        }

//        public int Add(T param)
//        {
//            Type t = typeof(T);
//            JObject rss = JObject.Parse(_DBFakeJason);
//            string json=JsonConvert.SerializeObject(param);

//            var dict = rss[t.Name].ToString();

//            rss[t.Name] = dict.Substring(0, dict.Length-1) + json + "]";

//            StringBuilder sb = new StringBuilder();
            
//            StringWriter sw = new StringWriter(sb);
//            //sb.Append(rss.ToString());

//            JsonWriter jsonWriter = new JsonTextWriter(sw);
//            jsonWriter.WriteRaw(rss.ToString());
//            //jsonWriter.W


//            using (FileStream fs = new FileStream(_ConnectionString, FileMode.Create, FileAccess.Write))
//            using (StreamWriter stw = new StreamWriter(fs, Encoding.UTF8, 512))
//            {
//                //string update = JsonConvert.SerializeObject(rss.ToString());
//                //string update = rss.ToString();
//                string update = sb.ToString();


//                sw.Write(update);
//                _DBFakeJason = update;
//            }

//            return 1;

//        }

//        public FakeRepository()
//        {
////            StreamReader sr = new StreamReader(@"C:\Users\student\Desktop\WikiLabo\LaboWiki.WCF\LaboWiki.DATA\App_Data\LaboWikiFake.json");
//            StreamReader sr = new StreamReader(@"..\..\FakeData\LaboWikiFake.json");

//            //StreamReader sr = new StreamReader(@"~\App_Data\LaboWikiFake.json");
//            _DBFakeJason = sr.ReadToEnd();
//            sr.Close();
//        }

//        public List<T> GetAll()
//        {
//            T tmp = default(T);
//            List<T> _list = GetList(tmp);

//            return _list;
//        }

//        public T GetElementByID(string id)
//        {
//            T tmp = (T)Activator.CreateInstance(typeof(T));
//            Type t = typeof(T);

//            foreach (PropertyInfo prop in t.GetProperties())
//            {
//                //If the primary key is 0 the check is bypassed
//                if (prop.Name == string.Format("{0}Id", t.Name))
//                {
//                    Type tl = prop.PropertyType;
//                    dynamic changedObj= Int32.Parse(id);
//                    prop.SetValue(tmp, changedObj);
//                    break;
//                }
//            }

//            List<T> list = GetList(tmp);

//            return list.Single();

//        }
//        public List<T> GetList(T param)
//        {
//            List<T> _list = new List<T>();
//            Type t = typeof(T);
//            JObject rss = JObject.Parse(_DBFakeJason);

//            if (rss[t.Name] == null)
//                return _list;

//            for (int i = 0; i < rss[t.Name].Count(); i++)
//            {
//                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(rss[t.Name][i].ToString());

//                //Loop on the property type and cast the value to the property type
//                T tmp = (T)Activator.CreateInstance(typeof(T));
//                foreach (var kv in dict)
//                {
//                    string propertyName = kv.Key;
//                    string propertyValue = kv.Value;

//                    foreach (PropertyInfo prop in t.GetProperties())
//                    {
//                        if (prop.Name == propertyName)
//                        {
//                            Type tl = prop.PropertyType;

//                            dynamic changedObj;

//                            //You have to use Nullable.GetUnderlyingType to get underlying type of Nullable.
//                            if (tl.IsGenericType && tl.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
//                                tl = Nullable.GetUnderlyingType(tl);

//                            changedObj = Convert.ChangeType(kv.Value, tl);
//                            prop.SetValue(tmp, changedObj);

//                            break;
//                        }
//                    }
//                }

//                bool add = true;
//                if (param != null)
//                {
//                    foreach (PropertyInfo prop in t.GetProperties())
//                    {
//                        object filterValue = GetPropertyValue(param, prop.Name);
//                        object Value = GetPropertyValue(tmp, prop.Name);

//                        //If the primary key is 0 the check is bypassed
//                        if (prop.Name == string.Format("{0}Id", t.Name) &&
//                            filterValue.Equals(0))
//                            continue;


//                        if (filterValue != null)
//                            if (!Value.Equals(filterValue))
//                            {
//                                add = false;
//                                break;
//                            }
//                    }
//                }

//                if(add)
//                    _list.Add(tmp);
//            }

//            return _list;
//        }

//        private object GetPropertyValue(object obj, string propertyName)
//        {
//            return obj.GetType().GetProperties()
//               .Single(pi => pi.Name == propertyName)
//               .GetValue(obj, null);
//        }

//        public int Remove(T param)
//        {
//            Type t = typeof(T);

//            JObject rss = JObject.Parse(_DBFakeJason);
//            int removeIndex = -1;

//            for (int i = 0; i < rss[t.Name].Count(); i++)
//            {
//                var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(rss[t.Name][i].ToString());
                
//                //all filds must be equal
//                foreach (var kv in dict)
//                {
//                    string propertyName = kv.Key;
//                    string propertyValue = kv.Value;

//                    foreach (PropertyInfo prop in t.GetProperties())
//                    {
//                        if (prop.Name == propertyName)
//                        {
//                            string value = param.GetType().GetProperty(prop.Name).GetValue(param, null).ToString();
//                            removeIndex = (value == propertyValue) ? i : -1;
//                        }
//                    }
//                }

//                if (removeIndex != -1)
//                    break;

//            }

//            rss[t.Name][removeIndex].Remove();

//            //rss.ToString();


//            using (StreamWriter sw = new StreamWriter(_ConnectionString))
//            {
//                string update = rss.ToString();
//                sw.Write(update);
//                _DBFakeJason = update;
//            }

//            return (removeIndex==-1)?-1:1;
//        }

//        public int Update(T param)
//        {
//            return -1;
//        }

        
//    }
//}
