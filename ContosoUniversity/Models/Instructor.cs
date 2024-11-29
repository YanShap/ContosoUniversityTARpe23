using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Cache;

namespace ContosoUniversity.Models
{
    public class Instructor
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        [Column("FirstName")]
        [Display(Name = "First Name")]

        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            { return LastName + ", " + FirstMidName; }
        }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Hired on")]

        public DateTime HireDate { get; set; }


        public ICollection<CourseAssignment>? CourseAssignments { get; set; }

        public OfficeAssignment? OfficeAssignment { get; set; }

        //Igaühel on oma kolm  unikaalset propertyt

        public int? Birthday { get; set; }

        public int? WorkYears { get; set; }

        [Display(Name = "Nicotine #:")]
        public NicotineNeeded? NicotineNeeded { get; set; }

    }

    public enum NicotineNeeded
    {
        EveryHour, EveryMinute, EverySecond,
    }
}