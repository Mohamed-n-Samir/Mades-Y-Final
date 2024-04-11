using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour, IInteractable
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            CanInteract = true;
            if (CanInteract)
            {
                // Debug.Log("interacted");
                Interact();
            }

        }

    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CanInteract = false;
        }
    }

    public virtual void Interact()
    {
    }
}