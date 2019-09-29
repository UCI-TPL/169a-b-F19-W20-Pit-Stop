using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public string part= "side"; //denotes the part of the enemy's car collided with
    public GameObject enemy;//The enemy gameobject
    public bool collidedrecently=false; //checks whether the enemy has collided recently with the player
    private float timer = 0.0f;

    private void Update()
    {
        //Caps collision with player by once every quarter of a second to prevent excessive damage from collisions
        if (collidedrecently)
        {
            if (timer >= .25f)
            {
                timer = 0.0f;
                collidedrecently = false;
            }
            timer += Time.deltaTime;
        }
    }



}
