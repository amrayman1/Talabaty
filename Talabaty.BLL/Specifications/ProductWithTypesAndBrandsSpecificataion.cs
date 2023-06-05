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
        public ProductWithTypesAndBrandsSpecificataion()
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }

        //Get Specific Product
        public ProductWithTypesAndBrandsSpecificataion(int id) : base(x => x.Id == id)
        {
            AddIncludes(x => x.ProductType);
            AddIncludes(x => x.ProductBrand);
        }

    }
}
