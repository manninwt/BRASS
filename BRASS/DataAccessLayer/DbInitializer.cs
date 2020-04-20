using BRASS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BRASS.DataAccessLayer
{
    public class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            context.Database.EnsureCreated();

            using (var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (!context.RouteStops.Any())
                    {
                        var routeStops = new RouteStops[]
                        {
                    new RouteStops{},
                    new RouteStops{},
                    new RouteStops{},
                    new RouteStops{},
                        };

                        foreach (RouteStops rs in routeStops)
                        {
                            context.RouteStops.Add(rs);
                        }

                        context.SaveChanges();
                    }

                    if (!context.Students.Any())
                    {
                        var students = new Students[]
                        {
                    new Students{FirstName="Timon",LastName="Mannings",ParentFirstName="Wade",ParentLastName="Mannings",
                        ParentPhoneNumber ="9374417246",StreetAddress ="9353 County Road 101",City="Belle Center",ZipCode="43310",StopId=3},
                    new Students{FirstName="Bryan",LastName="Huddleston",ParentFirstName="Bryan",ParentLastName="Huddleston",
                        ParentPhoneNumber ="5561234532",StreetAddress ="2307 Liberty Street",City="West Chester",ZipCode="45530",StopId=4},
                    new Students{FirstName="Braden",LastName="Lance",ParentFirstName="Scott",ParentLastName="Lance",
                        ParentPhoneNumber ="5567898965",StreetAddress ="2311 Forest Hill Drive",City="West Chester",ZipCode="45211",StopId=5},
                    new Students{FirstName="Nathan",LastName="Boehringer",ParentFirstName="Jim",ParentLastName="Boehringer",
                        ParentPhoneNumber ="5563214756",StreetAddress ="5834 Vine Street",City="Clifton",ZipCode="45532",StopId=6},
                        };

                        foreach (Students s in students)
                        {
                            context.Students.Add(s);
                        }

                        context.SaveChanges();
                    }

                    if (!context.School.Any())
                    {
                        var schools = new School[]
                        {
                    new School{SchoolName="Benjamin Logan Highschool",SchoolAddress="2424 County Road 47",SchoolCity="Bellefontaine",SchoolZipCode="43311",
                        NumbBuses=59,ArrivalTime=new TimeSpan(7,45,0),DepartureTime=new TimeSpan(15,15,0),RouteGroup ='A',}
                        };

                        foreach (School s in schools)
                        {
                            context.School.Add(s);
                        }

                        context.SaveChanges();
                    }

                    if (!context.Buses.Any())
                    {
                        var buses = new Buses[]
                        {
                    new Buses{BusNumb=1,Capacity=30,Condition="FUNCTIONAL",Handicap="YES"},
                    new Buses{BusNumb=2,Capacity=30,Condition="FUNCTIONAL",Handicap="NO"},
                    new Buses{BusNumb=3,Capacity=30,Condition="NOT FUNCTIONAL",Handicap="NO"},
                    new Buses{BusNumb=4,Capacity=30,Condition="FUNCTIONAL",Handicap="NO"},
                    new Buses{BusNumb=5,Capacity=30,Condition="NOT FUNCTIONAL",Handicap="NO"},
                    new Buses{BusNumb=6,Capacity=30,Condition="FUNCTIONAL",Handicap="NO"},
                        };

                        foreach (Buses b in buses)
                        {
                            context.Buses.Add(b);
                        }

                        context.SaveChanges();
                    }

                    if (!context.Drivers.Any())
                    {
                        var drivers = new Drivers[]
                        {
                    new Drivers{FirstName="Timon",LastName="Mannings",Condition="INACTIVE",StartAddress="9353 County Road 101",StartCity="Belle Center",
                        StartZipCode ="43310"},
                    new Drivers{FirstName="Nathan",LastName="Boehringer",Condition="ACTIVE",StartAddress="9353 County Road 101",StartCity="Belle Center",
                        StartZipCode ="43310"},
                    new Drivers{FirstName="Bryan",LastName="Huddleston",Condition="ACTIVE",StartAddress="9353 County Road 101",StartCity="Belle Center",
                        StartZipCode ="43310"}
                        };

                        foreach (Drivers d in drivers)
                        {
                            context.Drivers.Add(d);
                        }

                        context.SaveChanges();
                    }

                    if (!context.Routes.Any())
                    {
                        var routes = new Routes[]
                        {
                    new Routes{RouteGroup='A'},
                    new Routes{RouteGroup='A'},
                    new Routes{RouteGroup='A'},
                        };

                        foreach (Routes r in routes)
                        {
                            context.Routes.Add(r);
                        }

                        context.SaveChanges();
                    }

                    dbContextTransaction.Commit();
                    return;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.InnerException);
                    return;
                    //Log, handle or absorbe I don't care ^_^
                }
            }
        }
    }
}
