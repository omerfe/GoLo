# GoLo Games



### Project Structure
```
/src
* ApplicationCore
* Infrastructure
* Web



/tests
* UnitTests
```



### Migrations
```
/Infrastructure
Add-Migration InitialCreate -Context GoloContext -OutputDir "Data\Migrations"
Update-Database -Context GoloContext
Add-Migration InitialIdentity -Context GoloIdentityDbContext -OutputDir "Identity\Migrations"
Update-Database -Context GoloIdentityDbContext
```



### Packages
```
/ApplicationCore
Install-Package Ardalis.Specification -v 5.2.0



/Infrastructure
Install-Package Microsoft.EntityFrameworkCore -v 5.0.12
Install-Package Ardalis.Specification.EntityFrameworkCore -v 5.2.0
Install-Package Microsoft.AspNetCore.Identity.EntityFrameworkCore -v 5.0.12
Install-Package Microsoft.EntityFrameworkCore.SqlServer -v 5.0.12
Install-Package Microsoft.EntityFrameworkCore.Tools -v 5.0.12
```




### Resources
* https://github.com/dotnet-architecture/eShopOnWeb
* https://github.com/yigith/TechMarket
* https://www.connectionstrings.com/postgresql/
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-5.0#localization-middleware
* https://getbootstrap.com/docs/5.1/forms/layout/#inline-forms
* https://getbootstrap.com/docs/5.1/components/pagination/#alignment
* https://gist.github.com/yigith/c6f999788b833dc3d22ac6332a053dd1
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-context?view=aspnetcore-5.0
* https://docs.microsoft.com/en-us/ef/core/querying/related-data/eager
* https://docs.microsoft.com/en-us/aspnet/core/mvc/models/model-binding?view=aspnetcore-5.0#dictionaries-1
* https://github.com/dotnet/aspnetcore/issues/16663
* https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.modelbinderattribute.name?view=aspnetcore-5.0
* https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/write?view=aspnetcore-5.0