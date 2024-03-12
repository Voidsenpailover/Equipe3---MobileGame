using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuSelectionPoint;
    [SerializeField] GameObject _menuInfoPoint;
    [SerializeField] GameObject _menuFusionPoint;

    void Start()
    {
        GridBuildingSystem.OnSelectionMenuActive += SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated += UnsetSelectionMenu;

        GridBuildingSystem.OnInfoMenuActive += SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated += UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive += SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated += UnsetFusionMenu;
    }

    private void SetInfoMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuInfoPoint.SetActive(true);
    }
    private void UnsetInfoMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuInfoPoint.SetActive(false);
    }

    private void SetFusionMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuFusionPoint.SetActive(true);
    }

    private void UnsetFusionMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuFusionPoint.SetActive(false);
    }

    private void SetSelectionMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuSelectionPoint.SetActive(true);
    }

    private void UnsetSelectionMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuSelectionPoint.SetActive(false);
    }

    private void OnDestroy()
    {
        GridBuildingSystem.OnSelectionMenuActive -= SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated -= UnsetSelectionMenu;

        GridBuildingSystem.OnInfoMenuActive -= SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated -= UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive -= SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated -= UnsetFusionMenu;
    }
}
