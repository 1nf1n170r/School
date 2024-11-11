public class Sheep : Animal
{
    public Sheep(string name) : base(name)
    {

    }
    public override string MakeSound()
    {
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\sheep.wav")).PlaySync();
        return "Baaaaa";
    }
}
