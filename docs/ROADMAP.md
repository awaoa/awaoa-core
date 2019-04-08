Awaoa.Security.dll
    - Awaoa.Authentication
    - Awaoa.Authorization

Awaoa.Auditing.dll

Awaoa.Insights.dll
    - Awaoa.EventTracking (request etc.)
    - Awaoa.Logging
    - Awaoa.HealthChecking
        + Database(NoSQL, SQL. Like Azure Database for MySQL, Azure Database for SQL Server, MongoDB, CosmosDB etc.)
        + Cache(Redis etc.)
        + Storage(Disk, Object Storage Service. Like Azure Blob Storage Account, Azure Virtual Disk etc.)
        + MQ
        + Mail Server
        + Intetnet Access
        + Identity Service(Like Active Directory, Azure Active Directory, ADFS, IdentityServer etc.)

Awaoa.Services.dll
    - Service auto injection
    - IStorageService

Awaoa.EntityFrameworkCore.dll
    - Awaoa.Entities
        + IEntity: TPrimaryKey
        + ICreationEntity
            - Timespan: Creation
        + IModifyEntity
            - Timespan: LastModified
        + ISoftDeleteEntity
            - IsDeleted
    - Awaoa.Repositories
    - Awaoa.UnitOfWork

Awaoa.Web.dll
    - API service auto discovery
        + implemented from a special interface, like IHostedApiService
        + convert key words to http standard method, like create for HTTP post, update for HTTP PUT/PATCH, list and get for HTTP GET, delete and remove for HTTP DELETE etc.
        + support swagger docs.
        + routing
    - Common Web operations
        + File uploading controller
        + Verfication code controller
        + Error controller
        + AwaoaController for common basics of the controller

Awaoa.Extension.dll
    - common used of the default configurations for middleware and services.


Framework logging standards reference: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.2