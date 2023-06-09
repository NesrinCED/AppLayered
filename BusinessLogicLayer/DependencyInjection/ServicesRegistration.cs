﻿using BusinessLogicLayer.IServices;
using BusinessLogicLayer.Services;
using DataAccessLayer.IRepository;
using DataAccessLayer.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DependencyInjection
{
    public static class ServicesRegistration
    {


        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IProjectService,ProjectService>();

            services.AddScoped<ITemplateService, TemplateService>();

            services.AddScoped<ITemplateHistoryService, TemplateHistoryService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddScoped<IProjectRepository, ProjectRepository>();

            services.AddScoped<ITemplateRepository, TemplateRepository>();

            services.AddScoped<ITemplateHistoryRepository, TemplateHistoryRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IProjectAuthorizationRepository, ProjectAuthorizationRepository>();

            services.AddScoped<IProjectAuthorizationService, ProjectAuthorizationService>();

            return services;
        }
    }
}
