using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    Hero player;
    List<Tuple<string,string>> upgrades = new List<Tuple<string, string>>();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Hero>();
        upgrades.Add(new Tuple<string, string>("Speed Upgrade", "Boost your hero's speed."));
        upgrades.Add(new Tuple<string, string>("Cooldown Reduction 1", "You regain your action 1 faster."));
        upgrades.Add(new Tuple<string, string>("Cooldown Reduction 2", "You regain your action 2 faster."));
        upgrades.Add(new Tuple<string, string>("Cooldown Reduction 3", "You regain your action 3 faster."));
        upgrades.Add(new Tuple<string, string>("Health Up", "Heals you and increase your maximum health."));

        if (player.GetType() == typeof(Warrior))
        {
            upgrades.Add(new Tuple<string, string>("Sword Damage Up", "Your Sword deals more damage."));
            upgrades.Add(new Tuple<string, string>("Jump Damage Up", "Your jump deals more damage."));
            upgrades.Add(new Tuple<string, string>("Spin Damage Up", "Spinning deals more damage."));
            upgrades.Add(new Tuple<string, string>("Jump range up", "Your action 1 deals more damage. jump (action 2) goes farther."));
            upgrades.Add(new Tuple<string, string>("Extended damage zone", "The damage zone of your action 3 is larger."));
        }
        if (player.GetType() == typeof(Mage))
        {
            upgrades.Add(new Tuple<string, string>("Fireball Damage Up", "Your fireball deals more damage."));
            upgrades.Add(new Tuple<string, string>("Knockback Orb", "Your orb knockbacks enemies."));
            upgrades.Add(new Tuple<string, string>("Resistant Orb", "Your orb can touch more enemies before disappearing."));
            upgrades.Add(new Tuple<string, string>("Wall Size Up", "Your ice wall is bigger."));
            upgrades.Add(new Tuple<string, string>("Wall Damage", "Spawning your ice wall will damage enemies."));
        }
        if (player.GetType() == typeof(Rogue))
        {
            upgrades.Add(new Tuple<string, string>("Poisoned Knife", "Your knife poisons enemies."));
            upgrades.Add(new Tuple<string, string>("Throwing Knife Speed Up", "You throw your knife faster."));
            upgrades.Add(new Tuple<string, string>("Throwing Knife Damage Up", "Throwing your knife deals more damage."));
            upgrades.Add(new Tuple<string, string>("More Confusion", "The smoke bomb stuns enemies longer."));
            upgrades.Add(new Tuple<string, string>("More Smoke", "The smoke bomb is larger."));
        }
        if (player.GetType() == typeof(Hero))
        {
            Debug.Log("Problem");
        }
    }

    public Tuple<string, string>[] GetPossibleUpgrades()
    {
        Tuple<string, string>[] possibleUpgrades = new Tuple<string, string>[3];
        int index1 = UnityEngine.Random.Range(0, upgrades.Count);
        int index2 = UnityEngine.Random.Range(0, upgrades.Count);
        while (index1 == index2)
        {
            index2 = UnityEngine.Random.Range(0, upgrades.Count);
        }
        int index3 = UnityEngine.Random.Range(0, upgrades.Count);
        while (index3 == index2 || index3 == index2)
        {
            index3 = UnityEngine.Random.Range(0, upgrades.Count);
        }

        possibleUpgrades[0] = upgrades[index1];
        possibleUpgrades[1] = upgrades[index2];
        possibleUpgrades[2] = upgrades[index3];
        return possibleUpgrades;
    }
}
