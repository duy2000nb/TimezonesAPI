using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using TimezonesAPI.Entity;

namespace TimezonesAPI.Business
{
    public class TimezoneRepository : ITimezoneRepository
    {
        private readonly TimezoneContext _context;
        private IMemoryCache cache;

        public TimezoneRepository(TimezoneContext context, IMemoryCache cache)
        {
            _context = context;
            this.cache = cache;
        }

        private void setCache(List<Timezone> timezones) {
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
            cache.Set("Timezone", timezones, cacheOptions);
        }

        public async Task<List<Timezone>> GetAll()
        {

            if (!cache.TryGetValue("Timezone", out List<Timezone> timezones))
            {
                timezones = await _context._timezones.ToListAsync();
                setCache(timezones);
            }

            return timezones;
        }

        public async Task<Timezone> GetById(string id)
        {
            return await _context._timezones.FindAsync(id);
        }

        public async Task<List<Timezone>> GetByName(string name)
        {
            name = name.ToLower();
            
            if (!cache.TryGetValue("Timezone", out List<Timezone> timezones))
            {
                timezones = await _context._timezones.ToListAsync();
                setCache(timezones);
            }

            return timezones.Where(timezone => timezone.name.ToLower().Contains(name)).ToList();
        }

        public async Task SaveAsync(Timezone timezone)
        {
            if (timezone.id == null)
            {
                await _context._timezones.AddAsync(timezone);
                await _context.SaveChangesAsync();
            }

            if (TimezoneExists(timezone.id))
            {
                _context._timezones.Update(timezone);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(string id)
        {
            Timezone timezone = await GetById(id);
            _context._timezones.Remove(timezone);   
            await _context.SaveChangesAsync();
        }

        private bool TimezoneExists(string id)
        {
            return (_context._timezones?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
