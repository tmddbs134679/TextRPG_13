using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_13
{
    public class UIManager
    {

        public void PrintRandomMonster(Monster monster)
        {
            Console.WriteLine($"Lv.{monster.Stats.Lv} " +
                $"{monster.Stats.monsterName} " +
                $"HP {monster.Stats.monsterHP}");
        }

        public static void PrintEnemyPhase(Monster monster,int randomDamage) 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Battle!!\n");
            Console.ResetColor();




            Console.WriteLine($"Lv.{monster.Stats.Lv} {monster.Stats.monsterName}의 공격! ");
            Console.WriteLine($"을(를) 맞췄습니다. [데미지: {randomDamage}]\n");
            Console.WriteLine("HP {player} -> {player.health}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>", Color.DarkOrange);
        }

        public static void PrintPlayerLose()
        {
            Console.Clear();
            Console.WriteLine("You Lose\n",Color.Red);
            Console.ResetColor();

            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}\n");
            Console.WriteLine("\n0.다음");
            Console.Write(">>",Color.DarkOrange);
        }

        public static void PrintPlayerVitory(int maxMonster) 
        {
            Console.Clear();
            Console.WriteLine("Vicoty\n", Color.DarkOliveGreen);
            Console.ResetColor();

            Console.WriteLine($"던전에서 몬스터 {maxMonster}마리를 잡았습니다.");
            Console.WriteLine("Lv.{player.level} {player.name}");
            Console.WriteLine("HP{player.maxHP} -> {player.HP}");
            Console.WriteLine("\n0.다음");
            WriteColor(">>",Orange);
        }
        public static void WriteColor(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor ();
        }
    }
}
