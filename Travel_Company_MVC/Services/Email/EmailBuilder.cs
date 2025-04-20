namespace Travel_Company_MVC.Services.Email
{
    public class EmailBuilder : IEmailBuilder
    {
        

        private string _imageUrl="[imageUrl]";
        private string _header = "[header]";
        private string _body = "[body]";
        private string _url = "[url]";
        private string _linkTitle = "[linkTitle]";

        private readonly IWebHostEnvironment _webHostEnvironment;

        public EmailBuilder(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public EmailBuilder ImageUrl(string imageUrl)
        {
            _imageUrl = imageUrl;
            return this;
        }
        public EmailBuilder Header(string header)
        {
            _header = header;
            return this;
        }
        public EmailBuilder Body(string body)
        {
            _body = body;
            return this;
        }
        public EmailBuilder Url(string url)
        {
            _url = url;
            return this;
        }
        public EmailBuilder LinkTitle(string linkTitle)
        {
            _linkTitle = linkTitle;
            return this;
        }
        public string Build()
        {
            var filePath = $"{_webHostEnvironment.WebRootPath}/Templates/Email.html";

          

            using (StreamReader str = new(filePath))
            {
                var template = str.ReadToEnd();


                return template.Replace("[body]", _body)
                    .Replace("[linkTitle]", _linkTitle)
                    .Replace("[url]", _url)
                    ;
            }

            //return "test";

  
            //.Replace("[imageUrl]", _imageUrl)
            //.Replace("[header]", _header)
            //.Replace("[body]", _body)
            //.Replace("[url]", _url)
            //.Replace("[linkTitle]", _linkTitle);



        }
    }
}
