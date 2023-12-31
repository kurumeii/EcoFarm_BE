﻿using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.Tasks.ServicePackageFeatures.Commands.Create
{
    public class ServiceCreatedEvent : BaseEvent
    {
        public FarmingPackage ServicePackage { get; set; }

        public ServiceCreatedEvent(FarmingPackage servicePackage)
        {
            ServicePackage = servicePackage;
        }
    }
}