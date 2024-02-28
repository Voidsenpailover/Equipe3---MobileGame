using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    [CreateAssetMenuAttribute(menuName= "Scriptable Objects/Tourelle")]
public class Tourelle : ScriptableObject
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
        None,Burn,Stun,Push,Corrosion,Piques,
    }
    //[SerializeField] Sprite sprite;
    //string a changer possiblement en Text ou TextMeshPro
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
    [SerializeField] Effect effet1;
    [SerializeField] Effect effet2;



    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
