using ApplicationCore.Entities;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Specifications
{
    public class KeySpecification : Specification<Key>
    {
        public KeySpecification(int productId)
        {
            Query.Where(x => x.ProductId == productId)
                .Include(x => x.Product)
                .ThenInclude(p => p.Platform);
            Query.Where(x => x.ProductId == productId)
                .Include(x => x.Product)
                .ThenInclude(p => p.Game);
        }
        public KeySpecification(Guid keyCode)
        {
            Query.Where(x => x.KeyCode == keyCode);
        }
    }
}
