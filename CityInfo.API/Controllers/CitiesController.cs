using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
        }

        [HttpGet]
        public IActionResult getCities()
        {
            var cityEntities = _cityInfoRepository.GetCities();
            var result = new List<CityWithoutPointOfInterestDto>();

            foreach (var cityEntity in cityEntities)
            {
                result.Add(new CityWithoutPointOfInterestDto
                {
                    Id = cityEntity.Id,
                    Name = cityEntity.Name,
                    Description = cityEntity.Description
                });
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult getCity(int id, bool includePointOfInterest)
        {
            var cityToReturn = _cityInfoRepository.GetCity(id, includePointOfInterest);
            if (cityToReturn == null)
            {
                return NotFound();
            }

            if (includePointOfInterest)
            {
                var result = new CityDto()
                {
                    Id = cityToReturn.Id,
                    Name = cityToReturn.Name,
                    Description = cityToReturn.Description
                };

                foreach (var point in cityToReturn.PointsOfInterest)
                {
                    result.PointsOfInterest.Add(new PointOfInterestDto()
                    {
                        Id = point.Id,
                        Name = point.Name,
                        Description = point.Description
                    });
                }
                return Ok(result);
            }

            var citywithout = new CityWithoutPointOfInterestDto()
            {
                Id = cityToReturn.Id,
                Name = cityToReturn.Name,
                Description = cityToReturn.Description
            };
            return Ok(citywithout);
        }
    }
}
