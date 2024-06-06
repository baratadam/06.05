
namespace beadandó_;
public class Human : BasicHero, ISpieces
{
    public Human(int Id, string Name, decimal Value, Species_Enum species) : base(Id, Name, Value, species)
    { 
        Random random = new Random();
        ExtraStamina = random.Next(1, 100);
    }
    public int ExtraStamina { get; set; }

    public int ExtraAttack()
    {
        return Attack() + 50;
    }
}

