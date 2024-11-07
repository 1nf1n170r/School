public abstract class Vehicle : IWheels
{
    public int MaxRimSize { get; set; }
    public string Brand { get; init; }
    public string Model { get; init; }
    public DateTime ProductionDate { get; init; }
    public DateTime LastInspection { get; init; }

    // Must be overriden - Should not have a body, since that body must be overriden
    public abstract string InspectionStatus();

    // Doesnt have to be overriden
    public virtual string DisplayInfo()
    {
        return $"Brand: {Brand}, Model: {Model}, Production Date: {ProductionDate.ToShortDateString()}";
    }

    public Vehicle(string brand, string model, DateTime production, DateTime lastinspection)
    {
        Brand = brand;
        Model = model;
        ProductionDate = production;
        LastInspection = lastinspection;
    }



    public string GetInterfaceInfo()
    {
        IWheels vehicle = this;
        return vehicle.Info();
    }

    public abstract void SetTireType(bool isWinterTire);
}
