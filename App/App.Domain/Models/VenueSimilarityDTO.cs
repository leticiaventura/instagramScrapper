using App.Domain.Features.Base;
using App.Domain.Features.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public class VenueSimilarityDTO : Entity
    {
        public Venue Venue { get; set; }
        public decimal Similarity { get; set; }
        public decimal Distance { get; set; }
    }
}
