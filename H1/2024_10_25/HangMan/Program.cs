
internal class Program
{
    static async Task Main()
    {
        var word = await lib.WebScraper.YankDatRandWord();
        var game_word = "".PadRight(word.Length, '_').ToCharArray();
        uint wrong_guesses = 0;
        char c;
        Console.WriteLine("Try geussing: ");
        while ((c = Console.ReadKey(true).KeyChar.ToString().ToLower().ToCharArray()[0]) != '\0' && word.Select((c, i) => new { Char = c, Index = i }).Where(x => x.Char.ToString().ToLower().ToCharArray()[0] == c).Select(x => x.Index).ToArray().Select(v => (char)v).All(i => { game_word[i] = word[i]; return true; }) && (word.Contains(c) || (++wrong_guesses) < 5))
            Console.WriteLine(game_word);
    }
}
