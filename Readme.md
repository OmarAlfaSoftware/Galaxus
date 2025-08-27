# 🚀 Galaxus Integration Platform

[![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-15+-336791?style=flat-square&logo=postgresql)](https://www.postgresql.org/)
[![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-brightgreen?style=flat-square)](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
[![Status](https://img.shields.io/badge/Status-Architecture%20Phase-orange?style=flat-square)]()
[![Progress](https://img.shields.io/badge/Progress-10%25-orange?style=flat-square)]()

## 📋 Project Overview

Integration solution for **Galaxus Digitec AG**, Switzerland's leading online marketplace. This project implements automated product catalog management and order processing through Galaxus's FTP-based file exchange system using Clean Architecture with DDD principles.

### 🎯 Objectives
- Implement 3-stage product listing process with validation
- Automate inventory and price synchronization (15-minute intervals)
- Handle order processing from receipt to invoice
- Ensure Swiss market compliance (VAT, TARIC codes)
- Build maintainable solution using Clean Architecture

## 🏗️ Current Status: Architecture Phase ✅

### What's Completed
✅ **Clean Architecture structure with DDD compatibility established**
- Domain layer (Core) - Pure business logic
- Application layer - Use cases and orchestration  
- Infrastructure layer - External integrations
- API layer - Presentation and endpoints

### Solution Structure Created

```
GalaxusIntegration/
├── 📦 GalaxusIntegration.Api/
│   ├── Controllers/         # (planned)
│   ├── Middleware/          # (planned)
│   └── Program.cs          # ✅ Created
│
├── 📦 GalaxusIntegration.Application/
│   ├── Commands/           # (planned)
│   ├── Queries/            # (planned)
│   ├── DTOs/               # (planned)
│   └── DependencyInjection.cs  # ✅ Created
│
├── 📦 GalaxusIntegration.Core/
│   ├── Entities/           # (planned)
│   ├── ValueObjects/       # (planned)
│   └── Interfaces/         # (planned)
│
├── 📦 GalaxusIntegration.Infrastructure/
│   ├── Data/              # (planned)
│   ├── FileProcessing/    # (planned)
│   ├── Sftp/              # (planned)
│   └── DependencyInjection.cs  # ✅ Created
│
├── 📦 GalaxusIntegration.Tests/
│   └── (structure planned)
│
└── 📂 docs/
    └── 📄 INTEGRATION_GUIDE.md  # ✅ Galaxus documentation
```

### NuGet Packages Configured

#### ✅ **GalaxusIntegration.Api**
```xml
<PackageReference Include="FluentValidation.AspNetCore" Version="11.3.0" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Serilog.AspNetCore" Version="8.0.0" />
<PackageReference Include="Serilog.Sinks.PostgreSQL" Version="3.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.9.0" />
```

#### ✅ **GalaxusIntegration.Application**
```xml
<PackageReference Include="AutoMapper" Version="13.0.1" />
<PackageReference Include="FluentValidation" Version="11.11.0" />
<PackageReference Include="MediatR" Version="12.5.0" />
<PackageReference Include="Microsoft.Extensions.Logging.Abstractions" Version="9.0.0" />
```

#### ✅ **GalaxusIntegration.Core**
```xml
<PackageReference Include="FluentValidation" Version="11.11.0" />
<PackageReference Include="MediatR" Version="12.5.0" />
```

#### ✅ **GalaxusIntegration.Infrastructure**
```xml
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.0" />
<!-- Additional packages to be added for SFTP, CSV processing, etc. -->
```

#### ✅ **GalaxusIntegration.Tests**
```xml
<PackageReference Include="AutoFixture" Version="4.18.1" />
<PackageReference Include="coverlet.collector" Version="6.0.2" />
<PackageReference Include="FluentAssertions" Version="7.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.InMemory" Version="9.0.0" />
<PackageReference Include="Microsoft.NET.Test.Sdk" Version="17.11.1" />
<PackageReference Include="Moq" Version="4.20.72" />
<PackageReference Include="xunit" Version="2.9.2" />
<PackageReference Include="xunit.runner.visualstudio" Version="2.8.2" />
```

## 📊 Development Roadmap

### Current Phase 🔄

| Step | Status | Description | Notes |
|------|--------|-------------|-------|
| 1 | ✅ **DONE** | Create Clean Architecture with DDD | Solution structure established, packages configured |
| 2 | 🔄 **IN PROGRESS** | Design & Create Database Schema | **← Currently working on this** |
| 3 | ⏳ Pending | Implement 3-stage product file generation | Validate with FeedChecker tool |
| 4 | ⏳ Pending | XML file generation and parsing | OpenTRANS 2.1 format |
| 5 | ⏳ Pending | Local folder FTP simulation | Development testing |
| 6 | ⏳ Pending | Add Hangfire automation | Background job processing |
| 7 | ⏳ Pending | Core system synchronization | Schema alignment |
| 8 | ⏳ Pending | Integration testing | Full process validation |
| 9 | ⏳ Pending | Documentation & cleanup | Code comments, refactoring |
| 10 | ⏳ Pending | Final delivery | Complete documented solution |

### Next Immediate Tasks 📝

**Step 2: Database Design (Current Focus)**
- [ ] Design entity relationships
- [ ] Create PostgreSQL schema script
- [ ] Define indexes for performance
- [ ] Plan migration strategy
- [ ] Document schema decisions

## 🗄️ Database Design (In Progress)

### Planned Schema Structure
```sql
-- To be implemented in Step 2
-- Core entities identified:
-- • Products (with stages 1, 2, 3 data)
-- • Orders and OrderItems  
-- • FileProcessingLogs
-- • InventorySyncLogs
-- • ProductSpecifications
```

## 🛠️ Development Environment

### Prerequisites
- ✅ .NET 9.0 SDK installed
- ✅ Visual Studio 2022 / VS Code configured
- ⏳ PostgreSQL 15+ (to be installed)
- ⏳ FeedChecker Tool (to be obtained from Galaxus)
- ⏳ SFTP credentials (pending from Galaxus)

### Current Setup Status
```bash
# Solution created
✅ dotnet new sln -n GalaxusIntegration

# Projects added
✅ dotnet new classlib -n GalaxusIntegration.Core
✅ dotnet new classlib -n GalaxusIntegration.Application  
✅ dotnet new classlib -n GalaxusIntegration.Infrastructure
✅ dotnet new webapi -n GalaxusIntegration.Api
✅ dotnet new xunit -n GalaxusIntegration.Tests

# Project references configured
✅ Api → Application
✅ Application → Core
✅ Infrastructure → Core
✅ Api → Infrastructure
```

## 📚 Documentation

### Available
- ✅ [INTEGRATION_GUIDE.md](docs/INTEGRATION_GUIDE.md) - Complete Galaxus technical specifications
- ✅ Project structure documentation

### To Be Created
- ⏳ Database schema documentation (Step 2)
- ⏳ API endpoint documentation (Step 3-4)
- ⏳ Deployment guide (Step 9)
- ⏳ Testing documentation (Step 8)

## 🎯 Technical Decisions Made

### Architecture Choices
✅ **Clean Architecture**: Ensures separation of concerns and testability  
✅ **DDD Principles**: Rich domain model for complex business logic  
✅ **CQRS with MediatR**: Clean separation of commands and queries  
✅ **Repository Pattern**: Abstraction over data access  
✅ **PostgreSQL**: Robust, enterprise-ready database

### Pending Decisions
- ⏳ SFTP library selection (SSH.NET vs FluentFTP)
- ⏳ CSV processing approach (CsvHelper vs custom)
- ⏳ XML processing strategy (XDocument vs XmlSerializer)
- ⏳ Monitoring solution (Application Insights vs custom)

## 🔧 Configuration

### Development Configuration (Planned)
```json
// appsettings.Development.json (to be created in Step 2)
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=galaxus_integration;Username=;Password=",
    "HangfireConnection": "Host=localhost;Database=galaxus_hangfire;Username=;Password="
  },
  "Galaxus": {
    "SupplierId": "TBD",
    "Sftp": {
      "Host": "ftp-partner.digitecgalaxus.ch",
      "Username": "TBD",
      "Password": "TBD"
    }
  }
}
```

## 🧪 Testing Strategy

### Planned Test Coverage
- Unit Tests: Domain logic, validators, mappers
- Integration Tests: Database, file processing
- End-to-End Tests: Complete workflows
- Performance Tests: Large catalog handling

*Note: Test implementation begins after core functionality (Step 8)*

## 📈 Progress Tracking

### Completed Milestones
- ✅ Project initialization
- ✅ Architecture design
- ✅ Solution structure creation
- ✅ Package dependencies configured

### Current Sprint Focus
- 🔄 Database schema design
- 🔄 Entity modeling
- 🔄 Repository interfaces

### Time Invested
- Architecture Design: ~2 days
- Current Step (Database): In progress

## 🤝 Resources & References

### Documentation
- [Galaxus Integration Guide](docs/INTEGRATION_GUIDE.md)
- [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html)
- [Domain-Driven Design](https://martinfowler.com/tags/domain%20driven%20design.html)

### Technical Specifications
- [OpenTRANS 2.1](http://www.opentrans.org)
- [BMEcat Standard](http://www.bmecat.org)
- Galaxus FeedChecker (to be obtained)

### Support Contacts
- Galaxus Technical: pdi@digitecgalaxus.ch
- Project Repository: [GitHub/GitLab URL]

## 📝 Notes & Observations

### Architecture Phase Learnings
- Clean Architecture provides excellent separation for integration projects
- DDD concepts map well to Galaxus's domain (Products, Orders, Specifications)
- MediatR will help manage complex workflows (3-stage product process)

### Challenges Identified
- Galaxus uses file-based integration (no REST API)
- Strict validation requirements (ASCII 32-126, GTIN checks)
- Complex XML formats (OpenTRANS 2.1)
- 15-minute sync intervals require efficient processing

### Next Session Goals
- Complete database schema design
- Create initial entity models
- Set up PostgreSQL development database
- Begin repository interface definitions

---

**Project Started**: 22/8/2025  
**Last Updated**: 27/8/2025  
**Current Phase**: Architecture Complete, Database Design In Progress  
**Developer**: Abdelrahman Omar  
**Status**: Active Development - Step 2 of 10

*This README will be updated after each completed step to reflect current progress and learnings.*