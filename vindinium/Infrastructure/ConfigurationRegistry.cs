using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Mappers;
using StructureMap;
using vindinium.Infrastructure.Behaviors.Map;
using vindinium.Infrastructure.Behaviors.Models;
using vindinium.Infrastructure.Behaviors.Movement;
using vindinium.Infrastructure.Mappings;

namespace vindinium.Infrastructure
{
    public class ConfigurationRegistry : Registry
    {
        public ConfigurationRegistry()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<HeroMapping>();
            });
            config.AssertConfigurationIsValid();

            var mapper = config.CreateMapper();

            For<IMapper>().Add(() => mapper);

        }
    }
}
