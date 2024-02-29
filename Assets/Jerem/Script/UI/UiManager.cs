using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuRotationPoint;

    void Start()
    {
        GridBuildingSystem.OnTurretMenuActive += SetTurretMenu;
        GridBuildingSystem.OnTurretMenuDeactivated += UnsetTurretMenu;
    }

    private void UnsetTurretMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuRotationPoint.SetActive(false);
    }

    private void SetTurretMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuRotationPoint.SetActive(true);
    }

    private void OnDestroy()
    {
        GridBuildingSystem.OnTurretMenuActive -= SetTurretMenu;
    }
}
