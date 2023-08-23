using Microsoft.AspNetCore.Mvc;
using TimezonesAPI.Business;
using TimezonesAPI.Models;
using System.Web.Http.Cors;

namespace TimezonesAPI.Controllers
{
    [EnableCors(origins: "https://localhost:7101", headers: "*", methods: "*")]
    [Route("[controller]")]
    [ApiController]
    public class TimezoneController : ControllerBase
    {
        private readonly ITimezoneService timezoneService;

        public TimezoneController(ITimezoneService timezoneService)
        {
            this.timezoneService = timezoneService;
        }

        // GET: api/Timezone
        [HttpGet]
        public async Task<ActionResult<TimezoneResponseModelPagination>> Get_timezones(int page = 1, int size = 9)
        {
            TimezoneResponseModelPagination timezones = await timezoneService.GetAll(page, size);
            if (timezones == null)
            {
                return NotFound();
            }

            return timezones;
        }

        // GET: api/Timezone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TimezoneResponseModel>> GetTimezoneResponseModel(string id)
        {
            
            TimezoneResponseModel timezone = await timezoneService.GetById(id);
            
            if (timezone == null)
            {
                return NotFound();
            }

            return timezone;
        }

        [HttpGet("Name")]
        public async Task<ActionResult<TimezoneResponseModelPagination>> GetTimezoneResponseModelByName(string? name, int page = 1, int size = 9)
        {
            TimezoneResponseModelPagination timezones = await timezoneService.GetByName(name, page, size);

            if (timezones == null)
            {
                return NotFound();
            }

            return timezones;
        }

        // PUT: api/Timezone/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutTimezoneResponseModel(TimezoneResponseModel timezoneResponseModel)
        {
            timezoneService.SaveAsync(timezoneResponseModel);
            
            return NoContent();
        }

        // POST: api/Timezone
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TimezoneResponseModel>> PostTimezoneResponseModel(TimezoneResponseModel timezoneResponseModel)
        {
            timezoneService.SaveAsync(timezoneResponseModel);

            return NoContent() ;
        }

        // DELETE: api/Timezone/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTimezone(string id)
        {
            timezoneService.Delete(id);

            return NoContent();
        }
    }
}
