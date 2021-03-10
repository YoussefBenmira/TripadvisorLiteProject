using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Model;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SwaggerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpinionController : ControllerBase
    {
        private readonly TripAdvisorContext _TripAdvisorContext;

        public OpinionController(TripAdvisorContext TripAdvisorContext)
        {
            _TripAdvisorContext = TripAdvisorContext;

        }

        [HttpGet]
        public ActionResult<IEnumerable<OpinionDto>> Get()
        {
            return Ok(_TripAdvisorContext.Opinions.Select(element => element.ToDto()));
        }

        [HttpGet("{id}")]
        public ActionResult<OpinionDto> Get(int id)
        {
            Opinion opinion = null;
            opinion = _TripAdvisorContext.Opinions.SingleOrDefault(opi => opi.id == id);
            if (opinion != null)
            {
                return Ok(opinion.ToDto());
            }
            return NotFound();
            
        }

        [HttpPut]
        public ActionResult Put(int id, String content)
        {
            var opinion = _TripAdvisorContext.Opinions.SingleOrDefault(opi => opi.id == id);
            if (opinion != null)
            {
                opinion.Content = content;
                _TripAdvisorContext.SaveChanges();
                return Ok();
            }
            return NotFound();
        }

        [HttpPost("{locationid}")]
        public ActionResult Post(int locationid, Opinion opinion)
        {
            Location location = null;
            location = _TripAdvisorContext.Locations.Include(element => element.opinionList).SingleOrDefault(loc => loc.id == locationid);
            if (opinion.rateOpinion != 0)
            {
                double sum = opinion.rateOpinion;
                foreach (Opinion element in location.opinionList)
                {
                    sum += element.rateOpinion;
                }
                location.rateLocation = sum / (location.opinionList.Count + 1);
            }
            if (location == null)
            {
                return NotFound();
            }
            location.addOpinion(opinion);
            _TripAdvisorContext.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var opinion = _TripAdvisorContext.Opinions.SingleOrDefault(opi => opi.id == id);
            if (opinion != null)
            {
                _TripAdvisorContext.Opinions.Remove(opinion);
                _TripAdvisorContext.SaveChanges();
                return NotFound();
            }
            return Ok();

        }


    }
}