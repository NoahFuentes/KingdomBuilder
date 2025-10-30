using UnityEngine;

public class Councilman : Companion
{
    public override void Interact()
    {
        base.Interact();

        if (!canInteract) return;

    }
}
