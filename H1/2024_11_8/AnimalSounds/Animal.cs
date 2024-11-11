public abstract class Animal
{
    public string Name { get; set; }
    public abstract string MakeSound();
    public Animal(string name)
    {
        Name = name;
    }

}
