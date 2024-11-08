public class Dog : Animal
{
    public Dog(string name) : base(name)
    {

    }
    public override void MakeSound()
    {
        System.Console.WriteLine("Vow");
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\dog.wav")).PlaySync();
    }
}
