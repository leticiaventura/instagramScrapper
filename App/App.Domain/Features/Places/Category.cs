using App.Domain.Features.Base;
using App.Domain.Models;
using System.Collections.Generic;

namespace App.Domain.Features.Places
{
    public class Category : Entity
    {
        public Category()
        {
            Venues = new List<Venue>();
        }

        public string Name { get; set; }
        public string FoursquareId { get; set; }
        public IList<Venue> Venues { get; set; }

        public Category(RawCategory rawCategory)
        {
            FoursquareId = rawCategory.Id;
            Name = rawCategory.Name;
        }
    }
}
