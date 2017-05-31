using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsportProject.Models.DBmodels
{

    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options)
            : base(options)
        { }
        public DbSet<Contact> Contact { get; set; }
    }
    public class Contact
    {
        public int ContactID { get; set; }
        public string Message { get; set; }
        public string Answer { get; set; }
        public string Contactmailadress { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

