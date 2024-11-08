public class Cat : Animal
{
    public Cat(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        System.Console.WriteLine("Meau");
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\cat.wav")).PlaySync();
    }
}
