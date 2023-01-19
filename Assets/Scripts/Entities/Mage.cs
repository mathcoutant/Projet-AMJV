using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : Hero
{
    new public static string nameClass = "Mage";
    new public static int waveReached = 0;
    new public static int timesPlayed = 0;
    new static public bool hasWon = false;

    public Mage()
    {
        maxHealth = 20;
        health = maxHealth - 5;
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
