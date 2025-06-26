using System.Collections.Generic;
using UnityEngine;

public class WayPointManager : MonoBehaviour
{
    #region Instance
    private static WayPointManager _instance;
    public static WayPointManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("WayPoint Manager is NULL!");
            }
            return _instance;
        }
    }
    #endregion

    public List<Transform> wayPoints;

    private void Awake()
    {
        _instance = this;
    }
    public List<Transform> SendWayPoints()
    {
        return wayPoints;
    }
}
