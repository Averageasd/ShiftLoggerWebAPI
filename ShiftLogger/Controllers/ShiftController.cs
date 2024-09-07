using Microsoft.AspNetCore.Mvc;
using ShiftLogger.DTO;
using ShiftLogger.Helper;
using ShiftLogger.Models;
using ShiftLogger.Services;

namespace ShiftLogger.Controllers
{
    [Route("api/[controller]")]
    public class ShiftController : Controller
    {
        private IConfiguration _configuration;
        private ShiftService _shiftService;

        public ShiftController(IConfiguration configuration, ShiftService shiftService) {
           _configuration = configuration;
            _shiftService = shiftService;
        }
        [HttpGet]
        public async Task<ActionResult> GetShifts()
        {
            try
            {
                List<Shift> allShifts = await _shiftService.GetShifts();
                List<ShiftDTO> shiftDtos = allShifts.Select(shift => ToShiftDTO(shift)).ToList();
                return Ok(shiftDtos);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetSingleShift(Guid? id)
        {
            if (id == null)
            {
                return BadRequest("Id cannot be null");
            }

            try
            {
                var shift = await _shiftService.GetSingleShift(id.Value);
                if (shift == null)
                {
                    return BadRequest($"Shift with {id} does not exist");
                }
                return Ok(ToShiftDTO(shift));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult> AddShift([FromBody] Shift shift)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelStateErrorMessageGenerator.modelStateErrorMessage(ModelState));
            }

            try
            {
                var newShift = await _shiftService.AddShift(shift);
                return CreatedAtAction(nameof(GetSingleShift), new { id = shift.ShiftId }, ToShiftDTO(shift));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("delete/{shiftId}")]
        public async Task<ActionResult> DeleteShift(Guid? shiftId)
        {
            if (shiftId == null)
            {
                return BadRequest("shift id cannot be null");
            }

            try
            {
                var shift = await _shiftService.GetSingleShift(shiftId.Value);
                if (shift == null)
                {
                    return BadRequest($"shift with id {shiftId} does not exist");
                }
                await _shiftService.DeleteShift(shift.ShiftId!.Value);
                return NoContent();

            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }

        public static ShiftDTO ToShiftDTO(Shift shift)
        {
            return new ShiftDTO()
            {
                ShiftId = shift.ShiftId!.Value,
                StartTime = shift.StartTime,
                EndTime = shift.EndTime,
                ShiftDuration = DurationDisplay.DurationString(shift.StartTime, shift.EndTime),
            };
        }
    }
}
