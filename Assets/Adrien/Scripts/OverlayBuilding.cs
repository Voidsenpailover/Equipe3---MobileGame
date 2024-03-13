using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OverlayBuilding : MonoBehaviour
{
    [SerializeField] private TurretsData _turretData;
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _damage;
    [SerializeField] private TextMeshProUGUI _atkSpeed;
    [SerializeField] private TextMeshProUGUI _cost;
    
    private void Start()
    {
        GridBuildingSystem.OnTurretMenuActivated += SetActive;
        GridBuildingSystem.OnSelectionMenuDeactivated += SetInactive;
        _name.text = _turretData.Name;
        _damage.text = _turretData.Damage.ToString();
        _atkSpeed.text = _turretData.DelayBetweenAtk.ToString("0.0");
        _cost.text = _turretData.Cost.ToString();
    }

   

    private void OnDestroy()
    {
        GridBuildingSystem.OnTurretMenuActivated -= SetActive;
        GridBuildingSystem.OnSelectionMenuDeactivated -= SetInactive;
    }
    private void SetActive()
    {
        gameObject.SetActive(true);
    }
    
    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}
