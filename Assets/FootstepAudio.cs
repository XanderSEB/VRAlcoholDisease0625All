using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class FootstepAudio : MonoBehaviour
{
    public ActionBasedContinuousMoveProvider moveProvider; // Referenz zur Bewegung
    public float minSpeedToPlay = 0.1f; // Schwelle, ab wann Ton abgespielt wird

    private AudioSource audioSource;
    private CharacterController characterController;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (moveProvider == null)
            moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();

        characterController = FindObjectOfType<CharacterController>();
    }

    void Update()
    {
        if (characterController == null) return;

        // Bewegungsgeschwindigkeit abrufen
        float speed = characterController.velocity.magnitude;

        if (speed > minSpeedToPlay)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }
}