using UnityEngine;
using UnityEngine.UI;

public class SignTriggerInteraction : TriggerInteractionBase
{
    [SerializeField] private GameObject dialogBox;
    [SerializeField] private Text dialogText;


    public string dialog;


    public override void Interact()
    {
        // dialogBox.SetActive(true);
        // dialogText.text = dialog;
        contextClueOn.Raise();
    }



    public override void CancelInteract()
    {
            contextClueOff.Raise();
    }

}
