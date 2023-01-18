using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newshore.TechnicalTest.Domain.Interfaces;
using Newshore.TechnicalTest.Transverse.Dto;

namespace Newshore.ThecnicalTest.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JourneysController : Controller
    {
        #region Properties
        private readonly IMediator _mediator;
        private readonly IJourneyManagerDomain _journeyManager;
        #endregion

        #region Constructor
        public JourneysController(IMediator mediator, IJourneyManagerDomain journeyManager)
        {
            _mediator = mediator;
            _journeyManager = journeyManager;
        }
        #endregion

        #region Public methods
        [HttpPost(Name = "GetJourneys")]
        public async Task<IActionResult> GetPermissions(GetJourneysDto request)
        {
            try
            {
                List<JourneyDto> result = _journeyManager.GetJourneysByOriginAndDestination(request.Origin, request.Destination);

                if (result == null || !result.Any())
                {
                    throw new Exception("Solicitud no puede ser procesada. No fue posible calcular la ruta");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                ResponseDto<bool> response = new()
                {
                    IsSuccess = false,
                    ErrorMessage = ex.Message
                };

                return BadRequest(response);
            }
        }
        #endregion
    }
}
