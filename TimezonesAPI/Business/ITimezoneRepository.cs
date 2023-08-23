using TimezonesAPI.Entity;

namespace TimezonesAPI.Business
{
    public interface ITimezoneRepository
    {
        public const string message = "Not found record";
        public Task<List<Timezone>> GetAll();
        public Task<Timezone> GetById(string id);
        public Task<List<Timezone>> GetByName(string name);
        public Task SaveAsync(Timezone timezone);
        public Task Delete(string id);
    }
}
