using AutoMapper;
using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.DTOs.Responses;
using SBCW.DAL.Models;

namespace SBCW.BLL.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateProductMaps();
    }
    
    private void CreateProductMaps()
    {
        CreateMap<ProductRequest, Product>();
        CreateMap<Product, ProductResponse>();
    }
}