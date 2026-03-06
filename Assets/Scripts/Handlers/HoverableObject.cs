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
