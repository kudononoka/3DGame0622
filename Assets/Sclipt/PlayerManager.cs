using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Ground ground;
    private Animator _anim = null;
    private Rigidbody _rb = null;
    private bool isGround = false; //接地判定
   
    
    
    private Collider _attack;
    private Collider _attack2;
    public bool attack = false;
    public bool attack2 = false;
    [SerializeField] GameObject _effect;
    [SerializeField] GameObject _effect2;
    [SerializeField] AudioSource _audio;
    [SerializeField] AudioSource _audio2;


    private float walkSpeed = 5f; //歩く速度
    
    private float _maxHP = 200f;　//HP
    private float _nowHP = 200f;  //現在のHP
    public Slider slider;
    [SerializeField] Slider _mpSlider;
    private float _maxMP = 100f;
    private float _nowMP = 0f;
    private bool _spAction = false;
    public bool _mpNo = false;
    private bool _avoid = false;
    private float _avoidtime = 0;
   

    [SerializeField] GameObject _canvas;


    void Start()
    {
        _mpSlider.maxValue = _maxMP;　
        _mpSlider.value = _nowMP;
        slider.maxValue = _maxHP;
        slider.value = _nowHP;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _attack = GameObject.Find("playerAttack2").GetComponent<BoxCollider>();
        _attack2 = GameObject.Find("playerAttack").GetComponent<BoxCollider>();
        _attack.enabled = false;
    }

    void Update()
    {
        isGround = ground.IsGround();
        LateUpdate();

        if (Input.GetKeyDown(KeyCode.Z))
        {
            //attack = true;
            _anim.SetBool("attack", true);
            
        }
        else
        {
            _anim.SetBool("attack", false);
        }


        if (_spAction)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _anim.SetTrigger("attack3");

                _mpSlider.value = 0f;
                _spAction = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            _anim.SetBool("attack2", true);
        }
        else
        {
            _anim.SetBool("attack2", false);
        }
    }

    





    void LateUpdate()
    {
        if (isGround)
        {
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))  //回避判定
            {

                _avoid = true;

            }

            if(_avoid)
            {
                _avoidtime += Time.deltaTime;
                if(_avoidtime < 0.7f) //スペースが押された後から0.7たったら
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 4.2f);　
                }
                else
                {
                    _avoid = false;
                    _avoidtime = 0f;
                }
            }

           
            _rb.velocity = new Vector3(x * walkSpeed, 0, z * walkSpeed);


            if (x == 1)
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            if (x == -1)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            if (z == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            if (z == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            if (_rb.velocity.magnitude > 0.1f)
            {
                _anim.SetFloat("Speed", _rb.velocity.magnitude);

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    _anim.SetBool("run", true);
                    walkSpeed = 8;
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        _anim.SetBool("run", false);
                        _anim.SetFloat("Speed", 0f);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    _anim.SetFloat("Speed", 0f);
                }
                else
                {
                    _anim.SetBool("run", false);
                    walkSpeed = 5;
                }
            }
            else
            {
                _anim.SetBool("run", false);
                _anim.SetFloat("Speed", 0f);
            }
        }
    }



    public void Damage(int damage)
    {
        slider.value = slider.value - damage;
        if(slider.value == 0)
        {
            Destroy(this.gameObject);
            _canvas.SetActive(true);
        }
    }

    public void Mp(int mp)
    {
        if (_mpNo)
        {
            _mpSlider.value = 0;
        }
        else
        {
            _mpSlider.value = _mpSlider.value + mp;
            if (_mpSlider.value == 100)
            {
                _spAction = true;
            }
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

    public void Onattack2()
    {
        _attack2.enabled = true;
        _audio2.enabled = true;
    }

    public void Offattack2()
    {
        _attack2.enabled = false;
        _audio2.enabled=false;
    }

    public void OnAudio()
    {
        _audio.enabled = true;
    }

    public void OffAudio()
    {
        _audio.enabled = false;
    }

    public void OnAudio2()
    {
        _audio2.enabled = true;
    }

    public void OffAudio2()
    {
        _audio2.enabled = false;
    }

    public void Oneffect()
    {
        _effect.SetActive(true);
    }

    public void Offeffect()
    {
        _effect.SetActive(false);
    }

    public void Oneffect2()
    {
        _effect2.SetActive(true);
    }

    public void Offeffect2()
    {
        _effect2.SetActive(false);
    }

    public void Offmp()
    {
        _mpNo = true;
    }
    public void Onmp()
    {
        _mpNo = false;
    }
}