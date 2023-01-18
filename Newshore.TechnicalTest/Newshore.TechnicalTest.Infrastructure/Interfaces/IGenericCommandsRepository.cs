namespace Newshore.TechnicalTest.Infrastructure.Interfaces
{
    public interface IGenericCommandsRepository<T>
    {
        public Task<T> Create(T objectToCreate);

        public Task<bool> Update(T objectToUpdate);

        public Task<bool> Delete(T objectToDelete);
    }
}
