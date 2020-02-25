using FluentNHibernate.Mapping;
using BRASS.Models;


namespace BRASS.Mappings
{
    public class StudentsMap : ClassMap<Student>
    {
        public StudentsMap()
        {
            Id(x => x.StudentId).GeneratedBy.Guid();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.ParentFName);
            Map(x => x.ParentLName);
            Map(x => x.ParentPhoneNumber);
            Map(x => x.StudentStreetAddress);
            Map(x => x.City);
            Map(x => x.ZipCode);
            Table("StudentInfo");
        }
    }
}