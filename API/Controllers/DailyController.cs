using System.Net.Http;
using System.Net;
using System.Linq;



using AutoMapper;
using Core.Models;
using Core.Specifications;
using InfraStructure.Service;
using Microsoft.AspNetCore.Mvc;
using API.Errors;

namespace API.Controllers
{
    public class DailyController : BaseController
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public DailyController(ILogger<DailyController> logger, IUOW uow, IMapper mapper) : base(logger)
        {
            this._mapper = mapper;
            this._uow = uow;

        }

        [HttpGet]
        public async Task<IActionResult> GetDaily([FromQuery] DailyParam param)
        {
            var spec = new DailyWithSpecification(param);
            var dailies = await _uow.DailyRepository.GetAll(spec);





            var countSpec = new DailyCountAsyncWithSpecification(param);
            var dailiyCount = await _uow.DailyRepository.CountAsync(countSpec);

            param.IsPagination = false;

            var totalDailies = await _uow.DailyRepository.GetAll(new DailyWithSpecification(param));





            var dailiesDto = _mapper.Map<IReadOnlyList<DailyDto>>(dailies);
            foreach (var daily in dailiesDto)
            {
                var dailyBox = await _uow.DailyRepository.GetDailyBoexByDailyId(daily.Id.Value);
                daily.TaxNormal = dailyBox.Sum(x => x.TaxNormal);
                daily.Stamp = dailyBox.Sum(x => x.Stamp);
                daily.Taxsettlement = dailyBox.Sum(x => x.Taxsettlement);
                daily.Tax2 = dailyBox.Sum(x => x.Tax2);
                daily.Other = dailyBox.Sum(x => x.Other);

                daily.Total = dailyBox.Sum(x => x.SumTax);
                daily.TotalDevelopment = dailyBox.Sum(x => x.TaxDevelopment);
            }
            var responseData = new responseData<DailyDto>(dailiesDto, dailiyCount);

            foreach (var daily in totalDailies)
            {

                var dailyBox = await _uow.DailyRepository.GetDailyBoexByDailyId(daily.Id);



                responseData.TotalTaxNormal += dailyBox.Sum(x => x.TaxNormal);
                responseData.TotalStamp += dailyBox.Sum(x => x.Stamp);
                responseData.TotalTaxsettlement += dailyBox.Sum(x => x.Taxsettlement);
                responseData.TotalTax2 += dailyBox.Sum(x => x.Tax2);
                responseData.TotalOther += dailyBox.Sum(x => x.Other);


                responseData.TotalSumTax += dailyBox.Sum(x => x.SumTax);
                responseData.TotalTaxDevelopment += dailyBox.Sum(x => x.TaxDevelopment);


            }

            return Ok(responseData);
        }

        [HttpGet("search")]
        public async Task<IActionResult> FormSearch([FromQuery] FormParam param)
        {

            if (string.IsNullOrEmpty(param.Num224))
            {
                return RedirectToAction("GetDaily", new DailyParam());
            }

            var spec = new FormWithSpecification(param);
            spec.Includes.Add(x => x.DailyBoxes.Daily);


            var formFromDb = await _uow.FormRepository.GetAll(spec);

            var responseData = new responseData<DailyDto>();


            var dailtListDto = new List<DailyDto>();

            foreach (var daily in formFromDb)
            {

                var dailyDto = _mapper.Map<DailyDto>(daily.DailyBoxes.Daily);

                if (!dailtListDto.Any(x => x.Id == dailyDto.Id))
                    dailtListDto.Add(dailyDto);
            }
            responseData.Items = dailtListDto;
            responseData.TotalCount = responseData.Items.Count;

            responseData.TotalSumTax = formFromDb.Sum(x => x.SumTax);
            responseData.TotalTaxDevelopment = formFromDb.Sum(x => x.TaxDevelopment);

            return Ok(responseData);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDailyById(int id)
        {
            var daily = await _uow.DailyRepository.GetById(id);
            var dailyDto = _mapper.Map<DailyDto>(daily);

            return Ok(dailyDto);
        }
        [HttpPost]
        public async Task<IActionResult> PostDaily(DailyDto daily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dailyExits = await _uow.DailyRepository.GetAll(new DailyWithSpecification(new DailyParam() { DailyDate = daily.DailyDate }));
            if (dailyExits.Count > 0)
            {
                return BadRequest(new ApiResponse(2627));
            }
            var dailyToDb = _mapper.Map<Daily>(daily);



            _uow.DailyRepository.Add(dailyToDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest(new ApiResponse(2627));
            }

            return Ok();
        }

        [HttpPut("{dailyId}")]
        public async Task<IActionResult> PutDaily(int dailyId, DailyDto daily)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiValidationErrorResponse());
            }
            var dailyFromDb = await _uow.DailyRepository.GetById(dailyId);
            if (dailyFromDb == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var dailyExits = _uow.DailyRepository.GetAll(new DailyWithSpecification(new DailyParam() { DailyDate = daily.DailyDate }));
            if (dailyExits != null)
            {
                return BadRequest(new ApiResponse(2627));
            }
            dailyFromDb.DailyDate = daily.DailyDate;
            dailyFromDb.Name = daily.Name;

            _uow.DailyRepository.Update(dailyFromDb);

            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }
            return Ok();
        }


        [HttpDelete("{dailyId}")]
        public async Task<IActionResult> DeleteDaily(int dailyId)
        {
            var dailyFromDb = await _uow.DailyRepository.GetById(dailyId);
            if (dailyFromDb == null)
            {
                return NotFound();
            }
            _uow.DailyRepository.Remove(dailyFromDb);

            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest(new ApiResponse(500, "يوجد خطأ"));
            }

            return Ok();

        }
        [HttpGet("export")]
        public async Task<ActionResult> WriteFile([FromQuery] DailyParam param)
        {


            var totalDailies = await _uow.DailyRepository.GetAll(new DailyWithSpecification(param));


            var countSpec = new DailyCountAsyncWithSpecification(param);
            var dailiyCount = await _uow.DailyRepository.CountAsync(countSpec);


            var dailiesDto = _mapper.Map<IReadOnlyList<DailyDto>>(totalDailies);
            foreach (var daily in dailiesDto)
            {
                var dailyBox = await _uow.DailyRepository.GetDailyBoexByDailyId(daily.Id.Value);

                daily.TaxNormal = dailyBox.Sum(x => x.TaxNormal);
                daily.Stamp = dailyBox.Sum(x => x.Stamp);
                daily.Taxsettlement = dailyBox.Sum(x => x.Taxsettlement);
                daily.Tax2 = dailyBox.Sum(x => x.Tax2);
                daily.Other = dailyBox.Sum(x => x.Other);


                daily.Total = dailyBox.Sum(x => x.SumTax);
                daily.TotalDevelopment = dailyBox.Sum(x => x.TaxDevelopment);
            }
            var responseData = new responseData<DailyDto>(dailiesDto, dailiyCount);


            responseData.TotalTaxNormal = responseData.Items.Sum(x => x.TaxNormal);
            responseData.TotalStamp = responseData.Items.Sum(x => x.Stamp);
            responseData.TotalTaxsettlement = responseData.Items.Sum(x => x.Taxsettlement);
            responseData.TotalTax2 = responseData.Items.Sum(x => x.Tax2);
            responseData.TotalOther = responseData.Items.Sum(x => x.Other);


            responseData.TotalSumTax = responseData.Items.Sum(x => x.Total);

            responseData.TotalTaxDevelopment = responseData.Items.Sum(x => x.TotalDevelopment);






            ExportExcelFile<DailyDto> file = new ExportExcelFile<DailyDto>();



            var header = (new string[] { "id", "البيان", "اليوميه", "كسب عمل", "دمغه", "تسويه ضريبيه", "ضرائب باب ثانى", "اخرى", "اجمالى ضرائب", "اجمالى تنميه" });
            var content = (responseData.Items.ToList());
            var footer = (new string[] { "", "", "الاجمالى", responseData.TotalTaxNormal.ToString(), responseData.TotalStamp.ToString(), responseData.TotalTaxsettlement.ToString(), responseData.TotalTax2.ToString(), responseData.TotalOther.ToString(), responseData.TotalSumTax.ToString(), responseData.TotalTaxDevelopment.ToString() });

            var title = "يوميات";
            if (param.InMonth.HasValue)
            {
                title += " " + param.InMonth.Value.ToString("MMMM");
            }
            file.CreateWorkSheet(content, title, header, footer);

            string filePath = file.SaveAs(); ;
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;



            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "test.xlsx");

        }





    }
}
