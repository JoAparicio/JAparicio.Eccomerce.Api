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
    public class ProductController : ControllerBase
    {
        private readonly EcommerceDb context_;

        public ProductController(EcommerceDb context)
        {
            context_ = context;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {

            if (context_.Producto.Any())

                return Ok(context_.Producto);

            else

                return NoContent();

        }

        // GET api/<ProductController>/productId
        [HttpGet("{id}")]
        public ActionResult GetProductById(int id)
        {

            if (context_.Producto.Any(p => p.Id == id))

                return Ok(context_.Producto.FirstOrDefault(p => p.Id == id));

            else

                return NoContent();

        }



        // GET api/<ProductController>/categoryId
        [HttpGet("category/{id}")]
        public ActionResult GetProductByCategoryId(int id)
        {

            if (context_.Producto.Any(x => x.CategoryId == id))

                return Ok(context_.Producto.FirstOrDefault(c => c.CategoryId == id));

            else

                return NoContent();

        }


        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Add([FromBody] Product product)
        {
            if (!context_.Producto.Any(p => p.Id == product.Id))
            {
                context_.Producto.Add(product);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return BadRequest($"Ya existe una entidad con la id{product.Id}");

            }

        }


        // DELETE api/<ProductController>/productId
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            if(context_.Producto.Any(p => p.Id == id))
            {

                var productDelete = context_.Producto.Single(p => p.Id == id);


                context_.Producto.Remove(productDelete);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return NotFound($"No hay ningun producto con la id {id}");

            }
          
        }


        // PUT api/<ProductController>/productId
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] Product product)
        {

            if(context_.Producto.Any(p => p.Id == id))
            {

                var productUpdate = context_.Producto.Single(p => p.Id == id);


                context_.Producto.Remove(productUpdate);

                context_.Producto.Add(product);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return NotFound($"No hay ningun producto con la id {id}");

            }
         
        }

   
    }
}