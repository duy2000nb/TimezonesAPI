using TimezonesAPI.Models;

namespace TimezonesAPI.Business
{
    public interface ITimezoneService
    {
        public Task<TimezoneResponseModelPagination> GetAll(int page = 1, int size = 9);
        public Task<TimezoneResponseModel> GetById(string id);
        public Task<TimezoneResponseModelPagination> GetByName(string name, int page = 1, int size = 9);
        public Task SaveAsync(TimezoneResponseModel timezone);
        public Task Delete(string id);
    }
}
