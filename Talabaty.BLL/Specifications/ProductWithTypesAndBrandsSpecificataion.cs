using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaty.DAL.Entities;

namespace Talabaty.BLL.Specifications
{
    public class ProductWithTypesAndBrandsSpecificataion : BaseSpecification<Product>
    {
        //Get All Products
        public ProductWithTypesAndBrandsSpecificataion(string sort)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
            AddOrderBy(x => x.Name);


            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price); 
                        break;

                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }

        }

        //Get Specific Product
        public ProductWithTypesAndBrandsSpecificataion(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }

    }
}
