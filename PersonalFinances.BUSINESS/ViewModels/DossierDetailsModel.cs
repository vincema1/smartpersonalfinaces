using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.DATA.Utils;
using PersonalFinances.BUSINESS.Utils;
using PersonalFinances.DATA;
using System.Text;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class DossierDetailsModel
    {
        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        private POCO.dossier _dossier;
        private List<POCO.recordCategory> _RecordCategories;
        private List<POCO.recordSubcategory> _RecordSubcategories=new List<POCO.recordSubcategory>() ;
        private int _beginYear;
        private int _endYear;
        private int _dossierId;

        private string _userName;
        public string UserName { get { return _userName; } }


        private decimal _overallExpensesLast365;
        private decimal _overallRevenuesLast365;
        private List<POCO.yearlyExpensePerCategoryLine> _yearlyExpensePerCategoryLines;
        private string _JSDataArray;



        public decimal OverallExpensesLast365 { get { return _overallExpensesLast365; }}
        public decimal OverallRevenuesLast365 { get { return _overallRevenuesLast365; } }
        public decimal AverageExpensePerMonth { get { return _overallExpensesLast365/12; } }
        public decimal AverageExpensePerDay { get { return _overallExpensesLast365 /365; } }

        public string JSDataArray { get { return _JSDataArray; }}

        public POCO.dossier Dossier { get { return _dossier; } }


        public int BeginYear { get { return _beginYear; } }
        public int EndYear { get { return _endYear; } }


        public static List<DossierDetailsModel> ListDossierUser(string userName)
        {
            List<DossierDetailsModel> list = new List<DossierDetailsModel>();


            PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

            List<dossier> dossiers= (from d in _context.dossiers
                                        join u in _context.AspNetUsers on d.userId equals u.Id
                                        where u.UserName == userName
                                        select d).ToList();

            foreach (var dossier in dossiers)
            {
                DossierDetailsModel DDM = new DossierDetailsModel(dossier.dossierId);
                list.Add(DDM);
            }

            return list;

        }


        public DossierDetailsModel(int dossierId, string ConnectionString="")
        {
            _dossier = (from d in _context.dossiers
                        where d.dossierId == dossierId
                        select new POCO.dossier { dossierId = d.dossierId,
                                                 creationDate = d.creationDate,
                                                 dossierName = d.dossierName,
                                                 userId = d.userId }).FirstOrDefault();

            _dossierId = dossierId;

            //userName

            _userName = (from d in _context.dossiers
                         join u in _context.AspNetUsers on d.userId equals u.Id
                         where d.dossierId == dossierId
                         select u.UserName).Single();



            _RecordCategories = (from sc in (_context.recordCategories.Where(ec => ec.dossierId == dossierId).ToList())
                                 select new POCO.recordCategory
                                 {
                                     description = sc.description,
                                     dossierId = sc.dossierId,
                                     isExpense = sc.isExpense,
                                     recordCategoryId = sc.recordCategoryId,
                                   
                                 }).ToList();



            //TODO: test both methods
            _endYear = StoreProcedures.GetMaxYearDossier(dossierId);
            _beginYear = StoreProcedures.GetMinYearDossier(dossierId);

           
            var _RecordSubcategoriesResult = _context.GetrecordSubcategoriesByDossierId(dossierId);

            _RecordSubcategories = (from rscr in _RecordSubcategoriesResult
                                    select new POCO.recordSubcategory
                                    {
                                        description = rscr.description,
                                        recordCategoryId = rscr.recordCategoryId,
                                        recordSubcategoryId = rscr.recordCategoryId
                                    }).ToList();

            
            var myDate = DateTime.Now;
            var newDate = myDate.AddYears(-1);
            newDate = newDate.AddDays(1);

            string beginDate365dd = Utils.Utils.FormatDate(newDate);
            string endDate365dd = Utils.Utils.FormatDate(DateTime.Now);

            IncomeStatementTab expenses = new IncomeStatementTab(dossierId, beginDate365dd, endDate365dd, true);
            IncomeStatementTab revenues = new IncomeStatementTab(dossierId, beginDate365dd, endDate365dd, false);

            try
            { 
                if (!string.IsNullOrEmpty(ConnectionString))
                    SetStringArrayJS(expenses, ConnectionString);
            }
            catch(Exception e)
            {

            }

            _overallExpensesLast365 = expenses.totalDec;
            _overallRevenuesLast365= revenues.totalDec;


            //var mostExpensiveCategories = (from ex in expenses.report
            //                                where ex.bitmap == 1
            //                                orderby ex.total descending
            //                                select ex.category).Take(4);


            //var ado = new AdoRepository<POCO.yearlyExpensePerCategoryLine>(ConnectionString);



            //_yearlyExpensePerCategoryLines = ado.YearlyExpensePerCategory(dossierId, 
            //                                                              mostExpensiveCategories.ElementAt(0),
            //                                                              mostExpensiveCategories.ElementAt(1),
            //                                                              mostExpensiveCategories.ElementAt(2),
            //                                                              mostExpensiveCategories.ElementAt(3),
            //                                                              true);

                  



        }

        private void SetStringArrayJS(IncomeStatementTab expenses, string ConnectionString)
        {


            //Writing an array with this format for the chart
            /*['Month', 'Bolivia', 'Ecuador', 'Madagascar', 'Papua New Guinea', 'Rwanda'],
                       ['2004/05', 165, 938, 522, 998, 450],
                       ['2005/06', 135, 1120, 599, 1268, 288],
                       ['2006/07', 157, 1167, 587, 807, 397],
                       ['2007/08', 139, 1110, 615, 968, 215],
                       ['2008/09', 136, 691, 629, 1026, 366] */

            var mostExpensiveCategories = (from ex in expenses.report
                                           where ex.bitmap == 1
                                           orderby ex.total descending
                                           select ex.category).Take(4);


            var ado = new AdoRepository<POCO.yearlyExpensePerCategoryLine>(ConnectionString);

            _yearlyExpensePerCategoryLines = ado.YearlyExpensePerCategory(_dossierId,
                                                                          mostExpensiveCategories.ElementAt(0),
                                                                          mostExpensiveCategories.ElementAt(1),
                                                                          mostExpensiveCategories.ElementAt(2),
                                                                          mostExpensiveCategories.ElementAt(3),
                                                                          true);

            StringBuilder sb = new StringBuilder();

            //header
            sb.Append("['year'");
            foreach (var cat in mostExpensiveCategories)
            {
                sb.Append(",'" + cat + "'");
            }
            sb.Append("]");

            _JSDataArray += sb.ToString() + "\n";

            sb = new StringBuilder();

            for (int i = _beginYear; i <= _endYear; i++)
            {
                sb.Append(",['" + i + "'");
                foreach (var cat in mostExpensiveCategories)
                {
                    decimal tot = (from line in _yearlyExpensePerCategoryLines
                                   where line.year == i && line.category == cat
                                   select line.total).FirstOrDefault();

                    string strTot = tot.ToString().Replace(',', '.');

                    sb.Append("," + strTot );
                }
                sb.Append("]");

                _JSDataArray += sb.ToString() + "\n";
                sb = new StringBuilder();

            }
        }


        public string FirstDate
        {
            get
            {
                DateTime ret = _context.records.Where(r=> r.dossierId==_dossier.dossierId).Select(r => r.date).DefaultIfEmpty().Min();
                if (ret.Year == 1)
                    ret = DateTime.Now;

                return Utils.Utils.FormatDate(ret);
            }
        }
        public string LastDate
        {
            get
            {
                DateTime ret = _context.records.Where(r => r.dossierId == _dossier.dossierId).Select(r => r.date).DefaultIfEmpty().Max();
                //return ret.ToString("dd /MM/yyyy");
                if (ret.Year == 1)
                    ret = DateTime.Now;

                return Utils.Utils.FormatDate(ret);

            }
        }

    }
}