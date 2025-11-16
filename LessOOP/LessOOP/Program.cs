public class Program
{
    private static void Main()
    {
        Character[] team1 = new Character[] { new Warrior("Бронированный пидор", 1, 50, 6, 1) };
        Character[] team2 = new Character[] { new Mage("Волшебный хуй", 1, 50, 5, 100) };

        BattleManager battleStart = new BattleManager();

        battleStart.Battle(team1, team2);
    }
}