namespace beadandó_;

public abstract class BasicHero
{
    public BasicHero(int Id, string Name, decimal Value, Species_Enum species)
    {
        Random rand = new Random();
        Hp = rand.Next(0, 100);
        Shield = rand.Next(0, 100);
        Power = rand.Next(0, 100);
        Speed = rand.Next(0, 100);
        Stamina = rand.Next(0, 100);

        this.Id = Id;
        this.Name = Name;
        this.Value = Value;
        this.Species = species;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public int Hp { get; set; }
    public int Shield { get; set; }
    public int Power { get; set; }
    public int Speed { get; set; }
    public int Stamina { get; set; }
    public decimal Value { get; set; }
    public Species_Enum Species { get; set; }

    public int Attack()
    {
        return Speed + Power;
    }

}
