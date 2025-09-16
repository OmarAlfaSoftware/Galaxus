namespace GalaxusIntegration.Application.Interfaces;

public interface IProductExcelData
{
    public  string FileName { get; set; }
    public List<string> Headers { get; set; }
    public List<List<string>> Data { get; set; }
    public Task<string> GenerateDataFile();
}