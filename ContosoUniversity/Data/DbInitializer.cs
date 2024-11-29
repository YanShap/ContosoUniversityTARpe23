using ContosoUniversity.Data;
using ContosoUniversity.Models;
using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            if (context.Students.Any())
            {
                return;
            }

            var students = new Student[]
           {
                new Student {FirstMidName="Karl" , LastName="Ostrov",EnrollmentDate=DateTime.Parse("2068-04-12")},
                new Student {FirstMidName="Sophie" , LastName="Miller",EnrollmentDate=DateTime.Parse("2010-06-30")},
                new Student {FirstMidName="Dmitri" , LastName="Ivanov",EnrollmentDate=DateTime.Parse("2008-09-15")},
                new Student {FirstMidName="Elisabeth" , LastName="Brown",EnrollmentDate=DateTime.Parse("2053-12-11")},
                new Student {FirstMidName="Michael" , LastName="Stewart",EnrollmentDate=DateTime.Parse("2007-10-05")},
                new Student {FirstMidName="Alexander" , LastName="Wolff",EnrollmentDate=DateTime.Parse("2003-09-01")},
                new Student {FirstMidName="Nikolai" , LastName="Borisov",EnrollmentDate=DateTime.Parse("2022-05-15")},
                new Student {FirstMidName="Maxim" , LastName="Romanov",EnrollmentDate=DateTime.Parse("2013-02-28")},
                new Student {FirstMidName="Miles" , LastName="Jackson",EnrollmentDate=DateTime.Parse("2011-03-14")},
                new Student {FirstMidName="Lionel" , LastName="Messi",EnrollmentDate=DateTime.Parse("2000-04-10")},
            };

            //iga õpilane lisatakse ükshaaval läbi foreach tsükli
            foreach (Student student in students)
            {
                context.Students.Add(student);
            }
            //ja andmebaasi muudatused salvestatakse
            context.SaveChanges();

            var courses = new Course[]
            {
                new Course{CourseID=2051, Title="Physics", Credits=4},
                new Course{CourseID=4321, Title="History", Credits=3},
                new Course{CourseID=5005, Title="Spanish Language", Credits=2},
                new Course{CourseID=2098, Title="Statistics", Credits=3},
                new Course{CourseID=7712, Title="Algebra", Credits=3},
                new Course{CourseID=9247, Title="Geometry", Credits=2},
                new Course{CourseID=6578, Title="Art Appreciation", Credits=2},
                new Course{CourseID=1223, Title="World Literature", Credits=4},
            };
            context.Courses.AddRange(courses);
            context.SaveChanges();

            if (context.Enrollments.Any()) { return; }

            var enrollments = new Enrollment[]
             {
                new Enrollment{StudentID=1, CourseID=2051, Grade=Grade.A},
                new Enrollment{StudentID=1, CourseID=4321, Grade=Grade.B},
                new Enrollment{StudentID=1, CourseID=5005, Grade=Grade.C},

                new Enrollment{StudentID=2, CourseID=2098, Grade=Grade.B},
                new Enrollment{StudentID=2, CourseID=7712, Grade=Grade.F},
                new Enrollment{StudentID=3, CourseID=1223},

                new Enrollment{StudentID=4, CourseID=1223},
                new Enrollment{StudentID=4, CourseID=6578, Grade=Grade.F},

                new Enrollment{StudentID=5, CourseID=9247, Grade=Grade.C},

                new Enrollment{StudentID=6, CourseID=1223},

                new Enrollment{StudentID=7, CourseID=2051, Grade=Grade.A},

                new Enrollment{StudentID=10, CourseID=4321, Grade=Grade.A},
            };
            context.Enrollments.AddRange(enrollments);
            context.SaveChanges();

            if (context.Instructors.Any()) { return; }
            var instructors = new Instructor[]
            {
                new Instructor
                {
                    LastName = "Smith",
                    FirstMidName = "John",
                    HireDate = DateTime.Parse("2070-05-19"),
                },
            };
            context.Instructors.AddRange(instructors);
            context.SaveChanges();

            if (context.Departments.Any()) { return; }
            var departments = new Department[]
           {
                new Department
                {
                    Name = "Computer Science",
                    Budget = 5000,
                    StartDate = DateTime.Parse("2024-10-03"),
                    InstructorId = 1,
                    Aadress = "Main St 123",
                },
                new Department
                {
                    Name = "English",
                    Budget = 1000,
                    StartDate = DateTime.Parse("2024-08-02"),
                    InstructorId = 2,
                    Aadress = "Pae 14"
                },
            };
            context.Departments.AddRange(departments);
            context.SaveChanges();
        }
    }
}