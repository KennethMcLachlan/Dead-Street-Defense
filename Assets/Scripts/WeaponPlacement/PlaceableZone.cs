using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private float _emissionStrength = 2.0f;
    public bool isInPlacementZone;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            var draggable = other.GetComponent<Draggable>();
            draggable?.IsInZone();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            var draggable = other.GetComponent<Draggable>();
            draggable?.NotInZone();
        }
    }
}
