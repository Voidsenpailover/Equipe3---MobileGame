using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LexiqueTurret : MonoBehaviour
{
    
    [SerializeField] private TurretsData TourPrincipale;
    [SerializeField] private TurretsData TourNiveauApres;
    [SerializeField] private Sprite SpriteTour;
    [SerializeField] private TextMeshProUGUI Name;
    [SerializeField] private TextMeshProUGUI Description;
     [SerializeField] private TextMeshProUGUI DescriptionEffect;
    [SerializeField] private TextMeshProUGUI Cost;
    [SerializeField] private TextMeshProUGUI SellPrice;
    [SerializeField] private TextMeshProUGUI Atk;
    [SerializeField] private TextMeshProUGUI AtkSpeed;
    [SerializeField] private TextMeshProUGUI Range;
    
    
    void Start()
    {
        SpriteTour = TourPrincipale.Sprite;
        Name.text = TourPrincipale.Name;
        Description.text = TourPrincipale.Description;
        DescriptionEffect.text = TourPrincipale.DescriptionEffect;
        Cost.text = TourPrincipale.Cost.ToString();
        SellPrice.text = (TourPrincipale.Cost / 2).ToString();
        Atk.text = TourPrincipale.Damage + "dgts";
        AtkSpeed.text = TourPrincipale.DelayBetweenAtk.ToString("0.0") + "Atk/s";
        Range.text = TourPrincipale.RadAtk.ToString("0.0");
    }
}
