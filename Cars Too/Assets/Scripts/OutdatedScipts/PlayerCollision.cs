using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // There will be four separate childs under player with their own collision box each of which will have this script on it to determine collision orientation
    //same for enemies, but with the enemy collision script
    [SerializeField] private string part="side"; // Designates the part of the car the collision box is responsible for
    [SerializeField] private PlayerEntity p;

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collided "+collision.tag);
        if (collision.gameObject.CompareTag("Enemy")&&!collision.GetComponent<EnemyCollision>().collidedrecently)
        {
            p.CollidedEnemy(collision.gameObject.GetComponent<EnemyCollision>().enemy, collision.gameObject.GetComponent<EnemyCollision>().part, part);
            collision.GetComponent<EnemyCollision>().collidedrecently = true;
        }
    }

}
