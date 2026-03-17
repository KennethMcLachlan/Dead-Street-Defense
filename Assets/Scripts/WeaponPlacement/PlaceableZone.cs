using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private BoxCollider _boxCollider;
    [SerializeField] private ParticleSystem _psGlow;
    [SerializeField] private float _emissionStrength = 2.0f;
    public bool isInPlacementZone;
    private bool _isPlaced;
    private bool _isPositioned;
    private bool _isOccupied;

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

                var weapon = other.GetComponent<WeaponBehavior>();
                if (weapon != null)
                {
                    weapon.isActive = true;
                }

                other.GetComponent<SphereCollider>().enabled = true;
                other.GetComponent<ColorChange>().ResetToOriginal();

                _psGlow.Stop();

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
        _psGlow.Play();
        _boxCollider.enabled = true;
    }
}
