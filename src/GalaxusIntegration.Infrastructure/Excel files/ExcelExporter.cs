using GalaxusIntegration.Application.Interfaces;
using OfficeOpenXml;
using System.IO;

namespace GalaxusIntegration.Infrastructure.Excel_files
{
    /// <summary>
    /// this class is responsible for exporting data to Excel files.
    /// it's provide a method of GenerateExcelFile that takes a file name, headers, and data as input and returns a string of the path of the file stored in wwwroot of the app.
    /// </summary>
    public class ExcelExporter :IFileGenerationService
    {
        private readonly string rootPath;

        public ExcelExporter(string rootPath)
        {
            // Validate rootPath
            if (string.IsNullOrWhiteSpace(rootPath))
                throw new ArgumentException("Root path cannot be null or empty.", nameof(rootPath));
            // Ensure the directory exists or create it
            if (!Directory.Exists(rootPath))
                Directory.CreateDirectory(rootPath);
            // Assign the validated rootPath
            this.rootPath = rootPath;
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        /// <summary>
        /// the method to generate the excel file from the data provided,
        /// the headers length should be equal to the data length otherwise it will throw an exception
        /// the file will be saved in wwwroot folder of the app with the timestamp of the file
        /// </summary>
        /// <param name="data"> the data object containing file name, headers and actual data</param>
        /// <returns>a string of the path of the file in wwwroot</returns>
        public async Task<string> GenerateExcelFile(IProductExcelData data)
        {
            // Validate inputs
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            // check if file name is null or empty or whitespace 
            if (string.IsNullOrWhiteSpace(data.FileName))
                throw new ArgumentException("File name cannot be null or empty.", nameof(data.FileName));
            // check if headers or data is null or empty
            if (data.Headers == null || data.Headers.Count == 0)
                throw new ArgumentException("Headers cannot be null or empty.", nameof(data.Headers));
            // check if data is null or empty
            if (data.Data == null || data.Data.Count == 0)
                throw new ArgumentException("Data cannot be null or empty.", nameof(data.Data));
            // check if headers count is equal to data count of the first row
            if (data.Headers.Count != data.Data[0].Count)
                throw new ArgumentException("Headers and data must have the same count.");
            // Generate unique file name with timestamp
            var filePath = Path.Combine(rootPath, $"{Path.GetFileNameWithoutExtension(data.FileName)}_{DateTime.Now:yyyyMMddHHmmss}.xlsx");
            // Create and populate the Excel file
            using (var package = new ExcelPackage())
            {
                // Add a new worksheet to the empty workbook
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Write headers
                for (int i = 0; i < data.Headers.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = data.Headers[i];
                }

                // Write data
                foreach (var row in data.Data)
                {
                    
                for (int i = 0; i < row.Count; i++)
                {
                    worksheet.Cells[2, i + 1].Value = row[i];
                }
                }
                // save the file at the path
                package.SaveAs(new FileInfo(filePath));
            }

            return filePath;
        }
    }
}
