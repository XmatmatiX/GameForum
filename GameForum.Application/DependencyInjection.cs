using GameForum.Application.Interface;
using GameForum.Application.SendGrid;
using GameForum.Application.Service;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GameForum.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IMergedService, MergedService>();
            services.AddTransient<IRoleService, RoleService>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddTransient<IEmailSender, EmailSender>();
            
            return services;
        }
    }
}
