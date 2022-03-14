using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JAparicio.Ecommerce.Api.Data;
using JAparicio.Ecommerce.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JAparicio.Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly EcommerceDb context_;


        public CartController(EcommerceDb context)
        {
            context_ = context;
        }

        // GET: api/<CartController
        [HttpGet]
        public ActionResult<IEnumerable<Cart>> GetCart()
        {

            if (context_.Carrito.Any())

                return Ok(context_.Carrito.Include(p => p.Product));

            else

                return NoContent();

        }

        // GET api/<CartController>/5
        [HttpGet("{email}")]
        public ActionResult GetCartById(String email)
        {
            if (context_.Carrito.Any(x => x.Email == email))
            { 

                return Ok(context_.Carrito.Include(p => p.Product).FirstOrDefault(x => x.Email == email));

            }
            else
            {

                return NoContent();

            }
        }

        // POST api/<CartController>
        [HttpPost]
        public ActionResult<int> Add([FromBody] Cart carrito)
        {
            if (!context_.Carrito.Any(c => c.Id == carrito.Id))
            {

                context_.Carrito.Add(carrito);

                context_.SaveChanges();

                return Ok(carrito.Id);


            }
            else
            {

                return BadRequest($"Ya existe una entidad con la id{carrito.Id}");

            }

        }

        // PUT api/<CartController>/5
        [HttpPut("{email}/{idProducto}")]
        public ActionResult Put(String email, int idProducto, [FromBody] int quantity)
        {

             var cartUpdate = context_.Carrito.Include(c => c.Product).FirstOrDefault(c => c.Email == email && c.ProductId == idProducto);

            if(cartUpdate != null)
            {
                cartUpdate.Quantity = quantity;
                context_.SaveChanges();

                return Ok(cartUpdate);
            }
            else
            {
                return NotFound($"No existe el carrito con la id {idProducto}");
            }

        }

        // DELETE api/<CartController>/5
        [HttpDelete("{email}")]
        public ActionResult Delete(String email)
        {
            if (context_.Carrito.Any(c => c.Email == email ))
            {

                var categoryDelete = context_.Carrito.Where(c => c.Email == email);

                context_.Carrito.RemoveRange(categoryDelete);

                context_.SaveChanges();

                return Ok();

            }
            else
            {

                return NotFound($"No hay productos con el email {email}");

            }


        }

        [HttpDelete("{email}/{idProducto}")]
        public ActionResult DeleteCart(String email, int idProducto)
        {

            if(context_.Carrito.Any(c => c.Email == email && c.Id == idProducto))
            {
                var productDelete = context_.Carrito.Single(c => c.Email == email && c.Id == idProducto);
                context_.Carrito.Remove(productDelete);
                context_.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"No se encontro ningun producto en el carrito con la id {id}");
            }

        }

    }
}
