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
            IsHovering = true;
            if (IsHovering)
            {
                _colorChange.TurnBlue();
            }
        }
    }

    public void OnHoverExit()
    {
        Debug.Log($"Hover Exit: {gameObject.name}");
        _colorChange?.RestorePreviousColor();

        if (_colorChange != null)
        {
            IsHovering = false;
            if (!IsHovering)
            {
                _colorChange.RestorePreviousColor();
            }

        }
    }
}
