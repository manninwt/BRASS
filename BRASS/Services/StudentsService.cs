using System;
using System.Linq;
using BRASS.Models;

namespace BRASS.Services
{
    public class StudentsService
    {
        public static string AddStudent(string studentFirstName, string studentLastName, string parentFirstName, string parentLastName, string parentPhoneNumber, string studentStreetAddress, string city, string zipCode)
        {
            // create our NHibernate session factory  
            string connectionStringName = "Data Source=DESKTOP-UG9TO2R;Initial Catalog=BRASS;Integrated Security=True";
            var sessionFactory = SessionFactoryBuilder.BuildSessionFactory(connectionStringName, true, true);
            using (var session = sessionFactory.OpenSession())
            {
                // populate the database  
                using (var transaction = session.BeginTransaction())
                {
                    //Create student object
                    var student = new Students
                    {
                        FirstName = studentFirstName,
                        LastName = studentLastName,
                        ParentFName = parentFirstName,
                        ParentLName = parentLastName,
                        ParentPhoneNumber = parentPhoneNumber,
                        StudentStreetAddress = studentStreetAddress,
                        City = city,
                        ZipCode = zipCode
                    };
                    // Save student
                    session.SaveOrUpdate(student);
                    //session.SaveOrUpdate(student);
                    transaction.Commit();
                }
                using (var session2 = sessionFactory.OpenSession())
                {
                    //Retreive the student to be added
                    using (session2.BeginTransaction())
                    {
                        var student = session.CreateCriteria(typeof(BRASS.Models.Students)).List<BRASS.Models.Students>();
                        return StudentsService.GetStudentInfo(student.First());
                    }
                }
            }
        }

        public static string GetStudentInfo(Students student)
        {
            Console.WriteLine(student.FirstName);
            Console.WriteLine(student.LastName);
            Console.WriteLine(student.StudentStreetAddress);
            Console.WriteLine();
            return ("Success");
        }
    }
}
