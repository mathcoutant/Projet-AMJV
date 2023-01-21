using System;
using UnityEngine;

public class Hero : Entity
{
    public static string nameClass = "Hero";
    public static int waveReached = 0;
    public static int timesPlayed = 0;
    public static bool hasWon = false;
    protected Rigidbody rigidbody;
    protected HeroState state = HeroState.STATE_MOVE;
    
    protected enum HeroState
    {
        STATE_MOVE,
        STATE_STUN,
    }
    protected virtual void Start()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    public virtual void Action1()
    {
    }
    
    public virtual void Action2()
    {
    }

    public virtual void Action3()
    {
    }

    public void Move(Vector3 velocity)
    {
        if(state == HeroState.STATE_MOVE) rigidbody.velocity = velocity;
    }

    public void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }
}