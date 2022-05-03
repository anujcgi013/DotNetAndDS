using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data {
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommand()
        {
            var commands= new List<Command>
            {
                new Command{Id=0, HowTo="Prepare a Pizza", Line="Need an Oven", Plateform="Oven"},
                new Command{Id=1, HowTo="Prepare a Tea", Line="Need an gas", Plateform="gas & Oven"},
                new Command{Id=2, HowTo="Boil Egg", Line="Need some water", Plateform="pan"}
            };
            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{
                Id=0,
                HowTo="Prepare a Pizza",
                Line="Need an Oven",
                Plateform="Oven"};
        }

        public bool saveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
