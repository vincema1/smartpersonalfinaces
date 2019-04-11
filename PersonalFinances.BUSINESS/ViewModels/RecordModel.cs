using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.BUSINESS.Utils;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.Filters;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Data.Entity;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class RecordModel
    {
        public int recordId { get; set; }
        public int dossierId { get;  set; }
        public Nullable<int> recordSubcategoryId { get; set; }
        public Nullable<int> recordCategoryId { get; set; }
        public string subcategory { get; set; }
        public string category { get; set; }

        [IsValidDate(ErrorMessage = "Insert date as dd/mm/yyyy")]
        public string date { get; set; }

        [IsValidCurrency(ErrorMessage = "Insert number as 0.000,00")]
        public string revenue { get; set; }

        [IsValidCurrency(ErrorMessage = "Insert number as 0.000,00")]
        public string expense { get; set; }

        public string description { get; set; }
        public string comment { get; set; }

        public List<POCO.recordCategory> categories { get; set; }
        public List<POCO.recordSubcategory> subcategories { get; set; }

        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
       
        
        public RecordModel() { }

        public int Create()
        {

            record recordToCreate = new record();
            recordToCreate= UpdateFields(recordToCreate);
            
            db.records.Add(recordToCreate);
            db.SaveChanges();


            return recordToCreate.recordId;

        }


        private record UpdateFields(record recordToUpdate)
        {


            recordToUpdate.dossierId = this.dossierId;
            recordToUpdate.date = DateTime.Parse(this.date);
            recordToUpdate.expense = decimal.Parse(this.expense);
            recordToUpdate.revenue = decimal.Parse(this.revenue);

            bool isExpense = (recordToUpdate.expense > recordToUpdate.revenue);
            recordToUpdate.recordSubcategoryId = GetSubCategoryId(this.dossierId,
                                                                  isExpense,
                                                                  this.recordSubcategoryId ?? 0,
                                                                  this.subcategory,
                                                                  this.recordCategoryId ?? 0,
                                                                  this.category);
            recordToUpdate.description = (this.description == null) ? "" : this.description;
            recordToUpdate.comment = (this.comment == null) ? "" : this.comment; ;

            return recordToUpdate;

        }


        public int Update()
        {

            record recordToUpdate=db.records.Find(recordId);

            if (recordToUpdate == null)
                return 0;
            
            UpdateFields(recordToUpdate);
            db.Entry(recordToUpdate).State = EntityState.Modified;

            db.SaveChanges();

            return 1;

        }
        public int GetSubCategoryId(int dossierId,
                                    bool isExpense,
                                    int recordSubcategoryId, 
                                    string recordSubcategory,
                                    int recordCategoryId, 
                                    string recordCategory)
        {
            if (recordSubcategoryId != 0)
                return recordSubcategoryId;

            if (recordSubcategoryId == 0 &&
                this.recordCategoryId != 0)
            {
                return CreateSubCat(recordCategoryId,
                                    recordSubcategory);
            }

            if (this.recordSubcategoryId == 0 &&
                this.recordCategoryId ==0)
            {
                int recordCategoryIdNew = CreateCat(dossierId, recordCategory,isExpense);
                return CreateSubCat(recordCategoryIdNew,
                                    recordSubcategory);

            }

            return 0;
        }

        public int CreateSubCat(int categoryId, 
                                string description)
        {

            //Check if it exists already
            recordSubcategory subcat = db.recordSubcategories.Where(r => r.recordCategoryId == categoryId && r.description == description).FirstOrDefault();
            if (subcat != null)
                return subcat.recordSubcategoryId;
            
            subcat = new recordSubcategory();
            subcat.recordCategoryId = categoryId;
            subcat.description = description;

            db.recordSubcategories.Add(subcat);
            db.SaveChanges();
            
            return subcat.recordSubcategoryId;
        }

        public int CreateCat(int dossierId, 
                             string description, 
                             bool isExpense)
        {
            //Check if it exists already
            recordCategory cat = db.recordCategories.Where(r => r.isExpense == isExpense && r.description == description).FirstOrDefault();
            if (cat != null)
                return cat.recordCategoryId;

            cat = new recordCategory();

            cat.dossierId=dossierId;
            cat.description = description;
            cat.isExpense = isExpense;
            db.recordCategories.Add(cat);
            db.SaveChanges();

            return cat.recordCategoryId;
        }

        public RecordModel(int dossierId)
        {
         
            this.recordId = 0;
            this.dossierId = dossierId;
            this.date = DateTime.Now.ToString("dd/MM/yyyy");
            this.revenue = "0,00";
            this.expense = "0,00";
            this.description = "";
            this.comment = "";
            this.recordSubcategoryId = 0;
            this.recordCategoryId = 0;
            this.category = "";
            this.subcategory = "";

            GetCategories(dossierId);

        }
       

        public void GetCategories(int dossierId)
        {
            List<recordCategory> categories = db.recordCategories.Where(d => d.dossierId == dossierId).ToList();
            List<recordSubcategory> subcategories = (from sc in db.recordSubcategories
                                                     join c in db.recordCategories on sc.recordCategoryId equals c.recordCategoryId
                                                     where c.dossierId == dossierId
                                                     select sc).ToList();

            this.categories = FromListEntityToListPOCO<POCO.recordCategory, recordCategory>(categories);
            this.subcategories = FromListEntityToListPOCO<POCO.recordSubcategory, recordSubcategory>(subcategories);
        }

        //TODO: move somewhere else
        #region POCO generic methods   
        public List<P> FromListEntityToListPOCO<P, E>(List<E> entity)
        {
            List<P> listP = new List<P>();

            foreach (E item in entity)
            {
                P tmp = FromEntityToPOCO<P,E>(item);
                listP.Add(tmp);
            } 

            return listP;
        }
        public P FromEntityToPOCO<P, E>(E entity)
        {
            
            P tmp = (P)Activator.CreateInstance(typeof(P));

            Type p = typeof(P);
            Type e = typeof(E);

            foreach (PropertyInfo propT in p.GetProperties())
            {
                foreach (PropertyInfo propE in e.GetProperties())
                {
                    if (propT.Name==propE.Name)
                    {
                        object value = GetPropertyValue(entity, propE.Name);
                        SetPropertyValueWithCast(propT, tmp, value);
                        break;
                    }

                }
            }

            return tmp;
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

        #endregion
    }

}
