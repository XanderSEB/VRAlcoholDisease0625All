using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(AudioSource))]
public class FootstepAndBreathAudio : MonoBehaviour
{
    [Header("Bewegung")]
    public ActionBasedContinuousMoveProvider moveProvider;
    public float minSpeedToTrigger = 0.1f;

    [Header("Audio")]
    public AudioSource footstepAudio;
    public AudioClip breathingHeavyClip;
    public float heavyBreathDelay = 10f; // Zeit in Sekunden bis Schnaufen abgespielt wird

    private CharacterController characterController;
    private float moveTimer = 0f;
    private bool hasBreathed = false;

    void Start()
    {
        if (moveProvider == null)
            moveProvider = GetComponent<ActionBasedContinuousMoveProvider>();

        if (footstepAudio == null)
            footstepAudio = GetComponent<AudioSource>();

        characterController = FindObjectOfType<CharacterController>();

        if (footstepAudio != null)
        {
            footstepAudio.loop = true;
            footstepAudio.playOnAwake = false;
        }
    }

    void Update()
    {
        if (characterController == null) return;

        float speed = characterController.velocity.magnitude;

        if (speed > minSpeedToTrigger)
        {
            // Schritte
            if (!footstepAudio.isPlaying)
                footstepAudio.Play();

            // Timer hochzählen
            moveTimer += Time.deltaTime;

            // Atmen abspielen, wenn länger gelaufen
            if (moveTimer >= heavyBreathDelay && !hasBreathed)
            {
                hasBreathed = true;
                PlayBreathingSound();
            }
        }
        else
        {
            // Kein Schrittgeräusch bei Stillstand
            if (footstepAudio.isPlaying)
                footstepAudio.Stop();

            // Timer zurücksetzen
            moveTimer = 0f;
            hasBreathed = false;
        }
    }

    private void PlayBreathingSound()
    {
        if (breathingHeavyClip != null)
        {
            // Zusätzliche Audiosource verwenden für gleichzeitiges Abspielen
            AudioSource.PlayClipAtPoint(breathingHeavyClip, transform.position);
        }
    }
}