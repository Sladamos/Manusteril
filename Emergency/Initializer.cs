using Emergency.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergency
{
    internal class Initializer
    { 

        private ApplicationDbContext applicationDbContext;

        public Initializer(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void Initialize()
        {
            applicationDbContext.Ensure();
        }
    }
}
