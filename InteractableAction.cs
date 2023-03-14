using UnityEngine.Events;

public class InteractableAction : Interactable
{
    public UnityEvent OnInteract;
    public override void ActivateInteractable(bool activate)
    {
        base.ActivateInteractable(activate);
        OnInteract.Invoke();
    }
}
