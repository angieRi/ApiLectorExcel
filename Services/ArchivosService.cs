public interface IArchivoService
{
    void LeerArchivos(string rutaExcel, string rutaCsv);
}

public class ArchivoService : IArchivoService
{
    public void LeerArchivos(string rutaExcel, string rutaCsv)
    {
        //falta desarrollo de este metodo
        Console.WriteLine("Leyendo archivos...");
        Console.WriteLine($"Excel: {rutaExcel}");
        Console.WriteLine($"CSV: {rutaCsv}");
    }
}