using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Entity : MonoBehaviour
{
    //Base Class for enemies and player class with a bunch of functions for doing damage
    [SerializeField] Stats stats;
    [SerializeField] BaseStats basestats;

    void Start()
    {
        stats.InitializeStats(basestats);
    }

    //Deals damage based on ability modifer and power stats
    public void AbilityDamage(float abilitymod, Entity target)
    {
        target.ReceiveDamage(stats.power * abilitymod);
    }

    //used for general damage from environ or ramming, where damage is not calculated here
    public void Damage(float amount, Entity target)
    {
        target.ReceiveDamage(amount);
    }

    //Recieves all damage here which is reduced by armor, if the damage would kill it then calls Die
    public void ReceiveDamage(float damage)
    {
        stats.currenthp -= (damage - stats.armor);
        if (stats.currenthp <= 0.0f)
        {
            Die();
        }
    }

    private void Update()
    {
        
    }

    //play Dying Anims
    //Default just destorys gameobject
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
