# Galaxus Integration Project Structure

## Solution Architecture

```
GalaxusIntegration/
??? GalaxusIntegration.Api/              # Web API Layer
?   ??? Controllers/                      
?   ?   ??? ProductsController           # Product sync and management
?   ?   ??? OrdersController             # Order processing endpoints
?   ?   ??? FilesController              # File processing management
?   ?   ??? HealthController             # Health check endpoints
?   ??? Middleware/                      # Custom middleware
?   ?   ??? ErrorHandlingMiddleware      # Global error handling
?   ?   ??? LoggingMiddleware            # Request/response logging
?   ??? Filters/                         # Action filters
?
??? GalaxusIntegration.Application/      # Application Layer
?   ??? DTOs/                            # Data Transfer Objects
?   ?   ??? Products/                    # Product-related DTOs
?   ?   ??? Orders/                      # Order-related DTOs
?   ?   ??? Files/                       # File processing DTOs
?   ??? Interfaces/                      # Service interfaces
?   ?   ??? IFileProcessingService       
?   ?   ??? IProductService
?   ?   ??? IOrderService
?   ??? Services/                        # Service implementations
?   ??? Use Cases/                       # Business use cases
?
??? GalaxusIntegration.Core/             # Domain Layer
?   ??? Entities/                        # Domain entities
?   ?   ??? Product                      # Product domain model
?   ?   ??? Order                        # Order domain model
?   ?   ??? File                         # File processing model
?   ??? Domain Logic/                    # Domain business logic
?   ?   ??? ProductValidation           
?   ?   ??? OrderProcessing
?   ??? Business Rules/                  # Business rules
?
??? GalaxusIntegration.Infrastructure/   # Infrastructure Layer
?   ??? Database/                        # Database context and configs
?   ??? FTP/                            # SFTP client implementation
?   ??? Excel Files/                     # Excel file processing
?   ??? External APIs/                   # External service clients
?
??? GalaxusIntegration.Tests/           # Test Project
    ??? Unit Tests/
    ??? Integration Tests/
    ??? End-to-End Tests/

```

## Package Usage by Layer

### API Layer (GalaxusIntegration.Api)
- **Authentication & Security**
  - Microsoft.AspNetCore.Authentication.JwtBearer
- **API Documentation**
  - Swashbuckle.AspNetCore.*
  - Microsoft.AspNetCore.OpenApi
- **Logging**
  - Serilog.AspNetCore
  - Serilog.Sinks.PostgreSQL
- **Validation**
  - FluentValidation.AspNetCore

### Application Layer (GalaxusIntegration.Application)
- **Object Mapping**
  - AutoMapper
- **Validation**
  - FluentValidation
- **CQRS/Messaging**
  - MediatR
- **Logging**
  - Microsoft.Extensions.Logging.Abstractions

### Domain Layer (GalaxusIntegration.Core)
- **Domain Validation**
  - FluentValidation
- **Domain Events**
  - MediatR

### Infrastructure Layer (GalaxusIntegration.Infrastructure)
- **Database**
  - Npgsql.EntityFrameworkCore.PostgreSQL
- **File Processing**
  - CsvHelper
  - EPPlus
- **SFTP**
  - SSH.NET

### Test Layer (GalaxusIntegration.Tests)
- **Testing Framework**
  - xUnit
  - xunit.runner.visualstudio
- **Mocking**
  - Moq
- **Test Data**
  - AutoFixture
- **Assertions**
  - FluentAssertions
- **Database Testing**
  - Microsoft.EntityFrameworkCore.InMemory

## Key Integration Components

### 1. Product Integration (3-Stage Process)
```csharp
// Stage 1: Product Master Data
public class ProductMasterData
{
    public string ProviderKey { get; set; }    // Max 100 chars, ASCII 32-126
    public string Gtin { get; set; }           // 8,12,13,14 digits
    public string BrandName { get; set; }
    public string ProductCategory { get; set; }
    public string ProductTitle_de { get; set; } // Max 100 chars
    public string LongDescription_de { get; set; } // Max 4000 chars
    public string MainImageUrl { get; set; }    // Max 300 chars
}

// Stage 2: Commercial Data
public class CommercialData
{
    public string ProviderKey { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public int DeliveryTimeDays { get; set; }
    public int MOQ { get; set; } = 1;
    public int OQS { get; set; } = 1;
    public string Currency { get; set; } = "CHF";
    public decimal VAT_Rate { get; set; } = 7.7m;
}

// Stage 3: Product Specifications
public class ProductSpecification
{
    public string ProviderKey { get; set; }
    public string SpecificationKey { get; set; }   // Max 200 chars
    public string SpecificationValue { get; set; }
}
```

### 2. Order Processing Flow
```csharp
public enum OrderDocumentType
{
    GORDP,  // Order
    GORDRE, // Order Response
    GDISPN, // Dispatch Notification
    GINVCE, // Invoice
    GCANP,  // Cancel Request
    GCANCO  // Cancel Confirm
}

public class OrderDocument
{
    public OrderDocumentType Type { get; set; }
    public string OrderId { get; set; }
    public string SupplierId { get; set; }
    public DateTime Timestamp { get; set; }
}
```

### 3. SFTP Configuration
```csharp
public class SftpConfig
{
    public string Host { get; set; } = "ftp-partner.digitecgalaxus.ch";
    public int Port { get; set; } = 22;
    public int MaxParallelSessions { get; set; } = 5;
    public string[] WhitelistedIPs { get; set; } = new[]
    {
        "88.198.35.84",
        "85.10.200.14",
        "116.203.25.19"
    };
}
```

## Implementation Guidelines

1. **Data Validation**
   - Implement FluentValidation rules for all models
   - Add GTIN check digit validation
   - Validate file encodings (UTF-8)

2. **File Processing**
   - Implement retries for transient failures
   - Use parallel processing within limits
   - Maintain audit logs

3. **Error Handling**
   - Global exception middleware
   - Structured logging
   - Email notifications for failures

4. **Performance Optimization**
   - Background job scheduling
   - Connection pooling
   - Caching where appropriate

5. **Security**
   - JWT authentication
   - IP whitelisting
   - Secure credential storage

## Development Workflow

1. **Setup Development Environment**
   - Install required .NET 9 SDK
   - Configure user secrets
   - Setup PostgreSQL database

2. **Implementation Order**
   - Stage 1: Product master data
   - Stage 2: Commercial data
   - Stage 3: Product specifications
   - Order processing
   - Automation and monitoring

3. **Testing Strategy**
   - Unit tests for business logic
   - Integration tests for file processing
   - End-to-end tests for critical flows