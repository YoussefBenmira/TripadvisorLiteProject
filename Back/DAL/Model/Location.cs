
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DAL.Model
{
    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
        public string linkPicture { get; set; }
        public double rateLocation { get; set; }
        public List<Opinion> opinionList { get; set; }

        public Location()
        {
            opinionList = new List<Opinion>();
           
        }

        public void addOpinion(Opinion newOpinion)
        {
            opinionList.Add(newOpinion);
        }
    }
}