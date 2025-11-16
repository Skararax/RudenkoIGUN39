using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class BattleManager
{
    private Random _random = new Random();
    private StatesManager _statesManager = new StatesManager();

    public void Battle(Character[] teamA, Character[] teamB)
    {
        Console.WriteLine("СТАРТ БИТВЫ");

        int round = 1;

        while (IsTeamAlive(teamB) && IsTeamAlive(teamA)) 
        {
            Console.WriteLine($"Раунд {round}!");

            ExecuteTeamAttack(teamA, teamB, "Команда А");

            ExecuteTeamAttack(teamB, teamA, "Команда В");

            round++;

            Console.WriteLine("Нажмите Enter чтобы продолжить!");
            Console.ReadLine();
        }

        WhoIsAWinner(teamA, teamB);
    }

    private void ExecuteTeamAttack(Character[] attackers, Character[] defenders, string name) 
    {
        foreach (var attacker in attackers)
        {
            if (!attacker.IsAlive) continue;

            var aliveTargets = defenders.Where(t => t.IsAlive).ToArray();
            if (aliveTargets.Length == 0) break;

            States state = _statesManager.DecideAction(attacker);
            Character target = aliveTargets[_random.Next(0, aliveTargets.Length)];

            _statesManager.PickState(state, attacker, target);
        }
    }

    private void WhoIsAWinner(Character[] teamA, Character[] teamB) 
    {
        if (!IsTeamAlive(teamA))
        {
            foreach (Character i in teamA) 
            {
                Console.WriteLine($"Команда {i.Name} погибла!");
            }

        }

        if (!IsTeamAlive(teamB)) 
        {
            foreach (Character i in teamB)
            {
                Console.WriteLine($"Команда {i.Name} погибла!");
            }
        }
    }

    private bool IsTeamAlive(Character[] team)
    {
        return team.Any(t => t.IsAlive);
    }
}
