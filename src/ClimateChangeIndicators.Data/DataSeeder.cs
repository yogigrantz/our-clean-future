﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bogus;
using ClimateChangeIndicators.Data.Entities;
using Action = ClimateChangeIndicators.Data.Entities.Action;

namespace ClimateChangeIndicators.Data
{
    internal class DataSeeder
    {
        private readonly ModelBuilder _modelBuilder;

        public DataSeeder(ModelBuilder modelBuilder)
        {
            _modelBuilder = modelBuilder;
        }

        public void Init()
        {
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 1, Name = "Kilograms of carbon dioxide equivalent", Symbol = "kgCO₂e" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 2, Name = "Count", Symbol = "count" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 3, Name = "Megawatts", Symbol = "MW" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 4, Name = "Dollars", Symbol = "$" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 5, Name = "Percent", Symbol = "%" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 6, Name = "Proportion", Symbol = "proportion" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 7, Name = "Tons of carbon dioxide equivalent per litre", Symbol = "tCO₂e/L" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 8, Name = "Kilotons of carbon dioxide equivalent", Symbol = "ktCO₂e" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 9, Name = "Tons of carbon dioxide equivalent per person", Symbol = "tCO₂e/person" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 10, Name = "Litres", Symbol = "L" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 11, Name = "Gigawatt hours", Symbol = "GWh" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 12, Name = "Cubic metres", Symbol = "m³" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 13, Name = "Kilometres", Symbol = "km" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 14, Name = "Cubic kilometres", Symbol = "km³" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 15, Name = "Tons per person", Symbol = "t/person" });
            _modelBuilder.Entity<UnitOfMeasurement>().HasData(new UnitOfMeasurement { Id = 16, Name = "Kilotonnes per million chained (2012) dollars", Symbol = "kt/$1MM chained (2012)" });

            _modelBuilder.Entity<Area>().HasData(new Area { Id = 1, Title = "Transportion" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 2, Title = "Homes and buildings" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 3, Title = "Energy production" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 4, Title = "People and the environment" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 5, Title = "Communities" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 6, Title = "Innovation" });
            _modelBuilder.Entity<Area>().HasData(new Area { Id = 7, Title = "Leadership" });

            _modelBuilder.Entity<Goal>().HasData(new Goal { Id = 1, Title = "Reducing greenhouse gas emissions" });
            _modelBuilder.Entity<Goal>().HasData(new Goal { Id = 2, Title = "Ensuring Yukoners have access to reliable, affordable and renewable energy" });
            _modelBuilder.Entity<Goal>().HasData(new Goal { Id = 3, Title = "Adapting to the impacts of climate change" });
            _modelBuilder.Entity<Goal>().HasData(new Goal { Id = 4, Title = "Building a green economy" });

            _modelBuilder.Entity<Organization>().HasData(new Organization { Id = 1, Name = "Yukon Government" });
            _modelBuilder.Entity<Organization>().HasData(new Organization { Id = 2, Name = "ATCO Electric Yukon" });

            _modelBuilder.Entity<Department>().HasData(new Department { Id = 1, Name = "Community Services", ShortName = "CS" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 2, Name = "Economic Development", ShortName = "EcDev" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 3, Name = "Education", ShortName = "EDU" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 4, Name = "Energy, Mines and Resources", ShortName = "EMR" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 5, Name = "Environment", ShortName = "ENV" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 6, Name = "Executive Council Office", ShortName = "ECO" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 7, Name = "Finance", ShortName = "FIN" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 8, Name = "Health and Social Services", ShortName = "HSS" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 9, Name = "Highways and Public Works", ShortName = "HPW" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 10, Name = "Justice", ShortName = "JUS" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 11, Name = "Public Service Commission", ShortName = "PSC" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 12, Name = "Tourism and Culture", ShortName = "TC" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 13, Name = "Yukon Development Corporation", ShortName = "YDC" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 14, Name = "Yukon Energy Corporation", ShortName = "YEC" });
            _modelBuilder.Entity<Department>().HasData(new Department { Id = 15, Name = "Yukon Housing Corporation", ShortName = "YHC" });

            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 1, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 2, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 3, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 4, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 5, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 6, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 7, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 8, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 9, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 10, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 11, OrganizationId = 1 });
            _modelBuilder.Entity<Owner>().HasData(new Owner { Id = 12, OrganizationId = 2 });

            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 1, DepartmentId = 5, Name = "Climate Change Secretariat", OwnerId = 1 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 2, DepartmentId = 5, Name = "Water Resources", OwnerId = 2 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 3, DepartmentId = 5, Name = "Parks", OwnerId = 3 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 4, DepartmentId = 5, Name = "Fish and Wildlife", OwnerId = 4 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 5, DepartmentId = 5, Name = "Conservation Officer Services", OwnerId = 5 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 6, DepartmentId = 4, Name = "Forest Resources", OwnerId = 6 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 7, DepartmentId = 4, Name = "Agriculture", OwnerId = 7 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 8, DepartmentId = 4, Name = "Energy", OwnerId = 8 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 9, DepartmentId = 9, Name = "Supply Services", OwnerId = 9 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 10, DepartmentId = 9, Name = "Strategic Initiatives", OwnerId = 10 });
            _modelBuilder.Entity<Branch>().HasData(new Branch { Id = 11, DepartmentId = 12, Name = "Culture Services", OwnerId = 11 });

            _modelBuilder.Entity<Action>().HasData(new Action { Id = 1, ReferenceNumber = "", ReferenceText = "Increase the number of zero emission vehicles on our roads." });
            _modelBuilder.Entity<Action>().HasData(new Action { Id = 2, ReferenceNumber = "", ReferenceText = "Ensure Yukoners have access to reliable, affordable and renewable energy." });
            _modelBuilder.Entity<Action>().HasData(new Action { Id = 3, ReferenceNumber = "", ReferenceText = "Conduct energy assessments of Government of Yukon buildings to identify opportunities for energy efficiency and greenhouse gas reductions, with the first period of assessments completed by 2025 and the second period completed by 2030." });
            _modelBuilder.Entity<Action>().HasData(new Action { Id = 4, ReferenceNumber = "", ReferenceText = "Supply more of what we eat through sustainable local food production." });
            _modelBuilder.Entity<Action>().HasData(new Action { Id = 5, ReferenceNumber = "", ReferenceText = "LEADERSHIP" });

            _modelBuilder.Entity<Indicator>()
                .HasData(new Indicator {
                    Id = 1,
                    CollectionInterval = CollectionInterval.Biannual,
                    DataType = DataType.Cumulative,
                    Description = "Total number of heavy-duty zero emission vehicles registered in Yukon.",
                    Title = "ZEVs - Total heavy duty",
                    IsActive = true,
                    ActionId = 1,
                    OwnerId = 8,
                    UnitOfMeasurementId = 2
                });

            _modelBuilder.Entity<Indicator>()
                .HasData(new Indicator {
                    Id = 2,
                    CollectionInterval = CollectionInterval.Annual,
                    DataType = DataType.Incremental,
                    Description = "Total electricity generated off-grid during the reporting year (thermal and renewable).",
                    Title = "Off-grid generation - total",
                    IsActive = true,
                    ActionId = 2,
                    OwnerId = 12,
                    UnitOfMeasurementId = 2
                });

            _modelBuilder.Entity<Indicator>()
                .HasData(new Indicator {
                    Id = 3,
                    CollectionInterval = CollectionInterval.Biannual,
                    DataType = DataType.Cumulative,
                    Description = "Number of energy assessments of Government of Yukon buildings completed during the reporting year.",
                    Title = "YG buildings - energy assessments completed",
                    IsActive = true,
                    ActionId = 3,
                    OwnerId = 10,
                    UnitOfMeasurementId = 2
                });

            _modelBuilder.Entity<Indicator>()
                .HasData(new Indicator {
                    Id = 4,
                    CollectionInterval = CollectionInterval.Annual,
                    DataType = DataType.Incremental,
                    Description = "Yukon Agricultural Self-Sufficiency Indicator",
                    Title = "Food self-sufficiency",
                    IsActive = true,
                    ActionId = 4,
                    OwnerId = 7,
                    UnitOfMeasurementId = 2
                });

            _modelBuilder.Entity<Indicator>()
                .HasData(new Indicator {
                    Id = 5,
                    CollectionInterval = CollectionInterval.Annual,
                    DataType = DataType.Incremental,
                    Description = "Total GHGs from the Government of Yukon's operations",
                    Title = "YG GHGs - total",
                    IsActive = true,
                    ActionId = 5,
                    OwnerId = 1,
                    UnitOfMeasurementId = 1
                });

            //var indicatorId = 1;
            //var indicators = new Faker<Indicator>("en_CA")
            //    .RuleFor(i => i.Id, _ => indicatorId++)
            //    .RuleFor(i => i.CollectionInterval, _ => _.PickRandom<CollectionInterval>())
            //    .RuleFor(i => i.DataType, _ => _.PickRandom<DataType>())
            //    .RuleFor(i => i.Description, _ => "Total number of heavy-duty zero emission vehicles registered in Yukon.")
            //    .RuleFor(i => i.DisplayName, _ => "ZEVs - Total heavy duty")
            //    .RuleFor(i => i.IsActive, _ => true)
            //    .RuleFor(i => i.OurCleanFutureReferenceId, _ => 1)
            //    .RuleFor(i => i.OwnerId, _ => 1);
            //_modelBuilder.Entity<Indicator>().HasData(indicators.Generate(1));
        }
    }
}