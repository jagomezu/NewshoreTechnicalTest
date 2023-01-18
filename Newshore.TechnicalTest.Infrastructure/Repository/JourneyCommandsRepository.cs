using Microsoft.EntityFrameworkCore;
using Newshore.TechnicalTest.Infrastructure.Interfaces;
using Newshore.TechnicalTest.Transverse.Entities;
using Newshore.TechnicalTest.Transverse.Utils;
using Serilog;

namespace Newshore.TechnicalTest.Infrastructure.Repository
{
    public class JourneyCommandsRepository : IJourneyCommandsRepository
    {
        #region Properties
        private readonly SqlServerDbContext _dbContext;
        #endregion

        #region Constructor
        public JourneyCommandsRepository(SqlServerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Public methods
        public async Task<Journey> Create(Journey journey)
        {
            Log.Information("[Create Journey] -- Start --> Journey Info: {@JourneyInfo}", journey);

            try
            {
                _dbContext.Entry(journey).State = EntityState.Added;
                await _dbContext.SaveChangesAsync();
                await _dbContext.Entry(journey).ReloadAsync();

                Log.Information("[Create Journey] -- Success --> Journey Info: {@JourneyInfo}", journey);
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Create Journey] -- Error --> Journey Info: {@JourneyInfo}", ex, journey);

                throw;
            }

            return journey;
        }

        public async Task<bool> Delete(Journey journey)
        {
            Log.Information("[Delete Journey] -- Start --> Journey Info: {@JourneyInfo}", journey);
            bool result;

            try
            {
                _dbContext.Entry(journey).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Delete Journey] -- Success --> Journey Info: {@JourneyInfo}", journey);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Delete Journey] -- Error --> Journey Info: {@JourneyInfo}", ex, journey);

                throw;
            }

            return result;
        }

        public async Task<bool> Update(Journey journey)
        {
            Log.Information("[Update Journey] -- Start --> Journey Info: {@JourneyInfo}", journey);
            bool result;

            try
            {
                _dbContext.Entry(journey).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();

                Log.Information("[Update Journey] -- Success --> Journey Info: {@JourneyInfo}", journey);

                result = true;
            }
            catch (Exception ex)
            {
                LogUtils.WriteErrorLog("[Update Journey] -- Error --> Journey Info: {@JourneyInfo}", ex, journey);

                throw;
            }

            return result;
        }
        #endregion
    }
}
