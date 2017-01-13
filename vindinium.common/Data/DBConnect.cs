using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using vindinium.common.ClassMap;
using vindinium.common.Entities;

namespace vindinium.common.Data
{
    public static class DbConnect
    {
        public static ISessionFactory InitializeDBconnection()
        {
            var sessionFactory = Fluently.Configure()
                .Database(SQLiteConfiguration.Standard
                .UsingFile("vindinium.db"))
                //.InMemory())
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<PosMap>())
                    .ExposeConfiguration(Config)
                .BuildSessionFactory();
            return sessionFactory;
        }

        private static void Config(Configuration configuration)
        {

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(configuration).Create(true, true);
        }
    }
}
