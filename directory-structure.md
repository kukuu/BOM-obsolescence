# Directory Structure


```
BOM-compliance/
├── backend/
│   ├── BOM.Compliance.API/
│   │   ├── Controllers/
│   │   │   ├── MaterialsController.cs
│   │   │   ├── SubstitutionController.cs
│   │   │   ├── AuthController.cs
│   │   │   └── UploadController.cs
│   │   ├── Services/
│   │   │   ├── JwtService.cs
│   │   │   └── AuthService.cs
│   │   ├── Middleware/
│   │   │   ├── ExceptionHandlingMiddleware.cs
│   │   │   └── LoggingMiddleware.cs
│   │   ├── Program.cs
│   │   ├── appsettings.json
│   │   ├── appsettings.Development.json
│   │   └── BOM.Compliance.API.csproj
│   ├── BOM.Compliance.Application/
│   │   ├── Interfaces/
│   │   │   ├── IMaterialService.cs
│   │   │   ├── IMLDataExtractionService.cs
│   │   │   ├── ISupabaseService.cs
│   │   │   ├── IVendorSearchService.cs
│   │   │   └── IAuthService.cs
│   │   ├── Services/
│   │   │   ├── MaterialService.cs
│   │   │   ├── EnhancedMLDataExtractionService.cs
│   │   │   ├── VendorSearchService.cs
│   │   │   └── SupabaseService.cs
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
│   │   │       ├── EnhancedMLExtractionResponse.cs
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
│   │   │       └── AlternativeComponentConfiguration.cs
│   │   ├── Migrations/
│   │   │   ├── [Migration Files]
│   │   │   └── ApplicationDbContextModelSnapshot.cs
│   │   └── BOM.Compliance.Infrastructure.csproj
│   ├── tests/
│   │   ├── UnitTests/
│   │   │   ├── Services/
│   │   │   │   ├── MaterialServiceTests.cs
│   │   │   │   ├── MLDataExtractionServiceTests.cs
│   │   │   │   └── VendorSearchServiceTests.cs
│   │   │   ├── Controllers/
│   │   │   │   ├── MaterialsControllerTests.cs
│   │   │   │   └── SubstitutionControllerTests.cs
│   │   │   └── UnitTests.csproj
│   │   ├── IntegrationTests/
│   │   │   ├── DatabaseTests.cs
│   │   │   ├── ApiIntegrationTests.cs
│   │   │   └── IntegrationTests.csproj
│   │   └── TestHelpers/
│   │       ├── TestDataFactory.cs
│   │       └── MockServices.cs
│   ├── docker-compose.yml
│   ├── Dockerfile
│   ├── azure-pipelines.yml
│   └── BOM.Compliance.sln
├── frontend/
│   ├── public/
│   │   ├── index.html
│   │   ├── favicon.ico
│   │   └── manifest.json
│   ├── src/
│   │   ├── components/
│   │   │   ├── MaterialTable.js
│   │   │   ├── MaterialFilters.js
│   │   │   ├── ComplianceBadge.js
│   │   │   ├── AlternativeComponents.js
│   │   │   ├── PdfUploader.js
│   │   │   └── VendorInfo.js
│   │   ├── pages/
│   │   │   ├── Login.js
│   │   │   ├── Dashboard.js
│   │   │   ├── Materials.js
│   │   │   ├── MaterialUpload.js
│   │   │   ├── SubstitutionSearch.js
│   │   │   └── BulkSubstitution.js
│   │   ├── services/
│   │   │   ├── api.js
│   │   │   ├── auth.js
│   │   │   └── materialService.js
│   │   ├── contexts/
│   │   │   └── AuthContext.js
│   │   ├── hooks/
│   │   │   ├── useMaterials.js
│   │   │   ├── useSubstitution.js
│   │   │   └── useAuth.js
│   │   ├── utils/
│   │   │   ├── constants.js
│   │   │   ├── helpers.js
│   │   │   └── validation.js
│   │   ├── styles/
│   │   │   ├── App.css
│   │   │   └── components.css
│   │   ├── App.js
│   │   ├── App.test.js
│   │   ├── index.js
│   │   └── reportWebVitals.js
│   ├── package.json
│   ├── package-lock.json
│   ├── Dockerfile
│   └── .env.example
├── docs/
│   ├── api/
│   │   └── endpoints.md
│   ├── database/
│   │   └── schema.md
│   └── deployment/
│       └── setup-guide.md
├── scripts/
│   ├── setup-database.sh
│   ├── run-tests.sh
│   └── deploy-local.sh
├── README.md
├── RUNBOOK.md
└── docker-compose.override.yml

```
