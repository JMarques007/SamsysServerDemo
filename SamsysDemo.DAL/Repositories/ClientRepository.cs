using Microsoft.EntityFrameworkCore;
using SamsysDemo.Infrastructure.Entities;
using SamsysDemo.Infrastructure.Helpers;
using SamsysDemo.Infrastructure.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.DAL.Repositories
{
    public class ClientRepository :  IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context) 
        {
            _context = context;
        }     

        public async Task Delete(object id, string userDelete, string concurrencyToken)
        {
            Client? entityToDelete = await _context.Clients.FindAsync(id);
            if(entityToDelete is not null)
            {
                entityToDelete.IsRemoved = true;
                entityToDelete.DateRemoved = DateTime.Now;
                if (concurrencyToken != null)
                {
                    _context.Entry(entityToDelete).Property("ConcurrencyToken").OriginalValue = Convert.FromBase64String(concurrencyToken);
                }
            }
        }    

        public async Task<Client?> GetById(object id, string[]? includedProperties = null)
        {
            var item = await _context.Clients.FindAsync(id);
            if (item is not null)
            {
                var dbSet = _context.Clients.AsQueryable();
                if (includedProperties is not null)
                {
                    foreach (var property in includedProperties)
                    {
                        dbSet = dbSet.Include(property);
                    }
                    await dbSet.LoadAsync();
                }
            }
            return item;
        }

        public async Task Insert(Client entityToInsert)
        {
            await _context.Clients.AddAsync(entityToInsert);
        }

        public void Update(Client entityToUpdate, string concurrencyToken)
        {
            if (concurrencyToken != null)
            {
                _context.Entry(entityToUpdate).Property("ConcurrencyToken").OriginalValue = Convert.FromBase64String(concurrencyToken);
            }
        }
    }

}