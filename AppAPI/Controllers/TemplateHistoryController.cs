using BusinessLogicLayer.DTO;
using BusinessLogicLayer.IServices;
using Microsoft.AspNetCore.Mvc;

namespace AppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemplateHistoryController : ControllerBase
    {
        private readonly ITemplateHistoryService _templateHistoryService;

        public TemplateHistoryController(ITemplateHistoryService templateHistoryService)
        {
            _templateHistoryService = templateHistoryService;

        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_templateHistoryService.GetAll());
        }
        
        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var templateHistoryDTO = _templateHistoryService.GetById(id);

            if (templateHistoryDTO == null)
            {
                return NotFound();
            }
            return Ok(templateHistoryDTO);
        }

        [HttpGet("historic/{templateId:Guid}")]
        public IActionResult GetHistoricByTemplateId([FromRoute] Guid templateId)
        {
            var templateHistoryDTO = _templateHistoryService.GetHistoricByTemplateId(templateId);

            if (templateHistoryDTO == null)
            {
                return NotFound();
            }
            return Ok(templateHistoryDTO);
        }

        /* [HttpPost]
         public IActionResult Add([FromBody] CreateTemplateHistoryDTO templateHistoryDTO)
         {
             return Ok(_templateHistoryService.Add(templateHistoryDTO));

         }


         [HttpDelete]
         [Route("{id:Guid}")]
         public IActionResult Delete([FromRoute] Guid id)
         {
             var templateHistoryDTO = _templateHistoryService.GetById(id);

             if (templateHistoryDTO == null)
             {
                 return BadRequest("TemplateHistory Not Found !!!");
             }
             _templateHistoryService.Delete(id);

             return Ok(templateHistoryDTO);
         }

         [HttpPut]
         [Route("{id:Guid}")]
         public IActionResult Update([FromRoute] Guid id, TemplateHistoryDTO templateHistoryDTO)
         {
             var templateHistory = _templateHistoryService.GetById(id);

             if (templateHistory == null)
             {
                 return NotFound();
             }

             return Ok(_templateHistoryService.Update(id, templateHistoryDTO));
         }
         [HttpGet]
         [Route("{name}")]
         public IActionResult GetByName([FromRoute] string name)
         {
             if (name == null)
             {
                 return BadRequest();
             }
             var templateHistory = _templateHistoryService.GetByName(name);

             if (templateHistory == null)
             {
                 return NotFound();
             }
             return Ok(templateHistory);
         }
        */
    }
}
