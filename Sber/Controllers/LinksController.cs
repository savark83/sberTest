using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Sber.Controllers
{
    using System;
    using Sber.App.Entity;
    using Sber.App.Services;

    [Produces("application/json")]
    [Route("api/Links")]
    public class LinksController : Controller
    {
        private const string IdCookieName = "uid";

        private readonly ILinksService _linksService;

        public LinksController(ILinksService linksService)
        {
            this._linksService = linksService;
        }
        // GET: api/Links
        [HttpGet]
        public IEnumerable<Link> Get()
        {
            var userId = GetUserId();

            if (userId == null)
                return null;

            return this._linksService.GetByUser(userId.Value);
        }

        // GET: api/Links/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(string id)
        {
            return this._linksService.Get(id)?.Value;
        }

        // POST: api/Links
        [HttpPost]
        public string Post([FromBody]string linkValue)
        {
            var userId = GetUserId();
            if (userId == null)
            {
                userId = Guid.NewGuid();
                this.Response.Cookies.Append(IdCookieName, userId.Value.ToString());
            }
            return this._linksService.Create(linkValue, userId.Value).ShortValue;
        }

        private Guid? GetUserId()
        {
            var cookie = this.Request.Cookies[IdCookieName];

            if (Guid.TryParse(cookie, out var userId)) return userId;

            return null;
        }
    }
}
