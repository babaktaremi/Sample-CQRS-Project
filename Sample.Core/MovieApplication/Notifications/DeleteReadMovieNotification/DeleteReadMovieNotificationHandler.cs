using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Sample.Core.MovieApplication.BackgroundWorker.Common.Channels;

namespace Sample.Core.MovieApplication.Notifications.DeleteReadMovieNotification
{
    public class DeleteReadMovieNotificationHandler : INotificationHandler<DeleteReadMovieNotification>
    {
        private readonly DeleteModelChannel _channel;

        public DeleteReadMovieNotificationHandler(DeleteModelChannel channel)
        {
            _channel = channel;
        }

        public Task Handle(DeleteReadMovieNotification notification, CancellationToken cancellationToken)
        {
            return _channel.AddAsync(notification.MovieId, cancellationToken);
        }
    }
}