using RepositoryLayer.Infrastructures;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWork.UnitOfWork
{
    public interface IDatabaseTutorUnitOfWork
    {
        IUserRepo UserRepository { get; }
        IClassRepo ClassRepo { get; }
        IAssignmentRepo AssignmentRepo { get; }
        Task<bool> Save();
    }
}
