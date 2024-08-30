using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Student_Course_MVC_MasterDetails.Models
{
    public class BookingCourseDbContext:DbContext
    {
        
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<BookingCourseEntry> BookingCourseEntries { get; set; }
    }
}