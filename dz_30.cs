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
    private LegacyScanner scanner;

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

    public Notification(IMessageSender sender)
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
        Console.WriteLine("TV включен");
    }

    public void SetVolume(int level)
    {
        Console.WriteLine("Громкость TV: " + level);
    }

    public void SetMovieMode()
    {
        Console.WriteLine("Режим фильма включен");
    }
}

public class AudioSystem
{
    public void TurnOn()
    {
        Console.WriteLine("Аудиосистема включена");
    }

    public void SetSurroundSound()
    {
        Console.WriteLine("Объемный звук включен");
    }
}

public class LightingControl
{
    public void DimLights()
    {
        Console.WriteLine("Свет приглушен");
    }

    public void TurnOffExtraLights()
    {
        Console.WriteLine("Лишний свет выключен");
    }
}

public class SubscriptionService
{
    public void CheckSubscription()
    {
        Console.WriteLine("Подписка проверена");
    }

    public void StartStreaming()
    {
        Console.WriteLine("Стриминговый сервис запущен");
    }
}

public class SmartHomeFacade
{
    private TVSystem tv;
    private AudioSystem audio;
    private LightingControl light;
    private SubscriptionService subscription;

    public SmartHomeFacade()
    {
        tv = new TVSystem();
        audio = new AudioSystem();
        light = new LightingControl();
        subscription = new SubscriptionService();
    }

    public void WatchMovie()
    {
        subscription.CheckSubscription();
        tv.TurnOn();
        tv.SetVolume(20);
        tv.SetMovieMode();
        audio.TurnOn();
        audio.SetSurroundSound();
        light.DimLights();
        light.TurnOffExtraLights();
        subscription.StartStreaming();
    }
}

public class Program
{
    public static void Main()
    {
        ICustomLogger logger = new ScannerAdapter(new LegacyScanner());
        logger.LogMessage("Проверка безопасности");

        Notification urgent = new UrgentNotification(new SmsSender());
        urgent.Notify("Срочное уведомление");

        Notification info = new InfoNotification(new EmailSender());
        info.Notify("Информационное сообщение");

        SmartHomeFacade smartHome = new SmartHomeFacade();
        smartHome.WatchMovie();
    }
}
