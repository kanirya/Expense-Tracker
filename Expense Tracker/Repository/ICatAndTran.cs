

using Expense_Tracker.Models;

namespace Expense_Tracker.Repository
{
    public interface ICatAndTran
    {
        List<Transaction> getAllTransaction();
        Transaction getTransactionById(int id);
    }
}
