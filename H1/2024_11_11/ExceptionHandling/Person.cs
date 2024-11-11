class Person(string fornavn, string efternavn)
{
    public string Fornavn { get; set; } = fornavn;
    public string Efternavn { get; set; } = efternavn;
    public string FullName => $"{this.Fornavn} {this.Efternavn}";
}
