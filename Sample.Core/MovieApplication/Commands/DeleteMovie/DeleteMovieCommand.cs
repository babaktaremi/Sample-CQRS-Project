using MediatR;
using Sample.Core.Common.Marks;

namespace Sample.Core.MovieApplication.Commands.DeleteMovie
{
   public class DeleteMovieCommand:IRequest<bool>,ICommitable
    {
        public int MovieId { get; set; }
    }
}
