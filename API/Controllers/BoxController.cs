using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{

    public class BoxController : BaseController
    {



        private readonly IMapper _mapper;
        private readonly IUOW _uow;

        public BoxController(ILogger<BoxController> logger, IMapper mapper, IUOW uow) : base(logger)
        {
            this._uow = uow;
            this._mapper = mapper;

        }


        [HttpGet()]
        public async Task<IActionResult> GetCollages([FromQuery] BoxParam param)
        {
            param.IsPagination = false;
            var spec = new BoxWithSpecification(param);
            var boxesFromDb = await _uow.BoxRepository.GetAll(spec);
            if (boxesFromDb == null)
            {
                return NotFound();
            }
            var BoxToReturn = _mapper.Map<IReadOnlyList<BoxDto>>(boxesFromDb);

            return Ok(BoxToReturn);

        }
    }
}