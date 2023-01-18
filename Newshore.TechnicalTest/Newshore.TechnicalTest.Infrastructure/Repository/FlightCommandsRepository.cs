using Microsoft.EntityFrameworkCore;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Infrastructure.Repository
{
    public class FlightCommandsRepository : IFlightCommandsRepository
    {
        #region Properties
        private readonly SqlServerDbContext _dbContext;
        #endregion

        #region Constructor
        public FlightCommandsRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public async Task<Flight> Create(Flight flight)
        {
            Log.Information("[Create flight] -- Start --> Flight Info: {@FlightInfo}", flight);

            try
            {
                _dbContext.Entry(flight).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(flight).ReloadAsync();

                Log.Information("[Create flight] -- Success --> Flight Info: {@FlightInfo}", flight);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Create flight] -- Error --> Flight Info: {@FlightInfo}", ex, flight);

                throw;
            }

            return flight;
        }

        public async Task<bool> Delete(Flight flight)
        {
            Log.Information("[Delete flight] -- Start --> Flight Info: {@FlightInfo}", flight);
            bool result;

            try
            {
                _dbContext.Entry(flight).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Delete flight] -- Success --> Flight Info: {@FlightInfo}", flight);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Delete flight] -- Error --> Flight Info: {@FlightInfo}", ex, flight);

                throw;
            }

            return result;
        }

        public async Task<bool> Update(Flight flight)
        {
            Log.Information("[Update flight] -- Start --> Flight Info: {@FlightInfo}", flight);
            bool result;

            try
            {
                _dbContext.Entry(flight).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Update flight] -- Success --> Flight Info: {@FlightInfo}", flight);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Update flight] -- Error --> Flight Info: {@FlightInfo}", ex, flight);

                throw;
            }

            return result;
        }
        #endregion
    }
}
