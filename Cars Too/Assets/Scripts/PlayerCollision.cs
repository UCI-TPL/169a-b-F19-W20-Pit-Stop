using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string part="side"; // Designates the part of the car the collision box is responsible for
    [SerializeField] private PlayerEntity p;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            p.CollidedEnemy(collision.gameObject.GetComponent<EnemyCollision>().enemy, collision.gameObject.GetComponent<EnemyCollision>().part, part);
        }
    }

}
