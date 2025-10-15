# Directory Structure


```
BOM-Compliance-repository/
├── backend/
│   ├── BOM.Compliance.API/
│   │   ├── Controllers/
│   │   │   ├── MaterialsController.cs
│   │   │   └── AuthController.cs
│   │   ├── Middleware/
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   └── LoggingMiddleware.cs
│   │   ├── Services/
│   │   │   ├── JwtService.cs
│   │   │   └── AuthService.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   └── BOM.Compliance.API.csproj
│   ├── BOM.Compliance.Application/
│   │   ├── Interfaces/
│   │   │   ├── IMaterialService.cs
│   │   │   ├── IMLDataExtractionService.cs
│   │   │   ├── IVendorSearchService.cs
│   │   │   └── IAuthService.cs
│   │   ├── Services/
│   │   │   ├── MaterialService.cs
│   │   │   ├── MLDataExtractionService.cs
│   │   │   └── VendorSearchService.cs
│   │   ├── Models/
│   │   │   ├── Requests/
│   │   │   │   ├── CreateMaterialRequest.cs
│   │   │   │   ├── FindAlternativesRequest.cs
│   │   │   │   └── BulkAlternativesRequest.cs
│   │   │   ├── Responses/
│   │   │   │   ├── ExtractionResult.cs
│   │   │   │   ├── ComponentSubstitution.cs
│   │   │   │   ├── SubstitutionResponse.cs
│   │   │   │   └── BulkSubstitutionResponse.cs
│   │   │   └── ExternalApis/
│   │   │       ├── MLExtractionResponse.cs
│   │   │       ├── LLMRAGSearchResponse.cs
│   │   │       └── VendorComponent.cs
│   │   └── BOM.Compliance.Application.csproj
│   ├── BOM.Compliance.Domain/
│   │   ├── Entities/
│   │   │   ├── Material.cs
│   │   │   ├── MaterialSpecification.cs
│   │   │   ├── AlternativeComponent.cs
│   │   │   ├── ComplianceData.cs
│   │   │   ├── LifecycleEvent.cs
│   │   │   └── User.cs
│   │   ├── Enums/
│   │   │   ├── MaterialType.cs
│   │   │   ├── MaterialStatus.cs
│   │   │   └── UserRole.cs
│   │   └── BOM.Compliance.Domain.csproj
│   ├── BOM.Compliance.Infrastructure/
│   │   ├── Data/
│   │   │   ├── ApplicationDbContext.cs
│   │   │   ├── DatabaseSeeder.cs
│   │   │   └── Configurations/
│   │   │       ├── MaterialConfiguration.cs
│   │   │       ├── MaterialSpecificationConfiguration.cs
│   │   │       ├── AlternativeComponentConfiguration.cs
│   │   │       ├── LifecycleEventConfiguration.cs
│   │   │       └── UserConfiguration.cs
│   │   └── BOM.Compliance.Infrastructure.csproj
│   ├── tests/
│   │   ├── UnitTests/
│   │   │   ├── Services/
│   │   │   │   ├── MaterialServiceTests.cs
│   │   │   │   └── MLDataExtractionServiceTests.cs
│   │   │   └── UnitTests.csproj
│   │   └── IntegrationTests/
│   │       ├── DatabaseTests.cs
│   │       └── IntegrationTests.csproj
│   ├── docker-compose.yml
│   ├── Dockerfile
│   └── BOM.Compliance.sln
├── frontend/
│   ├── public/
│   │   ├── index.html
│   │   └── favicon.ico
│   ├── src/
│   │   ├── components/
│   │   │   ├── Navbar.js
│   │   │   ├── MaterialTable.js
│   │   │   ├── MaterialFilters.js
│   │   │   ├── ComplianceBadge.js
│   │   │   ├── AlternativeComponents.js
│   │   │   └── PdfUploader.js
│   │   ├── pages/
│   │   │   ├── Login.js
│   │   │   ├── Dashboard.js
│   │   │   ├── Materials.js
│   │   │   ├── MaterialUpload.js
│   │   │   ├── SubstitutionSearch.js
│   │   │   └── BulkSubstitution.js
│   │   ├── services/
│   │   │   ├── api.js
│   │   │   └── materialService.js
│   │   ├── contexts/
│   │   │   └── AuthContext.js
│   │   ├── hooks/
│   │   │   ├── useAuth.js
│   │   │   └── useMaterials.js
│   │   ├── utils/
│   │   │   ├── constants.js
│   │   │   ├── helpers.js
│   │   │   └── validation.js
│   │   ├── styles/
│   │   │   └── App.css
│   │   ├── App.js
│   │   ├── index.js
│   │   └── reportWebVitals.js
│   ├── package.json
│   ├── Dockerfile
│   └── .env.example
├── README.md
├── RUNBOOK.md
└── setup-dev.sh

```
