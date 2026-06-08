
using CertiForge.Application.DTOs;
using CertiForge.Application.Interfaces.QuestionsChoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace LSC.SmartCertify.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [RequiredScope(RequiredScopesConfigurationKey = "AzureAdB2C:Scopes:Read")]
    [Authorize]//we are adding this attribute to make sure that only authenticated users with the required scope can access the endpoints in this controller. This is a security measure to protect the API and ensure that only authorized users can perform actions related to choices.
    public class ChoicesController : ControllerBase
    {
        private readonly IChoiceService _service;

        public ChoicesController(IChoiceService service)
        {
            _service = service;
        }

        [HttpGet("{questionId}")]
        public async Task<ActionResult<IEnumerable<ChoiceDto>>> GetChoices(int questionId)
        {
            return Ok(await _service.GetAllChoicesAsync(questionId));
        }

        [HttpGet("{questionId}/{id}")]
        public async Task<ActionResult<ChoiceDto>> GetChoice(int questionId, int id)
        {
            var choice = await _service.GetChoiceByIdAsync(id);
            return choice == null ? NotFound() : Ok(choice);
        }   

        [HttpPost]
        [RequiredScopeOrAppPermission(
         RequiredScopesConfigurationKey = "AzureAD:Scopes:Write",
         RequiredAppPermissionsConfigurationKey = "AzureAD:AppPermissions:Write"
        )]
        [Authorize]
        public async Task<IActionResult> CreateChoice([FromBody] CreateChoiceDto dto)
        {
            await _service.AddChoiceAsync(dto);
            return Created(); //CreatedAtAction(nameof(GetChoices), new { questionId = dto.QuestionId });
        }

        [HttpPut("{id}")]
        [RequiredScopeOrAppPermission(
         RequiredScopesConfigurationKey = "AzureAD:Scopes:Write",
         RequiredAppPermissionsConfigurationKey = "AzureAD:AppPermissions:Write"
        )]
        [Authorize]
        public async Task<IActionResult> UpdateChoice(int id, [FromBody] UpdateChoiceDto dto)
        {
            await _service.UpdateChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [RequiredScopeOrAppPermission(
         RequiredScopesConfigurationKey = "AzureAD:Scopes:Write",
         RequiredAppPermissionsConfigurationKey = "AzureAD:AppPermissions:Write"
        )]
        [Authorize]
        public async Task<IActionResult> UpdateUserChoice(int id, [FromBody] UpdateUserChoice dto)
        {
            await _service.UpdateUserChoiceAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [RequiredScopeOrAppPermission(
         RequiredScopesConfigurationKey = "AzureAD:Scopes:Write",
         RequiredAppPermissionsConfigurationKey = "AzureAD:AppPermissions:Write"
        )]
        [Authorize]
        public async Task<IActionResult> DeleteChoice(int id)
        {
            await _service.DeleteChoiceAsync(id);
            return NoContent();
        }
    }

}