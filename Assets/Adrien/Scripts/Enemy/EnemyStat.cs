using UnityEngine;

[CreateAssetMenu(fileName = "Enemy-", menuName = "Scriptable Objects/Enemies", order = 0)]
public class EnemyStat : ScriptableObject
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private Vulnerability _vulnerability;
    [SerializeField] private int _hits;
    [SerializeField] private float _speed;
    [SerializeField] private int _money;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _damage;
    [SerializeField] private bool _canBeStunned;
    


    
    public EnemyTypes Enemy { get => _enemyType;set => _enemyType = value;}
    public Vulnerability Vulnerability { get => _vulnerability; set => _vulnerability = value;}
    public int Hits { get => _hits; set => _hits = value;}
    public float Speed { get => _speed; set => _speed = value;}
    public int Money { get => _money; set => _money = value;}
    public SpriteRenderer Sprite { get => _spriteRenderer; set => _spriteRenderer = value;}
    public int Damage { get => _damage; set => _damage = value;}
    public bool CanBeStunned { get => _canBeStunned; set => _canBeStunned = value;}
    
}
