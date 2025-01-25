namespace Expense_Tracker.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Price {  get; set; }
        public string image_path {  get; set; }=null!;
    }
}
