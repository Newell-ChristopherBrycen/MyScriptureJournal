using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyScriptureJournal.Models;

namespace MyScriptureJournal.Data
{
    public class MyScriptureContext : DbContext
    {
        public MyScriptureContext(DbContextOptions<MyScriptureContext> options) : base(options)
        {

        }

        public DbSet<MyScriptureJournal.Models.Scripture> Scripture { get; set; }
    }
}

