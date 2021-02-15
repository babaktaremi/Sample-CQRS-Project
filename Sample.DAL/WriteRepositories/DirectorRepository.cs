using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.DAL.Model.WriteModels;

namespace Sample.DAL.WriteRepositories
{
    public class DirectorRepository
    {
        private readonly ApplicationDbContext _db;

        public DirectorRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Director> GetDirectorAsync(string name, CancellationToken cancellationToken = default)
        {
            return _db.Directors.FirstOrDefaultAsync(d => d.FullName == name, cancellationToken: cancellationToken);
        }

        public async Task AddDirectorAsync(Director director, CancellationToken cancellationToken = default)
        {
            await _db.Directors.AddAsync(director, cancellationToken);
        }
    }
}