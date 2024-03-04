using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret-", menuName = "Scriptable Objects/Turret", order = 0)]
public class TurretsData : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _cost;
    [SerializeField] private int _damage;
    [SerializeField] private float _delayBetweenAtk;
    [SerializeField] private float _radAtk;
    [SerializeField] private GameObject _AtkSFX;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private TurretType _type;
    [SerializeField] private int _level;
    [SerializeField] private TurretAtk _atkType;
    
    
    public string Name { get => _name; set => _name = value; }
    public string Description { get => _description; set => _description = value; }
    public int Cost { get => _cost; set => _cost = value; }
    public int Damage { get => _damage; set => _damage = value; }
    public float DelayBetweenAtk { get => _delayBetweenAtk; set => _delayBetweenAtk = value;}
    public float RadAtk { get => _radAtk; set => _radAtk = value; }
    public GameObject AtkSFX { get => _AtkSFX; set => _AtkSFX = value; }
    public Sprite Sprite { get => _sprite; set => _sprite = value; }
    public TurretType Type { get => _type; set => _type = value; }
    public TurretAtk AtkType { get => _atkType; set => _atkType = value; }
    public int Level { get => _level; set => _level = value; }
}
