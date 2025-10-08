# BOM Obsolescence Compliance
This BOM Compliance System addresses the critical challenge of component obsolescence in manufacturing and construction industries. The platform solves the expensive and time-consuming problem of electronic components and building materials becoming obsolete, which can halt production lines and delay projects costing companies millions. By automatically identifying at-risk components and providing compliant substitutes, the system prevents production stoppages and ensures regulatory compliance throughout product lifecycles.

The solution leverages a data science modern technology stack with C# .NET 8 for the backend API, PostgreSQL with Supabase for the comprehensive materials database, and React for the responsive frontend interface. It incorporates advanced data science capabilities through machine learning models that extract component metadata from PDF datasheets, while LLM and RAG pipelines intelligently search for alternative components when originals are discontinued. The system includes robust error handling and role-based authentication, distinguishing between Data Analysts with read-only access and Senior Managers with full read-write privileges.


## Folder Structure

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
