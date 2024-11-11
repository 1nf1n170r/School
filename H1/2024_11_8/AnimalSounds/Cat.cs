public class Cat : Animal
{
    public Cat(string name) : base(name)
    {

    }
    public override string MakeSound()
    {
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\cat.wav")).PlaySync();
        return "Meau";
    }
}
