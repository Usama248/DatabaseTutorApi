using CommonLayer;
using CommonLayer.Helper;
using CommonLayer.Helpers;
using DatabaseTutor.DTOs;
using EntityLayer.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CommonLayer.Constants;

namespace RepositoryLayer.DataSeeder
{
    public class Seeder
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DatabaseTutorDbContext _context;
        private static string createdById;

        public Seeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = _serviceProvider.GetRequiredService<DatabaseTutorDbContext>();
        }

        public async Task Seed()
        {
            await _context.Database.MigrateAsync();
            await AddRoles();
            await AddUsers();
        }

        private async Task AddUsers()
        {
            var _userManager = _serviceProvider.GetRequiredService<UserManager<DatabaseTutorUser>>();
            var _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var Admin = new DatabaseTutorUser()
            {
                UserName= "Admin@yopmail.com",
                FirstName = "Super",
                LastName = "Admin",
                Email = "Admin@yopmail.com",
            };
            var chkAdmin = await _userManager.CreateAsync(Admin, "Test@0000");
            if (chkAdmin.Succeeded)
            {
                var result1 = _userManager.AddToRoleAsync(Admin, Roles.Admin);
            }

            var Student = new DatabaseTutorUser()
            {
                FirstName = "Student",
                LastName = "One",
                Email = "Student@yopmail.com",
                UserName= "Student@yopmail.com"
            };
            var chkStudent = await _userManager.CreateAsync(Student, "Test@0000");
            if (chkStudent.Succeeded)
            {
                var result1 = _userManager.AddToRoleAsync(Student, Roles.Student);
            }

            var Teacher = new DatabaseTutorUser()
            {
                FirstName = "Teacher",
                LastName = "One",
                Email = "Teacher@yopmail.com",
                UserName= "Teacher@yopmail.com",
            };

            var chkTeacher = await _userManager.CreateAsync(Teacher, "Test@0000");
            if (chkTeacher.Succeeded)
            {
                var result1 = _userManager.AddToRoleAsync(Teacher, Roles.Teacher);
            }
        }

        private async Task<ResponseDTO<bool>> AddRoles()
        {
            var isSuccessfull = true;
            if (!await _context.Roles.AnyAsync())
            {
                var _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
             var roles = new List<IdentityRole>()
            {
               new IdentityRole() { Name = Roles.Admin},
               new IdentityRole() { Name = Roles.Student},
               new IdentityRole() { Name = Roles.Teacher},
            };
                foreach (var role in roles)
                {
                    var result = await _roleManager.CreateAsync(role);
                    if (!result.Succeeded)
                        isSuccessfull = false;
                }
            }

            if (isSuccessfull)
                return Responses.OKAdded("Roles", isSuccessfull);
            else
                return Responses.BadRequest("Error While Adding Roles", isSuccessfull);
        }

    }
}
