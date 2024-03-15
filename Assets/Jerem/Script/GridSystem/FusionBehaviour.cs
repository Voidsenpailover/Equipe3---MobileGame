using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionBehaviour : MonoBehaviour
{
    [SerializeField] private GridBuildingSystem gridBuildingSystem;
    private List<TurretsData> turretsData;
    private void Start()
    {
        turretsData = gridBuildingSystem.TurretsData;
    }
    public TurretsData SpawningRightTower(TurretsData turret1, TurretsData turret2)
    {
        switch (turret1.Type)
        {
            case TurretType.Feu:
                if(turret2.Type == TurretType.Vent)
                {
                    //Souffre1
                    return turretsData[22];
                }
                else if (turret2.Type == TurretType.Eau)
                {
                    //Pyrite1
                    return turretsData[18];
                }
                else if (turret2.Type == TurretType.Terre)
                {
                    //Phosphore1
                    return turretsData[16];
                }
                break;
            case TurretType.Eau:
                if (turret2.Type == TurretType.Feu)
                {
                    //Pyrite1
                    return turretsData[18];
                }
                else if (turret2.Type == TurretType.Vent)
                {
                    //Mercure1
                    return turretsData[14];
                }
                else if (turret2.Type == TurretType.Terre)
                {
                    //Sel1
                    return turretsData[20];
                }
                break;
            case TurretType.Vent:
                if (turret2.Type == TurretType.Feu)
                {
                    //Souffre1
                    return turretsData[22];
                }
                else if (turret2.Type == TurretType.Eau)
                {
                    //Mercure1
                    return turretsData[14];
                }
                else if (turret2.Type == TurretType.Terre)
                {
                    //Fulgurite1
                    return turretsData[12];
                }
                break;
            case TurretType.Terre:
                if (turret2.Type == TurretType.Eau)
                {
                    //Sel1
                    return turretsData[20];
                }
                else if (turret2.Type == TurretType.Vent)
                {
                    //Fulgurite1
                    return turretsData[12];
                }
                else if (turret2.Type == TurretType.Feu)
                {
                    //Phosphore1
                    return turretsData[16];
                }
                break;
            default:
                break;
        }
        return turret2;
    }

}