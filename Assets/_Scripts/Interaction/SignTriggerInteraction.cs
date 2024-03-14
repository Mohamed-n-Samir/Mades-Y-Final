using UnityEngine;
using UnityEngine.UI;

public class SignTriggerInteraction : TriggerInteractionBase
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;
    
    public string dialog;

    public override void Interact()
    {
        dialogBox.SetActive(true);
        dialogText.text = dialog;
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        if(dialogBox != null){
            dialogBox.SetActive(false);
        }
            // dialogBox.SetActive(false);

    }

}
