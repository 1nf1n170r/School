var cat = AnimalFactory.CreateAnimal(EnumAnimal.Cat, "CatName");
var dog = AnimalFactory.CreateAnimal(EnumAnimal.Dog, "DogName");
var sheep = AnimalFactory.CreateAnimal(EnumAnimal.Sheep, "SheepName");

Console.WriteLine(cat.MakeSound());
Console.WriteLine(dog.MakeSound());
Console.WriteLine(sheep.MakeSound());
