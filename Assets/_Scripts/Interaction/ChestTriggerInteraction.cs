using UnityEngine;

public class ChestTriggerInteraction : TriggerInteractionBase
{
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
