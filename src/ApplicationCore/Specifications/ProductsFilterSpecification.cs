using ApplicationCore.Entities;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class ProductsFilterSpecification : Specification<Product>
    {
        //public ProductsFilterSpecification(int? genreId, int? platformId)
        //{
        //    Query.Include(x => x.Game.Genres);
        //    Query.Include(x => x.Discounts);

        //    if (genreId.HasValue)
        //        Query.Where(x => x.Game.Genres.Any(x => x.Id == genreId));
        //    if (platformId.HasValue)
        //        Query.Where(x => x.PlatformId == platformId);
        //}

        //public ProductsFilterSpecification(int? genreId, int? platformId, int skip, int take) : this(genreId, platformId)
        //{
        //    Query.Skip(skip).Take(take);
        //}

        public ProductsFilterSpecification(List<int> genreIds, List<int> platformIds)
        {
            Query.Include(x => x.Game.Genres);
            Query.Include(x => x.Discounts);

            if (genreIds.Count > 0)
                Query.Where(x => x.Game.Genres.Any(x => genreIds.Contains(x.Id)));
            if (platformIds.Count > 0)
                Query.Where(x => platformIds.Contains(x.PlatformId));
        }

        public ProductsFilterSpecification(List<int> genreIds, List<int> platformIds, int skip, int take) : this(genreIds, platformIds)
        {
            Query.Skip(skip).Take(take);
        }

    }
}
