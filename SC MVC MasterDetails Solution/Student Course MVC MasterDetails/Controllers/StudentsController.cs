using Student_Course_MVC_MasterDetails.Models;
using Student_Course_MVC_MasterDetails.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Student_Course_MVC_MasterDetails.Controllers
{
    public class StudentsController : Controller
    {
        private BookingCourseDbContext db=new BookingCourseDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(c=> c.BookingCourseEntries.Select(y=>y.Course)).OrderByDescending(x=>x.StudentId).ToList();

            return View(students);
        }

        public ActionResult AddNewCourse(int? id)
        {
            ViewBag.Course = new SelectList(db.Courses.ToList(), "CourseId", "CourseName", (id != null) ? id.ToString() : "");
            return PartialView("_addNewCourse");
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(StudentVM studentVM, int[] courseId)
        {
            if (ModelState.IsValid)
            {
                Student student = new Student()
                {
                    StudentName=studentVM.StudentName,
                    BirthDate = studentVM.BirthDate,
                    Age = studentVM.Age,
                    MaritalStatus = studentVM.MaritalStatus
                };

                //Image
                HttpPostedFileBase file = studentVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    student.Picture = filePath;
                }
                //All Course 

                foreach (var item in courseId)
                {
                    BookingCourseEntry bookingCourseEntry = new BookingCourseEntry()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        CourseId = item
                    };
                    db.BookingCourseEntries.Add(bookingCourseEntry);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                return View();
        }

        public ActionResult Edit(int? id)
        {
            Student student =db.Students.First(x=>x.StudentId == id);
            var studentCourses = db.BookingCourseEntries.Where(x => x.StudentId == id).ToList();

            StudentVM studentVM =new StudentVM()
            {
                StudentId = student.StudentId,
                StudentName = student.StudentName,
                Age = student.Age,
                BirthDate = student.BirthDate,
                Picture = student.Picture,
                MaritalStatus = student.MaritalStatus
            };

            if (studentCourses.Count() > 0)
            {
                foreach (var item in studentCourses)
                {
                    studentVM.StudentCourseList.Add(item.StudentId);
                }
            }

            return View(studentVM);
        }
        [HttpPost]
        public ActionResult Edit(StudentVM studentVM, int[] CourseId)
        {
            if (ModelState.IsValid)
            {
                Student student  = new Student()
                {
                    StudentId = studentVM.StudentId,
                    StudentName = studentVM.StudentName,
                    BirthDate = studentVM.BirthDate,
                    Age = studentVM.Age,
                    MaritalStatus = studentVM.MaritalStatus
                };

                //Image
                HttpPostedFileBase file = studentVM.PictureFile;
                if (file != null)
                {
                    string filePath = Path.Combine("/Images", DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName));
                    file.SaveAs(Server.MapPath(filePath));
                    student.Picture = filePath;
                }
                else
                {
                    //string fName= clientVM.PictureFile.FileName.ToString();
                    student.Picture = studentVM.Picture;
                }

                //Course Delete
                var existsCourseEntry = db.BookingCourseEntries.Where(x => x.StudentId == student.StudentId).ToList();

                foreach (var bookingCourseEntry in existsCourseEntry)
                {
                    db.BookingCourseEntries.Remove(bookingCourseEntry);
                }

                //Add Course
                foreach (var item in CourseId)
                {
                    BookingCourseEntry bookingCourseEntry = new BookingCourseEntry()
                    {
                        Student = student,
                        StudentId = student.StudentId,
                        CourseId = item
                    };
                    db.BookingCourseEntries.Add(bookingCourseEntry);
                }
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Delete(int? id)
        {
            var student = db.Students.Find(id);
            var existsCourseEntry = db.BookingCourseEntries.Where(x => x.StudentId == student.StudentId).ToList();

            foreach (var CourseEntries in existsCourseEntry)
            {
                db.BookingCourseEntries.Remove(CourseEntries);
            }
            db.Entry(student).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}