using System;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Opgavehaefte
{
    class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Point(in int x, in int y)
        {
            X = x;
            Y = y;
        }
        public Point((int, int) tuple)
        {
            X = tuple.Item1;
            Y = tuple.Item2;
        }
    }
    static class CM
    {
        public static T? Parse<T>(string val)
        {
            try
            {
                return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(val) ?? default;
            }
            catch (Exception)
            {
                return default;
            }
        }

        /// <summary>
        /// Ask for a string, depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static T? Ask<T>(string qestion, bool newline, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadLine();
            Console.WriteLine();
            Console.ForegroundColor = std_color;
            return Parse<T>(ans ?? "");
        }

        /// <summary>
        /// Ask for a string, depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static T? AskReg<T>(string qestion, bool newline, Regex regex, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var (x, _) = Console.GetCursorPosition();
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;

            string output = "";
            ConsoleKeyInfo c;
            while ((c = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                switch (c.Key)
                {
                    case ConsoleKey.Backspace:
                        output = output.Remove(output.Length - 1);
                        break;
                    default:
                        output += c.KeyChar;
                        break;
                }
                
                if (regex.IsMatch(output))
                    Console.Write(output);
                    
            }
            Console.ForegroundColor = std_color;
            return Parse<T>(output);
        }

        /// <summary>
        /// Ask for key depending on question
        /// </summary>
        /// <param name="qestion"></param>
        /// <param name="newline"></param>
        /// <param name="print_val"></param>
        /// <param name="qu_color"></param>
        /// <param name="ans_color"></param>
        /// <returns></returns>
        public static ConsoleKey AskKey(string qestion, bool newline, bool printVal, ConsoleColor quColor = ConsoleColor.DarkBlue, ConsoleColor ansColor = ConsoleColor.Green)
        {
            var std_color = Console.ForegroundColor;
            Console.ForegroundColor = quColor;
            if (newline) Console.WriteLine(">> " + qestion);
            else Console.Write(">> " + qestion);
            Console.ForegroundColor = ansColor;
            var ans = Console.ReadKey(!printVal).Key;
            Console.WriteLine();
            Console.ForegroundColor = std_color;
            return ans;
        }
        
        public static Point DynMove(Point min, Point max)
        {
            var (x, y) = Console.GetCursorPosition();
            ConsoleKey key;
            while ((key = Console.ReadKey().Key) != ConsoleKey.Enter)
            {
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        if (y - 1 >= min.Y) y--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (y + 1 <= max.Y) y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (x - 1 >= min.X) x--;
                        break;
                    case ConsoleKey.RightArrow:
                        if (x + 1 <= max.X) x++;
                        break;
                }
                Console.SetCursorPosition(x, y);
            }
            return new Point(x, y);
        }



    }
    class TicTacToe
    {
        private readonly bool OverrideTokens = false;
        private (char token1, char token2) Tokens = ('X', 'O');
        private readonly char EmptySpaceChar = ' ';
        public TicTacToe(bool override_tokens, (char, char) tokens, char emptyspace_char)
        {
            OverrideTokens = override_tokens;
            Tokens = tokens;
            EmptySpaceChar = emptyspace_char;
            this.StartGame();
        }
        private void MoveInsert(char token, ref char[,] plads, bool overrideToken, int moveY = 0, int moveX = 0)
        {
            //Move the game to where i want it
            for (int i = 0; i < moveY; i++)
                Console.WriteLine();
            //Show game with correct table-values
            Console.SetCursorPosition(moveX, moveY);
            Console.WriteLine("╔═══╦═══╦═══╗");
            Console.SetCursorPosition(moveX, moveY + 1);
            Console.WriteLine($"║ {plads[0, 0]} ║ {plads[0, 1]} ║ {plads[0, 2]} ║");
            Console.SetCursorPosition(moveX, moveY + 2);
            Console.WriteLine("╠═══╬═══╬═══╣");
            Console.SetCursorPosition(moveX, moveY + 3);
            Console.WriteLine($"║ {plads[1, 0]} ║ {plads[1, 1]} ║ {plads[1, 2]} ║");
            Console.SetCursorPosition(moveX, moveY + 4);
            Console.WriteLine("╠═══╬═══╬═══╣");
            Console.SetCursorPosition(moveX, moveY + 5);
            Console.WriteLine($"║ {plads[2, 0]} ║ {plads[2, 1]} ║ {plads[2, 2]} ║");
            Console.SetCursorPosition(moveX, moveY + 6);
            Console.WriteLine("╚═══╩═══╩═══╝");

            int x = 0;
            int y = 0;
            while (true)
            {
                //Moving the x and y according to the moveX and moveX - its just a ternary operator, its only for moving the curser correct
                //And so that the user cant move the mouse long or less than the actual game height and width
                x = x < moveX + 2 ? moveX + 2 : x > moveX + 10 ? moveX + 10 : x;
                y = y < moveY + 1 ? moveY + 1 : y > moveY + 5 ? moveY + 5 : y;
                Console.SetCursorPosition(x, y);
                //Read the key that is pressed, to move the x and y value correct
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow: y -= 2; break;
                    case ConsoleKey.DownArrow: y += 2; break;
                    case ConsoleKey.LeftArrow: x -= 4; break;
                    case ConsoleKey.RightArrow: x += 4; break;
                    default:
                        //Converting the x and y values, to 0, 1 or 2 to check the array correctly
                        int checkX = x == moveX + 2 ? 0 : x == moveX + 6 ? 1 : x == moveX + 10 ? 2 : 0;
                        int checkY = y == moveY + 1 ? 0 : y == moveY + 3 ? 1 : y == moveY + 5 ? 2 : 0;
                        //Update the values
                        if (overrideToken)
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write(token);
                            plads[checkY, checkX] = token;
                            return;
                        }
                        else
                        {
                            if (plads[checkY, checkX] == EmptySpaceChar)
                            {
                                Console.SetCursorPosition(x, y);
                                Console.Write(token);
                                plads[checkY, checkX] = token;
                                return;
                            }
                            else
                            {
                                Console.SetCursorPosition(x, y);
                                Console.Write(plads[checkY, checkX]);
                            }
                        }
                        break;
                }
            }
        }
        private char? CheckForWinner(char[,] plads)
        {
            //Column winner - Looping through the places, and check if 3 in a column is the same, then return the winner. y is changing to check the values
            for (int y = 0; y < 3; y++)
                if (plads[0, y] != EmptySpaceChar && plads[0, y] == plads[1, y] && plads[1, y] == plads[2, y])
                    return plads[0, y];
            //Row winner - Looping through the places, and check if 3 in a row is the same, then return the winner. x is changing to check the values
            for (int x = 0; x < 3; x++)
                if (plads[x, 0] != EmptySpaceChar && plads[x, 0] == plads[x, 1] && plads[x, 1] == plads[x, 2])
                    return plads[x, 0];
            //Cross winner - Theres only 2 possible ways cross win, this checks for those 2 ways in 2 different if statements
            if (plads[0, 0] != EmptySpaceChar && plads[0, 0] == plads[1, 1] && plads[1, 1] == plads[2, 2])
                return plads[0, 0];
            if (plads[2, 0] != EmptySpaceChar && plads[2, 0] == plads[1, 1] && plads[1, 1] == plads[0, 2])
                return plads[2, 0];
            return null;
        }
        private bool AnyPlacesEmpty(char[,] plads)
        {
            //Loop trough the values and check if any is equal to
            //the 'EmptySpaceChar' if true, then return true, so
            //that the game continues
            //else return false, so that the game wont continue
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    if (plads[y, x] == EmptySpaceChar)
                        return true;
            return false;
        }
        void StartGame()
        {
            //Declare the variable
            char[,] plads = new char[3, 3];
            //Give each place the value of 'EmptySpaceChar'
            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    plads[y, x] = EmptySpaceChar;
            //if null then "Tie" else ... "Won!"
            char? winner;
            //Which player should start token1 (player1) or token2 (player2)?
            char currentUser = Tokens.token1;
            do
            {
                //Clear the console, to make space for the new table, with new and old values
                Console.Clear();
                //Who is it?
                Console.WriteLine(currentUser + "'s tur!");
                //Make the user choose a place to set their token
                MoveInsert(currentUser, ref plads, OverrideTokens, 1, 0);
                //Change the player
                currentUser = currentUser == Tokens.token1 ? Tokens.token2 : Tokens.token1;

                //Check if theres a winner or if its null.
                //If its null and not all places are used,
                //then do the loop else show if its a tie or if someone won
            } while ((winner = CheckForWinner(plads)) == null && AnyPlacesEmpty(plads));

            //Show who won or if its a tie, and move to the correct place to show the message
            Console.SetCursorPosition(0, 9);
            Console.WriteLine(winner == null ? "Tie!" : winner + " Won!");

            Console.ReadKey();
        }
    }
    class Assignments1
    {
        public static void assignment1_1()
        {
            var ans = CM.Ask<float>("Fahrenheit: ", false);
            var cel = Math.Round((ans - 32) * (5 / 9), 2);
            Console.WriteLine($"Celcius: {cel}");
        }
        public static void assignment1_2()
        {
            var ans = CM.Ask<float>("Celcius: ", false);
            var fah = Math.Round((ans * 9 / 5) + 32, 2);
            var kel = Math.Round(ans + 273.15, 2);
            var rea = Math.Round(ans * 0.8, 2);
            Console.WriteLine($"Fharenheit: {fah}");
            Console.WriteLine($"Kelvin: {kel}");
            Console.WriteLine($"Réaumur: {rea}");
        }
        public static void assignment1_3()
        {
            var kurs = CM.Ask<float>("Kurs: ", false);
            var val = CM.Ask<float>("Value: ", false);
            Console.WriteLine($"Dkk: {kurs * val}");
        }
        public static void assignment1_4and1_5()
        {
            var oms = CM.Ask<float>("Omsætning: ", false);
            var varomk = CM.Ask<float>("Variable Omkostninger: ", false);
            var fasomk = CM.Ask<float>("Faste Omkostninger: ", false);
            var daek = oms - varomk;
            var over = daek - fasomk;
            var daekgr = daek / oms * 100;
            var overgr = over / oms;
            Console.WriteLine($"Omsætning - VariableOmkostninger = Dækningsbidrag <=> \n\t{oms} - {varomk} = {daek}");
            Console.WriteLine($"Dækningsbidrag - FasteOmkostninger = Overskud <=> \n\t{daek} - {fasomk} = {over}");
            Console.WriteLine($"(Dækningsbidrag / Omsætning) * 100 = Dækningsgrad <=> \n\t({daek} / {oms}) * 100 = {daekgr}");
            Console.WriteLine($"(Overskud / Omsætning) = Overskudsgrad <=> \n\t({over} / {oms}) = {overgr}");
        }

        public static void assignment2_1and2_2()
        {
            var name = CM.Ask<string>("Name: ", false);
            var age = CM.Ask<int>("Age: ", false);
            string text;
            ConsoleColor color;
            if (age == 1) { text = "Du er lige født"; color = ConsoleColor.Red; }
            else if (age <= 5) { text = "Du kan begynde i børnehave"; color = ConsoleColor.Yellow; }
            else if (age <= 16) { text = "Du går i skole"; color = ConsoleColor.Green; }
            else { text = "Nu begynder alvoren"; color = ConsoleColor.Blue; }
            Console.ForegroundColor = color;
            Console.WriteLine($"Dit navn er {name}, du er {age} år gammel og {text}");
        }
        public static void assignment2_3()
        {
            var age = CM.Ask<int>("Age: ", false);
            var grade = CM.Ask<int>("Age: ", false);
            ConsoleColor color;
            if (((age >= 18 || age <= 24) && (grade >= 1 || grade <= 4)) || ((age >= 25 || age <= 29) && (grade >= 1 || grade <= 5))) { color = ConsoleColor.Red; }
            else if (((age >= 18 || age <= 24) && (grade >= 5 || grade <= 9)) || ((age >= 25 || age <= 29) && (grade >= 6 || grade <= 10))) { color = ConsoleColor.Yellow; }
            else if (((age >= 18 || age <= 24) && (grade >= 10 || grade <= 13)) || ((age >= 25 || age <= 29) && (grade >= 11 || grade <= 13))) { color = ConsoleColor.Green; }
            else { color = ConsoleColor.White; }
            Console.BackgroundColor = color;
            Console.WriteLine($"Alder Karakter");
        }

        public static void assignment3_1()
        {
            var daynum = CM.Ask<int>("Day Number (1-7): ", false);
            var day = daynum switch
            {
                1 => "Monday",
                2 => "Tuesday",
                3 => "Wednesday",
                4 => "Thursday",
                5 => "Friday",
                6 => "Saturday",
                7 => "Sunday",
                _ => "Out of bound",
            };
            Console.WriteLine($"I dag er det {day}");
        }
        public static void assignment3_2()
        {
            Dictionary<string, Func<float, float>> types = new()
            {
                { "f", (val) => {
                    return (float)((val * 9 / 5) + 32);
                }},
                { "k", (val) => {
                    return (float)(val + 273.15);
                }},
                { "r", (val) => {
                    return (float)(val * 0.8);
                }}
            };
            var type_str = CM.Ask<string>("Type: ", false) ?? "";
            if (types.TryGetValue(type_str.ToLower(), out Func<float, float>? type))
            {
                var value = CM.Ask<float>("Celcius: ", false);
                var result = type.Invoke(value);
                Console.WriteLine($"Celsius {value} er lig med {result} {type_str}");
            };
        }
        public static void assignment3_3()
        {
            var val = CM.Ask<int>("Which number: ", false);
            int calc;
            for (int i = 1; (calc = i * val) < 100; i++)
                Console.WriteLine($"{val} * {i} = {calc}");
        }
        public static void assignment4_1()
        {
            var val = (char)196;
            var length = 10;
            var x = CM.Ask<int>("Start X: ", false);
            var y = CM.Ask<int>("Start Y: ", false);
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine(val);
            }
        }
        public static void assignment4_2()
        {
            var val = (char)196;
            var length = 10;
            var x = CM.Ask<int>("Start X: ", false);
            var y = CM.Ask<int>("Start Y: ", false);
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine(val);
                Console.SetCursorPosition(x + i, y + 4);
                Console.WriteLine(val);
            }
        }
        public static void assignment4_3and4_4()
        {
            var val = (char)196;
            var length = 10;
            var x = CM.Ask<int>("Start X: ", false);
            var y = CM.Ask<int>("Start Y: ", false);
            for (int i = 0; i < length; i++)
            {
                Console.SetCursorPosition(x + i, y);
                Console.WriteLine(val);
                Console.SetCursorPosition(x + i, y + 4);
                Console.WriteLine(val);

                Console.SetCursorPosition(x, y + i);
                Console.WriteLine(val);
                Console.SetCursorPosition(x + length, y + i);
                Console.WriteLine(val);
            }
        }
    }
    class Assignments2
    {
        public static string assignment1_sub()
        {
            return "Hello World!";
        }
        public static void assignment1()
        {
            Console.WriteLine(assignment1_sub());
        }

        public static void assignment2_sub(string param)
        {
            Console.WriteLine(param);
        }
        public static void assignment2()
        {
            assignment2_sub(Console.ReadLine() ?? "");
        }

        public static float assignment3_sub(float[] nums)
        {
            return nums.Sum(x => x);
        }
        public static void assignment3()
        {
            var count = 3;
            float[] nums = new float[count];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = CM.Ask<float>($"Value {i + 1} = ", false);
            Console.WriteLine(assignment3_sub(nums));
        }

        public static float assignment3a_sub(float[] nums)
        {
            return nums.Aggregate((first, second) => first - second);
        }
        public static void assignment3a()
        {
            var count = 3;
            float[] nums = new float[count];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = CM.Ask<float>($"Value {i + 1} = ", false);
            Console.WriteLine(assignment3a_sub(nums));
        }

        public static float assignment3b_sub(float[] nums)
        {
            return nums.Aggregate((first, second) => first * second);
        }
        public static void assignment3b()
        {
            var count = 3;
            float[] nums = new float[count];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = CM.Ask<float>($"Value {i + 1} = ", false);
            Console.WriteLine(assignment3b_sub(nums));
        }

        public static float assignment3c_sub(float[] nums)
        {
            return (nums[0] - nums[1]) / nums[2];
        }
        public static void assignment3c()
        {
            var count = 3;
            float[] nums = new float[count];
            for (int i = 0; i < nums.Length; i++)
                nums[i] = CM.Ask<float>($"Value {i + 1} = ", false);
            Console.WriteLine(assignment3c_sub(nums));
        }

        public static void assignment7_sub()
        {

        }
        public static void assignment7()
        {
            var origin = new Point(Console.GetCursorPosition());
            var assignments = typeof(Assignments2).GetMethods().Where(
                x => !x.Name.EndsWith("_sub")
                && x.Name != "GetType"
                && x.Name != "ToString"
                && x.Name != "Equals"
                && x.Name != "GetHashCode"
                && x.Name != "assignment7"
            ).ToArray();
            while (CM.AskKey("Call another method (Y/N)? ", false, false) == ConsoleKey.Y)
            {
                foreach (var item in assignments)
                    Console.WriteLine(item.Name.Replace("assignment", ""));
                Console.SetCursorPosition(origin.X, origin.Y + 1);
                var move = CM.DynMove(new Point(origin.X, origin.Y + 1), new Point(origin.X, origin.Y + assignments.Length));
                Console.Clear();
                var delta_index = Math.Abs(origin.Y - move.Y) - 1;
                assignments[delta_index].Invoke(null, null);
            }
        }

        public static void assignment7a()
        {
            var from = CM.Ask<string>("From: ", false);
            var to = CM.Ask<string>("To: ", false);
            var value = CM.Ask<float>("Value: ", false);
            var celcius = from switch
            {
                "F" => (value - 32) * (5 / 9),
                "K" => (float)(value - 273.15),
                "R" => (float)(1.25 * value),
                // Default is celcius
                _ => value,
            };
            var result = to switch
            {
                "F" => (float)(celcius / (5 / 9) + 32),
                "K" => (float)(value + 273.15),
                "R" => (float)(value / 1.25),
                // Default is celcius
                _ => value,
            };
            Console.WriteLine($"Converted Value: {result}");
        }
        public static void assignment7b()
        {
            var value = CM.Ask<byte>("Value: ", false);
            Console.WriteLine($"Decimal: {value}");
            Console.WriteLine($"Hex: {value:x}");
            Console.WriteLine($"Binary: {value:b}");
        }

    }
    internal class Program
    {
        static void Main(string[] args)
        {
            RealMain();
        }
        static void RealMain()
        {
            Assignments2.assignment7b();
        }
    }
}
