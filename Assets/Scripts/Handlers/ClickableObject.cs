using UnityEngine;

public class ClickableObject : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        Debug.Log("Just Clicked on " + gameObject.name);
    }
}

   

