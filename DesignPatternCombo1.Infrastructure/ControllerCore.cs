using DesignPatternCombo1.Infrastructure.Mediator;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternCombo1.Infrastructure
{
    public class ControllerCore : ControllerBase
    {
        protected readonly IMediator _mediator;

        public ControllerCore(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
