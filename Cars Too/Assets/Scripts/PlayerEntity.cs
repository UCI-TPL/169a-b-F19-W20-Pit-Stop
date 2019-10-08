using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEntity : Entity
{
    CarStats cs;

    [SerializeField] int carParts;
    bool boostUnlocked = false;
    bool hackUnlocked = false;
    bool jumpUnlocked = false;
    bool missilesUnlocked = false;
    

    //initializes stats based off of the stats stored in carstats
    void Start()
    {
        /*
        cs = GameObject.FindObjectOfType<CarStats>();
        stats = cs.stats;
        cs.SpawnEquippedAbilities(this.gameObject);
        */

    }

    //Add Exp here after defeating enemies, if a levelup occurs, propagates it to the stats and abilities on carstats
    //potentially add ui popups of levelupbonuses here
    public void AddExp(float amt)
    {
        bool levelup=cs.exp.AddExp(amt);
        if (levelup)
        {
            int level = cs.exp.level-1;
            stats.UpgradeStats(cs.carinfo.levelbonuses[level]);
            cs.stats.UpgradeStats(cs.carinfo.levelbonuses[level]);
            cs.abils.UpgradeandUnlock(cs.carinfo.levelbonuses[level]);
        }
    }
    
    //Send Game Over message and stop the game
    public override void Die()
    {
        Debug.Log("Player has Died");
    }

    public void CollidedEnemy(GameObject enemy, string enemyposition, string playerposition)
    {
        
        //player and enemy speed calculated from vector 3s
        float playerspeed = Mathf.Sqrt(Mathf.Pow(this.gameObject.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(this.gameObject.GetComponent<Rigidbody>().velocity.z, 2));
        float enemyspeed =  Mathf.Sqrt(Mathf.Pow(enemy.GetComponent<Rigidbody>().velocity.x, 2) + Mathf.Pow(enemy.GetComponent<Rigidbody>().velocity.z, 2));
        float sidemultiplier = 1.5f; //damage multiplier when hit from the side
        float backmultiplier = 2.0f; // damage multiplier when hit from behind
        float frontmultiplier = 1.0f; //damage multiplier in a headon collision

        Debug.Log(playerspeed * (stats.weight * .1f) + "   " + enemyspeed * (stats.weight * .1f));
        Debug.Log("Player HP: " + stats.currenthp);
        //handles headon collision
        if (enemyposition == "front" && playerposition == "front")
        {
            Debug.Log(playerspeed * (stats.weight * .1f) + "   "+ enemyspeed * (stats.weight * .1f));
            //Deals damage to each based on the weight stat, and the velocity of the other
            Damage(frontmultiplier*playerspeed*(stats.weight*.1f), enemy.GetComponent<Entity>());
            Damage(frontmultiplier * enemyspeed * (stats.weight * .1f), this);
        }
        //handles front/side collision
        else if(enemyposition=="front"&& playerposition == "side")
        {
            Damage(sidemultiplier * playerspeed * (stats.weight * .1f), enemy.GetComponent<Entity>());
            Damage(frontmultiplier * enemyspeed * (stats.weight * .1f), this);
        }
        //handles front/back collision
        else if (enemyposition == "front" && playerposition == "back")
        {
            Damage(backmultiplier * playerspeed * (stats.weight * .1f), enemy.GetComponent<Entity>());
            Damage(frontmultiplier * enemyspeed * (stats.weight * .1f), this);
        }
        //handles side/front collision
        else if (enemyposition == "side" && playerposition == "front")
        {
            Damage(frontmultiplier * playerspeed * (stats.weight * .1f), enemy.GetComponent<Entity>());
            Damage(sidemultiplier * enemyspeed * (stats.weight * .1f), this);
        }
        //handles back/front collision
        else if (enemyposition == "back" && playerposition == "front")
        {
            Damage(frontmultiplier * playerspeed * (stats.weight * .1f), enemy.GetComponent<Entity>());
            Damage(backmultiplier * enemyspeed * (stats.weight * .1f), this);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("CarPart"))
        {
            carParts += other.GetComponent<CarPart>().GetValue();
            Destroy(other.gameObject);
        }
    }

    
}
