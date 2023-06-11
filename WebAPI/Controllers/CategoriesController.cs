using Data.Domains;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.Schema;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService service;

        public CategoriesController(ICategoryService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.Get(id));

        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
            };
            service.AddAsync(category);
            return Ok(request);
        }

        [HttpPut]
        public IActionResult Update([FromBody] CategoryRequest request, int id)
        {
            var category = new Category
            {
                Name = request.Name,
            };
            if (id == category.Id)
            {
                service.Update(category);
                return Ok(201);
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(204);
        }
    }
}