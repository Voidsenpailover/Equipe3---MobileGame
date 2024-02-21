using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : ScriptableObject
{
    // Start is called before the first frame update
    //Les int peuvent peut-etre 
    [SerializeField] float health;
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int defense;
    //[SerializeField] 
    //[SerializeField] int[] resistanceElémentaire =new int[4]; 
    [SerializeField] string name;
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
