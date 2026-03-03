using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


[Serializable]
public class PaymentException : Exception
{
    public string ErrorCode { get; }

    public PaymentException() { }

    public PaymentException(string message, string errorCode)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    protected PaymentException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        ErrorCode = info.GetString(nameof(ErrorCode));
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(ErrorCode), ErrorCode);
        base.GetObjectData(info, context);
    }
}


[Serializable]
public class InvalidCardException : PaymentException
{
    public InvalidCardException(string message)
        : base(message, "CARD_INVALID") { }

    protected InvalidCardException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

[Serializable]
public class InsufficientFundsException : PaymentException
{
    public InsufficientFundsException(string message)
        : base(message, "INSUFFICIENT_FUNDS") { }

    protected InsufficientFundsException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

[Serializable]
public class ExpiredCardException : PaymentException
{
    public ExpiredCardException(string message)
        : base(message, "CARD_EXPIRED") { }

    protected ExpiredCardException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}


[Serializable]
public class PayPalAccountNotFoundException : PaymentException
{
    public PayPalAccountNotFoundException(string message)
        : base(message, "PAYPAL_NOT_FOUND") { }

    protected PayPalAccountNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}



[Serializable]
public class InvalidCryptoAddressException : PaymentException
{
    public InvalidCryptoAddressException(string message)
        : base(message, "CRYPTO_INVALID_ADDRESS") { }

    protected InvalidCryptoAddressException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}

[Serializable]
public class CryptoNetworkException : PaymentException
{
    public CryptoNetworkException(string message)
        : base(message, "CRYPTO_NETWORK_ERROR") { }

    protected CryptoNetworkException(SerializationInfo info, StreamingContext context)
        : base(info, context) { }
}



public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount, string currency, string accountInfo);
}

public class CreditCardProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount, string currency, string accountInfo)
    {
        if (string.IsNullOrEmpty(accountInfo))
            throw new InvalidCardException("Неверный номер карты");

        if (accountInfo.Contains("expired"))
            throw new ExpiredCardException("Срок действия карты истёк");

        if (amount > 1000)
            throw new InsufficientFundsException("Недостаточно средств на карте");

        Console.WriteLine($"Оплата {amount} {currency} по карте прошла успешно.");
    }
}

public class PayPalProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount, string currency, string accountInfo)
    {
        if (string.IsNullOrEmpty(accountInfo))
            throw new PayPalAccountNotFoundException("Аккаунт PayPal не найден");

        if (amount > 500)
            throw new InsufficientFundsException("Недостаточно средств на PayPal");

        Console.WriteLine($"Оплата {amount} {currency} через PayPal прошла успешно.");
    }
}

public class CryptoProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount, string currency, string accountInfo)
    {
        if (string.IsNullOrEmpty(accountInfo))
            throw new InvalidCryptoAddressException("Неверный крипто-адрес");

        if (accountInfo.Contains("network"))
            throw new CryptoNetworkException("Проблемы с сетью блокчейн");

        Console.WriteLine($"Оплата {amount} {currency} через криптовалюту прошла успешно.");
    }
}



class Program
{
    static void Main()
    {
        List<IPaymentProcessor> processors = new List<IPaymentProcessor>
        {
            new CreditCardProcessor(),
            new PayPalProcessor(),
            new CryptoProcessor()
        };

        foreach (var processor in processors)
        {
            try
            {
                processor.ProcessPayment(1200, "USD", "test_account");
            }
            catch (PaymentException ex)
            {
                Console.WriteLine($"Ошибка оплаты: {ex.Message} (код: {ex.ErrorCode})");
                Console.WriteLine("Попробуйте другой способ оплаты.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Критическая ошибка: " + ex.Message);
                Console.WriteLine("Ошибка записана в лог.");
            }
            finally
            {
                Console.WriteLine("Ресурсы освобождены.");
                Console.WriteLine();
            }
        }
    }
}
