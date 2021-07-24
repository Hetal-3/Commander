using Commander.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommaderRepo
    {
        public void CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command> {
                 new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Ketlle and Pan" },
                 new Command { Id = 1, HowTo = "Cut a bread", Line = "Get a knife", Platform = "Knife and a chopping board" },
                 new Command { Id = 2, HowTo = "Make tea", Line = "Boil tea leaves,water,milk,sugar", Platform = "Kettle and a cup" },
        };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command { Id = 0, HowTo = "Boil an egg", Line = "Boil water", Platform = "Ketlle and Pan" };
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}
