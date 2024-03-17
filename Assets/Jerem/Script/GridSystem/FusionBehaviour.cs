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

                return (turret2.Type) switch
                {
                    TurretType.Vent => turretsData[22],//Souffre1
                    TurretType.Eau => turretsData[18],//Pyrite1
                    TurretType.Terre => turretsData[16],//Phosphore1
                    TurretType.Feu => turretsData[6],//Feu2
                    _ => turretsData[24]
                } ;
            case TurretType.Eau:

                return (turret2.Type) switch
                {
                    TurretType.Feu => turretsData[18],//Pyrite1
                    TurretType.Vent => turretsData[14],//Mercure1
                    TurretType.Terre => turretsData[20],//Sel1
                    TurretType.Eau => turretsData[5],//Eau2
                    _ => turretsData[24]

                };
            case TurretType.Vent:
                return (turret2.Type) switch
                {
                    TurretType.Feu => turretsData[22],//Souffre1
                    TurretType.Eau => turretsData[14],//Mercure1
                    TurretType.Terre => turretsData[12],//Fulgurite1
                    TurretType.Vent => turretsData[4],//Vent2
                    _ => turretsData[24]
                };
            case TurretType.Terre:
                return (turret2.Type) switch
                {
                    TurretType.Eau => turretsData[20],//Sel1
                    TurretType.Vent => turretsData[12],//Fulgurite1
                    TurretType.Feu => turretsData[16],//Phosphore1
                    TurretType.Terre => turretsData[7],//Terre2
                    _ => turretsData[24]
                };
            case TurretType.Soufre:
                return (turret2.Type) switch
                {
                    TurretType.Soufre => turretsData[23],//Soufre2
                    _ => turretsData[24]
                };
            case TurretType.Pyrite:
                return (turret2.Type) switch
                {
                    TurretType.Pyrite => turretsData[19],//Pyrite2
                    _ => turretsData[24]
                };
            case TurretType.Mercure:
                return (turret2.Type) switch
                {
                    TurretType.Mercure => turretsData[15],//Mercure2
                    _ => turretsData[24]
                };
            case TurretType.Sel:
                return (turret2.Type) switch
                {
                    TurretType.Sel => turretsData[21],//Sel2
                    _ => turretsData[24]
                };
            case TurretType.Fulgurite:
                return (turret2.Type) switch
                {
                    TurretType.Fulgurite => turretsData[13],//Fulgurite
                    _ => turretsData[24]
                };
            case TurretType.Phosphore:
                return (turret2.Type) switch
                {
                    TurretType.Phosphore => turretsData[17],//Phosphore
                    _ => turretsData[24]
                };
            default:
                break;
        }
        return turretsData[24];
    }

    public TurretsData SpawningRightTowerLevel3(TurretsData turret1, TurretsData turret2)
    {
        switch (turret1.Type)
        {
            case TurretType.Feu:
                return (turret2.Type) switch
                {
                    TurretType.Feu => turretsData[10],//Feu3
                    _ => turretsData[24]
                };
            case TurretType.Vent:
                return (turret2.Type) switch
                {
                    TurretType.Vent => turretsData[8],//Vent3
                    _ => turretsData[24]
                };
            case TurretType.Eau:
                return (turret2.Type) switch
                {
                    TurretType.Eau => turretsData[9],//Eau3
                    _ => turretsData[24]
                };
            case TurretType.Terre:
                return (turret2.Type) switch
                {
                    TurretType.Terre => turretsData[11],//Terre3
                    _ => turretsData[24]
                };
            default:
                break;
        }
        return turretsData[24];
    }

}