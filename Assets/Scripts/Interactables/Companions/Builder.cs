using UnityEngine;

public class Builder : Companion
{
    public override void Interact()
    {
        base.Interact();

        if (!canInteract) return;

        UIManager.Instance.OpenCenterCityMap();
    }
}
