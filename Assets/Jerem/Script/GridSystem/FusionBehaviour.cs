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
    public TurretsData SpawningRightTower(TurretType turret1, TurretType turret2)
    {
        switch (turret1)
        {
            case TurretType.Feu:
                if(turret2 == TurretType.Vent)
                {
                    //Souffre1
                    return turretsData[22];
                }
                else if (turret2 == TurretType.Eau)
                {
                    //Pyrite1
                    return turretsData[18];
                }
                else if (turret2 == TurretType.Terre)
                {
                    //Phosphore1
                    return turretsData[16];
                }
                break;
            case TurretType.Eau:
                if (turret2 == TurretType.Feu)
                {
                    //Pyrite1
                    return turretsData[18];
                }
                else if (turret2 == TurretType.Vent)
                {
                    //Mercure1
                    return turretsData[14];
                }
                else if (turret2 == TurretType.Terre)
                {
                    //Sel1
                    return turretsData[20];
                }
                break;
            case TurretType.Vent:
                if (turret2 == TurretType.Feu)
                {
                    //Souffre1
                    return turretsData[22];
                }
                else if (turret2 == TurretType.Eau)
                {
                    //Mercure1
                    return turretsData[14];
                }
                else if (turret2 == TurretType.Terre)
                {
                    //Fulgurite1
                    return turretsData[12];
                }
                break;
            case TurretType.Terre:
                if (turret2 == TurretType.Eau)
                {
                    //Sel1
                    return turretsData[20];
                }
                else if (turret2 == TurretType.Vent)
                {
                    //Fulgurite1
                    return turretsData[12];
                }
                else if (turret2 == TurretType.Feu)
                {
                    //Phosphore1
                    return turretsData[16];
                }
                break;
            default:
                break;
        }
        return turretsData[0];
    }

}