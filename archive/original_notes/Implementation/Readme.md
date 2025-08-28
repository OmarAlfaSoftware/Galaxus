Galaxus Integration Solution Documentation
This document explains the architecture, structure, and relationships of the Galaxus Integration solution.
---
```
GalaxusIntegration/
├── GalaxusIntegration.Api/
│   ├── Controllers/
│   │   └── FilesController.cs
│   └── Program.cs
├── GalaxusIntegration.Application/
│   ├── DTOs/
│   │   └── ProductDTOs/
│   │       ├── MediaDataDTO.cs
│   │       └── StockDataDTO.cs
│   ├── Interfaces/
│   │   └── IFileGenerationService.cs
│   ├── Services/
│   │   └── ProductFileService.cs
│   └── ProductData/
│       ├── ProductDataGenerator.cs
│       ├── ProductDataStrategyFactory.cs
│       └── Strategies/
│           ├── ProductDataStrategy.cs
│           ├── PriceDataStrategy.cs
│           ├── StockDataStrategy.cs
│           ├── MediaDataStrategy.cs
│           ├── AccessoryDataStrategy.cs
│           └── SpecificationDataStrategy.cs
├── GalaxusIntegration.Core/
│   └── [Domain Entities and Business Logic]
├── GalaxusIntegration.Infrastructure/
│   ├── Excel files/
│   │   └── ExcelExporter.cs
│   └── FileProcessing/
│       └── FileValidationService.cs
├── GalaxusIntegration.Shared/
│   └── Enum/
│       └── ProductDatatype.cs
└── GalaxusIntegration.Tests/
    └── [Test Files]
```
---
Layered Architecture
The solution is organized into six layers, each with a clear responsibility and reference structure:
# 1. API Layer (GalaxusIntegration.Api)

*	Purpose: Entry point, dependency injection, configuration, and HTTP endpoints.
*	References:
*	GalaxusIntegration.Application
*	GalaxusIntegration.Infrastructure
*	GalaxusIntegration.Shared
*	Notes:
*	Hosts controllers (e.g., FilesController).
*	Configures DI for services and infrastructure.
*	Serves static files (e.g., from wwwroot).

# 2. Core Layer (GalaxusIntegration.Core)

*	Purpose: Contains domain entities and core business logic.
*	References:
*	GalaxusIntegration.Shared
*	Notes:
*	No dependencies on Application or Infrastructure.

# 3. Application Layer (GalaxusIntegration.Application)

*	Purpose: Implements CQRS, DTOs, services, use cases, and business actions.
*	References:
*	GalaxusIntegration.Core
*	GalaxusIntegration.Shared
*	Notes:
*	Contains strategy pattern for product data generation.
*	Exposes interfaces for infrastructure implementations.

# 4. Infrastructure Layer (GalaxusIntegration.Infrastructure)

*	Purpose: Handles technical concerns (DB, Excel, FTP, file processing, etc.).
*	References:
*	GalaxusIntegration.Core
*	GalaxusIntegration.Application
*	GalaxusIntegration.Shared
*	Notes:
*	Implements interfaces defined in Application.
*	Example: ExcelExporter for Excel file generation.

# 5. Shared Layer (GalaxusIntegration.Shared)

*	Purpose: Contains enums, constants, and types shared across all projects.
*	References:
*	None (does not depend on any other project).

# 6. Tests Layer (GalaxusIntegration.Tests)

*	Purpose: Contains unit and integration tests for the solution.
*	References:
*	None (test projects typically reference the projects they test, but do not expose references).

---

# Key Dependencies and Relationships

*	Strategy Pattern Implementation:
*	IProductDataStrategy interface and its concrete implementations in Strategies/.
*	ProductDataStrategyFactory creates the appropriate strategy.
*	ProductFileService consumes the strategies.
*	Excel Generation Flow:
*	IFileGenerationService interface (Application).
*	ExcelExporter implementation (Infrastructure).
*	Used by ProductFileService to generate Excel files.
*	Data Transfer:
*	DTOs (e.g., MediaDataDTO, StockDataDTO).
*	ProductDataGenerator for assembling Excel data.
*	ProductDatatype enum (Shared).
*	API Layer:
*	Controllers expose endpoints.
*	Program.cs configures DI and middleware.

---
# The flow of the code

## first start from the apis

### 1- /api/files/{type} (POST)

#### Overview

This endpoint generates an Excel file for a specific product data type, determined by the {type} route parameter (an enum value from GalaxusIntegration.Shared.Enum). The generated file is stored in the wwwroot directory and the file path is returned in the response.

---
Detailed Flow
1.	Enum Selection
*	The client sends a POST request to /api/files/{type} with {type} being a value from the ProductDatatype enum (defined in GalaxusIntegration.Shared.Enum).
*	This enum determines which kind of product data (e.g., price, stock, media, etc.) will be generated.
2.	Service Invocation
*	The controller receives the request and calls the GenerateProductDataFileAsync method of ProductFileService (from GalaxusIntegration.Application.Services), passing the selected enum value.
3.	Strategy Resolution
*	ProductFileService uses ProductDataStrategyFactory to resolve the correct strategy implementation for the requested data type.
*	The factory acts like a switch, mapping each enum value to its corresponding strategy (e.g., PriceDataStrategy, StockDataStrategy, etc.).
4.	Data Generation
*	The selected strategy’s GenerateProductData method is called.
*	This method gathers the required data (potentially from the database or other sources) and constructs an IProductExcelData object, which contains the file name, headers, and data rows.
5.	Excel File Creation
*	The IProductExcelData object is passed to the ExcelExporter class (from GalaxusIntegration.Infrastructure.Excel_files), which implements IFileGenerationService.
*	ExcelExporter.GenerateExcelFile creates an Excel file using the provided structure and saves it to the wwwroot directory.
6.	Response
*	The file path of the generated Excel file (relative to wwwroot) is returned as the API response.
---
Key Points
*	Extensible: New product data types can be added by implementing new strategies and updating the factory.
*	Separation of Concerns: Each layer (API, Application, Infrastructure) has a clear responsibility.
*	Reusability: Strategies and the Excel exporter are decoupled and reusable.
*	Future-proof: The current implementation stores files in wwwroot, but can be easily extended to upload files to an FTP server or cloud storage.
---
Sequence Diagram (Textual) 
```
Client
  │
  │ POST /api/files/{type}
  ▼
FilesController
  │
  │ calls
  ▼
ProductFileService.GenerateProductDataFileAsync(type)
  │
  │ resolves
  ▼
ProductDataStrategyFactory.CreateStrategy(type)
  │
  │ calls
  ▼
Strategy.GenerateProductData()
  │
  │ returns IProductExcelData
  ▼
ExcelExporter.GenerateExcelFile(data)
  │
  │ returns file path
  ▼
FilesController
  │
  │ returns file path
  ▼
Client
```


---

#Summary

*	The solution follows clean architecture principles, with clear separation of concerns.
*	Each layer references only what it needs, minimizing coupling.
*	The strategy pattern allows for easy extension of product data types.
*	Infrastructure is swappable and implements interfaces from the Application layer.
*	Shared types are accessible everywhere but have no dependencies.
---