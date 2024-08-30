using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Student_Course_MVC_MasterDetails.Models
{
    public class BookingCourseEntry
    {
        public int BookingCourseEntryId { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }


        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
     
    }
}