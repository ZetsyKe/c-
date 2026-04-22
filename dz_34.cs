using System;
using System.Collections.Generic;

namespace SmartHomeCommand
{
    
    interface ICommand
    {
        void Execute();
        void Undo();
    }

    
    class Light
    {
        public bool IsOn { get; private set; }

        public void On()
        {
            IsOn = true;
            Console.WriteLine("Свет включен.");
        }

        public void Off()
        {
            IsOn = false;
            Console.WriteLine("Свет выключен.");
        }
    }

    
    class LightOnCommand : ICommand
    {
        private Light _light;

        public LightOnCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.On();
        }

        public void Undo()
        {
            _light.Off();
        }
    }

    
    class LightOffCommand : ICommand
    {
        private Light _light;

        public LightOffCommand(Light light)
        {
            _light = light;
        }

        public void Execute()
        {
            _light.Off();
        }

        public void Undo()
        {
            _light.On();
        }
    }

    
    class Thermostat
    {
        public int Temperature { get; private set; }

        public Thermostat(int initialTemperature = 22)
        {
            Temperature = initialTemperature;
        }

        public void SetTemperature(int temp)
        {
            Temperature = temp;
            Console.WriteLine($"Температура установлена: {temp}°C");
        }
    }

    
    class SetTemperatureCommand : ICommand
    {
        private Thermostat _thermostat;
        private int _newTemperature;
        private int _previousTemperature;

        public SetTemperatureCommand(Thermostat thermostat, int newTemperature)
        {
            _thermostat = thermostat;
            _newTemperature = newTemperature;
        }

        public void Execute()
        {
            _previousTemperature = _thermostat.Temperature;
            _thermostat.SetTemperature(_newTemperature);
        }

        public void Undo()
        {
            _thermostat.SetTemperature(_previousTemperature);
            Console.WriteLine("Изменение температуры отменено.");
        }
    }

    
    class MacroCommand : ICommand
    {
        private List<ICommand> _commands;

        public MacroCommand(List<ICommand> commands)
        {
            _commands = commands;
        }

        public void Execute()
        {
            foreach (var command in _commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            
            for (int i = _commands.Count - 1; i >= 0; i--)
            {
                _commands[i].Undo();
            }
        }
    }

    
    class RemoteControl
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            _command = command;
        }

        public void PressButton()
        {
            _command?.Execute();
        }

        public void PressUndo()
        {
            _command?.Undo();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Light light = new Light();
            Thermostat thermostat = new Thermostat(22);

            ICommand lightOn = new LightOnCommand(light);
            ICommand setTemp = new SetTemperatureCommand(thermostat, 18);

            RemoteControl remote = new RemoteControl();

            Console.WriteLine("1. Включение света:");
            remote.SetCommand(lightOn);
            remote.PressButton();

            Console.WriteLine("\n2. Изменение температуры:");
            remote.SetCommand(setTemp);
            remote.PressButton();

            Console.WriteLine("\n3. Undo для температуры:");
            remote.PressUndo();

            Console.WriteLine("\n4. Undo для света:");
            remote.SetCommand(lightOn);
            remote.PressUndo();

            Console.WriteLine("\n5. Макрокоманда 'Ухожу из дома':");
            ICommand leaveHome = new MacroCommand(new List<ICommand>
            {
                new LightOffCommand(light),
                new SetTemperatureCommand(thermostat, 16)
            });

            remote.SetCommand(leaveHome);
            remote.PressButton();

            Console.WriteLine("\n6. Undo макрокоманды:");
            remote.PressUndo();
        }
    }
}
