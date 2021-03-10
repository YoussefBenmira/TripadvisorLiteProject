using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Model;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Front.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly TripAdvisorContext _TripAdvisorContext;

        public LocationController(TripAdvisorContext TripAdvisorContext)
        {
            _TripAdvisorContext = TripAdvisorContext;

        }

        [HttpGet]
        public ActionResult<IEnumerable<LocationDto>> Get()
        {
            IEnumerable<LocationDto> enumerable = _TripAdvisorContext.Locations.Include(element => element.opinionList).Select(element => element.ToDto());
            return Ok(enumerable.OrderByDescending(x => x.rateLocation));
        }

        [HttpGet("{id}")]
        public ActionResult<LocationDto> Get(int id)
        {
            Location location = null;
            location = _TripAdvisorContext.Locations.SingleOrDefault(loc => loc.id == id);
            if (location != null)
            {
                return Ok(location.ToDto());
            }
            return NotFound();

        }

        [HttpPut]
        public ActionResult Put(int id, String picture)
        {
            var location = _TripAdvisorContext.Locations.SingleOrDefault(loc => loc.id == id);
            if (location != null)
            {
                location.linkPicture = picture;
                _TripAdvisorContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(Location location)
        {
            _TripAdvisorContext.Locations.Add(location);
            _TripAdvisorContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var location = _TripAdvisorContext.Locations.Include(element => element.opinionList).SingleOrDefault(loc => loc.id == id);
            if (location != null)
            {
                _TripAdvisorContext.Locations.Remove(location);
                _TripAdvisorContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }
    }
}
