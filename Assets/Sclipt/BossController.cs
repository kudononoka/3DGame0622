using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossController : MonoBehaviour
{
    private NavMeshAgent _nav;
    private Rigidbody _rb;
    private Animator _anim;
    public Transform _player;
    public float _time;
    public float enemyAttackInterval;
    public Slider _hp;
    private float _maxHP = 350;
    private float _nowHP = 350;
    private BoxCollider _attack;
    private CapsuleCollider _attack2;
    private float _angle = 180;
    private bool _die = false;
    private float _dietime = 0;
    private bool _rotate = false;
    private float _rotatetime = 0;

    [SerializeField] GameObject canvas;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioSource _audio2;
    [SerializeField] AudioSource _audio3;


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
        _attack = GameObject.Find("BossAttack").GetComponent<BoxCollider>();
        _attack2 = GameObject.Find("RotateAttack").GetComponent<CapsuleCollider>();
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

        _time += Time.deltaTime;
        if ((Vector3.Distance(transform.position, _player.transform.position)) < 7)
        {
            var playerdistance = _player.transform.position - this.transform.position;
            var angle = Vector3.Angle(playerdistance, this.transform.forward);
            if (angle <= _angle)
            {
                if (_time > 4)
                {
                    enemyAttackInterval = Random.Range(0, 10);
                    if (enemyAttackInterval > 7)
                    {
                        _rotate = true;
                        
                    }
                    else if (enemyAttackInterval > 5)
                    {
                        _anim.SetTrigger("3conbo");
                        _time = 0;
                    }
                    else if (enemyAttackInterval > 3)
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
        }
        else
        {
            if(_time > 4)
            {
                _anim.SetTrigger("Angle");
                float y = transform.rotation.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, y, 0);
                _time = 0;
            }
        }

        if(_rotate)
        {
            _rotatetime += Time.deltaTime;


            if (_rotatetime < 1.1f)
            {
                transform.Rotate(Vector3.down * Time.deltaTime * 200f);
            }
            else if (_rotatetime < 2f)
            {
                _attack2.enabled = true;
                _audio2.enabled = true;
                transform.Rotate(Vector3.up * Time.deltaTime * 600f);
            }
            else
            {
                _attack2.enabled=false;
                _audio2.enabled = false;
                _rotate = false;
                _rotatetime = 0;
                _time = 0;
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
    /*public void Onattack2()
    {
        _attack2.enabled = true;
        _audio2.enabled = true;
    }

    public void Offattack2()
    {
        _attack2.enabled = false;
        _audio2.enabled= false;
    }*/

    public void OnAudio()
    {
        _audio3.enabled = true;
    }

    public void OffAudio()
    {
        _audio3.enabled = false;
    }

}