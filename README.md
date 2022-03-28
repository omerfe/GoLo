# GoLo Games

GoLo Games is an e-commerce website that sells keys which users need to enter in order to activate the games they downloaded from various platforms such as Steam. Users can access the newest games, pre-order them before their release dates with available discounts and view their orders in detail. Admins have a personal dashboard which can be used to create, edit or delete controls such as games, platforms, discounts, keys and products. You can find a few videos below to preview the pages.

https://user-images.githubusercontent.com/92364088/160476167-80318282-773a-44c7-86b4-ac65cc89b740.mp4

https://user-images.githubusercontent.com/92364088/160474433-5adb507d-1da7-4f80-aaed-96de31b39695.mp4

https://user-images.githubusercontent.com/92364088/160475524-40a7adf9-85ac-4693-a41b-e7c02c8b47b2.mp4

https://user-images.githubusercontent.com/92364088/160476127-b8a48134-06e2-4c5b-9274-60ec0355fcd2.mp4

https://user-images.githubusercontent.com/92364088/160475504-25ca1a0a-5d49-4b42-92e0-33ea17c7ab05.mp4

https://user-images.githubusercontent.com/92364088/160476293-a2a4dcba-853b-440d-b246-09d26d284d7d.mp4

https://user-images.githubusercontent.com/92364088/160476311-a2dacfa0-1aba-4405-9568-779a61856f83.mp4

https://user-images.githubusercontent.com/92364088/160476319-def55e35-7f03-4b44-a7b1-1111c98f7509.mp4

https://user-images.githubusercontent.com/92364088/160476339-f49b5d66-4ef8-4678-91f6-8e4d4d3d0039.mp4

https://user-images.githubusercontent.com/92364088/160476438-37c6c7c9-5fc7-48c9-950d-c9f29971f5e7.mp4


### Project Structure
```
/src
* ApplicationCore
* Infrastructure
* Web
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
* https://stackoverflow.com/questions/39325414/line-break-in-html-with-n
* https://stackoverflow.com/questions/13513932/algorithm-to-detect-overlapping-periods
* https://www.cssscript.com/filterable-checkable-multi-select/
