using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private float _emissionStrength = 2.0f;
    public bool isInPlacementZone;
    private bool _isPlaced;
    private bool _isPositioned;

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
            isInPlacementZone = true;


            var draggable = other.GetComponent<Draggable>();
            draggable?.IsInZone();

            if (_isPlaced && !_isPositioned)
            {
                other.transform.position = gameObject.transform.position;
                _isPositioned = true;
                //If is positioned == true
                //Object can no longer be repositioned
                //draggable.enabled = false; //May need some reworking



                other.GetComponent<ColorChange>().ResetToOriginal();
                //Set IsNotSelected
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            var draggable = other.GetComponent<Draggable>();
            draggable?.NotInZone();

            isInPlacementZone = false;
            _isPlaced = false;
            _isPositioned = false;
        }
    }
}
