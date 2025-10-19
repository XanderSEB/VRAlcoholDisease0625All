using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectHeldTracker : MonoBehaviour
{
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private bool isHeld;

    void Awake()
    {
        // Get the XRGrabInteractable component attached to this GameObject
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (grabInteractable == null)
        {
            Debug.LogError("ObjectHeldTracker: Kein XRGrabInteractable gefunden an " + gameObject.name);
            return;
        }

        // Subscribe to the selectEntered and selectExited events
        grabInteractable.selectEntered.AddListener(OnSelectEntered);
        grabInteractable.selectExited.AddListener(OnSelectExited);
    }

    private void OnDestroy()
    {
        if (grabInteractable == null) return;

        // Unsubscribe from the events when the object is destroyed
        grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
        grabInteractable.selectExited.RemoveListener(OnSelectExited);
    }

    private void OnSelectEntered(SelectEnterEventArgs args)
    {
        isHeld = true;
        Debug.Log("Object is now held: " + gameObject.name);
    }

    private void OnSelectExited(SelectExitEventArgs args)
    {
        isHeld = false;
        Debug.Log("Object is no longer held: " + gameObject.name);
    }

    public bool IsHeld()
    {
        return isHeld;
    }
}