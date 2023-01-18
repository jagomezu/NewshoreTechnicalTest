using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Journeys
{
    public class GetAllJourneysQuery : IRequest<List<JourneyResponse>?>
    {
    }

    public class GetAllJourneysQueryHandler : IRequestHandler<GetAllJourneysQuery, List<JourneyResponse>?>
    {
        #region Properties
        private readonly IJourneyQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetAllJourneysQueryHandler(IJourneyQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<List<JourneyResponse>?> Handle(GetAllJourneysQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get all journeys] -- Start");
            List<JourneyResponse>? result = null;

            try
            {
                List<Journey>? journeyList = await _repository.GetAll();

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

                    Log.Information("[DOMAIN Get all journeys] -- Success -- Journey founds");
                }
                else
                {
                    Log.Warning($"[DOMAIN Get all journeys] -- Success -- Journeys not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get all journey] -- Error", ex);

                throw;
            }

            return result;
        }
        #endregion
    }
}
