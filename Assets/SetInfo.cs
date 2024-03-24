using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SetInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TurretName;
    [SerializeField] private TextMeshProUGUI _TurretAtk;
    [SerializeField] private TextMeshProUGUI _TurretAtkSpeed;
    [SerializeField] private TextMeshProUGUI _TurretCost;
    public Button _button;

    private TurretsData _turretDataTemp;
    [SerializeField] LevelManager LevelManager;
    
    public void SetTurretInfo(TurretsData turret)
    {
        _turretDataTemp = turret;
        _TurretName.text = turret.Name;
        _TurretAtk.text = turret.Damage.ToString("0.0");
        _TurretAtkSpeed.text = turret.DelayBetweenAtk.ToString("0.0");
        var cout = turret.Cost * 0.5f;
        _TurretCost.text = cout.ToString("0.0");
        if (_button != null)
        {
            _button.onClick.AddListener(OpenGrimoire);
        }
    }
    
    void OpenGrimoire()
    {
        Grimoire.Instance.OpenGrimoire();
        switch (_turretDataTemp.Type)
        {
            case TurretType.Feu:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourFeu();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourFeu2();
                        break;
                    case 3:
                        Grimoire.Instance.PageTourFeu3();
                        break;
                }
                break;
            case TurretType.Eau:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourEau();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourEau2();
                        break;
                    case 3:
                        Grimoire.Instance.PageTourEau3();
                        break;
                }
                break;
            case TurretType.Terre:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourTerre();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourTerre2();
                        break;
                    case 3:
                        Grimoire.Instance.PageTourTerre3();
                        break;
                }
                break;
            case TurretType.Vent:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourVent();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourVent2();
                        break;
                    case 3:
                        Grimoire.Instance.PageTourVent3();
                        break;
                }
                break;
            case TurretType.Fulgurite:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourFulgurite();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourFulgurite2();
                        break;
                }
                break;
            case TurretType.Phosphore:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourPhosphore();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourPhosphore2();
                        break;
                }
                break;
            case TurretType.Pyrite:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourPyrite();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourPyrite2();
                        break;
                }
                break;
            case TurretType.Mercure:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourMercure();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourMercure2();
                        break;
                }
                break;
            case TurretType.Sel:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourSel();
                        break;
                    case 2:
                        Grimoire.Instance.PageTourSel2();
                        break;
                }
                break;
            case TurretType.Soufre:
                switch (_turretDataTemp.Level)
                {
                    case 1:
                        Grimoire.Instance.PageTourSoufre();    
                        break;
                    case 2:
                        Grimoire.Instance.PageTourSoufre2();
                        break;
                }
                break;
        }
    }
    
    public void SetTurretFusion(TurretsData turret)
    {
        _turretDataTemp = turret;
        _TurretName.text = turret.Name;
        _TurretAtk.text = turret.Damage.ToString("0.0");
        _TurretAtkSpeed.text = turret.DelayBetweenAtk.ToString("0.0");
        var cout = turret.Cost;
        _TurretCost.text = cout.ToString("0.0");
    }

    public void SellTurret()
    {
        LevelManager.LooseMoneyForSell(_turretDataTemp);
    }
}
