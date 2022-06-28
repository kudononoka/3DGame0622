using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : MonoBehaviour
{
    ///<summary>NavMeshAgentコンポーネント</summary>
    private NavMeshAgent _nav;
    /// <summary>ボス敵のアニメーター</summary>
    private Animator _anim;
    /// <summary>プレイヤーの位置</summary>
    [SerializeField] Transform _player;


    //////////////////////////  攻撃　///////////////////////////////////

    /// <summary>攻撃ダメージ判定のコライダー　ボス敵の角に設置</summary>
    private BoxCollider _attack;
    /// <summary>攻撃ダメージ判定のコライダー　回転攻撃</summary>
    private CapsuleCollider _attackRotate;
    /// <summary>攻撃までのカウントダウン</summary>
    private float _attackTime;
    /// <summary>攻撃開始 4</summary>
    private float _attackPlay = 4;
    /// <summary>攻撃の種類分け</summary>
    public float enemyAttackInterval;

    /// <summary>回転攻撃判定</summary>
    private bool _rotate = false;
    /// <summary>回転攻撃の予備動作の制限時間 1.1</summary>
    private float _rotatePreparation = 1.1f;
    /// <summary>回転攻撃の制限時間 2</summary>
    private float _rotateAttackPlay = 2f;
    /// <summary>攻撃開始から攻撃終了までの時間</summary>
    private float _rotatetime = 0;

    /// <summary>通常攻撃音</summary>
    [SerializeField] AudioSource _attackAudio;
    /// <summary>回転攻撃音</summary>
    [SerializeField] AudioSource _rotateAttackAudio;

    //////////////////////////////////////////////////////////////////////


    /// <summary>HPのスライダー</summary>
    [SerializeField] Slider _hp;
    /// <summary>HPの最大値 350</summary>
    private float _maxHP = 350;
    /// <summary>現在のHP </summary>
    private float _nowHP = 350;
    /// <summary>視覚　180度</summary>
    private float _angle = 100;
    /// <summary>死判定</summary>
    private bool _die = false;
    /// <summary>ゲームクリアのキャンバス表示までのカウントダウン</summary>
    private float _dietime = 0;
    /// <summary>鳴き声</summary>
    [SerializeField] AudioSource _roar;

    /// <summary>ゲームクリア時のキャンバス</summary>
    [SerializeField] GameObject canvas;
    

    // Start is called before the first frame update
    void Start()
    {
        _hp.maxValue = _maxHP;
        _hp.value = _nowHP;
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _nav = GetComponent<NavMeshAgent>();
        enemyAttackInterval = Random.Range(0, 10);
        _attack = GameObject.Find("BossAttack").GetComponent<BoxCollider>();
        _attackRotate = GameObject.Find("RotateAttack").GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_die)
        {
            _anim.SetTrigger("die");
            _nav.SetDestination(this.transform.position);
            _dietime += Time.deltaTime;
            if (_dietime > 3)
            {
                canvas.SetActive(true);
            }
        }

        _attackTime += Time.deltaTime;
        if ((Vector3.Distance(transform.position, _player.transform.position)) < 7)
        {
            var playerdistance = _player.transform.position - this.transform.position;
            var angle = Vector3.Angle(playerdistance, this.transform.forward);
            if (angle <= _angle)
            {
                if (_attackTime > _attackPlay)
                {
                    enemyAttackInterval = Random.Range(0, 10);
                    if (enemyAttackInterval > 7)
                    {
                        _rotate = true;

                    }
                    else if (enemyAttackInterval > 5)
                    {
                        _anim.SetTrigger("3conbo");
                        _attackTime = 0;
                    }
                    else if (enemyAttackInterval > 3)
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
        }
        if ((Vector3.Distance(transform.position, _player.transform.position)) < 11)
        {
            _anim.SetFloat("walk", _nav.velocity.magnitude);
            _nav.SetDestination(_player.transform.position);

        }
        else
        {
            _nav.SetDestination(this.transform.position);
            _anim.SetFloat("walk", 0f);
        }
        

        if(_rotate)
        {
            _rotatetime += Time.deltaTime;


            if (_rotatetime < _rotatePreparation)
            {
                transform.Rotate(Vector3.down * Time.deltaTime * 200f);
            }
            else if (_rotatetime < _rotateAttackPlay)　
            {
                _attackRotate.enabled = true;
                _rotateAttackAudio.enabled = true;
                transform.Rotate(Vector3.up * Time.deltaTime * 600f);
            }
            else
            {
                _attackRotate.enabled=false;
                _rotateAttackAudio.enabled = false;
                _rotate = false;
                _rotatetime = 0;
                _attackTime = 0;
            }
        }

    }

    public void EnemyDamage(float enemydamage)
    {
        Debug.Log(enemydamage);
        _nowHP -= enemydamage;
        _hp.value = _nowHP;
        if(_hp.value <= 0)
        {
            _die = true;
        }

    }
    

    ////////////////////　攻撃アニメーションのイベント  /////////////////////////
    
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

    /////////////////////////////////////////////////////////////////////////////
    
    
    public void OnAudio()
    {
        _roar.enabled = true;
    }

    public void OffAudio()
    {
        _roar.enabled = false;
    }

}