using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
//using laboWiki.DATA.DataModel;
using PersonalFinances.DATA.POCO;
using PersonalFinances.DATA.DataModel;


namespace PersonalFinances.DATA
{
    public class FakeRepositoryXML<T> : IRepository<T>
    {
        private string _ConnectionString = "";

        public void IRepository(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        private FakeRepositoryXML() { }

        public FakeRepositoryXML(string ConnectionString)
        {
            _ConnectionString = ConnectionString;
        }

        //public void WriteEntriesLinq()
        //{
        //    //   string fileName = "xmlBidon.xml";
        //    List<record> entries = new List<record>()
        //    {
        //        new record{EntryId=1,Body="My body - 1", Title=" My title 1", CategoryId=1, OwnerId=1},
        //        new record{EntryId=1,Body="My body - 2", Title=" My title 2", CategoryId=2, OwnerId=2},
        //        new record{EntryId=1,Body="My body - 3", Title=" My title 3", CategoryId=3, OwnerId=3},
        //        new record{EntryId=1,Body="My body - 4", Title=" My title 4", CategoryId=4, OwnerId=4}
        //    };

        //    XDocument fromList = new XDocument();

        //    XElement query = new XElement(
        //        "LaboWiki", 
        //        from e in entries
        //        select 
        //        new XElement ("Entry", 
        //                      new XAttribute("RecordId", e.EntryId),
        //                      new XAttribute("Body", e.Body),
        //                      new XAttribute("Title", e.Title),
        //                      new XAttribute("CategoryId", e.CategoryId),
        //                      new XAttribute("OwnerId", e.OwnerId))
        //         );


        //    fromList.Add(query);

        //    //fromList.Save(@"C:\Users\student\Desktop\WikiLabo\LaboWiki.WCF\LaboWiki.DATA\App_Data\xmlBidonLinq.xml");


        //    int x = query.Elements().Where(e => e.Name == "Entry").Count();

        //    List<XElement> elements = query.Elements().Where(e => e.Name == "Entry").ToList();

        //    List<Entry> listOut = new List<Entry>();


        //    foreach (XElement elem in elements)
        //    {


        //        Entry entryTmp = new Entry();

                
        //        listOut.Add(entryTmp);
        //    }

        //}


        //public void WriteEntries()
        //{
        // //   string fileName = "xmlBidon.xml";
        //    List<Entry> entries = new List<Entry>()
        //    {
        //        new Entry{EntryId=1,Body="My body - 1", Title=" My title 1", CategoryId=1, OwnerId=1},
        //        new Entry{EntryId=1,Body="My body - 2", Title=" My title 2", CategoryId=2, OwnerId=2},
        //        new Entry{EntryId=1,Body="My body - 3", Title=" My title 3", CategoryId=3, OwnerId=3},
        //        new Entry{EntryId=1,Body="My body - 4", Title=" My title 4", CategoryId=4, OwnerId=4}
        //    };

        //    // Create an XmlWriterSettings object with the correct options. 
        //    XmlWriterSettings settings = new XmlWriterSettings();
        //    settings.Indent = true;
        //    settings.IndentChars = ("\t");
        //    //settings.OmitXmlDeclaration = true;

        //    using (XmlWriter writer= XmlWriter.Create(_ConnectionString, settings))
        //    {
        //        //Use indentation for readability.
        //        //writer.Formatting = Formatting.Indented;
        //        //writer.Indentation = 4;


        //        writer.WriteStartDocument();
        //        writer.WriteStartElement("LaboWiki");

        //        foreach (var entry in entries)
        //        {
        //            writer.WriteStartElement("Entry");

        //            writer.WriteStartAttribute("EntryId");
        //            writer.WriteValue(entry.EntryId);
        //            writer.WriteEndAttribute();

        //            writer.WriteStartAttribute("Title");
        //            writer.WriteValue(entry.Title);
        //            writer.WriteEndAttribute();

        //            writer.WriteStartAttribute("Body");
        //            writer.WriteValue(entry.Body);
        //            writer.WriteEndAttribute();

        //            writer.WriteStartAttribute("CategoryId");
        //            writer.WriteValue(entry.CategoryId);
        //            writer.WriteEndAttribute();

        //            writer.WriteStartAttribute("OwnerId");
        //            writer.WriteValue(entry.OwnerId);
        //            writer.WriteEndAttribute();
              
        //            writer.WriteEndElement();

        //        }

        //        writer.WriteEndElement();
        //        writer.WriteEndDocument();

        //    }
            
        //}

        public List<T> GetAll()
        {
            T tmp = default(T);
            return GetList(tmp);     
        }

        public T GetElementByID(string id)
        {
            Type t = typeof(T);
            T filter = (T)Activator.CreateInstance(typeof(T));
            PropertyInfo property = t.GetProperty(t.Name + "Id");
            SetPropertyValueWithCast(property, filter, id);

            T tmp = default(T);

            if (GetList(filter).Count() == 1)
                tmp= GetList(filter).Single();

            return tmp;

        }



   

        public List<T> GetList(T param)
        {
            Type t = typeof(T);

            List<XElement> elements = ListElementsByName(t.Name);

            List<T> list = new List<T>();

            foreach (XElement elem in elements)
            {
                T tmp = (T)Activator.CreateInstance(typeof(T));
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    if (elem.Attribute(prop.Name) == null)
                        continue;

                    string value = elem.Attribute(prop.Name).Value;
                    
                    SetPropertyValueWithCast(prop, tmp, value);
             
                }
                bool addElement = true;

                if (param != null)
                    addElement = (FilterParam(tmp, param));
                if (addElement)
                    list.Add(tmp);
            }

            return list;
        }

   


        public int Add(T param)
        {
            Type t = typeof(T);

            XDocument doc = XDocument.Load(_ConnectionString);
            XElement root = (from el in doc.Elements()
                             select el).Single();
            List<XElement> elements = root.Elements().Where(e => e.Name == t.Name).ToList();

            XElement ElemToAdd = new XElement(t.Name);

            foreach (PropertyInfo prop in t.GetProperties())
            {
                object value = GetPropertyValue(param, prop.Name);
                if (value!=null)
                    ElemToAdd.Add(new XAttribute(prop.Name, value.ToString()));

            }


            doc.Root.Add(ElemToAdd);
            doc.Save(_ConnectionString);

            return 1;
        }

        public int Remove(T param)
        {
            Type t = typeof(T);

            XDocument doc = XDocument.Load(_ConnectionString);
            XElement root = (from el in doc.Elements()
                             select el).Single();
            List<XElement> elements = root.Elements().Where(e => e.Name == t.Name).ToList();

            XElement ElemToRemove=null;

            foreach (XElement elem in elements)
            {
                bool remove = true;
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    if (elem.Attribute(prop.Name) == null)
                        continue;

                    object value = GetPropertyValue(param, prop.Name);

                    if (value == null)
                        continue;

                    string attrValue=elem.Attribute(prop.Name).Value;

                    remove = (value.ToString() == attrValue);

                    if (!remove)
                        break;

                }

                if (remove)
                {
                    if (ElemToRemove == null)
                        ElemToRemove = elem;
                    else
                        return -1;
                }
            }


            if (ElemToRemove!=null)
            { 
              //  doc.Root.Remove(ElemToRemove);
                ElemToRemove.Remove();
                doc.Save(_ConnectionString);
            }

            return 1;
        }

        public int Update(T param)
        {
            //Find the element with primary key
            Type t = typeof(T);
            object IdValue= GetPropertyValue(param, t.Name + "Id");
            
            XDocument doc= XDocument.Load(_ConnectionString); 
      
            List<XElement> list = (from node in doc.Descendants(t.Name)
                                  where node.Attribute(t.Name + "Id").Value == IdValue.ToString()
                                  select node).ToList();
            XElement ElemToUpdate;
            if (list.Count() == 1)
                ElemToUpdate = list.Single();
            else
                return -1;

            //Update attributes with value from the T param
            foreach (PropertyInfo prop in t.GetProperties())
            {
                object value = GetPropertyValue(param, prop.Name);
                if (value!=null)
                    ElemToUpdate.Attribute(prop.Name).Value = value.ToString();
            }

            doc.Save(_ConnectionString);

            return 1;
        }



        #region private methods
        private List<XElement> GetListXElementByName(string nameElem, out XDocument doc)
        {

            doc = XDocument.Load(_ConnectionString);
            XElement root = (from el in doc.Elements()
                             select el).Single();
            return root.Elements().Where(e => e.Name == nameElem).ToList();

        }

        private void SetPropertyValueWithCast(PropertyInfo prop,
                                             object objectInstance,
                                             object value)
        {
            Type tl = prop.PropertyType;
            dynamic changedObj;

            //You have to use Nullable.GetUnderlyingType to get underlying type of Nullable.
            if (tl.IsGenericType && tl.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                tl = Nullable.GetUnderlyingType(tl);

            changedObj = Convert.ChangeType(value, tl);
            prop.SetValue(objectInstance, changedObj);
        }

        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperties()
               .Single(pi => pi.Name == propertyName)
               .GetValue(obj, null);
        }

        private bool FilterParam(T elemToAdd, T param)
        {
            Type t = typeof(T);
            bool add = true;
            if (param != null)
            {
                foreach (PropertyInfo prop in t.GetProperties())
                {
                    object filterValue = GetPropertyValue(param, prop.Name);
                    object Value = GetPropertyValue(elemToAdd, prop.Name);

                    //If the primary key is 0 the check is bypassed
                    if (prop.Name == string.Format("{0}Id", t.Name) &&
                        filterValue.Equals(0))
                        continue;

                    if (filterValue != null)
                        if (!Value.Equals(filterValue))
                        {
                            add = false;
                            break;
                        }
                }
            }
            return add;
        }

        private List<XElement> ListElementsByName(string name)
        {
            XDocument doc = XDocument.Load(_ConnectionString);

            XElement root = (from el in doc.Elements()
                             select el).Single();
            return root.Elements().Where(e => e.Name == name).ToList();

        }

        #endregion

    }
}
