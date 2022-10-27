using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using AutoMapper;
using Core.Models;
using Core.Specifications;
using InfraStructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class DailyBoxController : BaseController
    {

        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public DailyBoxController(ILogger<DailyBoxController> logger, IUOW uow, IMapper mapper) : base(logger)
        {
            this._mapper = mapper;
            this._uow = uow;

        }
        [HttpGet()]
        public async Task<IActionResult> GetDailyBox([FromQuery] DailyBoxParam param)
        {

            param.IsPagination = false;

            var spec = new DailyBoxWithSpecification(param);
            spec.Includes.Add(x => x.Forms);
            spec.Includes.Add(x => x.Box);
            var dailyBoxes = await _uow.DailyBoxRepository.GetAll(spec);
            var dailyBoxesDto = _mapper.Map<IReadOnlyList<DailyBoxDto>>(dailyBoxes);


            var count = await _uow.DailyBoxRepository.CountAsync(new DailyBoxCountAsyncWithSpecification(param));

            var responseData = new responseData<DailyBoxDto>(dailyBoxesDto, count);

            responseData.TotalSumTax = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.SumTax);
            responseData.TotalStamp = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.Stamp);
            responseData.TotalOther = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.Other);
            responseData.TotalTaxNormal = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.TaxNormal);
            responseData.TotalTax2 = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.Tax2);
            responseData.TotalTaxsettlement = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.Taxsettlement);

            responseData.TotalTaxDevelopment = dailyBoxes.SelectMany(t => t.Forms).Sum(t => t.TaxDevelopment);


            return Ok(responseData);
        }
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetDailyById(int id)
        // {
        //     var dailyBoxes = await _uow.DailyBoxRepository.GetById(id);
        //     var dailyBoxesDto = _mapper.Map<DailyBoxDto>(dailyBoxes);

        //     return Ok(dailyBoxesDto);
        // }

        [HttpPost]
        public async Task<IActionResult> PostDailyBox(DailyBoxDto dailyBox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dailyToDb = _mapper.Map<DailyBoxes>(dailyBox);

            _uow.DailyBoxRepository.Add(dailyToDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }
            return Ok();
        }

        [HttpPut("{dailyBoxId}")]
        public async Task<IActionResult> PutDaily(int dailyBoxId, DailyBoxDto dailyBox)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var dailyBoxFromDb = await _uow.DailyBoxRepository.GetById(dailyBoxId);
            if (dailyBoxFromDb == null)
            {
                return NotFound();
            }
            dailyBoxFromDb.BoxId = dailyBox.BoxId;
            dailyBoxFromDb.DailyId = dailyBox.DailyId;
            dailyBoxFromDb.Name = dailyBox.Name;

            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }

            return Ok();
        }


        [HttpDelete("{dailyBoxId}")]
        public async Task<IActionResult> DeleteDaily(int dailyBoxId)
        {
            var dailyBoxFromDb = await _uow.DailyBoxRepository.GetById(dailyBoxId);
            if (dailyBoxFromDb == null)
            {
                return NotFound();
            }
            _uow.DailyBoxRepository.Remove(dailyBoxFromDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }
            return Ok();

        }

        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] DailyBoxParam param)
        {

            param.IsPagination = false;

            var spec = new DailyBoxWithSpecification(param);
            spec.Includes.Add(x => x.Daily);
            spec.Includes.Add(x => x.Forms);
            spec.Includes.Add(x => x.Box);
            spec.Includes.Add(x => x.Box.Collage);
            var dailyBoxes = await _uow.DailyBoxRepository.GetAll(spec);
            if (dailyBoxes.Count == 0)
            {
                return BadRequest(new ApiResponse(404));
            }




            ExportExcelFile<FormReportDto> file = new ExportExcelFile<FormReportDto>();
            var header = (new string[] { "رقم 224", "كسب العمل", "دمغه عاديه", "تسويه ضريبيه", "ضرائب باب ثانى ", "أخرى ", "أجمالى الضرائب", "تنميه", " الكليه", " الصندوق" });
            foreach (var dailyBoxData in dailyBoxes)
            {
                var dailyBox = new DailyBoxReportDto();
                if (dailyBox.data == null)
                {
                    dailyBox.data = new List<FormReportDto>();
                }
                foreach (var form in dailyBoxData.Forms)
                {
                    dailyBox.data.Add(new FormReportDto()
                    {


                        Other = form.Other,
                        Stamp = form.Stamp,
                        SumTax = form.SumTax,
                        Tax2 = form.Tax2,
                        TaxDevelopment = form.TaxDevelopment,
                        TaxNormal = form.TaxNormal,
                        Taxsettlement = form.Taxsettlement,

                        Box = dailyBoxData.Box.Name,
                        Collage = dailyBoxData.Box.Collage.Name,
                        Num224 = form.Num224,
                    });
                }


                var footer = (new string[] { "الاجمالى", dailyBox.data.Sum(x => x.TaxNormal).ToString(), dailyBox.data.Sum(x => x.Stamp).ToString(), dailyBox.data.Sum(x => x.Taxsettlement).ToString(), dailyBox.data.Sum(x => x.Tax2).ToString(), dailyBox.data.Sum(x => x.Other).ToString(), dailyBox.data.Sum(x => x.SumTax).ToString(), dailyBox.data.Sum(x => x.TaxDevelopment).ToString(), " ", " " });
                file.CreateWorkSheet(dailyBox.data.ToList(), dailyBoxData.Box.Name + "-" + dailyBoxData.Box.Collage.Name, header, footer);

            }


            //ExportExcelFile<FormReportDto> file = new ExportExcelFile<FormReportDto>();

            //  var total = dailyBox.data;

            // file.CreateHeader(new string[] { "رقم 224", "كسب العمل", "دمغه عاديه", "تسويه ضريبيه", "ضرائب باب ثانى ", "أخرى ", "أجمالى الضرائب", "تنميه", " الكليه", " الصندوق" });
            // file.CreateTableContent(dailyBox.data.ToList());

            // file.CreateFooter(new string[] { "الاجمالى", total.Sum(x => x.TaxNormal).ToString(), total.Sum(x => x.Stamp).ToString(), total.Sum(x => x.Taxsettlement).ToString(), total.Sum(x => x.Tax2).ToString(), total.Sum(x => x.Other).ToString(), total.Sum(x => x.SumTax).ToString(), total.Sum(x => x.TaxDevelopment).ToString(), " ", " " });

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