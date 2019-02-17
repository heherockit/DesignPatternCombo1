using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesignPatternCombo1.Infrastructure;
using DesignPatternCombo1.Infrastructure.Mediator;
using DesignPatternCombo1.Web.Features.Values;
using Microsoft.AspNetCore.Mvc;

namespace DesignPatternCombo1.Web.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ValuesController : ControllerCore
    {
        public ValuesController(IMediator mediator)
            : base(mediator)
        {

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get([FromQuery] ValueQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetExact([FromQuery] ExactQuery query)
        {
            return Ok(await _mediator.SendAsync(query));
        }
    }
}
