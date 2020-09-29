using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using urlShortener.Models;
using urlShortener.Services;

namespace urlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlDataService _service;
        public UrlShortenerController(IUrlDataService service)
        {
            _service = service;
        }
        [HttpGet]
        public ActionResult List()
        {
            return Ok(_service.List());
        }

        [HttpPost]
        public ActionResult Post(UrlDataEntryModel entry)
        {

            return Ok(_service.Post(entry));
        }

        [HttpGet("{id}")]
        public ActionResult<UrlData> RedirectUrl(string id)
        {
            return Redirect(_service.GetUrl(id));
        }

        [HttpGet("url/{id}")]
        public ActionResult<UrlData> Get(string id)
        {
            return Ok(_service.Get(id));
        }


    }
}
