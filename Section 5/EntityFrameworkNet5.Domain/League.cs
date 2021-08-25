using EntityFrameworkNet5.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkNet5.Domain
{
    public class League : BaseDomainObject
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
    }
}
