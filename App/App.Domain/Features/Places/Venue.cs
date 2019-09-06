using App.Domain.Features.Base;
using App.Domain.Features.Users;
using App.Domain.Models;
using Fastenshtein;
using System;
using System.Collections.Generic;

namespace App.Domain.Features.Places
{
    public class Venue : Entity
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public string FoursquareId { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<VenueSimilarityDTO> SimilarVenues { get; set; }
        public IList<InstagramUser> Visitors { get; set; }

        public Venue()
        {
            Categories = new List<Category>();
            Visitors = new List<InstagramUser>();
        }

        public Venue (RawVenue rawVenue)
        {
            FoursquareId = rawVenue.Id;
            Name = rawVenue.Name;
            Location = new Location(rawVenue.Location);
            Location.VenueId = Id;
            Categories = new List<Category>();
            foreach (var category in rawVenue.Categories)
                Categories.Add(new Category(category));
        }

        public void CalculateNameSimilarity()
        {
            Levenshtein lev = new Levenshtein(Name);
            foreach (var item in SimilarVenues)
            {
                decimal distance = lev.DistanceFrom(item.Venue.Name);
                item.Similarity = Name.Length > item.Venue.Name.Length ? distance / Name.Length : distance / item.Venue.Name.Length;
            }
        }
    }
}
