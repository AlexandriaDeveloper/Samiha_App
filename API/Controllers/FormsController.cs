using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Models;
using Core.Specifications;
using InfraStructure.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class FormsController : BaseController
    {
        private readonly IUOW _uow;
        private readonly IMapper _mapper;

        public FormsController(ILogger<FormsController> logger, IUOW uow, IMapper mapper) : base(logger)
        {
            this._mapper = mapper;
            this._uow = uow;
        }

        [HttpGet()]
        public async Task<IActionResult> GetForms([FromQuery] FormParam param)
        {


            var spec = new FormWithSpecification(param);
            var formsFromDb = await _uow.FormRepository.GetAll(spec);
            if (formsFromDb != null)
            {
                var formsDto = _mapper.Map<IReadOnlyList<FormDto>>(formsFromDb);



                var result = new FormListDto(formsDto);

                result.Items = formsDto;
                var dailyBoxSpec = new DailyBoxWithSpecification(new DailyBoxParam() { Id = param.DailyBoxId });
                dailyBoxSpec.Includes.Add(x => x.Daily);
                dailyBoxSpec.Includes.Add(x => x.Box.Collage);

                var box = await _uow.DailyBoxRepository.GetById(dailyBoxSpec);

                result.BoxName = box.Box.Name;
                result.Collage = box.Box.Collage.Name;
                result.Title = box.Daily.DailyDate.ToString();

                return Ok(result);
            }
            return NoContent();




        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetForm(int id)
        {

            var formsFromDb = await _uow.FormRepository.GetById(id);
            if (formsFromDb != null)
            {
                var formsDto = _mapper.Map<FormDto>(formsFromDb);
                return Ok(formsDto);
            }
            return NoContent();




        }



        [HttpPost()]
        public async Task<IActionResult> putForm(FormDto form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var formToDb = _mapper.Map<Form>(form);

            _uow.FormRepository.Add(formToDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }
            return Ok();

        }


        [HttpPut("{formId}")]
        public async Task<IActionResult> putForm(int formId, FormDto form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var formFromDb = await _uow.FormRepository.GetById(formId);
            if (formFromDb == null)
            {
                return NotFound();
            }
            formFromDb.DailyBoxId = form.DailyBoxId;
            formFromDb.Name = form.Name;
            formFromDb.Num224 = form.Num224;
            formFromDb.Stamp = form.Stamp;
            formFromDb.Tax2 = form.Tax2;
            formFromDb.TaxDevelopment = form.TaxDevelopment;
            formFromDb.Taxsettlement = form.Taxsettlement;
            formFromDb.Other = form.Other;
            formFromDb.TaxNormal = form.TaxNormal;


            _uow.FormRepository.Update(formFromDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }

            return Ok();

        }

        [HttpDelete("{formId}")]
        public async Task<IActionResult> putForm(int formId)
        {
            var formFromDb = await _uow.FormRepository.GetById(formId);
            if (formFromDb == null)
            {
                return NotFound();
            }
            _uow.FormRepository.Remove(formFromDb);
            if (!await _uow.SaveChangesAsync())
            {
                return BadRequest("يوجد خطأ");
            }

            return Ok();


        }


        [HttpGet("export")]
        public async Task<ActionResult> WriteFile([FromQuery] FormParam param)
        {


            var totalform = await _uow.FormRepository.GetAll(new FormWithSpecification(param));


            var dailiesDto = _mapper.Map<IReadOnlyList<FormDto>>(totalform);

            var responseData = new responseData<FormDto>(dailiesDto);

            responseData.TotalSumTax = responseData.Items.Sum(x => x.SumTax);

            responseData.TotalTaxDevelopment = responseData.Items.Sum(x => x.TaxDevelopment);

            responseData.TotalTaxNormal = responseData.Items.Sum(x => x.TaxNormal);
            responseData.TotalStamp = responseData.Items.Sum(x => x.Stamp);
            responseData.TotalTaxsettlement = responseData.Items.Sum(x => x.Taxsettlement);
            responseData.TotalTax2 = responseData.Items.Sum(x => x.Tax2);
            responseData.TotalOther = responseData.Items.Sum(x => x.Other);




            ExportExcelFile<FormDto> file = new ExportExcelFile<FormDto>();



            var header = (new string[] { "id", "البيان", "رقم 224", "كود الصندوق", "كسب عمل", "دمغه عاديه", "تسويه ضريبيه", "ضرائب باب ثانى", "اخرى", "اجمالى ضرائب", "رسم تنميه" });
            var tableContent = (responseData.Items.ToList());
            var footer = (new string[] { "", "", " ", "الاجمالى  ", responseData.TotalTaxNormal.ToString(), responseData.TotalStamp.ToString(), responseData.TotalTaxsettlement.ToString(), responseData.TotalTax2.ToString(), responseData.TotalOther.ToString(), responseData.TotalSumTax.ToString(), responseData.TotalTaxDevelopment.ToString() });


            var dailyBoxSpec = new DailyBoxWithSpecification(new DailyBoxParam() { Id = param.DailyBoxId });
            dailyBoxSpec.Includes.Add(x => x.Daily);
            dailyBoxSpec.Includes.Add(x => x.Box);
            dailyBoxSpec.Includes.Add(x => x.Box.Collage);

            var dailyBox = await _uow.DailyBoxRepository.GetById(dailyBoxSpec);
            var title = dailyBox.Box.Name + dailyBox.Daily.DailyDate.ToString("dd-MM-yy");
            file.CreateWorkSheet(tableContent, title, header, footer);

            string filePath = file.SaveAs(); ;
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;



            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", dailyBox.Daily.DailyDate.ToString("dd-MMMM-yyyy"));

        }

    }
}