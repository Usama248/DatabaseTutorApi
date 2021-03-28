using EntityLayer.DbContext.Entities;
using EntityLayer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DbContext
{
    public class DatabaseTutorUser : IdentityUser, ISoftDelete
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; }
        public ICollection<TeacherClass> TeacherClasses { get; set; }
        public  StudentClass StudentClass { get; set; }
    }
}
