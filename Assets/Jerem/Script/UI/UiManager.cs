using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        _waveText.text =  EnemySpawner._instance._currentRoundIndex.ToString();
        _maxWaveText.text = EnemySpawner._instance._rounds.Count.ToString();
        _moneyText.text = LevelManager.instance.money.ToString();
        _healthText.text = LevelManager.instance.HP.ToString();
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
