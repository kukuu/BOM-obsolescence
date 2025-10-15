# Complete Installation & Testing Steps

- Prerequisites Verification
```

# Check .NET 8
dotnet --version
# Should output: 8.0.100 or higher

# Check Node.js  
node --version
# Should output: 18.0.0 or higher

# Check Docker (optional)
docker --version

# Check PostgreSQL (if running locally)
psql --version

```
- Step 1: Backend Setup
```
cd BOM-Compliance-repository/backend

# Restore dependencies
dotnet restore

# Create database (ensure PostgreSQL is running)
createdb bom_compliance

# Run migrations
cd BOM.Compliance.API
dotnet ef database update

# Seed initial data (the seeder runs automatically on first startup)

```

- Step 2: Frontend Setup
```
cd BOM-Compliance-repository/frontend

# Install dependencies
npm install

# Create environment file  
cp .env.example .env.local

# Update .env.local with your API URL
echo "REACT_APP_API_BASE_URL=http://localhost:5000/api" > .env.local

```

- Step 3: Run Applications

Terminal 1 - Backend:

```
cd BOM-Compliance-repository/backend/BOM.Compliance.API
dotnet run
# API available at: http://localhost:5000
```

Terminal 2 - Frontend:
```
cd BOM-Compliance-repository/frontend
npm start
# Frontend available at: http://localhost:3000
```

- Step 4: Run Tests

Backend tests:

```
cd BOM-Compliance-repository/backend

# Run all tests
dotnet test

# Run specific test categories
dotnet test --filter "Category=Unit"
dotnet test --filter "Category=Integration"

# Run with coverage
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
```

Frontend tests:

```
cd BOM-Compliance-repository/frontend

# Run all tests
npm test

# Run tests once  
npm test -- --watchAll=false

# Run with coverage
npm test -- --coverage --watchAll=false
```

- Step 5: Docker Deployment (Alternative)

```
cd BOM-Compliance-repository/backend

# Build and run all services
docker-compose up -d --build

# Check running services
docker-compose ps

# View logs
docker-compose logs -f api
```

- Step 6: Verification
```
# Test API health
curl http://localhost:5000/health

# Test authentication
curl -X POST http://localhost:5000/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"analyst@company.com","password":"analyst123"}'

# Access frontend in browser
open http://localhost:3000
```

- Test Users

  - Data Analyst: analyst@company.com / analyst123 (Read-only access)

  - Senior Manager: manager@company.com / manager123 (Read/Write access)
