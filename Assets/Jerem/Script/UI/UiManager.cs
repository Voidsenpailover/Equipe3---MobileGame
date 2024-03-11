using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuRotationPoint;

    void Start()
    {
        GridBuildingSystem.OnTurretMenuActive += SetTurretMenu;
        GridBuildingSystem.OnTurretMenuDeactivated += UnsetTurretMenu;
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

 private void OnDestroy()
 {
     GridBuildingSystem.OnTurretMenuActive -= SetTurretMenu;
     GridBuildingSystem.OnTurretMenuDeactivated -= UnsetTurretMenu;
     EnemySpawner.OnWaveChanged -= UpdateWaveText;
     Bullet.OnMoneyChanged -= UpdateMoneyText;
     EnemyMovement.OnHealthChanged -= UpdateHealthText;
 }
}
