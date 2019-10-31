using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace task_01
{
    class Game
    {
        static int _playerHealth = 120;
        static int _playerAttack = 10;
        static int _playerAttackDmg;
        static string _playerChoice;
        static string _playerName;

        static int _monsterHealth = 100;
        static int _monsterAttack = 5;
        static int _monsterAttackDmg;
        static string _monsterChoice;

        static Random _rand = new Random();
        static void Main(string[] args)
        {
            Welcome();
            FightingLoop();
        }

        private static void Welcome()
        {
            Console.WriteLine("Welcome to Andrew's Text Based Game Thing!");

            Console.WriteLine("What's your name?");
            _playerName = Console.ReadLine();
            Console.WriteLine("Hello, " + _playerName);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("A monster is approaching... Defend yourself!");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private static void FightingLoop()
        {
            int monsterChoiceNumber;

            do
            {
                Console.WriteLine("You have " + _playerHealth + " health left");
                Console.WriteLine("The monster has " + _monsterHealth + " health left");

                Console.WriteLine("Will you (A)ttack or (D)efend? ");
                _playerChoice = Console.ReadLine();
                Console.WriteLine();

                monsterChoiceNumber = _rand.Next(0, 2);
                if (monsterChoiceNumber <= 0.5) _monsterChoice = "A";
                else _monsterChoice = "D";

                CalculateAttackDmg();

                _playerHealth -= _monsterAttackDmg;
                _monsterHealth -= _playerAttackDmg;

                Console.WriteLine("You hit the monster by {0} damage", _playerAttackDmg);
                Console.WriteLine("You took {0} damage from the monster!", _monsterAttackDmg);
            } while (_playerHealth > 0 && _monsterHealth > 0);

            if (_monsterHealth <= 0) Console.WriteLine("You have killed the monster!");
            else if (_playerHealth <= 0) Console.WriteLine("The monster has killed you");
            Console.ReadLine();
        }

        private static void CalculateAttackDmg()
        {
            if (_playerChoice == "A" && _monsterChoice == "A")
            {
                _playerAttackDmg = _playerAttack * _rand.Next(1, 5);
                _monsterAttackDmg = _monsterAttack * _rand.Next(1, 5);
            }
            else if (_playerChoice == "A" && _monsterChoice == "D")
            {
                _playerAttackDmg = _playerAttack * _rand.Next(1, 5) / 2;
                _monsterAttackDmg = 0;
            }
            else if (_playerChoice == "D" && _monsterChoice == "D")
            {
                _playerAttackDmg = 0;
                _monsterAttackDmg = 0;
            }
            else if (_playerChoice == "D" && _monsterChoice == "A")
            {
                _playerAttackDmg = 0;
                _monsterAttackDmg = _monsterAttack * _rand.Next(1, 5) / 2;
            }
            else
            {
                _playerAttackDmg = 0;
                _monsterAttackDmg = 0;
            }
        }
    }
}