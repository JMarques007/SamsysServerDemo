using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.Infrastructure.Entities
{
    public class Client
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; private set; }

        public bool IsRemoved { get; set; } = false;

        public DateTime? DateRemoved { get; set; }

        public void Update(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public void SetStatus(bool status)
        {
            IsActive = status;
        }

    }





}
