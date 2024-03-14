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
                    _ => turret2
                } ;
            case TurretType.Eau:

                return (turret2.Type) switch
                {
                    TurretType.Feu => turretsData[18],//Pyrite1
                    TurretType.Vent => turretsData[14],//Mercure1
                    TurretType.Terre => turretsData[20],//Sel1
                    _ => turret2

                };
            case TurretType.Vent:
                return (turret2.Type) switch
                {
                    TurretType.Feu => turretsData[22],//Souffre1
                    TurretType.Eau => turretsData[14],//Mercure1
                    TurretType.Terre => turretsData[12],//Fulgurite1
                    _ => turret2
                };
            case TurretType.Terre:
                return (turret2.Type) switch
                {
                    TurretType.Eau => turretsData[20],//Sel1
                    TurretType.Vent => turretsData[12],//Fulgurite1
                    TurretType.Feu => turretsData[16],//Phosphore1
                    _ => turret2
                };
            default:
                break;
        }
        return turret2;
    }

}