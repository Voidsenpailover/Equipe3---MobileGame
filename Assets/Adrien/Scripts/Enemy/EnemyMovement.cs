using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using System;
using System.Collections;

public class EnemyMovement : MonoBehaviour
  {
  
    public static event Action OnHealthChanged;

    public float MoveSpeed;
    private Rigidbody2D rb;
    private Transform target;
    [SerializeField] private int Point;
    private float radius = 0.8f;
    private SpriteRenderer _spriteRenderer;
    private bool reachedEnd;
    public float HP;
  
    
    public bool stunned;
    private Coroutine stunCoroutine;
    private float stunSeconds;
    
    private Coroutine stunDurCoroutine;
    private bool stunDur;
    private float stunDurCooldown = 5.0f;
    private int stunCount;
    
    public bool isBurning;
    private Coroutine burnCoroutine;
    private float burnSeconds;
    private float burnDamage;
    
    private Coroutine slowCoroutine;
    public bool isSlowed;
    private float slowSeconds;
    private float slowPercent;
    
    private Coroutine debuffCoroutine;
    public bool isDebuffed;
    private float debuffSeconds;
    public float debuffPercent = 1;
    
    public bool isMercure;
    public int mercureBonus;

    [SerializeField] private Animator _animator;
    private Animator _animatorChild;

    public EnemyStat EnemyStat {get; private set;}

    private void Start()
    {
      _animator = GetComponent<Animator>();
      rb = GetComponent<Rigidbody2D>();
      target = LevelManager.instance.Chemin[0];
      _animatorChild = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
      _animator.SetFloat("velocityX", rb.velocity.x);
      _animator.SetFloat("velocityY", rb.velocity.y);
      
      
      if (!reachedEnd && Vector2.Distance(target.position, transform.position) <= 0.1f)
      {
        Point++;
        rb.velocity = Vector2.zero;
        if (Point >= LevelManager.instance.Chemin.Length)
        {
          reachedEnd = true;
          LevelManager.instance.HP -= EnemyStat.Damage;
          OnHealthChanged?.Invoke();
          EnemySpawner._instance.EnemyReachedEndOfPath();
          Destroy(gameObject);
          if (LevelManager.instance.HP <= 0)
          {
            LevelManager.instance.GameOver();
          }
          return;
        }else
        {
          target = LevelManager.instance.Chemin[Point];
        }
      }
    }

    private void FixedUpdate()
    {
      if (!stunned) {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * MoveSpeed;
      } else {
        rb.velocity = Vector2.zero;
      }
    }

    
    
    
    private void OnDrawGizmos()
    {
#if UNITY_EDITOR

        Handles.color = Color.red;
      Handles.DrawWireDisc(transform.position, transform.forward, radius);
#endif
    }
  
    public void InitializeEnemies(EnemyStat enemyStat)
    {
      EnemyStat = enemyStat;
      MoveSpeed = enemyStat.Speed;
      HP = enemyStat.Hits;
      _animator.runtimeAnimatorController = enemyStat.AnimatorController;
    }

    public void ApplyStun(float duration)
    {
      if(stunned || stunDur) {
        return;
      }
      
      stunCount++;
      if(stunCount > 1) {
        stunDur = true;
        return;
      }
      stunned = true;
      _animatorChild.SetBool("Stun", true);
      stunSeconds = 0;
      if(stunCoroutine != null) {
        StopCoroutine(stunCoroutine);
      }
      
      stunSeconds =  duration;
      stunCoroutine = StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
      var start = Time.time;
      var total = stunSeconds;
      
      
      if(stunDurCoroutine != null) {
        StopCoroutine(stunDurCoroutine);
      }
      stunDurCoroutine = StartCoroutine(StunDur());
      
      while(stunSeconds > 0) {
        stunned = true;
        stunSeconds = total - (Time.time - start);
        yield return new WaitForEndOfFrame();
      }
      _animatorChild.SetBool("Stun", false);
      stunned = false;
      yield return null;
    }
    
    private IEnumerator StunDur()
    {
      var start = Time.time;
      var total = stunDurCooldown;
      
      var cooldown = stunDurCooldown;
      while(cooldown > 0) {
        cooldown = total - (Time.time - start);
        yield return new WaitForEndOfFrame();
      }
      
      stunDur = false;
      stunCount = 0;
      yield return null;
    }
    
    public void ApplyBurn(float duration, float damage)
    {
      if(isBurning) {
        return;
      }
      isBurning = true;
      _animatorChild.SetBool("Burn", true);
      burnSeconds = 0;
      if(burnCoroutine != null) {
        StopCoroutine(burnCoroutine);
      }
      burnDamage = damage;
      burnSeconds =  duration;
      burnCoroutine = StartCoroutine(Burn());
    }

    private IEnumerator Burn()
    {
      var start = Time.time;
      var total = burnSeconds;
      
      while (burnSeconds > 0) {
        isBurning = true;
        HP -= burnDamage;
        if(HP <= 0) {
          EnemySpawner._instance.EnemyReachedEndOfPath();
          LevelManager.instance.money += EnemyStat.Money;
          Destroy(gameObject);
        }
        burnSeconds = total - (Time.time - start);
        yield return new WaitForSeconds(1f);
      }
      _animatorChild.SetBool("Burn", false);  
      isBurning = false;
      yield return null;
    }
    
    
    
    public void ApplySlow(int duration, float percent)
    {
      if(isSlowed) {
        return;
      }
      
      isSlowed = true;
      _animatorChild.SetBool("Slow", true);
      slowPercent = percent;
      slowSeconds = 0;
      
      if(slowCoroutine != null) {
        StopCoroutine(slowCoroutine);
      }
      
      slowSeconds = duration;
      slowCoroutine = StartCoroutine(Slow());
    }
    
    private IEnumerator Slow()
    {
      var start = Time.time;
      var total = slowSeconds;
      
      while (slowSeconds > 0) {
        MoveSpeed *= slowPercent;
        slowSeconds = total - (Time.time - start);
        yield return new WaitForSeconds(slowSeconds);
      }
      
      MoveSpeed = EnemyStat.Speed;
      _animatorChild.SetBool("Slow", false);
      isSlowed = false;
      yield return null;
    }
    
    
    public void ApplyDebuff(int duration)
    {
      if(isDebuffed) {
        return;
      }
      
      isDebuffed = true;
      _animatorChild.SetBool("Debuff", true);
      debuffSeconds = 0;
      
      if(debuffCoroutine != null) {
        StopCoroutine(debuffCoroutine);
      }
      
      debuffSeconds = duration;
      debuffCoroutine = StartCoroutine(Debuff());
    }
    
    private IEnumerator Debuff()
    {
      var start = Time.time;
      var total = debuffSeconds;
      
      while (debuffSeconds > 0) {
        debuffSeconds = total - (Time.time - start);
        yield return new WaitForSeconds(debuffSeconds);
      }
      _animatorChild.SetBool("Debuff", false);
      isDebuffed = false;
      yield return null;
    }

    public void ApplyMercure(int money)
    {
      if (isMercure) return;
        isMercure = true;
        _animatorChild.SetBool("Mercure", true);
        mercureBonus = money;
    }
  }

