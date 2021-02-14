using MediatR;

namespace Sample.Core.MovieApplication.Notifications.AddReadMovieNotification
{
    public class AddReadModelNotification : INotification
    {
        public int MovieId { get; }

        public AddReadModelNotification(int movieId)
        {
            MovieId = movieId;
        }
    }
}