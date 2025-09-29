using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class EquipmentController : Controller
{
    private readonly EquipmentService _equipmentService;
    
    public EquipmentController(EquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    [Authorize]
    [HttpGet("Equipment")]
    public async Task<ActionResult<Equipment>> GetEquipmentById(int id)
    {
        if (id == 0)
        {
            return BadRequest("Equipment id cannot be 0");
        }
        
        return Ok(
            await _equipmentService.GetEquipment(id));
        
    }
    
    
    [HttpGet("getOptionData")]
    public async Task<IActionResult> GetOptionData()
    {
        var res = await _equipmentService.GetEquipmentListAsDropdown();

        return res.Status switch
        {
            ResultStatus.Success => Ok(res),
            ResultStatus.BadRequest => BadRequest(new { error = res.Message }),
            ResultStatus.NotFound => NotFound(new { error = res.Message }),
            ResultStatus.Error => StatusCode(500, new { error = res.Message }),
            _ => StatusCode(500, new { error = "Internal server error" })
        };
    }
}