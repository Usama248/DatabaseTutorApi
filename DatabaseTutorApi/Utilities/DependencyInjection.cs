using CommonLayer.Helpers.TokenService;
using EntityLayer.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.DataSeeder;
using RepositoryLayer.Infrastructures;
using RepositoryLayer.Repos;
using SonetCrm.Buisness.UnitOfWorks.Portal;
using UnitOfWork.UnitOfWork;

namespace DatabaseTutorApi.Buisness.DIHelper
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterCustomServices(this IServiceCollection services)
        {
            //DbContext
            services.AddDbContext<DatabaseTutorDbContext>();

            //Identity Service
            services.AddIdentity<DatabaseTutorUser,IdentityRole>(option =>
            {
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = true;
                option.Password.RequireUppercase = false;
                option.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<DatabaseTutorDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<ITokenService, TokenService>();

            #region DataBaseTutor Database Services
            services.AddScoped<IUserRepo, UserRepo>();
            services.AddScoped<IAssignmentRepo, AssignmentRepo>();
            services.AddScoped<IClassRepo,  ClassRepo>();
            #endregion


            //Seeder
            services.AddTransient<Seeder>();



            //Unit Of Work
            services.AddScoped<IDatabaseTutorUnitOfWork, DatabaseTutorUnitOfWork>();

            //Cache
            services.AddMemoryCache();

           

            return services;
        }
    }
}