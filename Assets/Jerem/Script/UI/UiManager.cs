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
    
    [SerializeField] private Sprite _bonusIcone1;
    [SerializeField] private Sprite _bonusIcone2;
    [SerializeField] private Sprite _bonusIcone3;
    [SerializeField] private Sprite _bonusSunMoon1;
    [SerializeField] private Sprite _bonusSunMoon2;
    [SerializeField] private Sprite _bonusSunMoon3;
    
    public List<CardData> _listCard;
    public static UiManager instance;
    private int indexBonus;
    private void Awake()
    {
        instance = this;
    }
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

 
 private void UpdateSlotBonus(CardData card)
 {
     indexBonus++;
     switch (indexBonus)
     {
         case 1:
             _bonusIcone1 = card.Icone;
             _bonusSunMoon1 = card.BouleSoleilLune;
             break;
         case 2:
             _bonusIcone2 = card.Icone;
             _bonusSunMoon2 = card.BouleSoleilLune;
             break;
         case 3:
             _bonusIcone3 = card.Icone;
             _bonusSunMoon3 = card.BouleSoleilLune;
             break;
     }
     _listCard.Add(card);
 }
 private void OnEnable()
 {
     GridBuildingSystem.OnTurretMenuActive += SetTurretMenu;
     GridBuildingSystem.OnTurretMenuDeactivated += UnsetTurretMenu; 
     EnemySpawner.OnWaveChanged += UpdateWaveText;
     Bullet.OnMoneyChanged += UpdateMoneyText;
     EnemyMovement.OnHealthChanged += UpdateHealthText;
     CardManager.CardSelected += UpdateSlotBonus;
 }

 private void OnDestroy()
 {
     GridBuildingSystem.OnTurretMenuActive -= SetTurretMenu;
     GridBuildingSystem.OnTurretMenuDeactivated -= UnsetTurretMenu;
     EnemySpawner.OnWaveChanged -= UpdateWaveText;
     Bullet.OnMoneyChanged -= UpdateMoneyText;
     EnemyMovement.OnHealthChanged -= UpdateHealthText;
     CardManager.CardSelected -= UpdateSlotBonus;
 }
}
