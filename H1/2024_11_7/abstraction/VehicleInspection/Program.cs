string carBrand = "Audi";
string carModel = "A6";
var carProductionDate = new DateTime(2018, 7, 1);
var carLastInspection = new DateTime(2022, 12, 8);
Vehicle car = new Car(carBrand, carModel, carProductionDate, carLastInspection);

string truckBrand = "Audi";
string truckModel = "A6";
var truckProductionDate = new DateTime(2018, 7, 1);
var truckLastInspection = new DateTime(2022, 12, 8);
Vehicle truck = new Truck(truckBrand, truckModel, truckProductionDate, truckLastInspection);

car.SetTireType(true);
Console.WriteLine(car.DisplayInfo());
Console.WriteLine(car.InspectionStatus());
Console.WriteLine($"Max rim size: {car.MaxRimSize}");
Console.WriteLine();
truck.SetTireType(true);
Console.WriteLine(truck.DisplayInfo());
Console.WriteLine(truck.InspectionStatus());
Console.WriteLine($"Max rim size: {truck.MaxRimSize}");

System.Console.WriteLine(car.GetInterfaceInfo());


