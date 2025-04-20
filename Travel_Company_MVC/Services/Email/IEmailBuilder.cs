using Microsoft.AspNetCore.Hosting;
using System.Reflection.PortableExecutable;

namespace Travel_Company_MVC.Services.Email
{
    public interface IEmailBuilder
    {

        EmailBuilder ImageUrl(string imageUrl);
        EmailBuilder Header(string header);
        EmailBuilder Body(string body);
        EmailBuilder Url(string url);
        EmailBuilder LinkTitle(string linkTitle);
        string Build();
    }
}
