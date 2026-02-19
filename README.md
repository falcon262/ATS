# ATS

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)](https://abp.io/docs/latest/framework/architecture/domain-driven-design) practises. All the fundamental ABP modules are already installed. Check the [Application Startup Template](https://abp.io/docs/latest/solution-templates/layered-web-application) documentation for more info.

### Pre-requirements

* [.NET9.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v18 or 20](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

* Check the `ConnectionStrings` in `appsettings.json` files under the `ATS.HttpApi.Host` and `ATS.DbMigrator` projects and change it if you need.

### Before running the application

* Run `abp install-libs` command on your solution folder to install client-side package dependencies. This step is automatically done when you create a new solution, if you didn't especially disabled it. However, you should run it yourself if you have first cloned this solution from your source control, or added a new client-side package dependency to your solution.
* Run `ATS.DbMigrator` to create the initial database. This step is also automatically done when you create a new solution, if you didn't especially disabled it. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

To generate a signing certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p af03f3da-bf12-4b91-9a25-299a8e3c4b6a
```

> `af03f3da-bf12-4b91-9a25-299a8e3c4b6a` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: [OpenIddict Certificate Configuration](https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios)

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/Deployment/Configuring-OpenIddict#production-environment) documentation for more information.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `ATS.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.
* `ATS.HttpApi.Host`: ASP.NET Core API application that is used to expose the APIs to the clients.
* `angular`: Angular application.

## Features

### Public Job Applications

This ATS includes a public job application feature that allows external candidates to apply to job postings without requiring upfront authentication.

#### How it Works

1. **Shareable Job Links**: Each published job automatically generates a unique, shareable public URL (e.g., `https://yourapp.com/apply/senior-software-engineer-a1b2c3d4`)
2. **Guest Application**: Candidates can fill out the application form without creating an account
3. **Post-Application Registration**: After submitting an application, candidates can create an account to track their application status
4. **Candidate Portal**: Once registered, candidates have read-only access to view their own application statuses

#### For Administrators

- View and copy the public application link from the job detail page
- All applications (public and internal) are managed in the same pipeline
- No difference in processing between public and internal applications

#### For Candidates

- Apply to jobs via public links without registration
- Upload resume (PDF, DOC, DOCX up to 5MB)
- Create account after application to track progress
- View application status and stage in candidate dashboard
- Receive updates on interview schedules and offers

#### Technical Details

- Public endpoints: `/api/public/jobs` (no authentication required)
- Candidate portal endpoints: `/api/candidate-portal` (authentication required)
- Resume files are stored in the database (configurable for external blob storage)
- Rate limiting and CAPTCHA can be added to prevent spam
- Email verification recommended before allowing candidate login


## Deploying the application

Deploying an ABP application follows the same process as deploying any .NET or ASP.NET Core application. However, there are important considerations to keep in mind. For detailed guidance, refer to ABP's [deployment documentation](https://abp.io/docs/latest/Deployment/Index).

### Additional resources


#### Internal Resources

You can find detailed setup and configuration guide(s) for your solution below:

* [Angular](./angular/README.md)

#### External Resources
You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-1)
* [Application Startup Template](https://abp.io/docs/latest/startup-templates/application/index)
