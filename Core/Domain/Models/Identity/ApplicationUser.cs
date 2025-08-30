using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System.Net;

[CollectionName("Users")]
public class ApplicationUser : MongoIdentityUser
{
    public string DisplayName { get; set; }
}
