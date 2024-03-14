using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TurretName;
    [SerializeField] private TextMeshProUGUI _TurretAtk;
    [SerializeField] private TextMeshProUGUI _TurretAtkSpeed;
    [SerializeField] private TextMeshProUGUI _TurretCost;
    
    
    public void SetTurretInfo(TurretsData turret)
    {
        _TurretName.text = turret.Name;
        _TurretAtk.text = turret.Damage.ToString("0.0");
        _TurretAtkSpeed.text = turret.DelayBetweenAtk.ToString("0.0");
        var cout = turret.Cost * 0.5f;
        _TurretCost.text = cout.ToString("0.0");
    }
    public void SetTurretFusion(TurretsData turret)
    {
        _TurretName.text = turret.Name;
        _TurretAtk.text = turret.Damage.ToString("0.0");
        _TurretAtkSpeed.text = turret.DelayBetweenAtk.ToString("0.0");
        var cout = turret.Cost;
        _TurretCost.text = cout.ToString("0.0");
    }
}
