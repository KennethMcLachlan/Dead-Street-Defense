using UnityEngine;

public class HoverableObject : MonoBehaviour, IHoverable
{
    private ColorChange _colorChange;
    public bool IsHovering { get; private set; }

    private void Awake()
    {
        _colorChange = GetComponent<ColorChange>();
    }

    public void OnHoverEnter()
    {
        Debug.Log($"Hover Enter: {gameObject.name}");
        if (_colorChange != null)
        {
            _colorChange.TurnBlue();
            IsHovering = true;
        }
    }

    public void OnHoverExit()
    {
        Debug.Log($"Hover Exit: {gameObject.name}");
        _colorChange?.RestorePreviousColor();
        IsHovering = false;
    }
}
