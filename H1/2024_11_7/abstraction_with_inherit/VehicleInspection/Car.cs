public class Car : Vehicle
{
    public override string DisplayInfo()
    {
        return $"Car: {Brand} {Model}";
    }
    public override string InspectionStatus()
    {
        if (ProductionDate < DateTime.Now.AddYears(-4) && LastInspection < DateTime.Now.AddYears(-2))
            return "Inspect it!";
        return "Youre good!";
    }
    public Car(string brand, string model, DateTime production, DateTime lastinspection)
    : base(brand, model, production, lastinspection)
    {
    }

    public override void SetTireType(bool isWinterTire)
    {
        MaxRimSize = isWinterTire ? 16 : 22;
    }
}
