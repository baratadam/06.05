using beadandó_;
public class Ork : BasicHero, ISpieces
{
    public Ork(int Id, string Name, decimal Value, Species_Enum species) : base(Id, Name, Value, species)
    {
        Random random = new Random();
        ExtraPower = random.Next(1, 100);
    }
    public int ExtraPower { get; set; }

    public int ExtraAttack()
    {
        return Attack() + (2 * ExtraPower);
    }
}
