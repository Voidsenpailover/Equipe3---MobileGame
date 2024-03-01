using UnityEngine;

[CreateAssetMenu(fileName = "Enemy-", menuName = "Scriptable Objects/Enemies", order = 0)]
public class EnemyStat : ScriptableObject
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private int _hits;
    [SerializeField] private float _speed;
    [SerializeField] private int _money;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private int _damage;
    [SerializeField] private bool _canBeStunned;
    [SerializeField] private bool _canBeSlowed;
    [SerializeField] private bool _canBeBurned;

    
    public EnemyTypes Enemy { get => _enemyType;set => _enemyType = value;}
    public int Hits { get => _hits; set => _hits = value;}
    public float Speed { get => _speed; set => _speed = value;}
    public int Money { get => _money; set => _money = value;}
    public SpriteRenderer Sprite { get => _spriteRenderer; set => _spriteRenderer = value;}
    public int Damage { get => _damage; set => _damage = value;}
    public bool CanBeStunned { get => _canBeStunned; set => _canBeStunned = value;}
    public bool CanBeSlowed { get => _canBeSlowed; set => _canBeSlowed = value;}
    public bool CanBeBurned { get => _canBeBurned; set => _canBeBurned = value;}
}
