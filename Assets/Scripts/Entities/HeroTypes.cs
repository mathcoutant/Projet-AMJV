using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeroTypes
{
    public class Warrior : Hero
    {
        new public static string nameClass = "Warrior";
        new public static int maxHealth = 50;
        new public static int waveReached = 0;
        new public static int timesPlayed = 0;
        new public static bool hasWon = false;

        new public int health = Warrior.maxHealth;

        new public void Action1()
        {

        }
        new public void Action2()
        {

        }
        new public void Action3()
        {

        }
    }

    public class Mage : Hero
    {
        new public static string nameClass = "Mage";
        new public static int maxHealth = 20;
        new public static int waveReached = 0;
        new public static int timesPlayed = 0;
        new public static bool hasWon = false;

        new public int health = Mage.maxHealth;

        new public void Action1()
        {

        }
        new public void Action2()
        {

        }
        new public void Action3()
        {

        }
    }

    public class Archer : Hero
    {
        new public static string nameClass = "Archer";
        new public static int maxHealth = 30;
        new public static int waveReached = 0;
        new public static int timesPlayed = 0;
        new public static bool hasWon = false;

        new public int health = Archer.maxHealth;

        new public void Action1()
        {

        }
        new public void Action2()
        {

        }
        new public void Action3()
        {

        }
    }
}
