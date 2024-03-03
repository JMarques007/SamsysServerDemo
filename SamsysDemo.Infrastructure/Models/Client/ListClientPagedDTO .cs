using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.Infrastructure.Models.Client
{
    public class ListClientPagedDTO
    {
        public long totalPages { get; set; }
        public IEnumerable<ClientDTO> clients { get; set; }
    }
}
