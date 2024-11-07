public class Measurement
{
    public float BMI { get; set; }

    public Measurement(float weight, float height)
    {
        BMI = weight / (height * 2);
    }

}
