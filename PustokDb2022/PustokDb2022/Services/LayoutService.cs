using PustokDb2022.DAL;
using PustokDb2022.Models;

namespace PustokDb2022.Services
{
    public class LayoutService
    {
        private readonly PustokDbContext _context;

        public LayoutService(PustokDbContext context)
        {
            _context = context;
        }

        public Dictionary<string, string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }

        public List<Genre> GetGenres()
        {
            return _context.Genres.ToList();
        }
    }
}
