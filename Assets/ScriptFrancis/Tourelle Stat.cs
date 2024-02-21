using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TourelleStat : MonoBehaviour
{
    // Start is called before the first frame update
    enum HitType
    {
        Line,Single,AOE
    }
    enum Astre
    {
        Day, Night,Equilibre
    }
    enum Effect
    {
        None,Burn,Stun,Push
    }
    [SerializeField] string name;
    [SerializeField] string description;
    [SerializeField] float degats;
    [SerializeField] float delai;
    [SerializeField] int cout;
    [SerializeField] int vente;
    [SerializeField] int rank;
    [SerializeField] int range;
    [SerializeField] bool[] terrainAutorisation = new bool[4];
    [SerializeField] HitType hitType;
    [SerializeField] int damageSecond;
    [SerializeField] int dureeEffect;


    
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
