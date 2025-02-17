using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Expense_Tracker.Models
{
    public class Room
    {

        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Room Number is Required")]
        [Column(TypeName ="nvarchar(50)")]
        
        public string RoomNo { set; get; }


        [Required(ErrorMessage = "floor is Required")]
        [Column(TypeName = "nvarchar(50)")]
        public string floor { set; get; }
        [Range(1,5)]
        public int seats { set; get; }

    }
}
