public interface IWheels
{
    public int MaxRimSize { get; set; }

    public abstract void SetTireType(bool isWinterTire);

    public string Info()
    {
        return "Brug mig for alle objeckter som køres på hjul.";
    }
}
