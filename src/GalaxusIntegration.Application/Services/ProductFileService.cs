using GalaxusIntegration.Application.Interfaces;
using GalaxusIntegration.Application.ProductData;
using GalaxusIntegration.Shared.Enum;

namespace GalaxusIntegration.Application.Services;

/// <summary>
///     Provides functionality to generate product-related files like ProductData, PriceData, StockData, MediaData,
///     AccessoryData and SpecificationData.
/// </summary>
/// <remarks>
///     This service supports file generation, with each data producing a file
///     that may be used as input for subsequent data. The generated files are returned as file paths in string
///     format. Implementations of this service are expected to define the specific behavior for each file data.
/// </remarks>
public class ProductFileService
{
    private readonly IFileGenerationService _fileGenerationService;
    public ProductFileService(IFileGenerationService fileGenerationService)
    {
        _fileGenerationService = fileGenerationService;
    }
    // Implementation of methods to generate different product-related files would go here.
    /// <summary>
    /// Generates a product data file based on the specified product data type.
    /// </summary>
    /// <param name="Type">The type of product data to generate, represented as a <see cref="ProductDataType"/> enumeration value.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the file path of the generated
    /// product data file.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the specified <paramref name="Type"/> does not match any known product data strategy.</exception>

    public async Task<string> GenerateProductDataFileAsync(ProductDataType Type)
    {
        //get strategy from factory
        var strategy = ProductDataStrategyFactory.CreateStrategy(Type);
        if(strategy!=null)
            {
            var generator = new ProductDataGenerator(strategy, _fileGenerationService); // Pass actual IFileGenerationService implementation
            return await generator.GenerateDataFile();
        }

        // Placeholder for actual implementation
        throw new InvalidOperationException("NO Enum Match");
    }
}