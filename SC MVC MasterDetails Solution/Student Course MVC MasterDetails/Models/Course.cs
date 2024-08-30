using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student_Course_MVC_MasterDetails.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required, Display(Name = "Course Name"), StringLength(50)]
        public string CourseName { get; set; }

        public virtual ICollection<BookingCourseEntry> BookingCourseEntries { get; set; }=new List<BookingCourseEntry>();
    }
}