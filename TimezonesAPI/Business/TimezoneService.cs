using TimezonesAPI.Models;
using TimezonesAPI.Entity;

namespace TimezonesAPI.Business
{
    public class TimezoneService : ITimezoneService
    {
        private readonly ITimezoneRepository _timezoneRepository;

        public TimezoneService(ITimezoneRepository timezoneRepository)
        {
            _timezoneRepository = timezoneRepository;
        }

        public async Task<TimezoneResponseModelPagination> GetAll(int page = 1, int size = 9)
        {
            var timezoneResponseModels = new List<TimezoneResponseModel>();
            List<Timezone> timezones = await _timezoneRepository.GetAll();
            int totalRecord = timezones.Count;
            int totalPages = totalRecord % size != 0 ? totalRecord / size + 1 : totalRecord / size;

            timezones = timezones.Skip((page - 1) * size).Take(size).ToList();
                       
            foreach (var timezone in timezones)
            {
                timezoneResponseModels.Add(new TimezoneResponseModel()
                {
                    id = timezone.id,
                    code = timezone.code,
                    name = timezone.name,
                    description = timezone.description,
                    offset = timezone.offset,
                    isDTS = timezone.isDTS,
                    utc = timezone.utc.Split(","),
                    order = timezone.order,
                    createdByUserId = timezone.createdByUserId,
                    lastModifiedByUserId = timezone.lastModifiedByUserId,
                    lastModifiedOnDate = timezone.lastModifiedOnDate,
                    createdOnDate = timezone.createdOnDate
                });
            }

            return new TimezoneResponseModelPagination()
            {
                currentPage = page, 
                totalPages = totalPages, 
                pageSize = size, 
                totalRecords = totalRecord, 
                content = timezoneResponseModels
            };
        }

        public async Task<TimezoneResponseModel> GetById(string id)
        {
            List<Timezone> timezones = await _timezoneRepository.GetAll();
            Timezone timezone = timezones.Where(item => item.id.Equals(id)).FirstOrDefault();

            var timezoneResponseModel = new TimezoneResponseModel()
            {
                id = timezone.id,
                code = timezone.code,
                name = timezone.name,
                description = timezone.description,
                offset = timezone.offset,
                isDTS = timezone.isDTS,
                utc = timezone.utc.Split(","),
                order = timezone.order,
                createdByUserId = timezone.createdByUserId,
                lastModifiedByUserId = timezone.lastModifiedByUserId,
                lastModifiedOnDate = timezone.lastModifiedOnDate,
                createdOnDate = timezone.createdOnDate
            };

            return timezoneResponseModel;
        }

        public async Task<TimezoneResponseModelPagination> GetByName(string name, int page = 1, int size = 9)
        {
            name = name == null ? string.Empty : name;
            List<Timezone> timezones = await _timezoneRepository.GetAll();
            timezones = timezones.Where(timezone => timezone.name.ToLower().Contains(name)).ToList();
            int totalRecord = timezones.Count;
            int totalPages = totalRecord % size != 0 ? totalRecord / size + 1 : totalRecord / size;
            var timezoneResponseModels = new List<TimezoneResponseModel>();

            timezones = timezones.Skip((page - 1) * size).Take(size).ToList();

            foreach (var timezone in timezones)
            {
                timezoneResponseModels.Add(new TimezoneResponseModel()
                {
                    id = timezone.id,
                    code = timezone.code,
                    name = timezone.name,
                    description = timezone.description,
                    offset = timezone.offset,
                    isDTS = timezone.isDTS,
                    utc = timezone.utc.Split(","),
                    order = timezone.order,
                    createdByUserId = timezone.createdByUserId,
                    lastModifiedByUserId = timezone.lastModifiedByUserId,
                    lastModifiedOnDate = timezone.lastModifiedOnDate,
                    createdOnDate = timezone.createdOnDate
                });
            }

            return new TimezoneResponseModelPagination()
            {
                currentPage = page,
                totalPages = totalPages,
                pageSize = size,
                totalRecords = totalRecord,
                content = timezoneResponseModels
            };
        }

        public async Task SaveAsync(TimezoneResponseModel timezoneResponseModel)
        {
            Timezone timezone = new Timezone()
            {
                id = timezoneResponseModel.id,
                code = timezoneResponseModel.code,
                name = timezoneResponseModel.name,
                description = timezoneResponseModel.description,
                offset = timezoneResponseModel.offset,
                isDTS = timezoneResponseModel.isDTS,
                utc = string.Join(",", timezoneResponseModel),
                order = timezoneResponseModel.order,
                createdByUserId = timezoneResponseModel.createdByUserId,
                lastModifiedByUserId = timezoneResponseModel.lastModifiedByUserId,
                lastModifiedOnDate = timezoneResponseModel.lastModifiedOnDate,
                createdOnDate = timezoneResponseModel.createdOnDate
            };
            await _timezoneRepository.SaveAsync(timezone);
        }

        public async Task Delete(string id)
        {
            await _timezoneRepository.Delete(id);
        }
    }
}
