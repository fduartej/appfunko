using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using appfunko.Models;
using appfunko.Service;

namespace appfunko.Controllers.Rest
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoApiController : ControllerBase
    {
        private readonly ProductoService _productoService;

        public ProductoApiController(ProductoService productoService)
        {
            _productoService = productoService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> List()
        {
            var productos = await _productoService.GetAll();
             if(productos == null)
                return NotFound();
            return Ok(productos);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> GetProducto(int? id)
        {
            var producto = await _productoService.Get(id);
            if(producto == null)
                return NotFound();
            return Ok(producto);
        }

        [HttpPost]
        public async Task<ActionResult<Producto>> CreateProducto(Producto producto){
            if (producto == null)
            {
                return BadRequest();
            }
            await _productoService.CreateOrUpdate(producto);
            return Ok(producto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProducto(int? id)
        {
            await _productoService.Delete(id);
            return Ok();
        }
    }
}