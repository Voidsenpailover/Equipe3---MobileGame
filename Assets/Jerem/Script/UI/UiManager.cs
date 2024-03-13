using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuRotationPoint;
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _maxWaveText;
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _healthText;
    void Start()
    {
        _waveText.text = EnemySpawner._instance._currentRoundIndex.ToString();
        _maxWaveText.text = EnemySpawner._instance._rounds.Count.ToString();
        GridBuildingSystem.OnTurretMenuActive += SetTurretMenu;
        GridBuildingSystem.OnTurretMenuDeactivated += UnsetTurretMenu;
        GridBuildingSystem.OnFusionMenuActive += SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated += UnsetFusionMenu;
        _moneyText.text = LevelManager.instance.money.ToString();
        _healthText.text = LevelManager.instance.HP.ToString();
    }


    private void SetFusionMenu(Vector3 pos)
    {
    private void UnsetTurretMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuRotationPoint.SetActive(false);
        Debug.Log("Tour mon bg");
    }

    private void UnsetFusionMenu()
    {
        throw new System.NotImplementedException();
    }

    private void SetTurretMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuRotationPoint.SetActive(true);
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
    private void UnsetTurretMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuRotationPoint.SetActive(false);
    }

 private void OnDestroy()
 {
     GridBuildingSystem.OnTurretMenuActive -= SetTurretMenu;
     GridBuildingSystem.OnTurretMenuDeactivated -= UnsetTurretMenu;
     EnemySpawner.OnWaveChanged -= UpdateWaveText;
     Bullet.OnMoneyChanged -= UpdateMoneyText;
     EnemyMovement.OnHealthChanged -= UpdateHealthText;
 }
}
