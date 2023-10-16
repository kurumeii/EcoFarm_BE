﻿using EcoFarm.Domain.Common;
using EcoFarm.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.Application.Features.EcoServiceFeature.Commands.CreateService
{
    public class ServiceCreatedEvent : BaseEvent
    {
        public ServicePackage ServicePackage { get; set; }

        public ServiceCreatedEvent(ServicePackage servicePackage)
        {
            ServicePackage = servicePackage;
        }
    }
}