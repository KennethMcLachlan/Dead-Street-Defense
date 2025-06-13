using UnityEngine;

public class HoverableObject : MonoBehaviour, IHoverable
{
    private ColorChange _colorChange;

    private void Awake()
    {
        _colorChange = GetComponent<ColorChange>();
    }

    public void OnHoverEnter()
    {
        Debug.Log($"Hover Enter: {gameObject.name}");
        _colorChange?.TurnBlue();
    }

    public void OnHoverExit()
    {
        Debug.Log($"Hover Exit: {gameObject.name}");
        _colorChange?.RestorePreviousColor();
    }
}
