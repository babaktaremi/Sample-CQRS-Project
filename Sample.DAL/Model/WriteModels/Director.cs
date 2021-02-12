using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.DAL.Model.WriteModels
{
   public class Director
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<Movie> Movies { get; set; }
    }
}
