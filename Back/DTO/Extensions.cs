using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DTO
{
   public static class Extensions
    {
        public static LocationDto ToDto(this Location entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            List<OpinionDto> listOfOpinionsDto = entity.opinionList.Select(element => element.ToDto()).ToList();

            return new LocationDto
            {
                id = entity.id,
                name = entity.name,
                linkPicture = entity.linkPicture,
                rateLocation = entity.rateLocation,
                opinionList = listOfOpinionsDto
            };
        }

        public static Location ToEntity(this LocationDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            List<Opinion> listOfOpinions = dto.opinionList.Select(element => element.ToEntity()).ToList();

            return new Location
            {
                id = dto.id,
                name = dto.name,
                linkPicture = dto.linkPicture,
                rateLocation = dto.rateLocation,
                opinionList = listOfOpinions
            };
        }

        public static OpinionDto ToDto(this Opinion entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new OpinionDto
            {
                id = entity.id,
                Content = entity.Content,
                rateOpinion = entity.rateOpinion
            };
        }

        public static Opinion ToEntity(this OpinionDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            return new Opinion
            {
                id = dto.id,
                Content = dto.Content,
                rateOpinion = dto.rateOpinion
            };
        }
    }
}
