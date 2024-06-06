using System.Security.Cryptography;

namespace beadandó_;

public static class FileService
{
    public static void Write(List<BasicHero> heroList)
    {

        var newRow = "";
        var rows = new List<string>();

        foreach (var hero in heroList)
        {
            newRow = $"{hero.Id},{hero.Name},{hero.Hp},{hero.Shield},{hero.Power},{hero.Speed},{hero.Stamina},{hero.Species},{Math.Round(hero.Value,2)}";
            rows.Add(newRow);
        }
        File.WriteAllLines("Heroes.csv", rows); 
    }

    public static List<BasicHero> Read()
    {
        string path = "Heroes.csv";
        var heroList = new List<BasicHero>();

        if (File.Exists(path))
        {
            var fileRows = File.ReadLines(path);

            foreach (var row in fileRows)
            {
                var data = row.Split(',');

                Species_Enum species = new Species_Enum();
                Enum.TryParse(data[7], out species);

                switch (species)
                {
                    case Species_Enum.Human:
                        var human = new Human(int.Parse(data[0]), data[1], decimal.Parse(data[8]), species);
                        heroList.Add(human);
                        break;
                    case Species_Enum.Fairy:
                        var fairy = new Fairy(int.Parse(data[0]), data[1], decimal.Parse(data[8]), species);
                        heroList.Add(fairy);
                        break;
                    case Species_Enum.Ork:
                        var ork = new Ork(int.Parse(data[0]), data[1], decimal.Parse(data[8]), species);
                        heroList.Add(ork);
                        break;
                }
            }
        }
        return heroList;
    }
}
