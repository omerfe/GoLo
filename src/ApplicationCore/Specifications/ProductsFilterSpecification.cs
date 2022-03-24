using ApplicationCore.Entities;
using ApplicationCore.Enums;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApplicationCore.Specifications
{
    public class ProductsFilterSpecification : Specification<Product>
    {
        public ProductsFilterSpecification(List<int> genreIds, List<int> platformIds, string searchText)
        {
            Query.Include(x => x.Game.Genres);
            Query.Include(x => x.Discounts);
            Query.Include(x => x.Platform);
            Query.Where(x => x.Keys.Where(y => y.Status).ToList().Count > 0);
            Query.Where(x => x.IsAvailable);
            if (genreIds.Count > 0)
                Query.Where(x => x.Game.Genres.Any(x => genreIds.Contains(x.Id)));
            if (platformIds.Count > 0)
                Query.Where(x => platformIds.Contains(x.PlatformId));
            if (!string.IsNullOrEmpty(searchText))
                Query.Where(x => x.Game.GameName.ToLower().Contains(searchText.ToLower()));
        }

        public ProductsFilterSpecification(List<int> genreIds, List<int> platformIds, int skip, int take, int? sortItem, string searchText) : this(genreIds, platformIds, searchText)
        {
            if (sortItem.HasValue)
                switch (sortItem)
                {
                    case (int)SortTypes.Price_Asc:
                        Query.OrderBy(x => x.ProductUnitPrice);
                        break;
                    case (int)SortTypes.Price_Desc:
                        Query.OrderByDescending(x => x.ProductUnitPrice);
                        break;
                    case (int)SortTypes.Release_Date_Asc:
                        Query.OrderBy(x => x.Game.ReleaseDate);
                        break;
                    case (int)SortTypes.Release_Date_Desc:
                        Query.OrderByDescending(x => x.Game.ReleaseDate);
                        break;
                    case (int)SortTypes.Name_Asc:
                        Query.OrderBy(x => x.Game.GameName);
                        break;
                    case (int)SortTypes.Name_Desc:
                        Query.OrderByDescending(x => x.Game.GameName);
                        break;
                }
            Query.Skip(skip).Take(take);

        }

    }
}
