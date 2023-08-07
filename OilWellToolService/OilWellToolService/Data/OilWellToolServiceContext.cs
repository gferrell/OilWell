using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OilWellToolService;

namespace OilWellToolService.Data
{
    public class OilWellToolServiceContext : DbContext
    {
        public OilWellToolServiceContext (DbContextOptions<OilWellToolServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<OilWellTool> OilWellTool { get; set; }
    }
}
