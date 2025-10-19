using UnityEngine;

public class Socket : MonoBehaviour
{
    [SerializeField] private string compareTag;
    [SerializeField] private GameObject attachPoint;
    [SerializeField] private GameObject ring;
    [SerializeField] private Pointsystem pointsystem;

    private Vector3 initialRingScale;

    private void Awake()
    {
        if (ring != null)
            initialRingScale = ring.transform.localScale;
        else
            Debug.LogError("Socket: 'ring' ist nicht zugewiesen!");

        if (attachPoint == null)
            Debug.LogError("Socket: 'attachPoint' ist nicht zugewiesen!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(compareTag)) return;

        Debug.Log("Inside Trigger: " + other.name);

        if (ring != null)
            ring.transform.localScale *= 1.5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(compareTag)) return;

        if (ring != null)
            ring.transform.localScale = initialRingScale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag(compareTag)) return;

        // Falls ObjectHeldTracker fehlt, hinzufügen
        if (!other.TryGetComponent<ObjectHeldTracker>(out ObjectHeldTracker tracker))
        {
            tracker = other.gameObject.AddComponent<ObjectHeldTracker>();
        }

        // Warten bis Objekt losgelassen wurde
        if (tracker.IsHeld()) return;

        // Verhindern, dass das Objekt mehrfach verarbeitet wird
        if (!other.TryGetComponent<SocketDespawnable>(out SocketDespawnable despawn))
        {
            despawn = other.gameObject.AddComponent<SocketDespawnable>();
        }

        if (despawn.hasBeenSocketed) return;

        despawn.hasBeenSocketed = true;

        // Punkte vergeben (optional)
        if (pointsystem != null)
        {
            pointsystem.add50Points();
        }

        // Objekt exakt am Attach-Point positionieren
        other.transform.position = attachPoint.transform.position;
        other.transform.rotation = attachPoint.transform.rotation;

        // Objekt verschwinden lassen
        Destroy(other.gameObject);

        // Ring zurückskalieren
        if (ring != null)
            ring.transform.localScale = initialRingScale;
    }
}