using UnityEngine;

public class ClickableObject : MonoBehaviour, IClickable
{
    void Start()
    {
    }

    public void Clickable()
    {
        //Debug.Log("Interacting with " + gameObject.name);
    }

    public void OnClick()
    {
        Debug.Log("Just Clicked on " + gameObject.name);
    }
}

   

