using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Student_Course_MVC_MasterDetails.Models.ViewModels
{
    public class StudentVM
    {
        public int StudentId { get; set; }
        [Required, Display(Name = "Client Name"), StringLength(50)]
        public string StudentName { get; set; }
        [Required, Display(Name = "Birth Date"), DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Picture { get; set; }
        [Display(Name = "Picture")]
        public HttpPostedFileBase PictureFile { get; set; }
        public bool MaritalStatus { get; set; }

        public List<int> StudentCourseList { get; set; }=new List<int>();
    }
}