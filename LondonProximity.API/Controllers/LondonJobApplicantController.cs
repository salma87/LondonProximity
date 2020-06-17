using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LondonProximity.API
{
    [ApiController]
    [Route("[controller]")]
    public class LondonJobApplicantController : Controller
    {
        ProximityCalculator proxi = new ProximityCalculator();
        private readonly IProximityCalculator _proximityCalculator;

        public LondonJobApplicantController(IProximityCalculator proximityCalculator) {
            _proximityCalculator = proximityCalculator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var applicants = await _proximityCalculator.AllApplicantsAsync();
                return Ok(applicants);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Unable to pull applicants from data store");
            }
        
        }
    }
}
