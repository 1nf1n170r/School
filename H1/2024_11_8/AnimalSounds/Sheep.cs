public class Sheep : Animal
{
    public Sheep(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        System.Console.WriteLine("Baaaaa");
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\sheep.wav")).PlaySync();
    }
}
