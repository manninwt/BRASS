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
            var sessionFactory = SessionFactoryBuilder.BuildSessionFactory(connectionStringName, true, true);
            using (var session = sessionFactory.OpenSession())
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
                using (var session2 = sessionFactory.OpenSession())
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

        public string getWhileLoopData()
        {
            string connectionStringName = "Data Source=DESKTOP-UG9TO2R;Initial Catalog=BRASS;Integrated Security=True";
            string htmlStr = "";
            SqlConnection thisConnection = new SqlConnection(connectionStringName);
            SqlCommand thisCommand = thisConnection.CreateCommand();
            thisCommand.CommandText = "SELECT * FROM StudentInfo";
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string Name = reader.GetString(1);
                string Pass = reader.GetString(2);
                htmlStr += "<tr><td>" + id + "</td><td>" + Name + "</td><td>" + Pass + "</td></tr>";
            }

            thisConnection.Close();
            return htmlStr;
        }
    }
}
