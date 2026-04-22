using System;

namespace NeoBankPipeline
{
    
    public class Transaction
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
    }

    
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        void Handle(Transaction transaction);
    }

    
    public abstract class BaseHandler : IHandler
    {
        private IHandler _nextHandler;

        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual void Handle(Transaction transaction)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle(transaction);
            }
        }
    }

    
    public class ValidationHandler : BaseHandler
    {
        public override void Handle(Transaction transaction)
        {
            Console.WriteLine("ValidationHandler: Проверка данных...");

            if (string.IsNullOrEmpty(transaction.Sender) ||
                string.IsNullOrEmpty(transaction.Receiver) ||
                transaction.Amount <= 0)
            {
                Console.WriteLine("❌ Ошибка: Некорректные данные транзакции");
                return;
            }

            Console.WriteLine("✔ Данные валидны");
            base.Handle(transaction);
        }
    }

   
    public class FraudCheckHandler : BaseHandler
    {
        public override void Handle(Transaction transaction)
        {
            Console.WriteLine("FraudCheckHandler: Проверка на мошенничество...");

            if (transaction.Amount > 50000)
            {
                Console.WriteLine("⚠ Требуется дополнительное подтверждение (сумма > 50000)");
                return;
            }

            Console.WriteLine("✔ Подозрений нет");
            base.Handle(transaction);
        }
    }

   
    public class CurrencyHandler : BaseHandler
    {
        public override void Handle(Transaction transaction)
        {
            Console.WriteLine("CurrencyHandler: Проверка валюты...");

            if (transaction.Currency != "USD")
            {
                Console.WriteLine($"💱 Конвертация {transaction.Currency} → USD");
                transaction.Currency = "USD";
            }

            Console.WriteLine("✔ Валюта OK");
            base.Handle(transaction);
        }
    }

    
    public class LimitHandler : BaseHandler
    {
        private const decimal DailyLimit = 100000;

        public override void Handle(Transaction transaction)
        {
            Console.WriteLine("LimitHandler: Проверка лимита...");

            if (transaction.Amount > DailyLimit)
            {
                Console.WriteLine("❌ Превышен дневной лимит");
                return;
            }

            Console.WriteLine("✔ Лимит не превышен");
            base.Handle(transaction);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            var transaction = new Transaction
            {
                Amount = 45000,
                Currency = "EUR",
                Sender = "Alice",
                Receiver = "Bob"
            };

            
            var validation = new ValidationHandler();
            var fraud = new FraudCheckHandler();
            var currency = new CurrencyHandler();
            var limit = new LimitHandler();

            validation
                .SetNext(fraud)
                .SetNext(currency)
                .SetNext(limit);

            
            validation.Handle(transaction);

            Console.WriteLine("\nОбработка завершена.");
        }
    }
}
