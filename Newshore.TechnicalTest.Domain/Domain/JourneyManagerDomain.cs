using MediatR;
using Microsoft.Extensions.Configuration;
using Newshore.TechnicalTest.Domain.Commands.Flights;
using Newshore.TechnicalTest.Domain.Commands.JourneyFlights;
using Newshore.TechnicalTest.Domain.Commands.Journeys;
using Newshore.TechnicalTest.Domain.Commands.Transports;
using Newshore.TechnicalTest.Domain.Interfaces;
using Newshore.TechnicalTest.Domain.Queries.Flights;
using Newshore.TechnicalTest.Domain.Queries.Journeys;
using Newshore.TechnicalTest.Domain.Queries.Transports;
using Newshore.TechnicalTest.Domain.ResponseModels;
using Newshore.TechnicalTest.Transverse.Dto;
using Newshore.TechnicalTest.Transverse.Utils;
using Newtonsoft.Json;
using Serilog;

namespace Newshore.TechnicalTest.Domain.Domain
{
    public class JourneyManagerDomain : IJourneyManagerDomain
    {
        #region Properties
        private readonly IConfiguration? _configuration;
        private readonly IMediator _mediator;
        private readonly string _apiJourneysUrl = string.Empty;
        private readonly int _maxTotalFlights = 0;
        private List<JourneyApiResponseDto>? _apiJourneys;
        #endregion

        #region Constructor
        public JourneyManagerDomain(IMediator mediator, IConfiguration configuration)
        {
            _configuration = configuration;
            _mediator = mediator;
            _apiJourneysUrl = _configuration?["AppSettings:JourneyInfoApi"]?.ToString() ?? string.Empty;
            _maxTotalFlights = Convert.ToInt32(_configuration?["AppSettings:MaxTotalFlights"] ?? "4");
        }
        #endregion

        #region Public methods
        public List<JourneyDto>? GetJourneysByOriginAndDestination(string origin, string destination)
        {
            Log.Information($"[DOMAIN Get Journey By Origin And Destination] -- Start --> Origin: {@origin}, Destination: {@destination}");
            List<JourneyDto>? result;
            
            try
            {
                result = GetExistsJourney(origin, destination);

                if (result == null)
                {
                    throw new Exception("Solicitud no puede ser procesada. No fue posible calcular la ruta");
                }

                Log.Information($"[DOMAIN Get Journey By Origin And Destination] -- Start --> Origin: {@origin}, Destination: {@destination}");
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Journey By Origin And Destination] -- Error --> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
                throw;
            }

            return result;
        }
        #endregion

        #region Private methods
        private List<JourneyDto>? GetExistsJourney(string origin, string destination)
        {
            Log.Information($"[DOMAIN Get Exists Journey] -- Start --> Origin: {@origin}, Destination: {@destination}");
            List<JourneyDto>? result = null;

            try
            {
                List<JourneyResponse>? journeyList = _mediator.Send(new GetJourneyListByPlacesQuery() { Origin = origin, Destination = destination}).Result;

                if (journeyList != null && journeyList.Any())
                {
                    result = new();

                    journeyList.ForEach(journey =>
                    {
                        List<FlightResponse>? flightList = _mediator.Send(new GetFlightListByJourneyQuery() { JourneyId = journey.Id }).Result;

                        if (flightList != null && flightList.Any())
                        {
                            journey.Flights = new();
                            flightList.ForEach(flight => 
                            {
                                flight.Transport = _mediator.Send(new GetTransportByIdQuery() { Id = flight.TransportId }).Result;
                                journey.Flights.Add(flight);
                            });
                        }

                        result.Add(GetJourneyDto(journey));
                    });
                }
                else
                {
                    result = GetApiJourney(origin, destination);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Exists Journey] -- Error --> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
                throw;
            }

            return result;
        }

        private JourneyDto GetJourneyDto(JourneyResponse journeyInfo)
        {
            Log.Information("[DOMAIN Get Journey Dto] -- Start --> Journey info: {@journeyInfo}", journeyInfo);
            JourneyDto result = new();

            try
            {
                result = new()
                {
                    Destination = journeyInfo.Destination,
                    Id = journeyInfo.Id,
                    Origin = journeyInfo.Origin,
                    Price = journeyInfo.Price,
                    IsDirectFlight = journeyInfo.IsDirectFlight,
                    IsRoundTripFlight = journeyInfo.IsRoundTripFlight,
                    Flights = new()
                };

                if (journeyInfo.Flights != null && journeyInfo.Flights.Any())
                {
                    journeyInfo.Flights.ForEach(flight =>
                    {
                        result.Flights.Add(new()
                        {
                            Id = flight.Id,
                            Destination = flight.Destination,
                            Origin = flight.Origin,
                            Price = flight.Price,
                            Transport = flight.Transport != null ? new() { Id = flight.TransportId, FlightCarrier = flight.Transport.FlightCarrier, FlightNumber = flight.Transport.FlightNumber } : new()
                        });
                    });
                }
                else
                {
                    Log.Warning("[DOMAIN Get Journey Dto] -- Start --> Journey info: {@journeyInfo} -- Journey not contains flights", journeyInfo );
                }

                Log.Information("[DOMAIN Get Journey Dto] -- Success --> Journey info: {@journeyInfo}", journeyInfo);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Journey Dto] -- Error --> Journey info: {@journeyInfo}", ex, journeyInfo);
                throw;
            }

            return result;
        }

        private JourneyDto GetJourneyDto(List<JourneyApiResponseDto> flights, string origin, string destination)
        {
            int flightsCount = flights?.Count ?? 0;

            Log.Information($"[DOMAIN Get Journey Dto] -- Start --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount}");

            try
            {
                double price = 0;
                JourneyDto journey = new()
                {
                    Origin = origin,
                    Destination = destination,
                    IsDirectFlight = flights != null && flights.Any() ? flights.Count == 1 || (flights.Count == 2 && flights.First().Origin == flights.Last().Destination) : false,
                    IsRoundTripFlight = flights != null && flights.Any() ? flights.First().Origin == flights.Last().Destination : false,
                    Flights = new()
                };

                if (flights != null && flights.Any())
                {
                    flights.ForEach(flight =>
                    {
                        journey.Flights.Add(new FlightDto()
                        {
                            Origin = flight.Origin,
                            Destination = flight.Destination,
                            Price = flight.Price,
                            Transport = new() { FlightCarrier = flight.FlightCarrier, FlightNumber = flight.FlightNumber }
                        });

                        price += flight.Price;
                    });
                }
                else
                {
                    Log.Warning($"[DOMAIN Get Journey Dto] -- Success --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount} -- Journey not contains flights");
                }

                journey.Price = price;

                Log.Information($"[DOMAIN Get Journey Dto] -- Success --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount}");

                return journey;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Journey Dto] -- Error --> Origin: {origin}, Destination: {destination}, flights count: {flightsCount}", ex, origin, destination, flightsCount);
                throw;
            }
        }

        private List<JourneyDto>? GetApiJourney(string origin, string destination)
        {
            Log.Information($"[DOMAIN Get Api Journey] -- Start --> Origin: {origin}, Destination: {destination}");
            List<JourneyDto>? result = null;

            try
            {
                if (_apiJourneys != null && _apiJourneys.Any())
                {
                    result = CalculateJourney(origin, destination);

                    if (result != null && result.Any())
                    {
                        result.ForEach(journey => SaveJourneyInfoFromApi(ref journey));
                    }
                }
                else
                {
                    _apiJourneys = JsonConvert.DeserializeObject<List<JourneyApiResponseDto>>(GetJourneyInfoFromApi().Result);

                    result = GetApiJourney(origin, destination);
                }

                Log.Information($"[DOMAIN Get Api Journey] -- Success --> Origin: {origin}, Destination: {destination}");

                return result;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Api Journey] -- Error --> Origin: {@origin}, Destination: {@destination}", ex, origin, destination);
            }

            return result;
        }

        private void SaveJourneyInfoFromApi(ref JourneyDto journey) 
        {
            Log.Information("[DOMAIN Save journey info from api] -- Start --> Journey: {@journey}", journey);

            try
            {
                JourneyResponse journeyInfo = _mediator.Send(new CreateJourneyCommand() { Destination = journey.Destination, Origin = journey.Origin, Price = journey.Price, IsDirectFlight = journey.IsDirectFlight, IsRoundTripFlight = journey.IsRoundTripFlight }).Result;

                if (journeyInfo != null)
                {
                    journey.Id = journeyInfo.Id;
                    journeyInfo.Flights = new();

                    journey.Flights.ForEach(flight =>
                    {
                        TransportResponse transportInfo = _mediator.Send(new CreateTransportCommand() { FlightCarrier = flight.Transport.FlightCarrier, FlightNumber = flight.Transport.FlightNumber }).Result;

                        if (transportInfo != null)
                        {
                            flight.Id = flight.Id;
                            FlightResponse flightInfo = _mediator.Send(new CreateFlightCommand() { Destination = flight.Destination, Origin = flight.Origin, Price = flight.Price, TransportId = transportInfo.Id }).Result;

                            if (flightInfo != null)
                            {
                                flight.Id = flightInfo.Id;
                                flight.Transport.Id = transportInfo.Id;
                                journeyInfo.Flights.Add(flightInfo);

                                _mediator.Send(new CreateJourneyFlightCommand() { FlightId = flightInfo.Id, JourneyId = journeyInfo.Id });
                            }
                        }
                    });

                    Log.Information("[DOMAIN Save journey info from api] -- Success --> Journey: {@journey}", journey);
                }
                else
                {
                    Log.Warning("[DOMAIN Save journey info from api] -- Success --> Journey: {@journey} -- Journey no saved on database", journey);
                }
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Save journey info from api] -- Start --> Journey: {@journey}", ex, journey);
            }
        }

        private async Task<string> GetJourneyInfoFromApi()
        {
            Log.Information("[DOMAIN Get Journey Info From Api] -- Start");
            string result = string.Empty;

            try
            {

                using (HttpClient client = new())
                {
                    HttpResponseMessage response = await client.GetAsync(_apiJourneysUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }

                Log.Information("[DOMAIN Get Journey Info From Api] -- Success");
            }
            catch (Exception ex) 
            {
                LogUtils.WriteErrorLog("[DOMAIN Get Journey Info From Api] -- Error", ex);
                throw;
            }

            return result;
        }

        private List<JourneyDto> CalculateJourney(string origin, string destination)
        {
            Log.Information("[DOMAIN Calculate Journey] -- Start");
            List<JourneyDto> result = new();

            try
            {
                List<List<JourneyApiResponseDto>> allApiJourneys = new();
                List<List<JourneyApiResponseDto>>? apiJourneys = GetApiJourneysByOriginAndDestination(origin, destination);

                if (apiJourneys != null && apiJourneys.Any())
                {
                    allApiJourneys.AddRange(apiJourneys);
                    List<List<JourneyApiResponseDto>>? apiReturnJourneys = GetApiJourneysByOriginAndDestination(destination, origin);

                    apiJourneys.ForEach(apiJourney =>
                    {
                        if (apiReturnJourneys != null && apiReturnJourneys.Any())
                        {
                            apiReturnJourneys.ForEach(apiReturnJourney =>
                            {
                                List<JourneyApiResponseDto> totalJourney = new();
                                totalJourney.AddRange(apiJourney);
                                totalJourney.AddRange(apiReturnJourney);
                                allApiJourneys.Add(totalJourney);
                            });
                        }
                        else
                        {
                            Log.Warning("[DOMAIN Calculate Journey] -- Success -->  return journeys not found");
                        }
                    });
                }
                else
                {
                    Log.Warning("[DOMAIN Calculate Journey] -- Success -->  journeys not found");
                }

                if (allApiJourneys != null && allApiJourneys.Any())
                {
                    allApiJourneys.ForEach(apiJourney =>
                    {
                        result.Add(GetJourneyDto(apiJourney, origin, destination));
                    });
                }

                Log.Information("[DOMAIN Calculate Journey] -- Success");
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[DOMAIN Calculate Journey] -- Error", ex);
                throw;
            }

            return result;
        }

        private List<List<JourneyApiResponseDto>>? GetApiJourneysByOriginAndDestination(string origin, string destination)
        {
            Log.Information($"[DOMAIN Get Api Journeys by origin and destination] -- Start --> Origin: {origin}, Destination: {destination}");

            List<List<JourneyApiResponseDto>>? result = new();

            if (_apiJourneys != null && _apiJourneys.Any())
            {
                List<JourneyApiResponseDto> found = _apiJourneys.Where(j => j.Origin == origin).ToList();

                if (found != null && found.Any())
                {
                    foreach (JourneyApiResponseDto flight in found)
                    {
                        List<JourneyApiResponseDto> journey = new() { flight };
                        if (flight.Destination != destination)
                        {
                            journey.AddRange(GetFlights(journey, flight.Destination, destination, ref result));
                        }
                    }
                }
                else
                {
                    Log.Warning($"[DOMAIN Get Api Journeys by origin and destination] -- Start --> Origin: {origin}, Destination: {destination} -- API journeys with origin {origin} not found");
                }
            }
            else
            {
                Log.Warning($"[DOMAIN Get Api Journeys by origin and destination] -- Success --> Origin: {origin}, Destination: {destination} -- API journeys not found.");
            }

            Log.Information($"[DOMAIN Get Api Journeys by origin and destination] -- Success --> Origin: {origin}, Destination: {destination}");

            return result;
        }

        private List<JourneyApiResponseDto> GetFlights(List<JourneyApiResponseDto> currentJourney, string origin, string destination, ref List<List<JourneyApiResponseDto>> fullJourneys)
        {
            List<JourneyApiResponseDto> result = new();
            List<string> currentOrigins = currentJourney.Select(j => j.Origin).ToList();
            Log.Information("[DOMAIN Get flights] -- Start --> Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins}", origin, destination, currentOrigins);

            if (_apiJourneys != null && _apiJourneys.Any())
            {
                List<JourneyApiResponseDto> found = _apiJourneys.Where(j => j.Origin == origin && !currentOrigins.Contains(j.Destination)).ToList();

                if (found != null && found.Any())
                {
                    foreach (JourneyApiResponseDto flight in found)
                    {
                        result.AddRange(currentJourney);
                        result.Add(flight);

                        if (flight.Destination != destination)
                        {
                            List<JourneyApiResponseDto> addFlights = GetFlights(result, flight.Destination, destination, ref fullJourneys);
                            if (addFlights == null || !addFlights.Any())
                            {
                                if (result.Last().Destination == destination)
                                {
                                    fullJourneys.Add(result);
                                }
                                result.Clear();
                            }
                            else
                            {
                                if (!result.Any())
                                {
                                    result.AddRange(currentJourney);
                                }
                            }
                        }
                        else
                        {
                            List<JourneyApiResponseDto> journeyToAdd = new();
                            journeyToAdd.AddRange(result);
                            if (journeyToAdd.Count <= _maxTotalFlights)
                            {
                                fullJourneys.Add(journeyToAdd);
                            }
                            result.Clear();
                        }
                    }
                }
                else
                {
                    Log.Warning("[DOMAIN Get flights] -- Success --> Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins} -- API Journeys with origin {origin} not found.", origin, destination, currentOrigins, origin);
                }
            }
            else
            {
                Log.Warning("[DOMAIN Get flights] -- Start --> Origin: {origin}, Destination: {destination}, Current origins: {@currentOrigins}", origin, destination, currentOrigins);
            }

            return result;

        }
        #endregion
    }
}
