using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Hero
{
    new public static string nameClass = "Warrior";
    new public static int waveReached = 0;
    new public static int timesPlayed = 0;
    new static public bool hasWon = false;

    public Warrior()
    {
        maxHealth = 50;
        health = maxHealth;
    }
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
