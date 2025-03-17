using Microsoft.AspNetCore.Mvc;
using System.IO;

[ApiController]
[Route("api/[controller]")]
public class ArchivosController : ControllerBase
{
    private readonly IArchivoService _archivoService;

    public ArchivosController(IArchivoService archivoService)
    {
        _archivoService = archivoService;
    }

    [HttpPost("leer")]
    public IActionResult LeerArchivos([FromForm] IFormFile archivoExcel, [FromForm] IFormFile archivoCsv)
    {
        if (archivoExcel == null || archivoCsv == null)
        {
            return BadRequest("Debes subir ambos archivos (Excel y CSV).");
        }

        var rutaExcel = Path.GetTempFileName();
        var rutaCsv = Path.GetTempFileName();

        using (var streamExcel = new FileStream(rutaExcel, FileMode.Create))
        using (var streamCsv = new FileStream(rutaCsv, FileMode.Create))
        {
            archivoExcel.CopyTo(streamExcel);
            archivoCsv.CopyTo(streamCsv);
        }

        _archivoService.LeerArchivos(rutaExcel, rutaCsv);

        System.IO.File.Delete(rutaExcel);
        System.IO.File.Delete(rutaCsv);

        return Ok("Archivos leídos correctamente.");
    }
}