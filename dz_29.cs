using System;

public interface ICustomLogger
{
    void LogMessage(string message);
}

public class LegacyScanner
{
    public void ExecuteScan(string data)
    {
        Console.WriteLine("Scanning: " + data);
    }
}

public class ScannerAdapter : ICustomLogger
{
    private readonly LegacyScanner scanner;

    public ScannerAdapter(LegacyScanner scanner)
    {
        this.scanner = scanner;
    }

    public void LogMessage(string message)
    {
        scanner.ExecuteScan(message);
    }
}

public interface IMessageSender
{
    void SendMessage(string text);
}

public class SmsSender : IMessageSender
{
    public void SendMessage(string text)
    {
        Console.WriteLine("SMS: " + text);
    }
}

public class EmailSender : IMessageSender
{
    public void SendMessage(string text)
    {
        Console.WriteLine("Email: " + text);
    }
}

public abstract class Notification
{
    protected IMessageSender sender;

    protected Notification(IMessageSender sender)
    {
        this.sender = sender;
    }

    public abstract void Notify(string message);
}

public class UrgentNotification : Notification
{
    public UrgentNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string message)
    {
        sender.SendMessage("ПРИОРИТЕТ! " + message);
    }
}

public class InfoNotification : Notification
{
    public InfoNotification(IMessageSender sender) : base(sender) { }

    public override void Notify(string message)
    {
        sender.SendMessage(message);
    }
}

public class TVSystem
{
    public void TurnOn()
    {
        Console.WriteLine("TV On");
    }

    public void SetChannel()
    {
        Console.WriteLine("Channel Set");
    }
}

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Audio On");
    }

    public void SetVolume(int level)
    {
        Console.WriteLine("Volume: " + level);
    }
}

public class LightingControl
{
    public void DimLights()
    {
        Console.WriteLine("Lights Dimmed");
    }

    public void TurnOffLights()
    {
        Console.WriteLine("Lights Off");
    }
}

public class SubscriptionService
{
    public void CheckSubscription()
    {
        Console.WriteLine("Subscription Checked");
    }

    public void StartStreaming()
    {
        Console.WriteLine("Streaming Started");
    }
}

public class SmartHomeFacade
{
    private readonly TVSystem tv;
    private readonly AudioSystem audio;
    private readonly LightingControl lighting;
    private readonly SubscriptionService subscription;

    public SmartHomeFacade()
    {
        tv = new TVSystem();
        audio = new AudioSystem();
        lighting = new LightingControl();
        subscription = new SubscriptionService();
    }

    public void WatchMovie()
    {
        tv.TurnOn();
        audio.TurnOn();
        audio.SetVolume(20);
        lighting.DimLights();
        subscription.CheckSubscription();
        subscription.StartStreaming();
        tv.SetChannel();
    }
}

public class Program
{
    public static void Main()
    {
        ICustomLogger logger = new ScannerAdapter(new LegacyScanner());
        logger.LogMessage("Security log");

        Notification urgent = new UrgentNotification(new SmsSender());
        urgent.Notify("Сервер недоступен");

        Notification info = new InfoNotification(new EmailSender());
        info.Notify("Ежедневный отчет готов");

        SmartHomeFacade smartHome = new SmartHomeFacade();
        smartHome.WatchMovie();
    }
}
