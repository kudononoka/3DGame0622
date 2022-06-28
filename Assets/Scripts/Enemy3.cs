using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


///////////////////////////   EnemyMoveスクリプトと同一  ///////////////////////////

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy3 : MonoBehaviour　
{
    private NavMeshAgent _nav;
    private Rigidbody _rb;
    private Animator _anim;
    public Transform _player;
    private float _time;
    private float _dietime = 0;
    public float enemyAttackInterval;
    public Slider _hp;
    private float _maxHP = 200;
    private float _nowHP = 200;
    private BoxCollider _attack;
    public bool _die = false;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioSource _audio2;
    private float _angle = 10;



    // Start is called before the first frame update
    void Start()
    {
        _hp.maxValue = _maxHP;
        _hp.value = _nowHP;
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _nav = GetComponent<NavMeshAgent>();
        enemyAttackInterval = Random.Range(0, 10);
        _attack = GameObject.Find("enemyAttack3").GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if ((Vector3.Distance(transform.position, _player.transform.position)) < 2.5)
        {
            var diff = _player.transform.position - transform.position;
            var angle = Vector3.Angle(transform.forward, diff);
            if (angle <= _angle)
            {
                if (_time > 4)
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
            else　
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
                if (_dietime > 5)
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
        Debug.Log(enemydamage);
        _nowHP -= enemydamage;
        _hp.value = _nowHP;
        if (_hp.value <= 0)
        {
            _die = true;
        }
    }

    public void Onattack()
    {

        _attack.enabled = true;
        _audio.enabled = true;
    }

    public void Offattack()
    {
        _attack.enabled = false;
        _audio.enabled = false;
    }

    public void OnAudio()
    {
        _audio2.enabled = true;
    }
    public void OffAudio()
    {
        _audio2.enabled = false;
    }
}

