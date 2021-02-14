using MediatR;

namespace Sample.Core.MovieApplication.Notifications.DeleteReadMovieNotification
{
    public class DeleteReadMovieNotification : INotification
    {
        public int MovieId { get; }

        public DeleteReadMovieNotification(int movieId)
        {
            MovieId = movieId;
        }
    }
}