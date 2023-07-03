using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabaty.BLL.Specifications;
using Talabaty.DAL.Entities;

namespace Talabaty.BLL.ProductSpecifications
{
    public class ProductWithTypesAndBrandsSpecificataion : BaseSpecification<Product>
    {
        //Get All Products
        public ProductWithTypesAndBrandsSpecificataion(ProductSpecParams productSpecParams) :
            base(x => (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
            (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId))
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productSpecParams.PageSize * productSpecParams.PageIndex - 1, productSpecParams.PageSize);


            if (!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
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
