using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.ProductData.Strategies;

namespace GalaxusIntegration.Application.ProductData
{
    public class ProductDataGenerator : IProductExcelData
    {
        private readonly IProductDataStrategy _strategy;
        private readonly IFileGenerationService _fileGenerationService;

        public string FileName { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> Data { get; set; }

        public ProductDataGenerator()
        {
            Headers = new List<string>();
            Data = new List<List<string>>();
        }

        public ProductDataGenerator(IProductDataStrategy strategy, IFileGenerationService fileGenerationService)
        {
            _strategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
            _fileGenerationService = fileGenerationService ?? throw new ArgumentNullException(nameof(fileGenerationService));
            Headers = new List<string>();
            Data = new List<List<string>>();
        }

        public async Task<string> GenerateDataFile()
        {
            if (_strategy == null)
                throw new InvalidOperationException("Strategy not set");

            var productData = await _strategy.GenerateProductData();
            FileName = productData.FileName;
            Headers = productData.Headers;
            Data = productData.Data;

            // Use the file generation service to create the Excel file
            return await _fileGenerationService.GenerateExcelFile(this);
        }
    }
}
