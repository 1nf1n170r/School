namespace SearchMachine;

using Newtonsoft.Json;

internal readonly record struct Birthday(DateTime Date)
{
    public int Age => DateTime.Today.Year - Date.Year -
        (DateTime.Today.Month < Date.Month ||
        (DateTime.Today.Month == Date.Month && DateTime.Today.Day < Date.Day) ? 1 : 0);
}
internal record SchoolPerson(string FirstName, string LastName, DateTime Birth, string[] Subjects)
{
    [JsonIgnore]
    public Birthday Birthday { get; init; } = new Birthday(Birth);
    public string FullName => $"{FirstName} {LastName}";
}
internal record Teacher(string FirstName, string LastName, DateTime Birth, string[] Subjects) : SchoolPerson(FirstName, LastName, Birth, Subjects);
internal record Student(string FirstName, string LastName, DateTime Birth, string[] Subjects) : SchoolPerson(FirstName, LastName, Birth, Subjects)
{
    public string PrintFormat => (Birthday.Age < 18 ? "\x1B[31m" : "") + $"Student: {FullName}\x1B[0m";
}

internal class Program
{
    private static Student[] Students { get; set; } = [];
    private static Teacher[] Teachers { get; set; } = [];
    static void Main()
    {
        Start();
        Logic();
    }
    static void Start()
    {
        Students = (JsonConvert.DeserializeObject<Student[]>(File.ReadAllText(@"P:\School\H1\Exams\2024_11_15\SearchMachine\assets\Students.json")) ?? []).OrderBy(x => x.FullName).ToArray();
        Teachers = (JsonConvert.DeserializeObject<Teacher[]>(File.ReadAllText(@"P:\School\H1\Exams\2024_11_15\SearchMachine\assets\Teachers.json")) ?? []).OrderBy(x => x.FullName).ToArray();
    }
    static void Logic()
    {
        const uint MENU_LENGTH = 3;
        do
        {
            Console.WriteLine("1) Søg lærer");
            Console.WriteLine("2) Søg elever");
            Console.WriteLine("3) Søg fag");
            var action = Lib.Input.AskCond<uint>("Action: ", input => uint.TryParse(input, out uint i) && i <= MENU_LENGTH && i > 0, false, _ => null);
            switch (action)
            {
                case 1:
                    {
                        for (int i = 0; i < Teachers.Length; i++)
                            Console.WriteLine($"{i + 1}) {Teachers[i].FullName}");
                        var teacher = Teachers[Lib.Input.AskCond<uint>("Teacher: ", input => uint.TryParse(input, out uint i) && i <= Teachers.Length && i > 0, false, _ => null) - 1];
                        foreach (var subject in teacher.Subjects)
                        {
                            Console.WriteLine($"\tSubject: {subject}");
                            var students = Students.Where(x => x.Subjects.Contains(subject)).ToArray();
                            Console.WriteLine($"\tAmount of students: {students.Length}");
                            foreach (var student in students)
                                Console.WriteLine($"\t\t{student.PrintFormat}");
                        }
                    }
                    break;

                case 2:
                    {
                        var student_name = Lib.Input.AskCond<string>("Student: ", input => Students.Any(x => x.FullName == input), false, _ => null);
                        var student = Students.Where(x => x.FullName == student_name).First();
                        foreach (var subject in student.Subjects)
                            Console.WriteLine($"Subject {subject}\nTeacher: {Teachers.First(x => x.Subjects.Contains(subject)).FullName}\n");
                    }
                    break;

                case 3:
                    {
                        var subjects = Teachers.SelectMany(x => x.Subjects).Distinct().ToArray();
                        for (int i = 0; i < subjects.Length; i++)
                            Console.WriteLine($"{i + 1}) {subjects[i]}");
                        var subject = subjects[Lib.Input.AskCond<uint>("Subject: ", input => uint.TryParse(input, out uint i) && i <= subjects.Length && i > 0, false, _ => null) - 1];
                        var students = Students.Where(s => s.Subjects.Contains(subject)).ToArray();
                        Console.WriteLine($"Subject: {subject}\nTeacher: {Teachers.First(x => x.Subjects.Any(x => x == subject)).FullName}\nAmount of students: {students.Length}");
                        foreach (var student in students)
                            Console.WriteLine(student.PrintFormat);
                    }
                    break;
            }
        } while (Lib.Input.AskKey("Another one (y/n)? ", false, true) == ConsoleKey.Y); ;
    }
}
