using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.Infrastructure.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        Task SaveAsync();

        IClientRepository ClientRepository { get; }

    }
}
