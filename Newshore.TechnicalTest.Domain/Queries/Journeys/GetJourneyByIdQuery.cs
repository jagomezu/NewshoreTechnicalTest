using MediatR;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Queries.Journeys
{
    public class GetJourneyByIdQuery : IRequest<JourneyResponse?>
    {
        public int Id { get; set; }
    }

    public class GetJourneyByIdQueryHandler : IRequestHandler<GetJourneyByIdQuery, JourneyResponse?>
    {
        #region Properties
        private readonly IJourneyQueriesRepository _repository;
        #endregion

        #region Constructor
        public GetJourneyByIdQueryHandler(IJourneyQueriesRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region Public methods
        public async Task<JourneyResponse?> Handle(GetJourneyByIdQuery request, CancellationToken cancellationToken)
        {
            Log.Information("[DOMAIN Get journey by id -- Start --> Id: {@Id}]", request.Id);
            JourneyResponse? result = null;

            try
            {
                Journey? journeyInfo = await _repository.GetById(request.Id);

                if (journeyInfo != null)
                {
                    result = new()
                    {
                        Destination= journeyInfo.Destination,
                        Id= journeyInfo.Id,
                        Origin= journeyInfo.Origin,
                        Price = journeyInfo.Price
                    };

                    Log.Information("[DOMAIN Get journey by id -- Success --> Id: {@Id} -- Journey found: {@JourneyInfo}", request.Id, result);
                }
                else
                {
                    Log.Warning($"[DOMAIN Get journey by id -- Success --> Id: {request.Id} -- Journey not found");
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get journey by id -- Error --> Id: {@Id}", ex, request.Id);

                throw;
            }

            return result;
        }
        #endregion
    }
}
