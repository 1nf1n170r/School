using System.ComponentModel;
using System.Data.Common;

internal class Program
{
    static void Main()
    {
        //Default admin
        var admin = new Admin("Johan Milter", "Jakobsen");
        var bank = new Bank(new());
        do
        {
            Console.Write("(A)dd account. (D)elete account. (U)pdate account: ");
            try {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.A:
                        {
                            Console.Write("Fornavn: ");
                            var fornavn = Console.ReadLine() ?? "";
                            Console.Write("Efternavn: ");
                            var efternavn = Console.ReadLine() ?? "";
                            bank.AddAccount(fornavn, efternavn, admin);
                        }
                        break;
                    case ConsoleKey.R:
                        {
                            Console.Write("Id: ");
                            var id = uint.Parse(Console.ReadLine() ?? "");
                            bank.DeleteAccount(id);
                        }
                        break;
                    case ConsoleKey.D:
                        {
                            Console.Write("Id: ");
                            var id = uint.Parse(Console.ReadLine() ?? "");
                            Console.Write("Value: ");
                            var value = float.Parse(Console.ReadLine() ?? "");
                            Console.WriteLine($"Your account has been updated. Ballance: {bank.Deposit(id, value)}");
                        }
                        break;
                    case ConsoleKey.W:
                        {
                            Console.Write("Id: ");
                            var id = uint.Parse(Console.ReadLine() ?? "");
                            Console.Write("Value: ");
                            var value = float.Parse(Console.ReadLine() ?? "");
                            Console.WriteLine($"Your account has been updated. Ballance: {bank.Withdraw(id, value)}");
                        }
                        break;
                    case ConsoleKey.Escape:
                        return;
                }
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Try again");
            }
            catch (FormatException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Try again");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e);
                Console.WriteLine("Try again");
            }
            catch(Exception e){
                Console.WriteLine(e);
                Console.WriteLine("Try again");
            }
        } while (Console.ReadKey().Key == ConsoleKey.Enter);
    }
}



