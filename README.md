# Blog
# Overview
This project is a part of the Blog application, focusing on the domain entities. The primary entities include Post and Comment, both of which inherit from a base class BaseEntity. These entities are designed to represent the core data structures used within the Blog application.
Entities
## BaseEntity
The BaseEntity class provides common properties that are shared across multiple entities in the domain. It includes properties for tracking creation and modification metadata, as well as a soft delete flag.
``` C#
using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        [StringLength(maximumLength: 30)]
        public string? CreatedBy { get; set; }

        public DateTime? CreatedAt { get; set; }

        [StringLength(maximumLength: 30)]
        public string? ModifiedBy { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public bool? IsDeleted { get; set; } = false;
    }
}
```

## Post
The Post class represents a blog post. It also inherits from BaseEntity and includes properties for the post's title, content, and associated comments.
``` C#
namespace Blog.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
```

## Comment
The Comment class represents a comment made on a blog post. It inherits from BaseEntity and includes additional properties specific to comments.
``` C#
namespace Blog.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
```
# Running the Project with .NET Core and Entity Framework
To run the project, follow these steps:
1.	Install .NET Core SDK:
•	Ensure you have the .NET Core SDK installed. You can download it from the official .NET website.

3.	Install Entity Framework Core:
•	Add the necessary EF Core packages to your project. You can do this via the NuGet Package Manager or the command line:
```
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.SqlServer
   dotnet add package Microsoft.EntityFrameworkCore.Tools
```

3.	Set the Database Connection String:
•	Update the appsettings.json file with your database connection string.

5.	Run database update:
•	Set up the database schema:
```
   dotnet ef database update
```

5. Run the Application:
•	Use the following command to run your application:
```
   dotnet run
```
