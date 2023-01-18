using Newshore.TechnicalTest.Transverse.Dto;

namespace Newshore.TechnicalTest.Domain.Interfaces
{
    public interface IJourneyManagerDomain
    {
        public List<JourneyDto>? GetJourneysByOriginAndDestination(string origin, string destination);
    }
}
