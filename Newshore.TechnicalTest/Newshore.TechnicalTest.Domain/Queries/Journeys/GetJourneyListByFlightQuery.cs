using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Journeys
{
    public class GetJourneyListByJourneyQuery : IRequest<List<JourneyResponse>?>
    {
        public int FlightId { get; set; }
    }

    public class GetJourneyListByJourneyQueryHandler : IRequestHandler<GetJourneyListByJourneyQuery, List<JourneyResponse>?>
    {
        #region Properties
        private readonly IJourneyQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetJourneyListByJourneyQueryHandler(IJourneyQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<JourneyResponse>?> Handle(GetJourneyListByJourneyQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"[DOMAIN Get journeys by flight] -- Start --> flight id: {request.FlightId}");
            List<JourneyResponse>? result = null;

            try
            {
                List<Journey>? journeyList = await _repository.GetListByFlightId(request.FlightId);

                if (journeyList != null)
                {
                    result = new();

                    journeyList.ForEach(journeyInfo =>
                    {
                        result.Add(new JourneyResponse()
                        {
                            Destination = journeyInfo.Destination,
                            Id = journeyInfo.Id,
                            Origin = journeyInfo.Origin,
                            Price = journeyInfo.Price
                        });
                    });

                    Log.Information($"[DOMAIN Get journeys by flight] -- Success --> flight id: {request.FlightId} -- Journeys founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get journeys by flight] -- Success --> flight id: {request.FlightId} -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get journeys by flight] -- Error --> flight id: {Flight}", ex, request.FlightId);

                throw;
            }

            return result;
        }
        #endregion
    }
}
