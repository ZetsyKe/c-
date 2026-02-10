using System;

namespace OOP_Tasks_11_20
{
    class SimpleList<T>
    {
        private T[] _data;
        private int _count;

        public SimpleList()
        {
            _data = new T[4];
            _count = 0;
        }

        public int Count { get { return _count; } }

        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        public void Add(T item)
        {
            if (_count == _data.Length)
            {
                T[] newArr = new T[_data.Length * 2];
                for (int i = 0; i < _data.Length; i++) newArr[i] = _data[i];
                _data = newArr;
            }
            _data[_count] = item;
            _count++;
        }
    }

    // 11
    class CalendarEvent
    {
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int DurationMinutes { get; set; }

        public CalendarEvent(string name, DateTime date, int durationMinutes)
        {
            EventName = name;
            EventDate = date;
            DurationMinutes = durationMinutes;
        }

        public bool IsToday
        {
            get { return EventDate.Date == DateTime.Today; }
        }

        public TimeSpan? TimeUntilEvent
        {
            get
            {
                if (EventDate <= DateTime.Now) return null;
                return EventDate - DateTime.Now;
            }
        }
    }

    // 12
    enum Currency { RUB, USD, EUR }

    class Money
    {
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public Money(decimal amount, Currency currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public Money ConvertTo(Currency targetCurrency, decimal exchangeRate)
        {
            return new Money(Amount * exchangeRate, targetCurrency);
        }
    }

    class Wallet
    {
        private SimpleList<Money> _items = new SimpleList<Money>();

        public void AddMoney(Money money)
        {
            _items.Add(money);
        }

        public decimal TotalInRub
        {
            get
            {
                decimal sum = 0m;
                for (int i = 0; i < _items.Count; i++)
                {
                    Money m = _items[i];
                    if (m.Currency == Currency.RUB) sum += m.Amount;
                    else if (m.Currency == Currency.USD) sum += m.Amount * 90m;
                    else if (m.Currency == Currency.EUR) sum += m.Amount * 100m;
                }
                return sum;
            }
        }
    }

    // 13
    class AppSettings
    {
        private string _theme;
        private int _fontSize;
        private bool _isNotificationsEnabled;

        public string Theme
        {
            get { return _theme; }
            set
            {
                if (value == "Light" || value == "Dark" || value == "System") _theme = value;
                else _theme = "System";
            }
        }

        public int FontSize
        {
            get { return _fontSize; }
            set
            {
                if (value >= 8 && value <= 72) _fontSize = value;
                else _fontSize = 14;
            }
        }

        public bool IsNotificationsEnabled
        {
            get { return _isNotificationsEnabled; }
            set { _isNotificationsEnabled = value; }
        }

        public AppSettings()
        {
            ResetToDefault();
        }

        public AppSettings(string theme, int fontSize, bool isNotificationsEnabled)
        {
            Theme = theme;
            FontSize = fontSize;
            IsNotificationsEnabled = isNotificationsEnabled;
        }

        public void ResetToDefault()
        {
            Theme = "System";
            FontSize = 14;
            IsNotificationsEnabled = true;
        }
    }

    // 14
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(int id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }

    class TechProduct : Product
    {
        public int WarrantyMonths { get; set; }

        public TechProduct(int id, string name, decimal price, int warrantyMonths)
            : base(id, name, price)
        {
            WarrantyMonths = warrantyMonths;
        }
    }

    class Cart
    {
        private SimpleList<Product> _items = new SimpleList<Product>();

        public void Add(Product product)
        {
            _items.Add(product);
        }

        public void Add(Product product, int quantity)
        {
            if (quantity < 1) quantity = 1;
            for (int i = 0; i < quantity; i++)
                _items.Add(new Product(product.Id, product.Name, product.Price));
        }

        public void Add(TechProduct product)
        {
            _items.Add(product);
            Console.WriteLine("Warranty (months): " + product.WarrantyMonths);
        }

        public decimal TotalPrice
        {
            get
            {
                decimal sum = 0m;
                for (int i = 0; i < _items.Count; i++)
                    sum += _items[i].Price;
                return sum;
            }
        }
    }

    // 15
    enum PlayerRank { Novice, Adept, Expert, Master }

    class Player
    {
        public string Name { get; set; }
        public int Experience { get; private set; }

        public PlayerRank Rank
        {
            get
            {
                if (Experience <= 999) return PlayerRank.Novice;
                if (Experience <= 4999) return PlayerRank.Adept;
                if (Experience <= 9999) return PlayerRank.Expert;
                return PlayerRank.Master;
            }
        }

        public Player(string name, int startExperience)
        {
            Name = name;
            if (startExperience < 0) startExperience = 0;
            Experience = startExperience;
        }

        public void GainExperience(int amount)
        {
            if (amount < 0) amount = 0;
            Experience += amount;
        }
    }

    // 16
    class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int DurationMinutes { get; set; }
        public double Rating { get; set; }

        public Movie(string title, string genre, int durationMinutes, double rating)
        {
            Title = title;
            Genre = genre;
            DurationMinutes = durationMinutes;
            if (rating < 0) rating = 0;
            if (rating > 10) rating = 10;
            Rating = rating;
        }
    }

    class MovieLibrary
    {
        private static SimpleList<Movie> _global = new SimpleList<Movie>();

        public static int TotalMoviesCount
        {
            get { return _global.Count; }
        }

        public void AddMovie(Movie m)
        {
            _global.Add(m);
        }

        public SimpleList<Movie> GetMoviesByGenre(string genre)
        {
            SimpleList<Movie> result = new SimpleList<Movie>();
            for (int i = 0; i < _global.Count; i++)
            {
                if (string.Equals(_global[i].Genre, genre, StringComparison.OrdinalIgnoreCase))
                    result.Add(_global[i]);
            }
            return result;
        }

        public static SimpleList<Movie> GetGlobalTopRated(int count)
        {
            if (count < 0) count = 0;
            if (count > _global.Count) count = _global.Count;

            Movie[] arr = new Movie[_global.Count];
            for (int i = 0; i < _global.Count; i++) arr[i] = _global[i];

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (arr[j].Rating < arr[j + 1].Rating)
                    {
                        Movie tmp = arr[j];
                        arr[j] = arr[j + 1];
                        arr[j + 1] = tmp;
                    }
                }
            }

            SimpleList<Movie> top = new SimpleList<Movie>();
            for (int i = 0; i < count; i++) top.Add(arr[i]);
            return top;
        }
    }

    // 17
    class Temperature
    {
        private double _celsius;

        public double Celsius
        {
            get { return _celsius; }
            set { _celsius = value; }
        }

        public double Fahrenheit
        {
            get { return _celsius * 9.0 / 5.0 + 32.0; }
            set { _celsius = (value - 32.0) * 5.0 / 9.0; }
        }

        public double Kelvin
        {
            get { return _celsius + 273.15; }
            set { _celsius = value - 273.15; }
        }

        public Temperature()
        {
            _celsius = 0;
        }

        public Temperature(double celsius)
        {
            _celsius = celsius;
        }

        public Temperature(string text)
        {
            _celsius = 0;

            if (text == null) return;
            text = text.Trim();

            string[] parts = text.Split(' ');
            if (parts.Length < 2) return;

            string numberPart = parts[0].Trim();
            string unitPart = parts[1].Trim().ToUpper();

            double value;
            if (!double.TryParse(numberPart, out value))
            {
                string fixedNumber = numberPart.Replace('.', ',');
                double.TryParse(fixedNumber, out value);
            }

            if (unitPart.StartsWith("C")) _celsius = value;
            else if (unitPart.StartsWith("F")) Fahrenheit = value;
            else if (unitPart.StartsWith("K")) Kelvin = value;
        }

        public override string ToString()
        {
            return $"{Celsius:F1}°C ({Fahrenheit:F1}°F, {Kelvin:F1}K)";
        }
    }

    // 18
    enum ReservationStatus { Pending, Confirmed, Cancelled, Completed }

    class Reservation
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public int TableNumber { get; set; }
        public DateTime DateTime { get; set; }

        private ReservationStatus _status;

        public ReservationStatus Status
        {
            get { return _status; }
            private set
            {
                if (!IsTransitionAllowed(_status, value))
                    throw new InvalidOperationException("Недопустимый переход статуса: " + _status + " -> " + value);

                _status = value;
            }
        }

        public Reservation(int id, string customerName, int tableNumber, DateTime dateTime)
        {
            Id = id;
            CustomerName = customerName;
            TableNumber = tableNumber;
            DateTime = dateTime;
            _status = ReservationStatus.Pending;
        }

        private bool IsTransitionAllowed(ReservationStatus from, ReservationStatus to)
        {
            if (from == to) return true;

            if (from == ReservationStatus.Cancelled) return false;
            if (from == ReservationStatus.Completed) return false;

            if (from == ReservationStatus.Pending)
            {
                return to == ReservationStatus.Confirmed || to == ReservationStatus.Cancelled;
            }

            if (from == ReservationStatus.Confirmed)
            {
                return to == ReservationStatus.Cancelled || to == ReservationStatus.Completed;
            }

            return false;
        }

        public void Confirm()
        {
            Status = ReservationStatus.Confirmed;
        }

        public void Cancel()
        {
            Status = ReservationStatus.Cancelled;
        }

        public void MarkAsCompleted()
        {
            Status = ReservationStatus.Completed;
        }
    }

    // 19
    abstract class Shape
    {
        public abstract double Area { get; }
        public abstract double Perimeter { get; }

        public virtual void PrintInfo()
        {
            Console.WriteLine(GetType().Name + ": Area=" + Area.ToString("F2") + ", Perimeter=" + Perimeter.ToString("F2"));
        }
    }

    class Circle : Shape
    {
        public double Radius { get; set; }

        public Circle(double radius)
        {
            if (radius < 0) radius = 0;
            Radius = radius;
        }

        public override double Area { get { return Math.PI * Radius * Radius; } }
        public override double Perimeter { get { return 2 * Math.PI * Radius; } }
    }

    class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Rectangle(double width, double height)
        {
            if (width < 0) width = 0;
            if (height < 0) height = 0;
            Width = width;
            Height = height;
        }

        public override double Area { get { return Width * Height; } }
        public override double Perimeter { get { return 2 * (Width + Height); } }
    }

    class Triangle : Shape
    {
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        public Triangle(double a, double b, double c)
        {
            if (a < 0) a = 0;
            if (b < 0) b = 0;
            if (c < 0) c = 0;
            A = a; B = b; C = c;
        }

        public override double Perimeter { get { return A + B + C; } }

        public override double Area
        {
            get
            {
                double p = Perimeter / 2.0;
                double under = p * (p - A) * (p - B) * (p - C);
                if (under <= 0) return 0;
                return Math.Sqrt(under);
            }
        }
    }

    class ShapeCollection
    {
        private SimpleList<Shape> _items = new SimpleList<Shape>();

        public void AddShape(Shape shape)
        {
            _items.Add(shape);
        }

        public void PrintAllAreas()
        {
            for (int i = 0; i < _items.Count; i++)
                Console.WriteLine(_items[i].GetType().Name + " area: " + _items[i].Area.ToString("F2"));
        }

        public double GetTotalPerimeter()
        {
            double sum = 0;
            for (int i = 0; i < _items.Count; i++)
                sum += _items[i].Perimeter;
            return sum;
        }
    }

    // 20
    enum PrivacyLevel { Public, FriendsOnly, Private }

    class UserProfile
    {
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public int FriendsCount { get; private set; }
        public int PostsCount { get; private set; }
        public PrivacyLevel Privacy { get; set; }

        public int Age
        {
            get
            {
                int age = DateTime.Today.Year - BirthDate.Year;
                if (BirthDate.Date > DateTime.Today.AddYears(-age)) age--;
                return age;
            }
        }

        public UserProfile(string username, DateTime birthDate, int friendsCount, int postsCount, PrivacyLevel privacy)
        {
            Username = username;
            BirthDate = birthDate;
            FriendsCount = friendsCount < 0 ? 0 : friendsCount;
            PostsCount = postsCount < 0 ? 0 : postsCount;
            Privacy = privacy;
        }

        public UserProfile(string username, DateTime birthDate)
        {
            Username = username;
            BirthDate = birthDate;
            FriendsCount = 0;
            PostsCount = 0;
            Privacy = PrivacyLevel.Public;
        }

        public void AddFriend()
        {
            FriendsCount++;
        }

        public void AddPost()
        {
            PostsCount++;
        }

        public string GetPublicInfo()
        {
            if (Privacy == PrivacyLevel.Public)
                return $"Username: {Username}, Age: {Age}, Friends: {FriendsCount}, Posts: {PostsCount}";
            if (Privacy == PrivacyLevel.FriendsOnly)
                return $"Username: {Username}, Age: {Age}";
            return $"Username: {Username}";
        }
    }

    class SocialNetwork
    {
        private SimpleList<UserProfile> _users = new SimpleList<UserProfile>();

        public void AddUser(UserProfile user)
        {
            _users.Add(user);
        }

        public UserProfile FindMostActiveUser()
        {
            if (_users.Count == 0) return null;

            UserProfile best = _users[0];
            double bestScore = Score(best);

            for (int i = 1; i < _users.Count; i++)
            {
                double s = Score(_users[i]);
                if (s > bestScore)
                {
                    bestScore = s;
                    best = _users[i];
                }
            }

            return best;
        }

        private double Score(UserProfile u)
        {
            if (u.FriendsCount == 0)
            {
                if (u.PostsCount > 0) return double.MaxValue;
                return 0.0;
            }
            return (double)u.PostsCount / (double)u.FriendsCount;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Task 11 ===");
            CalendarEvent e1 = new CalendarEvent("Past event", DateTime.Now.AddHours(-5), 60);
            CalendarEvent e2 = new CalendarEvent("Today event", DateTime.Today.AddHours(18), 90);
            CalendarEvent e3 = new CalendarEvent("Future event", DateTime.Now.AddDays(2).AddHours(3), 45);

            PrintEvent(e1);
            PrintEvent(e2);
            PrintEvent(e3);

            Console.WriteLine("\n=== Task 12 ===");
            Wallet w = new Wallet();
            w.AddMoney(new Money(1000m, Currency.RUB));
            w.AddMoney(new Money(10m, Currency.USD));
            w.AddMoney(new Money(5m, Currency.EUR));
            Console.WriteLine("Total in RUB: " + w.TotalInRub);

            Console.WriteLine("\n=== Task 13 ===");
            AppSettings s = new AppSettings();
            PrintSettings(s);

            s.Theme = "Blue";
            s.FontSize = 3;
            s.IsNotificationsEnabled = false;
            PrintSettings(s);

            s.ResetToDefault();
            PrintSettings(s);

            Console.WriteLine("\n=== Task 14 ===");
            Cart cart = new Cart();
            Product p1 = new Product(1, "Bread", 50m);
            Product p2 = new Product(2, "Milk", 80m);
            TechProduct tp = new TechProduct(3, "Phone", 25000m, 24);

            cart.Add(p1);
            cart.Add(p2, 3);
            cart.Add(tp);
            Console.WriteLine("Total price: " + cart.TotalPrice);

            Console.WriteLine("\n=== Task 15 ===");
            Player pl = new Player("Alex", 0);
            PrintPlayer(pl);
            pl.GainExperience(1200);
            PrintPlayer(pl);
            pl.GainExperience(4000);
            PrintPlayer(pl);
            pl.GainExperience(6000);
            PrintPlayer(pl);

            Console.WriteLine("\n=== Task 16 ===");
            MovieLibrary lib1 = new MovieLibrary();
            MovieLibrary lib2 = new MovieLibrary();

            lib1.AddMovie(new Movie("Movie A", "Action", 120, 7.8));
            lib1.AddMovie(new Movie("Movie B", "Drama", 95, 9.1));
            lib2.AddMovie(new Movie("Movie C", "Action", 110, 8.5));
            lib2.AddMovie(new Movie("Movie D", "Comedy", 100, 6.9));

            Console.WriteLine("Total movies (global): " + MovieLibrary.TotalMoviesCount);

            SimpleList<Movie> action = lib1.GetMoviesByGenre("Action");
            Console.WriteLine("Action movies:");
            for (int i = 0; i < action.Count; i++)
                Console.WriteLine(action[i].Title + " (" + action[i].Rating + ")");

            SimpleList<Movie> top2 = MovieLibrary.GetGlobalTopRated(2);
            Console.WriteLine("Top 2 rated:");
            for (int i = 0; i < top2.Count; i++)
                Console.WriteLine(top2[i].Title + " (" + top2[i].Rating + ")");

            Console.WriteLine("\n=== Task 17 ===");
            Temperature t1 = new Temperature();
            Temperature t2 = new Temperature(25.5);
            Temperature t3 = new Temperature("77.0 F");

            Console.WriteLine(t1.ToString());
            Console.WriteLine(t2.ToString());
            Console.WriteLine(t3.ToString());

            t2.Fahrenheit = 32;
            Console.WriteLine(t2.ToString());

            Console.WriteLine("\n=== Task 18 ===");
            Reservation r = new Reservation(1, "Ivan", 5, DateTime.Now.AddHours(2));
            Console.WriteLine("Status: " + r.Status);
            try
            {
                r.MarkAsCompleted();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            r.Confirm();
            Console.WriteLine("Status: " + r.Status);
            r.MarkAsCompleted();
            Console.WriteLine("Status: " + r.Status);
            try
            {
                r.Cancel();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("\n=== Task 19 ===");
            ShapeCollection sc = new ShapeCollection();
            sc.AddShape(new Circle(3));
            sc.AddShape(new Rectangle(4, 6));
            sc.AddShape(new Triangle(3, 4, 5));

            sc.PrintAllAreas();
            Console.WriteLine("Total perimeter: " + sc.GetTotalPerimeter().ToString("F2"));

            Console.WriteLine("\n=== Task 20 ===");
            SocialNetwork sn = new SocialNetwork();

            UserProfile u1 = new UserProfile("user1", new DateTime(2008, 5, 10), 10, 50, PrivacyLevel.Public);
            UserProfile u2 = new UserProfile("user2", new DateTime(2007, 2, 1));
            u2.Privacy = PrivacyLevel.FriendsOnly;
            u2.AddFriend();
            u2.AddFriend();
            u2.AddPost();
            u2.AddPost();
            u2.AddPost();

            UserProfile u3 = new UserProfile("user3", new DateTime(2006, 11, 20), 0, 5, PrivacyLevel.Private);

            sn.AddUser(u1);
            sn.AddUser(u2);
            sn.AddUser(u3);

            Console.WriteLine(u1.GetPublicInfo());
            Console.WriteLine(u2.GetPublicInfo());
            Console.WriteLine(u3.GetPublicInfo());

            UserProfile mostActive = sn.FindMostActiveUser();
            if (mostActive != null)
                Console.WriteLine("Most active: " + mostActive.Username);

            Console.WriteLine("\nDone.");
        }

        static void PrintEvent(CalendarEvent ev)
        {
            Console.WriteLine("Name: " + ev.EventName);
            Console.WriteLine("Date: " + ev.EventDate);
            Console.WriteLine("Duration: " + ev.DurationMinutes + " minutes");
            Console.WriteLine("IsToday: " + ev.IsToday);
            if (ev.TimeUntilEvent == null) Console.WriteLine("TimeUntilEvent: passed");
            else Console.WriteLine("TimeUntilEvent: " + ev.TimeUntilEvent.Value);
            Console.WriteLine("---");
        }

        static void PrintSettings(AppSettings s)
        {
            Console.WriteLine("Theme: " + s.Theme + ", FontSize: " + s.FontSize + ", Notifications: " + s.IsNotificationsEnabled);
        }

        static void PrintPlayer(Player p)
        {
            Console.WriteLine("Name: " + p.Name + ", Exp: " + p.Experience + ", Rank: " + p.Rank);
        }
    }
}
