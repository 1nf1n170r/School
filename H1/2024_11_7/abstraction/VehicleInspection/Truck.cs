public class Truck : Vehicle
{
    public override string DisplayInfo()
    {
        return $"Truck: {Brand} {Model}";
    }
    public override string InspectionStatus()
    {
        var oneYearAgo = DateTime.Now.AddYears(-1);
        if (ProductionDate < oneYearAgo && LastInspection < oneYearAgo)
            return "Inspect it!";
        return "Youre good!";
    }

    public Truck(string brand, string model, DateTime production, DateTime lastinspection)
    : base(brand, model, production, lastinspection)
    {

    }

    public override void SetTireType(bool isWinterTire)
    {
        MaxRimSize = isWinterTire ? 17 : 20;
    }

}




