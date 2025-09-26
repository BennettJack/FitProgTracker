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
}