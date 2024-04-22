using UnityEngine;

public class BreakBase : MonoBehaviour, IBreakable
{
    private Animator anim;

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Break()
    {
        anim.SetBool("Break", true);
    }

    public void StopBreaking()
    {
        anim.SetBool("Break", false);
        Destroy(gameObject);
    }
}