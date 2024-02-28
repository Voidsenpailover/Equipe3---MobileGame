using UnityEngine;

[CreateAssetMenu(fileName = "Enemy-", menuName = "Scriptable Objects/Enemies", order = 0)]
public class EnemyStat : ScriptableObject
{
    [SerializeField] private EnemyTypes _enemyType;
    [SerializeField] private int _hits;
    [SerializeField] private float _speed;
    [SerializeField] private int _money;
    public EnemyTypes Enemy
    {
        get => _enemyType;
        set => _enemyType = value;
    }
    public int Hits
    {
        get => _hits;
        set => _hits = value;
    }
    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }
    public int Money
    {
        get => _money;
        set => _money = value;
    }
}
