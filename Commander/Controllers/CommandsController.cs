using AutoMapper;
using Commander.Data;
using Commander.DTOs;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Controllers
{
    [Route("api/[controller]")]/* api/commands */
    [ApiController]//not mandatory, gives default behavior
    
    public class CommandsController:ControllerBase//ControllerBase doesnot include views
    {
        private readonly ICommaderRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommaderRepo repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            
        }


        //get api/commands read resources
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDTO>> GetAllCommands()
        {
            
                IEnumerable<Command> commandItems = _repository.GetAllCommands();
                //throw new Exception("Exception while starting");
                return Ok(_mapper.Map<IEnumerable<CommandReadDTO>>(commandItems));
            
            
        }

        
        //get api/commands/{id} read resource
        [HttpGet("{id}",Name = "GetCommandById")]
        public ActionResult<CommandReadDTO> GetCommandById(int id) {
            Command command=_repository.GetCommandById(id);
            if (command == null) {

                return NotFound();
            }

            return Ok(_mapper.Map<CommandReadDTO>(command));
        }

        //POST api/commands     create resource
        [HttpPost]
        public ActionResult<CommandReadDTO> CreateCommand(CommandCreateDTO commandCreateDTO)
        {
            var commandModel = _mapper.Map<Command>(commandCreateDTO);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();
            var commandReadDTO = _mapper.Map<CommandReadDTO>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id=commandReadDTO.Id},commandReadDTO);

            //return Ok(commandReadDTO);
        }

        //PUT api/commands/{id} update resource
        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id,CommandUpdateDTO commandUpdateDTO)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo != null) {
                _mapper.Map(commandUpdateDTO,commandModelFromRepo);//this updates in the database
                _repository.UpdateCommand(commandModelFromRepo);//actually not required just to maintain consistency if anything else comes up
                _repository.SaveChanges();
                return NoContent();
            }
            return NotFound();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id,JsonPatchDocument<CommandUpdateDTO> jsonPatchDocument)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null)
            {
                return NotFound();
            }
            var commandToPatch = _mapper.Map<CommandUpdateDTO>(commandModelFromRepo);//we want to apply patch to commandModel but we can't
                                                                                     //since we have commandUpdateDTO as type
                                                                                     //hence we convert commandModel to CommandUpdateDTO
            jsonPatchDocument.ApplyTo(commandToPatch, ModelState);//ModelState-->Whether validations are valid

            if (!TryValidateModel(commandToPatch)) {
                return ValidationProblem();
            
            }
            _mapper.Map(commandToPatch, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteCommand(int id)
        {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null) {
                return NotFound();
            }
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();
            return NoContent();
        }



    }

    }
