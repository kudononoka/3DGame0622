using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    /// <summary>接地判定のスクリプト</summary>
    [SerializeField] Ground ground;　
    /// <summary>プレイヤーのアニメーター</summary>
    private Animator _anim = null;　
    /// <summary>重力</summary>
    private Rigidbody _rb = null;　
    /// <summary>接地判定</summary>
    private bool isGround = false; 
   
    
    ////////////////////////////　攻撃　//////////////////////////////
    
    /// <summary>攻撃ダメージ判定のコライダー　左手の刀に設置</summary>
    private Collider _leftAttack;
    /// <summary>攻撃ダメージ判定のコライダー　右手の刀に設置</summary>
    private Collider _rightAttack;
    /// <summary>攻撃時のパーティカル 左手</summary>
    [SerializeField] GameObject _leftPartical;
    /// <summary>攻撃時のパーティカル 右手</summary>
    [SerializeField] GameObject _rightPartical;
    /// <summary>攻撃音　左手</summary>
    [SerializeField] AudioSource _leftaudio;　
    /// <summary>攻撃音　右手</summary>
    [SerializeField] AudioSource _rightaudio;

    //////////////////////////////////////////////////////////////////


    /// <summary>歩く速度</summary>
    private float walkSpeed = 5f;
    /// <summary>HPの最大値  200</summary>
    private float _maxHP = 200f;
    /// <summary>現在のHP</summary>
    private float _nowHP = 200f;
    /// <summary>HP用のスライダー</summary>
    [SerializeField] Slider _hpSlider;
    /// <summary>SP用のスライダー</summary>
    [SerializeField] Slider _spSlider;
    /// <summary>SPの最大値 100</summary>
    private float _maxSP = 100;
    /// <summary>最初のSPを0に</summary>
    private float _nowSP = 0;
    /// <summary>必殺技判定</summary>
    private bool isSpAction = false;
    /// <summary>攻撃してもSPは0に</summary>
    public bool isSpValue0 = false;
    /// <summary>回避判定</summary>
    private bool isAvoid = false; 
    /// <summary>回避移動時間</summary>
    private float _avoidtime = 0;
    /// <summary>回避移動時間の制限 0.7</summary>
    private float _avoidtimeLimit = 0.7f;
   
    /// <summary>ゲームオーバー時のキャンバス</summary>
    [SerializeField] GameObject _gameOver;


    void Start()
    {
        _hpSlider.maxValue = _maxHP;
        _hpSlider.value = _nowHP;
        _spSlider.maxValue = _maxSP;
        _spSlider.value = _nowSP;
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _leftAttack = GameObject.Find("playerAttack2").GetComponent<BoxCollider>();　
        _rightAttack = GameObject.Find("playerAttack").GetComponent<BoxCollider>();
    }

    void Update()
    {
        isGround = ground.IsGround();
        LateUpdate();　

        if (Input.GetKeyDown(KeyCode.Z))　
        {
            
            _anim.SetBool("attack", true);
            
        }
        else
        {
            _anim.SetBool("attack", false);
        }


        if (isSpAction)　
        {
            if (Input.GetKeyDown(KeyCode.C)) 
            {
                _anim.SetTrigger("attack3");
                _spSlider.value = 0f;
                isSpAction = false;
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
            float x = Input.GetAxisRaw("Horizontal"); //矢印キー移動
            float z = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))  
            {
                isAvoid = true;
            }

            if(isAvoid)
            {
                _avoidtime += Time.deltaTime;
                if(_avoidtime < _avoidtimeLimit) 
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 4.2f);　
                }
                else
                {
                    isAvoid = false;
                    _avoidtime = 0f;
                }
            }

           
            _rb.velocity = new Vector3(x * walkSpeed, 0, z * walkSpeed);　//移動


            if (x == 1)　//矢印キーの方向によって角度変更
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);　//右
            }
            if (x == -1)
            {
                transform.rotation = Quaternion.Euler(0, -90, 0); //左
            }
            if (z == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); //前
            }
            if (z == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); //後ろ
            }
            if (_rb.velocity.magnitude > 0.1f)
            {
                _anim.SetFloat("Speed", _rb.velocity.magnitude);

                if (Input.GetKey(KeyCode.LeftShift))　//矢印キー＋左shift：走行
                {
                    _anim.SetBool("run", true);
                    walkSpeed = 8;
                    if (Input.GetKeyDown(KeyCode.Space))　//回避
                    {
                        _anim.SetBool("run", false);
                        _anim.SetFloat("Speed", 0f);
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Space))　//回避
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
        _hpSlider.value = _hpSlider.value - damage;
        if(_hpSlider.value == 0)　//HPが0の時
        {
            Destroy(this.gameObject);　
            _gameOver.SetActive(true);　
        }
    }

    public void Sp(int sp)
    {
        if (isSpValue0) 
        {
            _spSlider.value = 0;
        }
        else
        {
            _spSlider.value = _spSlider.value + sp;
            if (_spSlider.value == _maxSP) 
            {
                isSpAction = true;　
            }
        }
    }



    /////////////////////  攻撃アニメーションのイベント  /////////////////////////
    
    public void Onattack()　
    { 
        _leftAttack.enabled = true;
        _leftaudio.enabled = true;
    }

    public void Offattack()　
    {
        _leftAttack.enabled = false;
        _leftaudio.enabled = false;
        
    }

    public void Onattack2()
    {
        _rightAttack.enabled = true;
        _rightaudio.enabled = true;
    }

    public void Offattack2()
    {
        _rightAttack.enabled = false;
        _rightaudio.enabled=false;
    }

    public void OnAudio()　//必殺技専用の音とパーティカル
    {
        _leftaudio.enabled = true;
    }

    public void OffAudio()
    {
        _leftaudio.enabled = false;
    }

    public void OnAudio2()
    {
        _rightaudio.enabled = true;
    }

    public void OffAudio2()
    {
        _rightaudio.enabled = false;
    }

    public void Oneffect()
    {
        _leftPartical.SetActive(true);
    }

    public void Offeffect()
    {
        _leftPartical.SetActive(false);
    }

    public void Oneffect2()
    {
        _rightPartical.SetActive(true);
    }

    public void Offeffect2()
    {
        _rightPartical.SetActive(false);
    }

    public void Offsp() 
    {
        isSpValue0 = true;
    }
    public void Onsp()
    {
        isSpValue0 = false;
    }
}