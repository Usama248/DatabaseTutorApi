using EntityLayer.DbContext;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Infrastructures;
using System;
using System.Threading.Tasks;
using UnitOfWork.UnitOfWork;

namespace SonetCrm.Buisness.UnitOfWorks.Portal
{
    public class DatabaseTutorUnitOfWork : IDatabaseTutorUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly DatabaseTutorDbContext _context;
        public DatabaseTutorUnitOfWork(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _context = _serviceProvider.GetRequiredService<DatabaseTutorDbContext>();
        }

        public IUserRepo UserRepository => _serviceProvider.GetRequiredService<IUserRepo>();
        public IClassRepo ClassRepo => _serviceProvider.GetRequiredService<IClassRepo>();
        public IAssignmentRepo AssignmentRepo => _serviceProvider.GetRequiredService<IAssignmentRepo>();

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
