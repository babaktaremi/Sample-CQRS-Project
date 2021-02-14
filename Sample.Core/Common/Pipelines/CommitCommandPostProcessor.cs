using System.Threading;
using System.Threading.Tasks;
using MediatR.Pipeline;
using Sample.Core.Common.Marks;
using Sample.DAL;

namespace Sample.Core.Common.Pipelines
{
    public class CommitCommandPostProcessor<TRequest, TResponse> : IRequestPostProcessor<TRequest, TResponse>
    {
        private readonly ApplicationDbContext _db;

        public CommitCommandPostProcessor(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Process(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            if (request is ICommitable)
            {
                if (response is null)
                    return;

                await _db.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
