using AspKnP231.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspKnP231.Controllers.Api
{
    [Route("api/sections")]
    [ApiController]
    public class SectionsController(DataContext dataContext) : ControllerBase
    {
        private readonly DataContext _dataContext = dataContext;

        private String StorageUrl => $"{Request.Scheme}://{Request.Host}/Storage/Item/";

        [HttpGet]
        public Object AllSections()
        {
            var sections = _dataContext
                .ShopSections
                .AsNoTracking()
                .Where(s => s.DeletedAt == null)
                .AsEnumerable()
                .Select(s => s with { ImageUrl = StorageUrl + s.ImageUrl });

            return sections;
        }

        [HttpGet("{id}")]
        public Object? ProductsBySection(String id)
        {
            var section = _dataContext
                .ShopSections
                .Include(s => s.Products)
                .AsNoTracking()
                .FirstOrDefault(s => s.DeletedAt == null && s.Id.ToString() == id || s.Slug == id);

            if (section != null)
            {
                section = section with
                {
                    ImageUrl = StorageUrl + section.ImageUrl,
                    Products = [..section.Products.Select(p => p with {
                        ImageUrl = p.ImageUrl == null ? null : StorageUrl + p.ImageUrl
                    })]
                };
            }

            return section;
        }
    }
}
/*
  MVC                      API
 GET /Home  | один        GET /Home  | різні
 POST /Home | ресурс      POST /Home | ресурси
 маршрутизація - 
  за адресою               за методом запиту
 
    тип повернення методів контролера
IActionResult             Object
переважно View            з автоматичним перетворенням до JSON

 */