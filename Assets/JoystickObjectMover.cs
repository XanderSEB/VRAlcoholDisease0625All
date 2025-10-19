using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class JoystickObjectMover : MonoBehaviour
{
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    public InputActionProperty joystickInput; // z. B. Primary2DAxis (Vector2)

    public float moveSpeed = 0.2f; // Bewegungsgeschwindigkeit

    private bool isHeld = false;
    private Transform interactorTransform;

    private void OnEnable()
    {
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnDisable()
    {
        grabInteractable.selectEntered.RemoveListener(OnGrab);
        grabInteractable.selectExited.RemoveListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        isHeld = true;
        interactorTransform = args.interactorObject.transform;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        isHeld = false;
        interactorTransform = null;
    }

    private void Update()
    {
        if (isHeld && interactorTransform != null)
        {
            Vector2 input = joystickInput.action.ReadValue<Vector2>();
            float moveZ = input.y * moveSpeed * Time.deltaTime;

            // Objekt entlang der Vorwärtsrichtung des Controllers verschieben
            transform.position += interactorTransform.forward * moveZ;
        }
    }
}