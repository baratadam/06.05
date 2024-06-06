using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Xml.Serialization;

namespace beadandó_;

public  class GeneratorService
{
    Menu Input()
    {
        Menu choice = new Menu();
        bool check = true;

        while (check)
        {
            Console.WriteLine("Choose: [1-Generate] [2-List] [3-Attack] [4-Exit]");

            if (Enum.TryParse(Console.ReadLine(), out choice))
                return choice;
            else
                Console.WriteLine("Wrong data!");
        }
        return choice;
    }


    public async Task Switch()
    {
        var heroList = new List<BasicHero>();
        heroList = FileService.Read();
        Species_Enum species = new Species_Enum();
        var client = new HttpClient();

        bool exit = false;
        while (!exit)
        {
            var choice = Input();
            Random random = new Random();
            switch (choice)
            {
                case Menu.Generate:
                    for (int i = 0; i < 3; i++)
                    {
                        species = (Species_Enum)random.Next(1, 3);
                        var generatedName = await client.GetFromJsonAsync<Rootobject>("https://api.parser.name/?api_key=ded0d2fd47afd87e1293146d0a2e0666&endpoint=generate&country_code=HU");
                        var name = $"{generatedName.data.First().name.lastname.name} {generatedName.data.First().name.firstname.name}";
                        decimal value = 2 / 5;

                        switch (species)
                        {
                            case Species_Enum.Human:
                                var human1 = new Human(GetId(),name,await GetValue(),species);
                                heroList.Add(human1);
                                break;
                            case Species_Enum.Ork:
                                var ork = new Ork(GetId(), name, await GetValue(), species);
                                heroList.Add(ork);
                                break;
                            case Species_Enum.Fairy:
                                var fairy = new Fairy(GetId(), name, await GetValue(), species);
                                heroList.Add(fairy);
                                break;
                        }

                        int GetId()
                        {
                            int id = 0;
                            if (heroList.Count == 0)
                                id = 1;
                            else
                                id = heroList.Count+1;

                            return id;
                        }

                        async Task<decimal> GetValue()
                        {
                            bool check = false;
                            decimal decimalNum = 0;
                            while (!check)
                            {
                                Console.Write("Give a value to your hero (HUF):");
                                if (decimal.TryParse(Console.ReadLine(), out decimalNum))
                                    check = true;
                                else
                                    Console.Write("Wrong format, ");
                            }

                            var client = new HttpClient();
                            var xmlResult = await client.GetStringAsync("http://api.napiarfolyam.hu?bank=mnb&valuta=eur&datum=20240606");
                            var serializer = new XmlSerializer(typeof(Arfolyamok));
                            Arfolyamok result;

                            using TextReader reader = new StringReader(xmlResult);
                            result = (Arfolyamok)serializer.Deserialize(reader);

                            return decimalNum / result.Deviza.Item.Kozep.First();
                        }
                    }
                    break;
                case Menu.List:
                    if (heroList.Count == 0)
                        Console.WriteLine("The list is empty");
                    else
                        listHeroes();

                    void listHeroes()
                    {
                        foreach (var hero in heroList)
                        {
                            if (hero is Human human)
                                Console.WriteLine($"ID:{hero.Id} Name: {hero.Name} Species: {hero.Species}, Hp: {hero.Hp} Shield: {hero.Shield} Power: {hero.Power} Speed: {hero.Speed} Stamina: {hero.Stamina} ExtraStamina: {human.ExtraStamina} Value: {Math.Round(hero.Value, 2)}, Attack : {human.ExtraAttack()}");
                            else if (hero is Ork ork)
                                Console.WriteLine($"ID:{hero.Id} Name: {hero.Name} Species: {hero.Species}, Hp: {hero.Hp} Shield: {hero.Shield} Power: {hero.Power} Speed: {hero.Speed} Stamina: {hero.Stamina} ExtraStamina: {ork.ExtraPower} Value: {Math.Round(hero.Value, 2)}, Attack : {ork.ExtraAttack()}");
                            else if (hero is Fairy fairy)
                                Console.WriteLine($"ID:{hero.Id} Name: {hero.Name} Species: {hero.Species}, Hp: {hero.Hp} Shield: {hero.Shield} Power: {hero.Power} Speed: {hero.Speed} Stamina: {hero.Stamina} ExtraStamina: {fairy.ExtraSpeed} Value: {Math.Round(hero.Value, 2)}, Attack : {fairy.ExtraAttack()}");
                        }
                    }
               
                    break;
                case Menu.Attack:
                    //int parse
                    Console.WriteLine("First fighter ID: ");
                    var first = int.Parse(Console.ReadLine());

                    Console.WriteLine("Second fighter ID: ");
                    var second = int.Parse(Console.ReadLine());

                    var firstFighter = heroList.FirstOrDefault(hero=>hero.Id == first);
                    var secondFighter = heroList.FirstOrDefault(hero => hero.Id == second);

                    var attack = 0;
                    if (firstFighter is Human human)
                        attack = human.ExtraAttack();
                    else if (firstFighter is Ork ork)
                        attack = ork.ExtraAttack();
                    else if (firstFighter is Fairy fairy)
                        attack = fairy.ExtraAttack();

                    if(attack > secondFighter.Shield )
                        Console.WriteLine("The first fighter won.");
                    else
                        Console.WriteLine("The second fighter won.");
                    break;
                case Menu.Exit:
                    FileService.Write(heroList);
                    exit = true;
                    break;
            }
        }
    }
}
