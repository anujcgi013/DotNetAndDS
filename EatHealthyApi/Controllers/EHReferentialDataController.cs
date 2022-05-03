using System;
using System.Collections.Generic;
using System.Dynamic;
using AutoMapper;
using EatHealthyApi.Data.RefrentialData;
using EatHealthyApi.Dtos.ReferentialDataDtos;
using EatHealthyApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EatHealthyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EHReferentialDataController : ControllerBase
    {
        private readonly IDislikeDataRepo _dislikeRepository;
        private readonly IFoodPreferenceDataRepo _preferenceRepository;
        private readonly IActivityTypeDataRepo _activityTypeRepository;
        private readonly IGoalsDataRepo _goalRepository;
        private readonly IVarietyTypeDataRepo _varietyTypeRepository;
        private readonly IMapper _mapper;
        public EHReferentialDataController(IDislikeDataRepo dislikeRepository,
        IFoodPreferenceDataRepo preferenceRepository, IActivityTypeDataRepo activityTypeRepository,
        IGoalsDataRepo goalRepository, IVarietyTypeDataRepo varietyTypeRepository,
        IMapper mapper)
        {
            _dislikeRepository = dislikeRepository;
            _activityTypeRepository = activityTypeRepository;
            _preferenceRepository = preferenceRepository;
            _goalRepository = goalRepository;
            _varietyTypeRepository = varietyTypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetReferentialData")]
        public IActionResult GetReferentialData()
        {
            try
            {
                var dislikes = _dislikeRepository.GetAllDislikeItems();
                var activities = _activityTypeRepository.GetAllActivityTypes();
                var preferences = _preferenceRepository.GetAllFoodPreferenceItems();
                var goals = _goalRepository.GetAllGoalItems();
                var varieties = _varietyTypeRepository.GetAllVarietyTypes();
                ReferentialCreateDtos aggregateObject=new ReferentialCreateDtos();
                aggregateObject.Dislikes=_mapper.Map<IEnumerable<DislikeReadDto>>(dislikes);
                aggregateObject.Activities= _mapper.Map<IEnumerable<ActivityTypeReadDto>>(activities);
                aggregateObject.FoodPreferences= _mapper.Map<IEnumerable<FoodPreferencesReadDto>>(preferences);
                aggregateObject.Goals= _mapper.Map<IEnumerable<GoalReadDto>>(goals);
                aggregateObject.Varities= _mapper.Map<IEnumerable<VarietyTypeReadDto>>(varieties);
                return Ok (aggregateObject);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("CreateDislike")]
        public IActionResult CreateDislike(DislikeCreateDto dislikeItems)
        {
            var dislikeItem = _mapper.Map<DislikeItems>(dislikeItems);
            dislikeItem.CreatedDate = DateTime.Now.ToString("dd-MMM-yyyy");
            dislikeItem.isDeleted = false;
            try
            {
                _dislikeRepository.CreateDislikeItem(dislikeItem);
                _dislikeRepository.saveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("GetAllDislike")]
        public ActionResult<IEnumerable<DislikeReadDto>> GetAllDislike()
        {
            try
            {
                var dislikeItems = _dislikeRepository.GetAllDislikeItems();
                return Ok(_mapper.Map<IEnumerable<DislikeReadDto>>(dislikeItems));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}