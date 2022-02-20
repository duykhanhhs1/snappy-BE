using _468_.Net_Fundamentals.Domain.Interface;
using _468_.Net_Fundamentals.Domain.Interface.Services;
using _468_.Net_Fundamentals.Domain.Repositories;
using _468_.Net_Fundamentals.Domain.ViewModels;
using _468_.Net_Fundamentals.Infrastructure;
using _468_.Net_Fundamentals.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace _468_.Net_Fundamentals.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure DbContext with Scoped lifetime   
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            }
            );

            services.AddScoped<Func<ApplicationDbContext>>((provider) => () => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork, UnitOfWorkBase>();


            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>)); 
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<ICardService, CardService>()
                .AddScoped<IProjectService, ProjectService>()
                .AddScoped<ITodoService, TodoService>()
                .AddScoped<ITagService, TagService>()
                .AddScoped<IBusinessService, BusinessService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IActivityService, ActivityService>()
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<ICommentService, CommentService>();

        }

        public static IServiceCollection AddCurrentUser(this IServiceCollection services)
        {
            return services.AddScoped<ICurrrentUser>(options =>
            {
                var httpContext = options.GetRequiredService<IHttpContextAccessor>().HttpContext;

                // 1. Check Authorization header
                // If null return null
                if (httpContext?.User?.Identity?.IsAuthenticated == true)
                {
                    var identity = httpContext.User.Identity as ClaimsIdentity;
                    /*Gets list of  claims
*/                  IEnumerable<Claim> claim = identity.Claims;

                    // Gets name from claims. Generally it's an email address.
                    var userNameClaim = claim
                        .Where(x => x.Type == "Name")
                        .FirstOrDefault().Value;

                    var userIdClaim = claim
                       .Where(x => x.Type == "Id")
                       .FirstOrDefault().Value;

                    // 3. Set claims info to CurrentUser
                    return new CurrentUser()
                    {
                        Id = userIdClaim,
                        UserName = userNameClaim
                    };
                }
                else
                {
                    //Handle what happens if that isn't the case
                    return null;
                    throw new Exception("The authorization header is either empty or isn't Bearer.");
                }      
            });
        }

    }
}
