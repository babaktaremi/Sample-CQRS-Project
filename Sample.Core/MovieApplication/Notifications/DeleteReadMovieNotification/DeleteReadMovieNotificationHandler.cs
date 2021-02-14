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

        public async Task Handle(DeleteReadMovieNotification notification, CancellationToken cancellationToken)
        {
            await _channel.AddToChannelAsync(notification.MovieId, cancellationToken);
        }
    }
}