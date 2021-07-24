using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class SqlCommanderRepo : ICommaderRepo
    {
        private readonly CommanderContext _context;

        public SqlCommanderRepo(CommanderContext commanderContext)
        {
            _context = commanderContext;
        }

        public void CreateCommand(Command command)
        {
            if (command==null) {
                throw new ArgumentNullException(nameof(command));
            
            }
            _context.Commands.Add(command);
        }

        public void DeleteCommand(Command command)
        {
            if (command == null) {
                throw new ArgumentNullException(nameof(command));
            }
            _context.Commands.Remove(command);
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(command=>command.Id==id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges()>=0);//unless you call saveChanges,changes won't be reflected in our database
        }

        public void UpdateCommand(Command command)
        {
            //do nothing
        }
    }
}
