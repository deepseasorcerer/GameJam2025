using UnityEngine;
using UnityEngine.Device;

public class InteractableTest : InteractableBase
{
    [SerializeField] private Material green;
    [SerializeField] private Material red;
    [SerializeField] private MeshRenderer meshRenderer;

    private bool isGreen;

    private void Start()
    {
        SetColorGreen();
    }

    private void SetColorGreen()
    {
        isGreen = true;
        meshRenderer.material = green;
    }

    private void SetColorRed()
    {
        isGreen = false;
        meshRenderer.material = red;
    }

    protected override void PerformInteraction()
    {
        if (isGreen)
        {
            SetColorRed();
        }
        else
        {
            SetColorGreen();
        }
    }

    protected override void OnInteractionCancelled()
    {
        base.OnInteractionCancelled();
    }

}
