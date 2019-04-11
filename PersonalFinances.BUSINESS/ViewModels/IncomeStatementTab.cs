using PersonalFinances.DATA.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace PersonalFinances.BUSINESS.ViewModels
{
    
    public class IncomeStatementTab
    {

        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        //values to be displayed in the tab
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public string total
        { get
            {
                if (this.totalDec == 0)
                    return "0,00";
                
                string ret = this.totalDec.ToString();
                return ret.Substring(0, ret.Length - 2);
;            }
        }

        public decimal totalDec
        {
            get
            {
                if (this.report.Count() == 0)
                    return 0;

                return (from a in this.report
                        where (a.bitmap == 3)
                        select a.total).FirstOrDefault();
            }
        }


        //actual aggregate data
        public List<IncomeStatementLine> report { get; }


        //constructor
        public IncomeStatementTab(int dossierId,
                                  string beginDate, 
                                  string endDate,
                                  bool isExpense)
        {
            List<IncomeStatementLine> _IncomeStatementExpenses = GetIncomeStatementLines(dossierId, beginDate, endDate, isExpense);


            this.beginDate = beginDate;
            this.endDate = endDate;
            this.report = _IncomeStatementExpenses;

        }

        private List<IncomeStatementLine> GetIncomeStatementLines(int dossierId,
                                                                 string beginDate,
                                                                 string endDate,
                                                                 bool isExpense)
        {
            List<IncomeStatementLine> list = new List<IncomeStatementLine>();


            //TODO: replace with a generic method with EF implememntation


            ObjectResult<CreateReport_IncomeStatement_Result> listTmp = _context.CreateReport_IncomeStatement(dossierId, beginDate, endDate, isExpense);

            foreach (var item in listTmp)
            {
                IncomeStatementLine bsl = new IncomeStatementLine();
                bsl.bitmap = item.bitmap;
                bsl.category = item.category;
                bsl.subcategory = item.subcategory;
                bsl.total = item.total ?? 0;

                list.Add(bsl);

            }


            return list;


        }


        public List<IncomeStatementLine> GetTotalCategories()
        {

            return this.report.Where(b=>b.bitmap==1).ToList();

        }

    }

}