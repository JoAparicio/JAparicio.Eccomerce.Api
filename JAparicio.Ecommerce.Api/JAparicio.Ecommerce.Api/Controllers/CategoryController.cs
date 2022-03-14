using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAparicio.Ecommerce.Api.Data;
using JAparicio.Ecommerce.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JAparicio.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EcommerceDb context_;

        public CategoryController(EcommerceDb context)
        {   
            context_ = context;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetCategory()
        {

            if (context_.Categoria.Any())

                return Ok(context_.Categoria);

            else

                return NoContent();

        }


        // GET api/<CategoryController>/categoryId
        [HttpGet("{id}")]
        public ActionResult GetCategoryById(int id)
        {

            if (context_.Categoria.Any(x => x.Id == id))

                return Ok(context_.Categoria.FirstOrDefault(x => x.Id == id));

            else

                return NoContent();

        }


        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Add([FromBody] Category category)
        {
            if (!context_.Categoria.Any(x => x.Id == category.Id))
            {

                context_.Categoria.Add(category);

                context_.SaveChanges();

                return Ok();


            }
            else
            {

                return BadRequest($"Ya existe una entidad con la id{category.Id}");

            }

        }


        //DELETE api/<CategoryController>/categoryId
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if(context_.Categoria.Any(x => x.Id == id))
            {

                var categoryDelete = context_.Categoria.Single(x => x.Id == id);

                context_.Categoria.Remove(categoryDelete);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return NotFound($"No hay productos con la id {id}");

            }
           

        }


        // PUT api/<CategoryController>/categoryId
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Category category)
        {
            if(context_.Categoria.Any(x => x.Id == id))
            {

                var categoryUpdate = context_.Categoria.Single(x => x.Id == id);

                context_.Categoria.Remove(categoryUpdate);

                context_.Categoria.Add(category);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return NotFound($"No hay ninguna Categoria con la id {id}");

            }
           

        }

    
    }
}