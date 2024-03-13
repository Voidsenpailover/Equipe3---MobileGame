using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuSelectionPoint;
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _maxWaveText;
    [SerializeField] GameObject _menuInfoPoint;
    [SerializeField] GameObject _menuFusionPoint;
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _healthText;
    void Start()
    {
        _waveText.text = EnemySpawner._instance._currentRoundIndex.ToString();
        _maxWaveText.text = EnemySpawner._instance._rounds.Count.ToString();
        GridBuildingSystem.OnSelectionMenuActive += SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated += UnsetSelectionMenu;

        GridBuildingSystem.OnInfoMenuActive += SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated += UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive += SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated += UnsetFusionMenu;
        _moneyText.text = LevelManager.instance.money.ToString();
        _healthText.text = LevelManager.instance.HP.ToString();
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

 private void UpdateWaveText()
 {
     _waveText.text =  EnemySpawner._instance._currentRoundIndex.ToString();
 }
 private void UpdateMoneyText()
 {
     _moneyText.text = LevelManager.instance.money.ToString();
 }
 private void UpdateHealthText()
 {
     _healthText.text = LevelManager.instance.HP.ToString();
 }

 private void OnEnable()
 {
     GridBuildingSystem.OnTurretMenuActive += SetTurretMenu;
     GridBuildingSystem.OnTurretMenuDeactivated += UnsetTurretMenu; 
     EnemySpawner.OnWaveChanged += UpdateWaveText;
     Bullet.OnMoneyChanged += UpdateMoneyText;
     EnemyMovement.OnHealthChanged += UpdateHealthText;
 }


 private void OnDestroy()
 {
         GridBuildingSystem.OnSelectionMenuActive -= SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated -= UnsetSelectionMenu;

        GridBuildingSystem.OnInfoMenuActive -= SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated -= UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive -= SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated -= UnsetFusionMenu;

        GridBuildingSystem.OnFusionMenuActive -= SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated -= UnsetFusionMenu;
             EnemySpawner.OnWaveChanged -= UpdateWaveText;
     Bullet.OnMoneyChanged -= UpdateMoneyText;
     EnemyMovement.OnHealthChanged -= UpdateHealthText;
 }
}
