using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }



}
