using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer[] _renderers;
    private Material[] _originalMaterials;

    private GameObject _muzzleFlash;

    void Start()
    {
        // Get all Renderer components on this object and children
        _renderers = GetComponentsInChildren<Renderer>();

        // Cache original materials so we can reset later
        _originalMaterials = new Material[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            // Use material to create instance unique to this object
            _renderers[i].material = new Material(_renderers[i].material);
            _originalMaterials[i] = _renderers[i].material;
        }

        _muzzleFlash = transform.Find("Muzzle_Flash")?.gameObject;
        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            TurnRed();

        if (Input.GetKeyDown(KeyCode.B))
            TurnBlue();

        if (Input.GetKeyDown(KeyCode.G))
            TurnGreen();

        if (Input.GetKeyDown(KeyCode.L))
            ResetToOriginal();
    }

    public void TurnRed()
    {
        DisableMuzzleFlash();

        foreach (var mat in _originalMaterials)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.red);
            Color color = mat.color;
            color.a = 0.1f;
            mat.color = color;
        }

    }

    public void TurnBlue()
    {
        DisableMuzzleFlash();

        foreach (var mat in _originalMaterials)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.cyan);
            Color color = mat.color;
            color.a = 0.1f;
            mat.color = color;
        }

    }

    public void TurnGreen()
    {
        DisableMuzzleFlash();

        foreach (var mat in _originalMaterials)
        {
            mat.EnableKeyword("_EMISSION");
            mat.SetColor("_EmissionColor", Color.green);
            Color color = mat.color;
            color.a = 0.1f;
            mat.color = color;
        }

    }

    public void ResetToOriginal()
    {
        foreach (var renderer in _renderers)
        {
            // Restore original shared material to fully reset appearance
            renderer.material = renderer.sharedMaterial;
        }

        EnableMuzzleFlash();
    }

    private void EnableMuzzleFlash()
    {
        if (_muzzleFlash != null)
        {
            _muzzleFlash.SetActive(true);
        }
    }

    private void DisableMuzzleFlash()
    {
        if ( _muzzleFlash != null)
        {
            _muzzleFlash.SetActive(false);
        }
    }
}



//    using UnityEngine;

//public class ColorChange : MonoBehaviour
//{
//    private MeshRenderer[] _renderers;
//    private Material[] _materials;

//    void Start()
//    {
//        _renderers = GetComponentsInChildren<MeshRenderer>();
//        _materials = new Material[_renderers.Length];

//        for (int i = 0; i < _renderers.Length; i++)
//        {
//            // Use `.material` to get an instance per object (not sharedMaterial)
//            _materials[i] = _renderers[i].material;
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.R)) TurnRed();
//        if (Input.GetKeyDown(KeyCode.B)) TurnBlue();
//        if (Input.GetKeyDown(KeyCode.G)) TurnGreen();
//        if (Input.GetKeyDown(KeyCode.L)) FullColor();
//    }

//    public void TurnRed()
//    {
//        ApplyColor(Color.red, 0.1f);
//    }

//    public void TurnBlue()
//    {
//        ApplyColor(Color.cyan, 0.1f);
//    }

//    public void TurnGreen()
//    {
//        ApplyColor(Color.green, 0.1f);
//    }

//    public void FullColor()
//    {
//        ApplyColor(Color.white, 1.0f);
//    }

//    private void ApplyColor(Color emissionColor, float alpha)
//    {
//        foreach (var mat in _materials)
//        {
//            if (mat == null) continue;

//            mat.EnableKeyword("_EMISSION");
//            mat.SetColor("_EmissionColor", emissionColor);

//            Color baseColor = mat.color;
//            baseColor.a = alpha;
//            mat.color = baseColor;
//        }
//    }
//}


