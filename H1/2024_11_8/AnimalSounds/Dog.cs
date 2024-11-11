public class Dog : Animal
{
    public Dog(string name) : base(name)
    {

    }
    public override string MakeSound()
    {
        new System.Media.SoundPlayer(Path.Combine(Environment.CurrentDirectory, @"assets\dog.wav")).PlaySync();
        return "Vow";
    }
}
