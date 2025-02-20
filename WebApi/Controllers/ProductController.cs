using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly DbapiContext _dbcontext;

        public ProductController(DbapiContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpGet]
        [Route("GetProducts")]
        public IActionResult GetProducts()
        {
            List<Producto> list = new List<Producto>();
            try
            {
                list = _dbcontext.Productos.Include(cat => cat.IdCategoriaNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = list });
            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = ex.Message, response = list });
            }
        }


        [HttpGet]
        [Route("{idProducto:int}")]
        public IActionResult Product(int idProducto)
        {
            Producto producto = _dbcontext.Productos.Find(idProducto);

            if(producto == null)
            {
                return BadRequest("Producto no encontrado");
            }

            try
            {
                producto = _dbcontext.Productos.Include(cat => cat.IdCategoriaNavigation).Where(p => p.IdProducto == idProducto).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { message = "ok", response = producto });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, new { message = ex.Message, response = producto });
            }
        }

    }
}
