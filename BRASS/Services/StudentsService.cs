using System;
using System.Data.SqlClient;
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
            using (var session = NHibernateHelper.OpenSession(connectionStringName))
            {
                // populate the database  
                using (var transaction = session.BeginTransaction())
                {
                    //Create student object
                    var student = new Student
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
                using (var session2 = NHibernateHelper.OpenSession(connectionStringName))
                {
                    //Retreive the student to be added
                    using (session2.BeginTransaction())
                    {
                        var student = session.CreateCriteria(typeof(BRASS.Models.Student)).List<BRASS.Models.Student>();
                        return StudentsService.GetStudentInfo(student.First());
                    }
                }
            }
        }

        public static string GetStudentInfo(Student student)
        {
            Console.WriteLine(student.FirstName);
            Console.WriteLine(student.LastName);
            Console.WriteLine(student.StudentStreetAddress);
            Console.WriteLine();
            return ("Success");
        }
    }
}
