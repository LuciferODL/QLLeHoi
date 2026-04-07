using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QLLeHoi.Models;

namespace QLLeHoi.Data
{
    public class QLLeHoiContext : DbContext
    {
        public QLLeHoiContext (DbContextOptions<QLLeHoiContext> options)
            : base(options)
        {
        }

        public DbSet<QLLeHoi.Models.Festival> Festival { get; set; } = default!;
        public DbSet<QLLeHoi.Models.Organizer> Organizer { get; set; } = default!;
    }
}
