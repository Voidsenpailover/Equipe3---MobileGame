using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : ScriptableObject
{
    [SerializeField] int monnaie;
    [SerializeField] float pv;
    [SerializeField] int nbManche;
    [SerializeField] int manchaMax;
    [SerializeField] bool pause;
    //[SerializeField] Sprite sprite;
    // Start is called before the first frame update

    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
