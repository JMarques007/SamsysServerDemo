using SamsysDemo.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork // public class UnitOfWork<T> where T : DbContext (uma class que criamos e tudo o resto herda) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }


        private IClientRepository _clientRepository;
        public IClientRepository ClientRepository
        {
            get
            {
                if (_clientRepository is null)
                {
                    _clientRepository = new ClientRepository(_context);
                }
                return _clientRepository;
            }
        }
    }

}