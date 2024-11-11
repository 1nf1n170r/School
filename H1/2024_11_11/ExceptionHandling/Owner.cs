class Owner(string fornavn, string efternavn, uint id)
  : Person(fornavn, efternavn)
{
    public uint Id { get; set; } = id;
}

