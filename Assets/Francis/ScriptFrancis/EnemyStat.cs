using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    //[CreateAssetMenu(menuName= "Scriptable Objects/Enemy")]
public class EnemyStat2 : ScriptableObject
{
    // Start is called before the first frame update
    //Les int peuvent peut-etre 
    /*public GameObject gameObject; 
    Sprite sprite = gameObject.GetComponant<Sprite>();//*/
    [SerializeField] float health;
    [SerializeField] float healthMax;//utile si soin
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] int defense;
    [SerializeField] int monnaieDonn�e;
    [SerializeField] float dur�eEffet1;
    [SerializeField] float dur�eEffet2;
    //[SerializeField] Sprite sprite;
    

    //[SerializeField] 
    //[SerializeField] 
    //[SerializeField] int[] resistanceEl�mentaire =new int[4]; 
    //string a changer possiblement en Text
    [SerializeField] string name;
    [SerializeField] string description;
    /*void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }//*/
}
