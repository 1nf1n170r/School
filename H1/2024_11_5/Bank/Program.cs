internal class Program
{
    static void Main()
    {
        //Default admin
        var admin = new Admin("Johan Milter", "Jakobsen");
        var bank = new Bank(new());
        do
        {
            System.Console.Write("(A)dd account. (D)elete account. (U)pdate account: ");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.A:
                    System.Console.Write("Fornavn: ");
                    var fornavn = Console.ReadLine() ?? "";
                    System.Console.Write("Efternavn: ");
                    var efternavn = Console.ReadLine() ?? "";
                    bank.AddAccount(fornavn, efternavn, admin);
                    break;
                case ConsoleKey.D:
                    System.Console.Write("Id: ");
                    if (uint.TryParse(Console.ReadLine() ?? "", out var id1))
                        bank.DeleteAccount(id1);
                    break;
                case ConsoleKey.U:
                    System.Console.Write("Id: ");
                    var id_str = Console.ReadLine() ?? "";
                    System.Console.Write("Value: ");
                    if (uint.TryParse(id_str, out var id2) && uint.TryParse(Console.ReadLine() ?? "", out var value))
                        System.Console.WriteLine($"Your account has been updated. Ballance: {bank.UpdateAccountBallance(id2, value)}");
                    break;
                case ConsoleKey.Escape:
                    return;
            }
        } while (Console.ReadKey().Key == ConsoleKey.Enter);
    }
}



