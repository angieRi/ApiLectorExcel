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
}