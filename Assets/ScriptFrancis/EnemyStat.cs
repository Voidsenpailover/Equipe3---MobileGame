using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(menuName= "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    // Start is called before the first frame update
    //Les int peuvent peut-etre 
    /*public GameObject gameObject; 
    Sprite sprite = gameObject.GetComponant<Sprite>();//*/
    [SerializeField] float health;
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int defense;
    //[SerializeField] 
    //[SerializeField] 
    //[SerializeField] int[] resistanceEl�mentaire =new int[4]; 
    [SerializeField] string name;
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
