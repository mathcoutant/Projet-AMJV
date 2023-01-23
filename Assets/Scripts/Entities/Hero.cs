using UnityEngine;

public class Hero : Entity
{
    public static string nameClass = "Hero";
    public static int waveReached = 0;
    public static int timesPlayed = 0;
    public static bool hasWon = false;
    public float speed = 20f;
    private PopupManager popupManager;
    protected Animator animator;
    protected PlayerController playerController;
    protected Rigidbody rigidbody;
    protected HeroState state = HeroState.STATE_MOVE;
    public int xpPoints = 0;
    public int nextLevelXpPoints;
    [SerializeField] int level = 0;

    protected enum HeroState
    {
        STATE_MOVE,
        STATE_STUN,
    }
    protected virtual void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        popupManager = FindObjectOfType<PopupManager>();
        CalculateRequiredXpPoints();
    }


    private void CalculateRequiredXpPoints()
    {
         nextLevelXpPoints = (level + 1) * (level + 1);
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

    public void Move(Vector3 input)
    {
        if (state == HeroState.STATE_MOVE)
        {
            rigidbody.velocity = input * (speed * Time.deltaTime);
            input = Quaternion.LookRotation(transform.forward, Vector3.up) * input;
            animator.SetFloat("moveY",input.x);
            animator.SetFloat("moveX",input.z);
        }
    }

    public void Rotate(Quaternion rotation)
    {
        transform.rotation = rotation;
    }

    public void IncrementXP()
    {
        xpPoints++;
        if (xpPoints >= nextLevelXpPoints)
        {
            xpPoints -= nextLevelXpPoints;
            CalculateRequiredXpPoints();
            level++;
            popupManager.DisplayUpgradePopup();
        }
    }

    public virtual void ApplyUpgrade(string upgrade)
    {
        switch (upgrade)
        {
            case "Speed Upgrade":
                speed += 20f;
                break; 
            case "Cooldown Reduction 1":
                playerController.cooldownAction1 *= 0.95f;
                break;
            case "Cooldown Reduction 2":
                playerController.cooldownAction2 *= 0.95f;
                break;
            case "Cooldown Reduction 3":
                playerController.cooldownAction3 *= 0.95f;
                break;
            case "Health Up":
                maxHealth += 5;
                health = maxHealth;
                break;
        }
    }
}