using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;

namespace Sample.Core.MovieApplication.Notifications.AddReadMovieNotification
{
    public class AddReadModelNotificationHandler : INotificationHandler<AddReadModelNotification>
    {
        private readonly ReadModelChannel _readModelChannel;

        public AddReadModelNotificationHandler(ReadModelChannel readModelChannel)
        {
            _readModelChannel = readModelChannel;
        }

        public Task Handle(AddReadModelNotification notification, CancellationToken cancellationToken)
        {
            return _readModelChannel.AddAsync(notification.MovieId, cancellationToken);
        }
    }
}