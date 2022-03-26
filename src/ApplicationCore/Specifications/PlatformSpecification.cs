using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class PlatformSpecification : Specification<Platform>
    {
        public PlatformSpecification(string platformName)
        {
            Query.Where(x => x.PlatformName == platformName);
        }
        public PlatformSpecification(int platformId)
        {
            Query.Where(x => x.Id == platformId);
            Query.Include(x => x.Products);
        }
    }
}
