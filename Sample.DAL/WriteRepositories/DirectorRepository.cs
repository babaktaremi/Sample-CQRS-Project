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

        public Task<Director> GetDirector(string name, CancellationToken cancellationToken)
        {
            return _db.Directors.FirstOrDefaultAsync(d => d.FullName == name, cancellationToken: cancellationToken);
        }

        public void AddDirector(Director director)
        {
            _db.Directors.Add(director);
        }
    }
}
