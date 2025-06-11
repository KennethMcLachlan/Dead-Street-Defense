using UnityEngine;

public class PlaceableZone : MonoBehaviour
{
    [SerializeField] private float _emissionStrength = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            Renderer renderer = other.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material mat = renderer.material;

                //Get and Set Emission Color
                mat?.EnableKeyword("_EMISSION");
                mat.SetColor("_EmissionColor", Color.green);

                //Lower Alpha Channel
                Color color = mat.color;
                color.a = 0.1f;
                mat.color = color;
            }
        }

        //if other .tag == weapon 
        //Get the other.object's information
        //Change the mesh and emission of the object to green

            // if the bool for the release of the click button == true/false
                //other.tansform.position == this.gameObject transform.position
                //Return mesh and emission to normal on the object
                //Maybe make other.object a child of this object******
    }
}
