using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour
{
    [SerializeField] private GameObject _pointPrefab;

    void OnEnable()
    {
        GridBuildingSystem.OnPointCreated += WhenPointIsCreated;
    }

    private void WhenPointIsCreated(Vector3Int obj)
    {
        GameObject point = Instantiate(_pointPrefab, new Vector3(obj.x +0.5f, obj.y + 0.5f, obj.z), Quaternion.identity, transform);
    }

    private void OnDestroy()
    {
        GridBuildingSystem.OnPointCreated -= WhenPointIsCreated;
    }
}
