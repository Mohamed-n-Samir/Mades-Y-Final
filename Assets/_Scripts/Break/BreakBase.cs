using UnityEngine;

public class BreakBase : MonoBehaviour, IBreakable
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    
    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
            Break();
    }

    public virtual void Break()
    {
    }
}