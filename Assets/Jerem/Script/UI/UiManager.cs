using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UiManager : MonoBehaviour
{
    [SerializeField] GridBuildingSystem gridBuildingSystem;

    [SerializeField] RectTransform _turretMenu;
    [SerializeField] GameObject _menuSelectionPoint;
    [SerializeField] GameObject _menuSelectionLPoint;
    [SerializeField] GameObject _menuSelectionRPoint;
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
    
    [SerializeField] private GameObject _cardsSlot;
    [SerializeField] private GameObject _banner;

    [SerializeField] private GameObject Check1;
    [SerializeField] private GameObject Check2;
    [SerializeField] private GameObject Check3;
    
    public List<CardData> _listCard;
    public static UiManager instance;
    private int indexBonus;

    private void OnEnable()
    {
        GridBuildingSystem.OnSelectionMenuActive += SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuLActive += SetSelectionLMenu;
        GridBuildingSystem.OnSelectionMenuRActive += SetSelectionRMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated += UnsetSelectionMenu;


        GridBuildingSystem.OnInfoMenuActive += SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated += UnsetInfoMenu;

        Draggable.OnFusionMenuActive += SetFusionMenu;
        Draggable.OnFusionMenuDeactivated += UnsetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated += UnsetFusionMenu;

        EnemySpawner.OnWaveChanged += UpdateWaveText;
        EnemyMovement.OnMoneyChanged += UpdateMoneyText;
        EnemyMovement.OnHealthChanged += UpdateHealthText;
        CardManager.CardSelected += UpdateSlotBonus;
    }
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
         var localWave = EnemySpawner._instance._currentRoundIndex + 1;
        _waveText.text =  localWave.ToString();
        _maxWaveText.text = EnemySpawner._instance._rounds.Count.ToString();
        _moneyText.text = LevelManager.instance.money.ToString();
        _healthText.text = LevelManager.instance.HP.ToString();
        if(PlayerPrefs.GetInt("Level1", 0) == 1)
        {
            Check1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level2", 0) == 1)
        {
            Check2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Level3", 0) == 1)
        {
            Check3.SetActive(true);
        }
    }

    private void Update()
    {
        switch (LevelManager.instance.CurrentState)
        {
            case LevelManager.GameState.MainMenu:
                UnsetInfoMenu();
                UnsetFusionMenu();
                UnsetSelectionMenu();
                break;
            case LevelManager.GameState.Victory:
                UnsetInfoMenu();
                UnsetFusionMenu();
                UnsetSelectionMenu();
                break;
            case LevelManager.GameState.GameOver:
                UnsetInfoMenu();
                UnsetFusionMenu();
                UnsetSelectionMenu();
                break;
            default: break;
        }
    }

    private void SetInfoMenu(Vector3 pos)
    {
        _turretMenu.position = pos;

        Vector3Int tempCellPos = gridBuildingSystem.GridLayout.WorldToCell(pos);
        _menuInfoPoint.GetComponent<SetInfo>().SetTurretInfo(GridBuildingSystem.TileDataBases[tempCellPos].GetComponent<Building>().Data);
        _menuInfoPoint.SetActive(true);
       
    }
    public void UnsetInfoMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuInfoPoint.SetActive(false);
    }
    
    private void SetFusionMenu(Vector3 pos, TurretsData data)
    {
        _turretMenu.position = pos;
        Vector3Int tempCellPos = gridBuildingSystem.GridLayout.WorldToCell(pos);
        _menuFusionPoint.GetComponent<SetInfo>().SetTurretFusion(data);
        _menuFusionPoint.SetActive(true);
    }

    public void UnsetFusionMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuFusionPoint.SetActive(false);
    }
    
    private void SetSelectionMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuSelectionPoint.SetActive(true);
        _cardsSlot.SetActive(true);
        _banner.SetActive(false);
    }
    private void SetSelectionLMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuSelectionLPoint.SetActive(true);
        _cardsSlot.SetActive(true);
        _banner.SetActive(false);
    }
    private void SetSelectionRMenu(Vector3 pos)
    {
        _turretMenu.position = pos;
        _menuSelectionRPoint.SetActive(true);
        _cardsSlot.SetActive(true);
        _banner.SetActive(false);
    }

    public void UnsetSelectionMenu()
    {
        _turretMenu.position = Vector3.zero;
        _menuSelectionPoint.SetActive(false);
        _menuSelectionLPoint.SetActive(false);
        _menuSelectionRPoint.SetActive(false);
        _cardsSlot.SetActive(false);
        _banner.SetActive(true);
    }
    
    private void UpdateSlotBonus(CardData card)
    {
         indexBonus++;
        
         switch (indexBonus)
         {
             case 1:
                _bonusIcone1.GetComponent<Image>().sprite = card.Icone;
                _bonusSunMoon1.GetComponent<Image>().sprite = card.BouleSoleilLune;
                 break;
             case 2:
                 _bonusIcone2.GetComponent<Image>().sprite = card.Icone;
                 _bonusSunMoon2.GetComponent<Image>().sprite = card.BouleSoleilLune;
                 break;
             case 3:
                _bonusIcone3.GetComponent<Image>().sprite = card.Icone;
                _bonusSunMoon3.GetComponent<Image>().sprite = card.BouleSoleilLune;
                 break;
         }
        
         _listCard.Add(card);
    }
 

    
     private void UpdateWaveText()
     {
         _waveText.text =  EnemySpawner._instance._currentRoundIndex.ToString();
     }
     public void UpdateMoneyText()
     {
         _moneyText.text = LevelManager.instance.money.ToString();
     }
     private void UpdateHealthText()
     {
         _healthText.text = LevelManager.instance.HP.ToString();
     }
    
 


    private void OnDestroy()
    {  
        GridBuildingSystem.OnSelectionMenuActive -= SetSelectionMenu;
        GridBuildingSystem.OnSelectionMenuLActive -= SetSelectionLMenu;
        GridBuildingSystem.OnSelectionMenuRActive -= SetSelectionRMenu;
        GridBuildingSystem.OnSelectionMenuDeactivated -= UnsetSelectionMenu;


        GridBuildingSystem.OnInfoMenuActive -= SetInfoMenu;
        GridBuildingSystem.OnInfoMenuDeactivated -= UnsetInfoMenu;

        Draggable.OnFusionMenuActive -= SetFusionMenu;
        Draggable.OnFusionMenuDeactivated -= UnsetFusionMenu;
        GridBuildingSystem.OnFusionMenuDeactivated -= UnsetFusionMenu;

        EnemySpawner.OnWaveChanged -= UpdateWaveText;
        EnemyMovement.OnMoneyChanged -= UpdateMoneyText;
        EnemyMovement.OnHealthChanged -= UpdateHealthText;
        CardManager.CardSelected -= UpdateSlotBonus;
    }
}
