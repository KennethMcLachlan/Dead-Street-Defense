using System.Runtime.CompilerServices;
using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private float _emissionStrength = 2.0f;
    public bool isInPlacementZone;
    private bool _isPlaced;
    private bool _isPositioned;
    private bool _isOccupied;
    [SerializeField] private BoxCollider _boxCollider;

    private void Update()
    {
        if (!_isPlaced)
        {
            if (isInPlacementZone && PlayerInputControls.Instance._actionButton.action.WasReleasedThisFrame())
            {
                _isPlaced = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            if (_isOccupied) return;
            isInPlacementZone = true;

            var draggable = other.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.IsInZone();
            }

            if (_isPlaced && !_isPositioned)
            {
                _isOccupied = true;
                other.transform.position = gameObject.transform.position;
                other.transform.rotation = gameObject.transform.rotation;
                other.transform.SetParent(transform);
                _isPositioned = true;

                var gatlingGun = other.GetComponent<GatlingBehavior>();
                if (gatlingGun != null)
                {
                    gatlingGun.isActive = true;
                }

                other.GetComponent<SphereCollider>().enabled = true;
                other.GetComponent<ColorChange>().ResetToOriginal();

                draggable.enabled = false;
                _boxCollider.enabled = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            isInPlacementZone = false;
            _isPlaced = false;
            _isPositioned = false;

            var draggable = other.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.NotInZone();
            }
        }
    }

    public void ResetZone()
    {
        _isPlaced = false;
        isInPlacementZone = false;
        _isPositioned = false;
        _isOccupied = false;
        _boxCollider.enabled = true;
    }
    //If gatlingGun is destroyed, reset the zone (Reenable the box Collider?)
}
