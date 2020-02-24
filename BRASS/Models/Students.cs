using BRASS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.Models
{
    public class Students
    {
        public virtual Guid StudentId
        {
            get;
            set;
        }
        public virtual string FirstName
        {
            get;
            set;
        }
        public virtual string LastName
        {
            get;
            set;
        }
        public virtual string ParentFName
        {
            get;
            set;
        }
        public virtual string ParentLName
        {
            get;
            set;
        }
        public virtual string ParentPhoneNumber
        {
            get;
            set;
        }
        public virtual string StudentStreetAddress
        {
            get;
            set;
        }
        public virtual string City
        {
            get;
            set;
        }
        public virtual string ZipCode
        {
            get;
            set;
        }
    }
}