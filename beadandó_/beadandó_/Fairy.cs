namespace beadandó_;
public class Fairy: BasicHero, ISpieces
{
    public Fairy(int Id, string Name, decimal Value, Species_Enum species) : base(Id, Name, Value, species)
    { 
        Random random = new Random();
        ExtraSpeed = random.Next(1, 100);
    }
    public int ExtraSpeed { get; set; }

    public int ExtraAttack()
    {
        return Attack() + (2* ExtraSpeed);
    }
}
