using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ShortLink.Application.DTO;
using ShortLink.Application.Services;
using ShortLink.Models;

namespace ShortLink.Controllers
{
    public class LinksController : ApiController
    {
        private string ClientId => HttpContext.Current.Request.Cookies["clientId"]?.Value;
        private readonly ILinkService _linkService;

        public LinksController(ILinkService linkService)
        {
            _linkService = linkService;
        }

        
        public async Task<IEnumerable<LinkDTO>> Get()
        {
            return (await _linkService.ClientLinks(ClientId)).ToList();
        }

       
        public async Task<LinkShortDTO> Post([FromBody]LinkRequest request)
        {
            if (!ModelState.IsValid || !IsValidUri(request.Url))
            {
                throw new ArgumentException("Incorrect URL");
            }
            return await _linkService.CreateLink(request.Url, ClientId);
        }

        private bool IsValidUri(string uri)
        {
            Uri validatedUri;
            return Uri.TryCreate(uri, UriKind.Absolute, out validatedUri);
        }
    }
}
