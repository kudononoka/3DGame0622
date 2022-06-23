using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Animator _anim;
    [SerializeField] Transform _player;
    private float _time; 
    private float _dietime = 0;
    public float enemyAttackInterval; 
    [SerializeField] Slider _hp;
    private float _maxHP = 200;　//HP最大値
    private float _nowHP = 200;　//現在のHP
    private BoxCollider _attack;　//攻撃時のコライダー
    public bool _die = false;
    [SerializeField] AudioSource _audio; //攻撃音
    [SerializeField] AudioSource _audio2;　//鳴き声
    private float _angle = 10;



    // Start is called before the first frame update
    void Start()
    {
        _hp.maxValue = _maxHP;　//スライダーの最大値200
        _hp.value = _nowHP;　//最初のHP200をスライダーに反映
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); //プレイヤーの位置を取得
        _nav = GetComponent<NavMeshAgent>();
        enemyAttackInterval = Random.Range(0, 10);
        _attack = GameObject.Find("enemyAttack").GetComponent<BoxCollider>();　//プレイヤーに与えるダメージのコライダー取得
    }

    // Update is called once per frame
    void Update()
    {
       
        _time += Time.deltaTime;
        if((Vector3.Distance(transform.position, _player.transform.position)) < 2.5) 
        {
            var diff = _player.transform.position - transform.position; //敵からプレイヤーまでのベクトル(ベクトルA)取得
            var angle = Vector3.Angle(transform.forward, diff);　//敵の前方のベクトルとベクトルAの角度取得
            if (angle <= _angle)　
            {
                if (_time > 4)　//4秒ごとに攻撃
                {
                    enemyAttackInterval = Random.Range(0, 10);
                    if (enemyAttackInterval > 8)
                    {
                        _anim.SetTrigger("3conbo");
                        _time = 0;
                    }
                    else if (enemyAttackInterval > 4)
                    {
                        _anim.SetTrigger("2Attack");
                        _time = 0;
                    }
                    else if (enemyAttackInterval > 0)
                    {
                        _anim.SetTrigger("1Attack");
                        _time = 0;
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
            if (_die)　//関連：行103
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
        if(_hp.value <= 0)  //HPが０の時　行81に反映
        {
            _die = true;
        }
    }


    public void Onattack()　//攻撃アニメーションイベント//ダメージを与えるコライダーを表示
    {
        
        _attack.enabled = true;
        _audio.enabled = true;
    }

    public void Offattack()　//非表示
    {
        _attack.enabled = false;
        _audio.enabled = false;
    }

    public void OnAudio()　//鳴き声//表示
    {
        _audio2.enabled = true;
    }
    public void OffAudio()　//非表示
    {
        _audio2.enabled = false;
    }
}
