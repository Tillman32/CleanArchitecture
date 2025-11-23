# GitHub Actions Workflows

This directory contains the CI/CD workflows for the CleanArchitecture project.

## Workflows

### 1. CI - Build and Test (`ci.yml`)

**Purpose:** Continuous Integration workflow that builds and tests the application on every push and pull request.

**Triggers:**
- Push to `main` or `master` branches
- Pull requests to `main` or `master` branches

**Steps:**
1. Checkout code
2. Setup .NET 5.0
3. Restore NuGet dependencies
4. Build solution in Release configuration
5. Run all tests with trx logger
6. Upload test results as artifacts
7. Generate test report (visible in PR checks)

**Artifacts:** Test results (.trx files)

---

### 2. Release - Publish to GitHub Packages (`release.yml`)

**Purpose:** Build and publish NuGet packages to GitHub Packages when a new release is created.

**Triggers:**
- GitHub release published
- Push of tags matching `v*` pattern (e.g., `v1.0.0`, `v2.1.3`)

**Steps:**
1. Checkout code
2. Setup .NET 5.0
3. Extract version from Git tag
4. Restore and build solution
5. Pack `CleanArchitecture.Core` library
6. Pack `CleanArchitecture.Infrastructure` library
7. Push packages to GitHub Packages
8. Upload packages as workflow artifacts

**Versioning:**
- Tags: Uses the tag version (e.g., `v1.2.3` → version `1.2.3`)
- Non-tags: Uses preview version (e.g., `1.0.0-preview.abc12345`)

**Packages Published:**
- `CleanArchitecture.Core` - Core business logic and entities
- `CleanArchitecture.Infrastructure` - Infrastructure and data access layer

**Artifacts:** NuGet packages (.nupkg files)

---

### 3. CD - Deploy Application (`deploy.yml`)

**Purpose:** Deploy the CleanArchitecture web application to various environments.

**Triggers:**
- Manual workflow dispatch with environment selection

**Environments:**
- Development
- Staging
- Production

**Steps:**
1. Checkout code
2. Setup .NET 5.0
3. Restore and build solution
4. Publish the ClientWeb application
5. Upload published artifacts
6. **Deployment steps (to be configured)**

**Configuration Required:**

This is a template workflow. To complete the deployment setup, add deployment steps for your target platform:

#### Azure App Service
```yaml
- name: Deploy to Azure Web App
  uses: azure/webapps-deploy@v2
  with:
    app-name: 'your-app-name'
    publish-profile: ${{ secrets.AZURE_WEBAPP_PUBLISH_PROFILE }}
    package: ./publish
```

#### AWS Elastic Beanstalk
```yaml
- name: Deploy to AWS
  uses: einaregilsson/beanstalk-deploy@v21
  with:
    aws_access_key: ${{ secrets.AWS_ACCESS_KEY_ID }}
    aws_secret_key: ${{ secrets.AWS_SECRET_ACCESS_KEY }}
    application_name: your-app
    environment_name: your-env
    version_label: ${{ github.sha }}
    deployment_package: deploy.zip
```

#### Docker
```yaml
- name: Build Docker image
  run: docker build -t your-registry/cleanarchitecture:${{ github.sha }} .

- name: Push to registry
  run: docker push your-registry/cleanarchitecture:${{ github.sha }}
```

#### SSH Deployment
```yaml
- name: Deploy via SSH
  uses: appleboy/scp-action@master
  with:
    host: ${{ secrets.SSH_HOST }}
    username: ${{ secrets.SSH_USER }}
    key: ${{ secrets.SSH_PRIVATE_KEY }}
    source: "./publish/*"
    target: "/var/www/cleanarchitecture"
```

**Artifacts:** Published application files

---

## Usage

### Running CI Workflow
The CI workflow runs automatically on every push and pull request. No manual action needed.

### Creating a Release
1. Create and push a version tag:
   ```bash
   git tag v1.0.0
   git push origin v1.0.0
   ```
2. Or create a GitHub Release through the web interface
3. The workflow will automatically build and publish NuGet packages

### Deploying the Application
1. Go to Actions tab in GitHub
2. Select "CD - Deploy Application" workflow
3. Click "Run workflow"
4. Select the target environment
5. Click "Run workflow" button

### Using Published Packages

After a successful release, the packages are available at:
```
https://github.com/Tillman32?tab=packages
```

To use in your project, add the GitHub Packages source to your `nuget.config`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="github" value="https://nuget.pkg.github.com/Tillman32/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="YOUR_GITHUB_USERNAME" />
      <add key="ClearTextPassword" value="YOUR_GITHUB_PAT" />
    </github>
  </packageSourceCredentials>
</configuration>
```

## Secrets Required

### For Release Workflow
- `GITHUB_TOKEN` - Automatically provided by GitHub Actions

### For Deploy Workflow (when configured)
Depending on your deployment target, you may need:
- `AZURE_WEBAPP_PUBLISH_PROFILE` - Azure Web App publish profile
- `AWS_ACCESS_KEY_ID` and `AWS_SECRET_ACCESS_KEY` - AWS credentials
- `SSH_HOST`, `SSH_USER`, `SSH_PRIVATE_KEY` - SSH deployment credentials
- Docker registry credentials

## Environment Configuration

GitHub Environments can be configured in repository Settings → Environments to add:
- Protection rules
- Required reviewers
- Environment secrets
- Deployment branches

## Status Badges

Add these badges to your README.md:

```markdown
![CI](https://github.com/Tillman32/CleanArchitecture/workflows/CI%20-%20Build%20and%20Test/badge.svg)
![Release](https://github.com/Tillman32/CleanArchitecture/workflows/Release%20-%20Publish%20to%20GitHub%20Packages/badge.svg)
```

## Notes

- All workflows use .NET 10.0
- Test results are automatically uploaded and viewable in the Actions tab
- Packages are versioned based on Git tags
- Deploy workflow is a template and requires configuration for actual deployment

## .NET Version

This project uses .NET 10.0, which is the latest version with the most recent features and security updates.

### .NET Support Information:
- [.NET Support Policy](https://dotnet.microsoft.com/platform/support/policy/dotnet-core)
- [What's new in .NET 10](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-10/overview)
