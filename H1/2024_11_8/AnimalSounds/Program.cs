var cat = AnimalFactory.CreateAnimal(EnumAnimal.Cat, "CatName");
var dog = AnimalFactory.CreateAnimal(EnumAnimal.Dog, "DogName");
var sheep = AnimalFactory.CreateAnimal(EnumAnimal.Sheep, "SheepName");

cat.MakeSound();
dog.MakeSound();
sheep.MakeSound();
