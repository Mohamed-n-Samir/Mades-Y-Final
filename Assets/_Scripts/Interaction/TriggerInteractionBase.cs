using UnityEngine;

public class TriggerInteractionBase : MonoBehaviour, IInteractable
{
    public GameObject Player { get; set; }
    public bool CanInteract { get; set; }

    
    [SerializeField] protected Signal contextClueOn;
    [SerializeField] protected Signal contextClueOff;
    // public InputManager inputHandler;

    public virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            Interact();
        }

    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInteract();
        }
    }

    public virtual void Interact()
    {
    }

    public virtual void CancelInteract() { }
}