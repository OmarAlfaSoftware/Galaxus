using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.Strategy_Builder.Entity_Builder;
using GalaxusIntegration.Core.Entities;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Strategy_Builder
{
    public class EntityBuilderStrategy
    {


        public IEntityBuilder GetStrategy(DocumentType type) 
        {
            switch (type)
            {
                case DocumentType.ORDER:
                    return new OrderBuilder();
                    break;
                default: break;
            }

            return null;
        }
    }
}
