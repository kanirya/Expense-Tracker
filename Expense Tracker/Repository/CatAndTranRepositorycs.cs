using Expense_Tracker.Models;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Repository
{
    public class CatAndTranRepositorycs : ICatAndTran
    {

        private ApplicationDbContext _context;
       public CatAndTranRepositorycs(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Transaction> getAllTransaction()
        {
            var Data = _context.Transactions.Include(t => t.Category);
            return Data.ToList();
        }

        public Transaction getTransactionById(int id)
        {
            return (_context.Transactions.Find(id));
        }
    }
}
