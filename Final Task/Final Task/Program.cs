public class Program()
{
    private static void Main()
    {
        var saveService = new FileSystemSaveLoadService("GameSaves");

        Console.WriteLine("Введите имя пользователя!");
        string userName = Console.ReadLine();

        GameProfil profil = LoadOrCreateProfile(saveService, userName);
        GameBalanceChecker balanceChecker = new GameBalanceChecker(profil);


        while (true)
        {

            if (!balanceChecker.CheckBalance(profil))
            {
                saveService.SaveData(profil.GetSaveData(), userName);
                break;
            }

            Console.WriteLine("\n=========================");
            Console.WriteLine("\nВыберите игру в которую хотели бы сыграть 1 - кости, 2 - БлэкДжек, 3 показать текущи баланс");

            string userEnter = Console.ReadLine();

            if (userEnter == "1")
            {
                StartDiceGame startDiceGame = new StartDiceGame(profil);
                saveService.SaveData(profil.GetSaveData(), userName);
                continue;
            }
            if (userEnter == "2")
            {
                StartPlayBlackJack startPlayBlackJack = new StartPlayBlackJack(profil);
                saveService.SaveData(profil.GetSaveData(), userName);
                continue;
            }
            if (userEnter == "3")
            {
                profil.ShowBalance();
                continue;
            }
            else
            {
                Console.WriteLine("Некорректный ввод!");
            }
        }
    }

    private static GameProfil LoadOrCreateProfile(ISaveLoadService<string> saveService, string userName)
    {
        string savedData = saveService.LoadData(userName);

        if (!string.IsNullOrEmpty(savedData))
        {
            Console.WriteLine("Профиль загружен!");
            return GameProfil.FromSaveData(savedData);
        }
        else
        {
            Console.WriteLine("Создан новый профиль!");
            return new GameProfil(userName);
        }

    }
}