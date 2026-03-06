using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private float _emissionStrength = 2.0f;
    public bool isInPlacementZone;
    private bool _isPlaced;
    private bool _isPositioned;
    [SerializeField] private BoxCollider _boxCollider;

    private void Update()
    {
        if (!_isPlaced)
        {
            if (isInPlacementZone && PlayerInputControls.Instance._actionButton.action.WasReleasedThisFrame()) // Need to add and has enough currency?
            {
                _isPlaced = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            isInPlacementZone = true;

            var draggable = other.GetComponent<Draggable>();
            if (draggable != null)
            {
                draggable.IsInZone();
            }

            if (_isPlaced && !_isPositioned)
            {
                other.transform.position = gameObject.transform.position;
                _isPositioned = true;

                var sphereCollider = other.GetComponent<SphereCollider>();
                if (sphereCollider != null)
                {
                    sphereCollider.enabled = true;
                }

                other.GetComponent<ColorChange>().ResetToOriginal();
            }

            if (_isPlaced && _isPositioned)
            {
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

            //if (!isInPlacementZone && PlayerInputControls.Instance._actionButton.action.WasReleasedThisFrame())
            //{
            //    //refund currency
            //    Destroy(other);
            //}
        }
    }
}
