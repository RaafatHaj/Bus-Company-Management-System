using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Encodings.Web;
using System.Text;
using TravelCompany.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Travel_Company_MVC.Services.Email
{
	// WebEncoders & HtmlEncoder & Encoding

	//Feature          WebEncoders(Microsoft.AspNetCore.WebUtilities) 
	//Purpose          Encodes data for safe URL transmission(Base64Url encoding)
	//Use Case         Protects data in URLs, avoids unsafe characters
	//Common Methods   Base64UrlEncode(), Base64UrlDecode()    
	//Example Usage    Encoding a token for URL safety


	//Feature          HtmlEncoder(System.Text.Encodings.Web) 
	//Purpose          Encodes special characters for HTML output(XSS protection).
	//Use Case         Prevents XSS attacks by encoding<> & " '.
	//Common Methods   Encode(), EncodeUtf8() 
	//Example Usage    Encoding a string for safe HTML display


	//Feature          Encoding(System.Text)
	//Purpose          Encodes text into different character sets(UTF-8, ASCII, etc.).
	//Use Case         Converts strings to byte arrays (e.g., for file storage or encryption).
	//Common Methods   Encoding.UTF8.GetBytes(), Encoding.UTF8.GetString()
	//Example Usage    Encoding a string into UTF-8 bytes.




	// Understanding Confirming Email Steps ...

	// 1. Generate Code => there is a built in function in identity to genetate it , it is uniqe with the user 
	//     the function is =>  var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

	// 2. Generate Url using Url.Page => pass the Code and userID as a parameters to it

	// 3. Build the Email and pass the url to it => example: <a hrem="url">confirm</a>

	// 4. Confirm Email when user click on link => link will take you to ConfirmEmail page
	//     where the code and userId will be extract from url parameters and confirm if it is valid for this user
	//     using the function =>  var result = await _userManager.ConfirmEmailAsync(user, code);
	//     if valid => will turn ConfirmedEmail from 0 to 1 in database ......

	public class EmailService :IEmailService
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailBuilder _emailBuilder;
        private readonly IEmailSender _emailSender;


        // Injecting these two interface so that i can access to Url and current HttpReaust which i can just access to them
        // in the controller becuse they part of controllerBase class ...
        // and you need to register some services in Program.cs so that you can work with them

        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EmailService(UserManager<ApplicationUser> userManager,
            IEmailBuilder emailBuilder,
            IUrlHelperFactory urlHelperFactory,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailBuilder = emailBuilder;
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
            _emailSender = emailSender;
        }

        public async Task SendConfimingEmailAsync(ApplicationUser user)
        {
            var callbackUrl = await GenerateConfirmationLink(user);

            var body = _emailBuilder.ImageUrl("test")
                            .Header("test")
                            .Body("Welcome to our Travel Company " +
                                  ".First, you need to confirm your account. Just press the button below.")
                            .Url(HtmlEncoder.Default.Encode(callbackUrl!))
                            .LinkTitle("Confirm Account")
                            .Build();


            await _emailSender.SendEmailAsync(user.Email!, "Confirm your email", body);

        }

        private async Task<string> GenerateConfirmationLink(ApplicationUser user)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new InvalidOperationException("HttpContext is null");

            var actionContext = new ActionContext(httpContext, httpContext.GetRouteData(), new ActionDescriptor());
            var urlHelper = _urlHelperFactory.GetUrlHelper(actionContext);


            // If i were to handle generating the link/url in the controller ther is no need to the previous code 
            // becouse i can get to Url and Requst dirrectly without injecting additional interfaces .... 

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var callbackUrl = urlHelper.Page(
                "/Account/ConfirmEmail",
                pageHandler: null, // the methods in the page ex: OnGetAsync, if there is multipule methods spicify which one here ...
                values: new { area = "Identity", userId = user.Id, code },
                protocol: httpContext.Request.Scheme);

           //var test = urlHelper.Page("/Account/ConfirmEmail",null, new { area = "Identity", userId = user.Id, code },httpContext.Request.Scheme); 

            return callbackUrl!;

        }
    }
}
