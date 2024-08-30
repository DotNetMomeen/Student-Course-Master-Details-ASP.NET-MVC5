using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student_Course_MVC_MasterDetails.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        [Required, Display(Name = "Client Name"), StringLength(50)]
        public string StudentName { get; set; }
        [Required, Display(Name = "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }

        [Display(Name = "Marital Status")]
        public bool MaritalStatus { get; set; }


        public virtual ICollection<BookingCourseEntry> BookingCourseEntries { get; set; } = new List<BookingCourseEntry>();
    }
}