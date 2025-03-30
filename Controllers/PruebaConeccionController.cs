using ApiLectorExcel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiLectorExcel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaConeccionController : ControllerBase
    {
        private readonly ApilectornetContext _context;

        public PruebaConeccionController(ApilectornetContext context)
        {
            _context = context;
        }
        [HttpGet("db-check")]
        public async Task<IActionResult> CheckDatabase()
        {
            try
            {
                var canConnect = await _context.Database.CanConnectAsync();
                var version = await _context.Database
                    .SqlQueryRaw<DbVersion>("SELECT VERSION() AS Version")
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                return Ok(new
                {
                    Status = "Conectado",
                    Version = version?.Version,
                    EfVersion = "8.0.4",
                    PomeloVersion = "8.0.0"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Status = "Error",
                    Message = ex.Message
                });
            }
        }
    }
}
public class DbVersion
{
    public string Version { get; set; } = string.Empty;
}