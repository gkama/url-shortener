using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using url.shortener.services;

namespace url.shortener.core.Controllers
{
    [ApiController]
    [Route("url")]
    public class UrlController : ControllerBase
    {
        private readonly IUrlRepository _repo;

        public UrlController(IUrlRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetUrlsAsync());
        }

        [HttpGet]
        [Route("id/{id}")]
        public async Task<IActionResult> GetUrlAsync([FromRoute]int id)
        {
            return Ok(await _repo.GetUrlAsync(id));
        }

        [HttpGet]
        [Route("publickey/{publickey}")]
        public async Task<IActionResult> GetUrlAsync([FromRoute]Guid publickey)
        {
            return Ok(await _repo.GetUrlAsync(publickey));
        }

        [HttpGet]
        [Route("target/{target}")]
        public async Task<IActionResult> GetUrlAsync([FromRoute]string target)
        {
            return Ok(await _repo.GetUrlAsync(target));
        }
    }
}
