using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuSelectionPoint;
    [SerializeField] GameObject _menuInfoPoint;
    [SerializeField] GameObject _menuFusionPoint;
    [SerializeField] TextMeshProUGUI _waveText;
    [SerializeField] TextMeshProUGUI _maxWaveText;
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _healthText;
    
    [SerializeField] private GameObject _bonusIcone1;
    [SerializeField] private GameObject _bonusIcone2;
    [SerializeField] private GameObject _bonusIcone3;
    [SerializeField] private GameObject _bonusSunMoon1;
    [SerializeField] private GameObject _bonusSunMoon2;
    [SerializeField] private GameObject _bonusSunMoon3;
    
    public List<CardData> _listCard;
    public static UiManager instance;
    private int indexBonus;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        GridBuildingSystem.OnSelectionMenuActive += SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated += UnsetSelectionMenu;
    

        GridBuildingSystem.OnInfoMenuActive += SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated += UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive += SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated += UnsetFusionMenu;
        
        _waveText.text =  EnemySpawner._instance._currentRoundIndex.ToString();
        _maxWaveText.text = EnemySpawner._instance._rounds.Count.ToString();
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
    
    private void UpdateSlotBonus(CardData card)
 {
     indexBonus++;
     switch (indexBonus)
     {
         case 1:
             _bonusIcone1.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon1.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
         case 2:
             _bonusIcone2.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon2.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
         case 3:
             _bonusIcone3.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon3.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
     }
     _listCard.Add(card);
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
             _bonusIcone1.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon1.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
         case 2:
             _bonusIcone2.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon2.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
         case 3:
             _bonusIcone3.GetComponent<SpriteRenderer>().sprite = card.Icone;
             _bonusSunMoon3.GetComponent<SpriteRenderer>().sprite = card.BouleSoleilLune;
             break;
     }
     _listCard.Add(card);
 }
 private void OnEnable()
 {
        EnemySpawner.OnWaveChanged += UpdateWaveText;
        Bullet.OnMoneyChanged += UpdateMoneyText;
        EnemyMovement.OnHealthChanged += UpdateHealthText;
     CardManager.CardSelected += UpdateSlotBonus;
 }

 private void OnDestroy()
 {
        GridBuildingSystem.OnSelectionMenuActive -= SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated -= UnsetSelectionMenu;

        GridBuildingSystem.OnInfoMenuActive -= SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated -= UnsetInfoMenu;

        GridBuildingSystem.OnFusionMenuActive -= SetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated -= UnsetFusionMenu;
        
        EnemySpawner.OnWaveChanged -= UpdateWaveText;
     Bullet.OnMoneyChanged -= UpdateMoneyText;
     EnemyMovement.OnHealthChanged -= UpdateHealthText;
     CardManager.CardSelected -= UpdateSlotBonus;
 }
}
