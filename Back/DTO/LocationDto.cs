using System;
using System.Collections.Generic;

namespace DTO
{
    public class LocationDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public string linkPicture { get; set; }
        public double rateLocation { get; set; }
        public List<OpinionDto> opinionList { get; set; }
    }
}
