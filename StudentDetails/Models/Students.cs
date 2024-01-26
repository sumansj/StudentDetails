using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDetails.Models
{
    public class Students
    {
        [Key]
        public int RollNumber { set; get; }

        [Column(TypeName = "nvarchar(50)")]
        [DisplayName("Student Name")]
        [Required(ErrorMessage ="Name must be filled")]
        public string StudentName { set; get; }

        [Column(TypeName = "nvarchar(10)")]
        [DisplayName("Phone Number")]
        [StringLength(10)]
        public string PhoneNumber { set; get; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Address")]
        [Required(ErrorMessage = "Address must be filled")]
        public string Address { set; get; }
    }
}
