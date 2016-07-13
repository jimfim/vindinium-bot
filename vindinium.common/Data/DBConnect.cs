using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using vindinium.common.Entities;

namespace vindinium.common.Data
{
    public static class DbConnect
    {
        public static ISessionFactory InitializeDBconnection()
        {
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .ConnectionString("Data Source=JIM-PC\\SQLEXPRESS;Integrated Security=SSPI;Initial Catalog=vindinium"))
                .Mappings(m =>
                    m.AutoMappings
                        .Add(AutoMap.AssemblyOf<Hero>()))
                    .ExposeConfiguration(Config)
                .BuildSessionFactory();
            return sessionFactory;
        }

        private static void Config(Configuration configuration)
        {

            // this NHibernate tool takes a configuration (with mapping info in)
            // and exports a database schema from it
            new SchemaExport(configuration).Create(false, true);
        }
    }
}
