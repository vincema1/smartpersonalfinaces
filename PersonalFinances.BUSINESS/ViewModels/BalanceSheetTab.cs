using PersonalFinances.DATA.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace PersonalFinances.BUSINESS.ViewModels
{
    
    public class BalanceSheetTab
    {

        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        //values to be displayed in the tab

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
        public List<BalanceSheetLine> report { get; }


        //constructor
        public BalanceSheetTab(int dossierId,
                               bool isAsset)
        {
            List<BalanceSheetLine> _BalanceSheetTab = GetBalanceSheetLines(dossierId, isAsset);
             this.report = _BalanceSheetTab;

        }

        private List<BalanceSheetLine> GetBalanceSheetLines(int dossierId,
                                                                 bool isAsset)
        {
            List<BalanceSheetLine> list = new List<BalanceSheetLine>();

            ObjectResult<CreateReport_BalanceSheet_Result> listTmp = _context.CreateReport_BalanceSheet(dossierId,isAsset);

            foreach (var item in listTmp)
            {
                BalanceSheetLine bsl = new BalanceSheetLine();
                bsl.bitmap = item.bitmap;
                bsl.category = item.category;
                bsl.subcategory = item.subcategory;
                bsl.total = item.total ?? 0;

                list.Add(bsl);

            }

            return list;
        }

        public List<BalanceSheetLine> GetTotalCategories()
        {
            return this.report.Where(b=>b.bitmap==1).ToList();
        }
    }

}