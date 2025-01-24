using System.Globalization;
using System.Linq;
using System.Transactions;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{

    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("ur-Pk");
      
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<ActionResult> Index()
        {

            DateTime StartDate = DateTime.Today.AddDays(-6);
            DateTime EndDate = DateTime.Today;

            List<Models.Transaction> SelectedTransactions = await _context.Transactions
                .Include(x => x.Category)
                .Where(y => y.Date >= StartDate && y.Date <= EndDate)
                .ToListAsync();
            // Total Income
            int TotalIncome = SelectedTransactions.Where(i => i.Category.Type == "Income").Sum(j => j.Amount);

            ViewBag.TotalIncome = TotalIncome.ToString("C0");
            culture.NumberFormat.CurrencyNegativePattern = 1;
            ViewBag.TotalIncome = String.Format(culture, "{0:C0}", TotalIncome);

            // Total Expense
            int TotalExpense = SelectedTransactions.Where(i => i.Category.Type == "Expense").Sum(j => j.Amount);
            ViewBag.TotalExpense = TotalExpense.ToString("C0");
          
            ViewBag.TotalExpense = String.Format(culture, "{0:C0}", TotalExpense);
            // Balance

            int Balance = TotalIncome - TotalExpense;
           
           
            ViewBag.Balance = String.Format(culture,"{0:C0}",Balance);
            // chart
            ViewBag.DoughnutChartData = SelectedTransactions.
                Where(i => i.Category.Type == "Expense").GroupBy(j => j.Category.CategoryId)
                .Select(k => new
                {
                    categoryTitleWithIcon = k.First().Category.Icon + " " + k.First().Category.Tital,
                    amount = k.Sum(j => j.Amount),
                    formattedAmount = k.Sum(j => j.Amount).ToString("C0", CultureInfo.CreateSpecificCulture("en-PK")),
        }).OrderByDescending(l=>l.amount).ToList();

            //Spine chart
            List<SplineChartData> IncomeSummary = SelectedTransactions.Where(i => i.Category.Type == "Income")
              .GroupBy(j => j.Date).Select(k => new SplineChartData()
              {
                  day=k.First().Date.ToString("dd-MMM"),
                  income=k.Sum(l=>l.Amount),
              }).ToList();  
            
            
            List<SplineChartData> ExpenseSummary = SelectedTransactions.Where(i => i.Category.Type == "Expense")
              .GroupBy(j => j.Date).Select(k => new SplineChartData()
              {
                  day=k.First().Date.ToString("dd-MMM"),
                  expense=k.Sum(l=>l.Amount),
              }).ToList();


            //Combine both lists

            string[] Last7Days = Enumerable.Range(0, 7).
                Select(i => StartDate.AddDays(i).ToString("dd-MMM"))
                .ToArray();

            ViewBag.SplineChartData = from day in Last7Days
                                      join income in IncomeSummary on day equals income.day
                                      into dayIncomeJoined
                                      from income in dayIncomeJoined.DefaultIfEmpty()
                                      join expense in ExpenseSummary on day equals expense.day into expenseJoined
                                      from expense in expenseJoined.DefaultIfEmpty()
                                      select new
                                      {
                                          day = day,
                                          income = income == null ? 0 : income.income,
                                          expense = expense == null ? 0 : expense.expense,
                                      };

            ViewBag.RecentTransactions = await _context.Transactions
              .Include(i => i.Category)
              .OrderByDescending(j => j.Date)
              .Take(5)
              .ToListAsync();


            return View();
        }
    }

    public class SplineChartData
    {
        public string day;
        public int income;
        public int expense;
    }
}
