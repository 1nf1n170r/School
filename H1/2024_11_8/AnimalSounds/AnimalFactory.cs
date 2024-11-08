public static class AnimalFactory
{
    public static Animal CreateAnimal(EnumAnimal animal, string name)
    {
        return animal switch
        {
            EnumAnimal.Dog => new Dog(name),
            EnumAnimal.Cat => new Cat(name),
            EnumAnimal.Sheep => new Sheep(name),
        };
    }
}
