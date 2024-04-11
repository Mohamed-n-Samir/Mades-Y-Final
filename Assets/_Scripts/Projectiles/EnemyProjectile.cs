using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{

    [SerializeField] float projectileSpeed;
    [SerializeField] int projectileDamge;
    [SerializeField] int distanceToLive;

    private Vector3 playerDirection;
    private Vector3 initProjectilePosition;
    private Animator projectileAnimator;
    private Rigidbody2D projectileRB;

    // Start is called before the first frame update
    void Start()
    {
        playerDirection = FindObjectOfType<Player>().transform.position - transform.position;
        playerDirection.Normalize();
        initProjectilePosition = transform.position;
        projectileAnimator = GetComponent<Animator>();
        projectileRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += playerDirection * projectileSpeed * Time.deltaTime;

        float distanceMoved = Vector3.Distance(initProjectilePosition, transform.position);

        if (distanceMoved <= distanceToLive && projectileRB)
        {
            projectileRB.velocity = playerDirection * projectileSpeed;
        }
        else if (!projectileRB.IsDestroyed())
        {
            projectileAnimator.SetBool("rollEnd", true);
            projectileRB.velocity = Vector3.zero;
            transform.position = transform.position;
            Destroy(projectileRB);
            Destroy(GetComponent<BoxCollider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player playerToAttack = collision.GetComponent<Player>();
            playerToAttack.Damage(projectileDamge);
        }
        else if (collision.CompareTag("Enemy"))
        {

        }
        else
        {
            projectileAnimator.SetBool("rollEnd", true);
        }

    }

}
