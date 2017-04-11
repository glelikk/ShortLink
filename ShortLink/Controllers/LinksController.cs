using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using ShortLink.Application.DTO;
using ShortLink.Application.Services;

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

       
        public async Task<string> Post([FromBody]string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL не может быть пустым");
            return await _linkService.CreateLink(url, ClientId);
        }


        
        public async Task<string> Get(string id)
        {
            return await _linkService.GetLink(id);
        }
    }
}
