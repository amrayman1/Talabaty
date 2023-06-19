using AutoMapper;
using AutoMapper.Execution;
using Talabaty.API.DTOS;
using Talabaty.DAL.Entities;

namespace Talabaty.API.Helpers
{
    // ProductUrlResolver To return the url of the API with pictureUrl
    public class ProductUrlResolver : IValueResolver<Product, ProductDTO, string>
    {
        IConfiguration _config;

        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
