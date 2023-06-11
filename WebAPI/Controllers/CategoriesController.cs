using Data.Domains;
using Microsoft.AspNetCore.Mvc;
using Service;

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
        public IActionResult GetAll()
        {
            return Ok(service.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(service.Get(id));

        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            service.AddAsync(category);
            return Ok(category);
        }

        [HttpPut]
        public IActionResult Update(Category category)
        {
            service.Update(category);
            return Ok(201);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            service.Delete(id);
            return Ok(204);
        }
    }
}