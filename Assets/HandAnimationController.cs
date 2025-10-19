using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class HandAnimationController : MonoBehaviour
{
    public Animator handAnimator;
    public InputActionProperty gripAction;

    void Update()
    {
        float gripValue = gripAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
