
using System;
using System.Threading.Tasks;
using MobilFit.Domain;

namespace MobilFit.Backend.Models
{
    

    public class LocalDataContext : DataContext
    {
        public System.Data.Entity.DbSet<MobilFit.Domain.User> Users { get; set; }

        internal Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        internal void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}