using System;

interface ICommand
{
    void Execute();
}

class SmartDevice
{
    public void LightOn() => Console.WriteLine("Light ON");
    public void LightOff() => Console.WriteLine("Light OFF");
    public void SetTemperature(int temp) => Console.WriteLine($"Temperature set to {temp}");
}

class LightCommand : ICommand
{
    private SmartDevice device;
    private bool state;

    public LightCommand(SmartDevice device, bool state)
    {
        this.device = device;
        this.state = state;
    }

    public void Execute()
    {
        if (state) device.LightOn();
        else device.LightOff();
    }
}

class ClimateCommand : ICommand
{
    private SmartDevice device;
    private int temperature;

    public ClimateCommand(SmartDevice device, int temperature)
    {
        this.device = device;
        this.temperature = temperature;
    }

    public void Execute()
    {
        device.SetTemperature(temperature);
    }
}

abstract class Handler
{
    protected Handler next;

    public Handler SetNext(Handler next)
    {
        this.next = next;
        return next;
    }

    public abstract void Handle(string request, string token);
}

class LoggingHandler : Handler
{
    public override void Handle(string request, string token)
    {
        Console.WriteLine($"Log: {request}");
        next?.Handle(request, token);
    }
}

class SecurityHandler : Handler
{
    public override void Handle(string request, string token)
    {
        if (token != "valid")
        {
            Console.WriteLine("Access denied");
            return;
        }
        next?.Handle(request, token);
    }
}

class ValidationHandler : Handler
{
    private SmartDevice device = new SmartDevice();

    public override void Handle(string request, string token)
    {
        ICommand command = null;

        if (request.ToLower() == "включить свет")
            command = new LightCommand(device, true);
        else if (request.ToLower() == "выключить свет")
            command = new LightCommand(device, false);
        else if (request.ToLower().StartsWith("установить температуру"))
        {
            var parts = request.Split(' ');
            if (int.TryParse(parts[^1], out int temp))
                command = new ClimateCommand(device, temp);
        }

        if (command == null)
        {
            Console.WriteLine("Invalid command");
            return;
        }

        command.Execute();
    }
}

class Program
{
    static void Main()
    {
        var logging = new LoggingHandler();
        var security = new SecurityHandler();
        var validation = new ValidationHandler();

        logging.SetNext(security).SetNext(validation);

        logging.Handle("Включить свет", "valid");
        logging.Handle("Установить температуру 22", "valid");
        logging.Handle("Выключить свет", "invalid");
    }
}
