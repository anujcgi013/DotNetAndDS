using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers 
{
 
    //"api/[controller]"
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController:ControllerBase
    {
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {               
             _repository = repository;
             _mapper= mapper;
        }
        
        //Get api/command
        [HttpGet]
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommand()
        {
            var commandData = _repository.GetAllCommand();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandData));
        }

        //Get api/command/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
         
         var commandData = _repository.GetCommandById(id);
         if(commandData!=null) 
         {
            return Ok(_mapper.Map<CommandReadDto>(commandData));
         }
         return NotFound();
        }

         //Post api/command
        [HttpPost]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
         var commandModel=_mapper.Map<Command>(commandCreateDto);
         _repository.CreateCommand(commandModel);
         _repository.saveChanges();
         var CommandReadDto = _mapper.Map<CommandReadDto>(commandModel);
         //return Ok(CommandReadDto);

         return CreatedAtRoute(nameof(GetCommandById), new {Id = CommandReadDto.Id}, CommandReadDto);
        }

        //Put api/command/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo==null)
            {
                NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.saveChanges();
            return NoContent();
        }

        // Patch api/command/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo==null)
            {
                NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);
            patchDoc.ApplyTo(commandToPatch,  ModelState);
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.saveChanges();
            return NoContent();
        }

        //Delete api/command/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo==null)
            {
                NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.saveChanges();
            return NoContent();
        }
    }

}