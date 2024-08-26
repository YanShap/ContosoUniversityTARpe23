using ContosoUniversity.Models;

namespace ContosoUniversity.Data
{
    public class DbInitializer
    {
        public static void Initializer(SchoolContext context)
        {
            context.Database.EnsureCreated();
            if (context.Students.Any()) ;
            {
                return;
            }
            var students = new Student[]
            {
                new Student(FirstMidName="Artjom"),LastName="Skatškov")
            }
        }
    }
}
