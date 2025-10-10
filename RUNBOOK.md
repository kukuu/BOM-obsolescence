# Complete Installation & Testing Steps

- Prerequisites Verification:

# Check .NET 8

```
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

- Step 1: Backend Setup: 

```
cd BOM-compliance/backend

# Restore dependencies
dotnet restore

# Create database (ensure PostgreSQL is running)
createdb bom_compliance

# Run migrations
cd BOM.Compliance.API
dotnet ef database update

# Seed initial data
dotnet run --seed
```