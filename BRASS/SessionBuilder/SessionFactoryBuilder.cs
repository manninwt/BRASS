using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
using System.Linq;
using System.Reflection;
using BRASS.Mappings;

    public class SessionFactoryBuilder
    {
        public static NHibernate.ISessionFactory BuildSessionFactory(string connectionStringName, bool create = false, bool update = false)
        {
        return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringName))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentsMap>())
                .ExposeConfiguration(cfg => BuildSchema(cfg, create, update)).BuildSessionFactory();
        }
        
        private static void BuildSchema(NHibernate.Cfg.Configuration config, bool create = false, bool update = false)
        {
            if (create)
            {
                new SchemaUpdate(config).Execute(false, update);
            }
            else
            {
                new SchemaExport(config).Create(false, true);
            }
        }
    }
