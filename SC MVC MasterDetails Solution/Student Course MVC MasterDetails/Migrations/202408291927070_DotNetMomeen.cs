namespace Student_Course_MVC_MasterDetails.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DotNetMomeen : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookingCourseEntries",
                c => new
                    {
                        BookingCourseEntryId = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingCourseEntryId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        CourseName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.Int(nullable: false, identity: true),
                        StudentName = c.String(nullable: false, maxLength: 50),
                        BirthDate = c.DateTime(nullable: false),
                        Age = c.Int(nullable: false),
                        Picture = c.String(),
                        MaritalStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingCourseEntries", "StudentId", "dbo.Students");
            DropForeignKey("dbo.BookingCourseEntries", "CourseId", "dbo.Courses");
            DropIndex("dbo.BookingCourseEntries", new[] { "CourseId" });
            DropIndex("dbo.BookingCourseEntries", new[] { "StudentId" });
            DropTable("dbo.Students");
            DropTable("dbo.Courses");
            DropTable("dbo.BookingCourseEntries");
        }
    }
}
