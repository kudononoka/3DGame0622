using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    /// <summary>NavMeshAgentコンポーネント</summary>
    private NavMeshAgent _nav;
    /// <summary>敵のアニメーター</summary>
    private Animator _anim;
    /// <summary>プレイヤーの位置</summary>
    [SerializeField] Transform _player;


    ////////////////////////////// 攻撃 //////////////////////////////////
    
    /// <summary>攻撃ダメージ判定のコライダー　敵の角に設置</summary>
    private BoxCollider _attack;
    /// <summary>攻撃開始までのカウントダウン</summary>
    private float _attackTime;
    /// <summary>攻撃開始 4</summary>
    private float _attackPlay = 4;
    /// <summary>攻撃の種類分け</summary>
    public float enemyAttackInterval;
    /// <summary>攻撃音</summary>
    [SerializeField] AudioSource _attackAudio;

    //////////////////////////////////////////////////////////////////////
    

    /// <summary>敵のスライダー</summary>
    [SerializeField] Slider _hp;
    /// <summary>HPの最大値 200</summary>
    private float _maxHP = 200;
    /// <summary>現在のHP </summary>
    private float _nowHP = 200;
    /// <summary>死判定</summary>
    public bool _die = false;
    private float _dietime = 0;
    /// <summary> 鳴き声</summary>
    [SerializeField] AudioSource _roar;　
    /// <summary>視覚　10度</summary>
    private float _angle = 10;


    // Start is called before the first frame update
    void Start()
    {
        _hp.maxValue = _maxHP;　
        _hp.value = _nowHP;　
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); 
        _nav = GetComponent<NavMeshAgent>();
        enemyAttackInterval = Random.Range(0, 10);
        _attack = GameObject.Find("enemyAttack").GetComponent<BoxCollider>();　
    }

    // Update is called once per frame
    void Update()
    {
       
        _attackTime += Time.deltaTime;
        if((Vector3.Distance(transform.position, _player.transform.position)) < 2.5) 
        {
            var diff = _player.transform.position - transform.position; 
            var angle = Vector3.Angle(transform.forward, diff);　
            if (angle <= _angle)　
            {
                if (_attackTime > _attackPlay)　
                {
                    enemyAttackInterval = Random.Range(0, 10);
                    if (enemyAttackInterval > 8)
                    {
                        _anim.SetTrigger("3conbo");
                        _attackTime = 0;
                    }
                    else if (enemyAttackInterval > 4)
                    {
                        _anim.SetTrigger("2Attack");
                        _attackTime = 0;
                    }
                    else if (enemyAttackInterval > 0)
                    {
                        _anim.SetTrigger("1Attack");
                        _attackTime = 0;
                    }
                }
            }
            else　//プレイヤーが敵の視覚から外れた時回転を開始
            {
                    transform.Rotate(Vector3.up * Time.deltaTime * 100f);
            }
        }
        if ((Vector3.Distance(transform.position, _player.transform.position)) < 7)　
        {
            _anim.SetFloat("walk", _nav.velocity.magnitude);
            _nav.SetDestination(_player.transform.position);
            if (_die)　
            {
                _anim.SetTrigger("Die");
                _nav.SetDestination(this.transform.position);
                _dietime += Time.deltaTime;
                if (_dietime > 5)　//アニメーションDieから５秒後に消失
                {
                    Destroy(this.gameObject);
                }
            }

        }
        else
        {
            _nav.SetDestination(this.transform.position);
            _anim.SetFloat("walk", 0f);
        }
    }
    public void EnemyDamage(float enemydamage)
    {
        _nowHP -= enemydamage;　 
        _hp.value = _nowHP;　
        if(_hp.value <= 0)  
        {
            _die = true;
        }
    }

    
    /////////////////// 攻撃アニメーションイベント //////////////////////////
    
    public void Onattack()　
    {
        
        _attack.enabled = true;
        _attackAudio.enabled = true;
    }

    public void Offattack()　
    {
        _attack.enabled = false;
        _attackAudio.enabled = false;
    }

    ////////////////////////////////////////////////////////////////////////

    public void OnAudio()　
    {
        _roar.enabled = true;
    }
    public void OffAudio()　
    {
        _roar.enabled = false;
    }
}
