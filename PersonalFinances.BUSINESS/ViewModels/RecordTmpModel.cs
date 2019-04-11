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
    public class RecordTmpModel
    {
        public int recordTmpId { get; set; }
        public int dossierId { get;  set; }
        public string dossierName { get; set; }
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

      
        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
       
        
        public RecordTmpModel() { }

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
           
            recordToUpdate.description = (this.description == null) ? "" : this.description;
            recordToUpdate.comment = (this.comment == null) ? "" : this.comment; ;

            return recordToUpdate;

        }


        public int Update()
        {

            record recordToUpdate=db.records.Find(recordTmpId);

            if (recordToUpdate == null)
                return 0;
            
            UpdateFields(recordToUpdate);
            db.Entry(recordToUpdate).State = EntityState.Modified;

            db.SaveChanges();

            return 1;

        }
       
        
        public RecordTmpModel(int dossierId)
        {
         
            this.recordTmpId = 0;
            this.dossierId = dossierId;
            this.date = DateTime.Now.ToString("dd/MM/yyyy");
            this.revenue = "0,00";
            this.expense = "0,00";
            this.description = "";
            this.comment = "";
            this.category = "";
            this.subcategory = "";

        }

        public static void Delete(int dossierId, int importRecordTmpId)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            importRecordTmp irt = db.importRecordTmps.Where(i => i.dossierId == dossierId && i.importRecordTmpId ==                                                             importRecordTmpId).Single();

            db.importRecordTmps.Remove(irt);
            db.SaveChanges();

        }

        public static List<POCO.record> GetDoubles(int dossierId, int importRecordTmpId)
        {
            var list = new List<POCO.record>();
            
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
            importRecordTmp irt = db.importRecordTmps.Find(importRecordTmpId);


            list = (from rec in db.sp_getListDuplicates(irt.dossierId,
                                                         irt.revenue,
                                                         irt.expense,
                                                         irt.date,
                                                         irt.description,
                                                         irt.comment)
                       select new POCO.record
                       {
                           dossierId = rec.dossierId,
                           description = rec.description,
                           comment = rec.comment,
                           date = rec.date,
                           revenue = rec.revenue,
                           expense = rec.expense,
                           CategoryDescription=rec.category,
                           SubcategoryDescription = rec.subcategory

                       }).ToList();


            return list;
        }

    }

}
