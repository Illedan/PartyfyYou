using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Partify.Storage.Server.Mode;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Partify.Storage.WebApi.Controller
{
    [Route("api/[controller]")]
    public class Mode : ControllerBase
    {
        private readonly IModeService m_modeService;

        public Mode(IModeService modeService)
        {
            m_modeService = modeService;
        }
        // GET: api/values
        [HttpGet]
        [ProducesResponseType(typeof(ModeResult), 200)]
        public async Task<IActionResult> Get()
        {
            var result = await m_modeService.GetAllModes();
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModeResult), 200)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await m_modeService.GetModeById(id);
            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
