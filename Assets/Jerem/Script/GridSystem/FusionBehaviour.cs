using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionBehaviour : MonoBehaviour
{
    private TurretType SpawningRightTower(TurretType turret1, TurretType turret2)
    {
        switch (turret1)
        {
            case TurretType.Feu:
                if(turret2 == TurretType.Vent)
                {
                    return TurretType.Soufre;
                }
                else if (turret2 == TurretType.Eau)
                {
                    return TurretType.Pyrite;
                }
                else if (turret2 == TurretType.Terre)
                {
                    return TurretType.Phosphore;
                }
                break;
            case TurretType.Eau:
                if (turret2 == TurretType.Feu)
                {
                    return TurretType.Pyrite;
                }
                else if (turret2 == TurretType.Vent)
                {
                    return TurretType.Mercure;
                }
                else if (turret2 == TurretType.Terre)
                {
                    return TurretType.Sel;
                }
                break;
            case TurretType.Vent:
                if (turret2 == TurretType.Feu)
                {
                    return TurretType.Soufre;
                }
                else if (turret2 == TurretType.Eau)
                {
                    return TurretType.Mercure;
                }
                else if (turret2 == TurretType.Terre)
                {
                    return TurretType.Fulgurite;
                }
                break;
            case TurretType.Terre:
                if (turret2 == TurretType.Eau)
                {
                    return TurretType.Sel;
                }
                else if (turret2 == TurretType.Vent)
                {
                    return TurretType.Fulgurite;
                }
                else if (turret2 == TurretType.Feu)
                {
                    return TurretType.Phosphore;
                }
                break;
            default:
                break;
        }
        return turret1;
    }

}