# Cloud CI/CD deployment:   Docker/Kubernetes 

- AWS Infrastructure Setup:

  - Use EKS for managed Kubernetes clusters

  - Implement ECR for private Docker registry

  - Configure IAM roles for secure service access

  - Set up ALB/NLB for ingress control

  - Utilize CloudWatch for monitoring and logs

  - Implement VPC design for network isolation

- CI/CD Pipeline Design:

  - Build Docker images in pipeline stages

  - Scan images for vulnerabilities (Trivy/Aqua)

  - Deploy to EKS using Helm charts

  - Implement blue-green deployments

  - Configure HPA for auto-scaling

  - Use secrets management (AWS Secrets Manager)

- Key Bottlenecks:

  - Image build times and layer caching

  - Resource contention in shared clusters

  - Network latency in multi-AZ deployments

  - IAM and security policy complexity

  - Storage performance for stateful workloads

  - Cost management of cluster resources
