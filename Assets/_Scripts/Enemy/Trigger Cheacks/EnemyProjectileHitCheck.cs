using UnityEngine;
using System.Linq;

public class EnemyProjectileHitCheck : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!collision.CompareTag("Enemy"))
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("rollEnd", true);
        }
        if(collision.CompareTag("Player")){
            Debug.Log("Player");
        }


    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}