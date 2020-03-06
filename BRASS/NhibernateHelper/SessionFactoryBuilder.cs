using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate;
using BRASS.Mappings;

    public class NHibernateHelper
    {
        public static NHibernate.ISession OpenSession(string connectionStringName, bool create = false, bool update = false)
        {

        ISessionFactory sessionFactory = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionStringName))
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<StudentsMap>())
            .ExposeConfiguration(cfg => new SchemaExport(cfg)
            .Create(false, false))
            .BuildSessionFactory();
        return sessionFactory.OpenSession();

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
