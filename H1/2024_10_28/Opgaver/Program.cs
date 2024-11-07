using Lib;
using Opgaver;

namespace Program
{
    internal class Program
    {
        static void Main()
        {
            Advanced.Opgave2.Run();
        }
    }
}
namespace Begynder
{
    public static class Opgave1
    {
        public static void Run()
        {
            System.Console.WriteLine("Hello World!");
        }
    }
    public static class Opgave2
    {
        public static void Run()
        {
        }
    }
}
namespace Rutinerede
{
    public static class Opgave1
    {
        public static void Run()
        {

        }
    }
}
namespace Advanced
{
    public static class Opgave1
    {
        struct User
        {
            public string _name;
            public string _number;
            public string _email;
            public User(string name, string number, string email)
            {
                _name = name;
                _number = number;
                _email = email;
            }
        }
        struct Users : IDisposable
        {
            private string _path;
            public List<User> _inner;
            public Users(string path)
            {
                _path = path;
                _inner = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(path)) ?? [];
            }

            public void Dispose()
            {
                var text = Newtonsoft.Json.JsonConvert.SerializeObject(_inner, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_path, text);
            }
        }
        private static Users _users;
        public static void AddUser()
        {
            var user = new User(
                Input.AskReg<string>("Name: ", new(@"^[A-Z][a-zA-Z]+(?: [A-Z][a-zA-Z]+)*$"), false) ?? "",
                Input.AskReg<string>("Email: ", new(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"), false) ?? "",
                Input.AskReg<string>("Phone: ", new(@"[0-9]+"), false) ?? ""
            );
            _users._inner.Add(user);
        }
        public static void DeleteUser()
        {
            var user_to_delete = Input.Ask<string>("Email: ", false);
            if (user_to_delete != null)
                _users._inner.RemoveAll(x => x._email.StartsWith(user_to_delete));
        }
        public static void EditUser()
        {
            var user_to_edit = Input.Ask<string>("Email: ", false);
            if (user_to_edit != null)
            {
                var userIndex = _users._inner.FindIndex(x => x._email.StartsWith(user_to_edit));
                if (userIndex != -1)
                {
                    var user = _users._inner[userIndex];
                    user._name = Input.Ask<string>("New Name: ", false) ?? user._name;
                    user._number = Input.Ask<string>("New Phone: ", false) ?? user._number;
                    user._email = Input.Ask<string>("New Email: ", false) ?? user._email;
                    _users._inner[userIndex] = user;
                }
            }
        }
        public static void Filter()
        {
            var filter_email = Input.Ask<string>("Filter Email: ", false) ?? "";
            foreach (var user in _users._inner.Where(x => x._email.StartsWith(filter_email)))
                Console.WriteLine($"Name: {user._name} | Email: {user._email} | Phone: {user._number}");
        }
        public static void Run()
        {
            _users = new(@"P:\School\H1\2024_10_28\Opgaver\Advanced\Opgave1\phonebook.json");
            ConsoleKey key = ConsoleKey.Home;
            bool stop = false;
            do
            {
                Console.WriteLine("(A)dd | (F)ilter | (D)elete | (E)dit | (Escape)");
                switch (key)
                {
                    case ConsoleKey.A:
                        AddUser();
                        break;
                    case ConsoleKey.F:
                        Filter();
                        break;
                    case ConsoleKey.D:
                        DeleteUser();
                        break;
                    case ConsoleKey.E:
                        EditUser();
                        break;
                    case ConsoleKey.Escape:
                        stop = true;
                        break;
                }
            } while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape && !stop);
            Console.WriteLine("Program closed");
            _users.Dispose();
        }
    }
    public static class Opgave2
    {
        struct User
        {
            public string _name;
            public uint _workhours;
            public string _type;
            public float _sallery;
            public float _fradrag;
            public User(string name, uint workhours, string type, float sallery, float fradrag)
            {
                _name = name;
                _workhours = workhours;
                _type = type;
                _sallery = sallery;
                _fradrag = fradrag;
            }
        }
        struct Users: IDisposable
        {
            private string _path;
            public List<User> _inner;
            public Users(string path)
            {
                _path = path;
                _inner = Newtonsoft.Json.JsonConvert.DeserializeObject<List<User>>(File.ReadAllText(path)) ?? [];
            }

            public void Dispose()
            {
                var text = Newtonsoft.Json.JsonConvert.SerializeObject(_inner, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText(_path, text);
            }
        }
        private static Users _users;

        public static void AddUser()
        {
            var user = new User(
                Input.AskReg<string>("Name: ", new(@"^[A-Z][a-zA-Z]+(?: [A-Z][a-zA-Z]+)*$"), false) ?? "",
                Input.AskReg<uint>("Type: ", new(@"^[\w-\.]"), false),
                Input.AskReg<string>("Workhours: ", new(@"[0-9.]+"), false),
                Input.AskReg<float>("Sallery: ", new(@"[0-9.]+"), false),
                Input.AskReg<float>("Tax: ", new(@"[0-9.]+"), false)
            );
            _users._inner.Add(user);
        }
        public static void DeleteUser()
        {
            var user_to_delete = Input.Ask<string>("Name: ", false);
            if (user_to_delete != null)
                _users._inner.RemoveAll(x => x._name.StartsWith(user_to_delete));
        }
        public static void EditUser()
        {
            var user_to_edit = Input.Ask<string>("Email: ", false);
            if (user_to_edit != null)
            {
                var userIndex = _users._inner.FindIndex(x => x._name.StartsWith(user_to_edit));
                if (userIndex != -1)
                {
                    var user = _users._inner[userIndex];
                    user._name = Input.Ask<string>("New Name: ", false) ?? user._name;
                    user._workhours = Input.Ask<uint?>("New Workhours: ", false) ?? user._workhours;
                    user._type = Input.Ask<string?>("New Type: ", false) ?? user._type;
                    user._sallery = Input.Ask<float?>("New Sallery: ", false) ?? user._sallery;
                    user._fradrag = Input.Ask<float?>("New Fradrag: ", false) ?? user._fradrag;
                    _users._inner[userIndex] = user;
                }
            }
        }
        public static void Filter()
        {
            var filter_email = Input.Ask<string>("Filter Name: ", false) ?? "";
            foreach (var user in _users._inner.Where(x => x._name.StartsWith(filter_email)))
            {
                Console.WriteLine($"Name: {user._name} | Workhours: {user._workhours} | Type: {user._type} | Sallery: {user._sallery} | Fradrag: {user._fradrag}");
            }
        }

        public static void Run()
        {
            _users = new(@"P:\School\H1\2024_10_28\Opgaver\Advanced\Opgave2\personal.json");
            ConsoleKey key = ConsoleKey.Home;
            bool stop = false;
            do
            {
                Console.WriteLine("(A)dd | (F)ilter | (D)elete | (E)dit | (Escape)");
                switch (key)
                {
                    case ConsoleKey.A:
                        AddUser();
                        break;
                    case ConsoleKey.F:
                        Filter();
                        break;
                    case ConsoleKey.D:
                        DeleteUser();
                        break;
                    case ConsoleKey.E:
                        EditUser();
                        break;
                    case ConsoleKey.Escape:
                        stop = true;
                        break;
                }
            } while ((key = Console.ReadKey(true).Key) != ConsoleKey.Escape && !stop);
            Console.WriteLine("Program closed");
            _users.Dispose();
        }
    }
}

