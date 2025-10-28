# BOM Obsolescence Compliance

This is a **C#** application.  

This BOM Compliance System addresses the critical challenge of component obsolescence in manufacturing and construction industries. The platform solves the expensive and time-consuming problem of electronic components and building materials becoming obsolete, which can halt production lines and delay projects costing companies millions. By automatically identifying at-risk components and providing compliant substitutes, the system prevents production stoppages and ensures regulatory compliance throughout product lifecycles.

The solution leverages a data science modern technology stack with C# .NET 8 for the backend API, PostgreSQL with Supabase for the comprehensive materials database, and React for the responsive frontend interface. It incorporates advanced data science capabilities through machine learning models that extract component metadata from PDF datasheets, while LLM and RAG pipelines intelligently search for alternative components when originals are discontinued. The system includes robust error handling and role-based authentication, distinguishing between Data Analysts with read-only access and Senior Managers with full read-write privileges.  
 
- **Algorithm Workflow:** 

The system implements a sophisticated four-tier decision tree that sequentially searches internal databases, vendor APIs, Vector Search - LLM+RAG pipelines, and fallback fuzzy matching to ensure optimal component substitution with comprehensive metadata extraction including production years, compliance status, vendor pricing, and stock availability. 

## Artefacts 
https://github.com/kukuu/BOM-obsolescence/blob/main/artefacts.md 

## Directory Structure
https://github.com/kukuu/BOM-obsolescence/blob/main/directory-structure.md 

## Metadata Extraction Worflow
https://github.com/kukuu/BOM-obsolescence/blob/main/metadata-extraction-workflow.md 

##  Code Repository

https://github.com/kukuu/BOM-Compliance-repo (**PRIVATE**)

## Cloud CI/CD Deployment
https://github.com/kukuu/BOM-obsolescence/blob/main/cloud-CI-CD-deployment.md

## Verification Checklist
https://github.com/kukuu/BOM-obsolescence/blob/main/verification-checklist.md
 
## RUNBOOK
https://github.com/kukuu/BOM-obsolescence/blob/main/RUNBOOK.md


