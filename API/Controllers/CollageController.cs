using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class CollageController : BaseController
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;
        public CollageController(ILogger<CollageController> logger, IUOW uow, IMapper mapper) : base(logger)
        {
            this._uow = uow;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult> GetCollage()
        {
            var CollagesFromDb = await _uow.CollageRepository.GetAll();
            var CollageToReturn = _mapper.Map<IReadOnlyList<CollageDto>>(CollagesFromDb);
            return Ok(CollageToReturn);


        }
    }
}