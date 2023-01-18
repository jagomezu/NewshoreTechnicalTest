using Microsoft.EntityFrameworkCore;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Infrastructure.Repository
{
    public class JourneyFlightCommandsRepository : IJourneyFlightCommandsRepository
    {
        #region Properties
        private readonly SqlServerDbContext _dbContext;
        #endregion

        #region Constructor
        public JourneyFlightCommandsRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public async Task<JourneyFlight> Create(JourneyFlight journeyFlight)
        {
            Log.Information("[Create journey flight] -- Start --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);

            try
            {
                _dbContext.Entry(journeyFlight).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(journeyFlight).ReloadAsync();

                Log.Information("[Create journey flight] -- Success --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Create journey flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, journeyFlight);

                throw;
            }

            return journeyFlight;
        }

        public async Task<bool> Delete(JourneyFlight journeyFlight)
        {
            Log.Information("[Delete journey flight] -- Start --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);
            bool result;

            try
            {
                _dbContext.Entry(journeyFlight).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Delete journey flight] -- Success --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Delete journey flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, journeyFlight);

                throw;
            }

            return result;
        }

        public async Task<bool> Update(JourneyFlight journeyFlight)
        {
            Log.Information("[Update journey flight] -- Start --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);
            bool result;

            try
            {
                _dbContext.Entry(journeyFlight).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Update journey flight] -- Success --> Journey Flight Info: {@JourneyFlightInfo}", journeyFlight);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Update journey flight] -- Error --> Journey Flight Info: {@JourneyFlightInfo}", ex, journeyFlight);

                throw;
            }

            return result;
        }
        #endregion
    }
}
