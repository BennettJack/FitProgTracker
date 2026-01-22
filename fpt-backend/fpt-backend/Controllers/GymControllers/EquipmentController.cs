using fpt_backend.Data.Models.GymModels;
using fpt_backend.Services.GymServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace fpt_backend.Controllers.GymControllers;

[Route("api/[controller]")]
public class EquipmentController : Controller
{
    private readonly IEquipmentService _equipmentService;
    
    public EquipmentController(IEquipmentService equipmentService)
    {
        _equipmentService = equipmentService;
    }

    //[Authorize]
    [HttpGet("Equipment")]
    public async Task<ActionResult<Equipment>> GetEquipmentById(int id)
    {
        if (id == 0)
        {
            return BadRequest("Equipment id cannot be 0");
        }
        
        return Ok(
            await _equipmentService.GetByIdAsync(id));
        
    }
    
    
    [HttpGet("getOptionData")]
    public async Task<IActionResult> GetOptionData()
    {
        var res = await _equipmentService.GetListAsDropdownAsync();

        return Ok(res);
    }
}