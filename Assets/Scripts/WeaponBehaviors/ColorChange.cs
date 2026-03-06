using UnityEngine;

public enum ColorState
{
    Original,
    Red,
    Green,
    Blue,
}

public class ColorChange : MonoBehaviour
{
    private Renderer[] _renderers;
    private Material[][] _originalMaterials;

    [SerializeField] private float _transparencyStrength = 0.4f;

    private ColorState _currentState = ColorState.Original;
    private ColorState _previousState = ColorState.Original;

    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _originalMaterials = new Material[_renderers.Length][];

        for (int i = 0; i < _renderers.Length; i++)
        {
            Material[] originalMats = _renderers[i].materials;
            _originalMaterials[i] = new Material[originalMats.Length];

            Material[] instanceMats = new Material[originalMats.Length];
            for (int j = 0; j < originalMats.Length; j++)
            {
                instanceMats[j] = new Material(originalMats[j]);
                _originalMaterials[i][j] = new Material(originalMats[j]);
            }

            _renderers[i].materials = instanceMats;
        }

        TurnRed();
    }

    public void TurnRed()
    {
        SetColor(Color.red, ColorState.Red);
    }

    public void TurnGreen()
    {
        SetColor(Color.green, ColorState.Green);
    }

    public void TurnBlue()
    {
        _previousState = _currentState;

        SetColor(Color.cyan, ColorState.Blue);
    }

    public void RestorePreviousColor()
    {
        switch (_previousState)
        {
            case ColorState.Red:
                TurnRed();
                break;
            case ColorState.Green:
                TurnGreen();
                break;
            case ColorState.Original:
                ResetToOriginal();
                break;
        }
    }

    public void ResetToOriginal()
    {
        _previousState = ColorState.Original;
        _currentState = ColorState.Original;

        for (int i = 0; i < _renderers.Length; i++)
        {
            Material[] restored = new Material[_originalMaterials[i].Length];
            for (int j = 0; j < restored.Length; j++)
            {
                restored[j] = new Material(_originalMaterials[i][j]);
            }
            _renderers[i].materials = restored;
        }
    }

    private void SetColor(Color targetColor, ColorState newState)
    {
        foreach (var renderer in _renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", targetColor);
                }

                SetMaterialToTransparent(mat);

                if (mat.HasProperty("_Color"))
                {
                    Color color = targetColor;
                    color.a = _transparencyStrength;
                    mat.color = color;
                }
            }
        }
        _currentState = newState;
    }

    private void SetMaterialToTransparent(Material mat)
    {
        if (mat == null) return;

        mat.SetFloat("_Mode", 3);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
    }
}