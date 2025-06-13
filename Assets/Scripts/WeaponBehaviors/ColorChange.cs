using UnityEngine;

public class ColorChange : MonoBehaviour
{
    private Renderer[] _renderers;
    private Material[][] _originalMaterials;

    private GameObject _muzzleFlash;

    [SerializeField] private float _transparencyStrength = 0.4f;

    void Start()
    {
        // Get all Renderer components on this object and children
        _renderers = GetComponentsInChildren<Renderer>();

        // Cache original materials so we can reset later
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

            //// Use material to create instance unique to this object
            //_renderers[i].material = new Material(_renderers[i].material);
            //_originalMaterials[i] = _renderers[i].material;
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

        foreach (var renderer in _renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.red);
                }

                SetMaterialToTransparent(mat);

                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = _transparencyStrength;
                    mat.color = color;
                }
            }

        }
    }

    public void TurnBlue()
    {
        DisableMuzzleFlash();
        foreach (var renderer in _renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.cyan);
                }

                SetMaterialToTransparent(mat);

                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = _transparencyStrength;
                    mat.color = color;
                }
            }
        }
    }

    public void TurnGreen()
    {
        DisableMuzzleFlash();

        foreach (var renderer in _renderers)
        {
            foreach (var mat in renderer.materials)
            {
                if (mat.HasProperty("_EmissionColor"))
                {
                    mat.EnableKeyword("_EMISSION");
                    mat.SetColor("_EmissionColor", Color.green);
                }

                SetMaterialToTransparent(mat);

                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = _transparencyStrength;
                    mat.color = color;
                }
            }
        }
    }

    public void ResetToOriginal()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            Material[] restored = new Material[_originalMaterials[i].Length];
            for (int j= 0; j < restored.Length; j++)
            {
                restored[j] = new Material(_originalMaterials[i][j]);
            }
            _renderers[i].materials = restored;
        }

        EnableMuzzleFlash();
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



