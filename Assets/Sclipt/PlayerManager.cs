using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Ground ground;　//接地判定のスクリプト取得
    private Animator _anim = null;　//アニメーター
    private Rigidbody _rb = null;　//重力
    private bool isGround = false; //接地判定
   
    
    
    private Collider _attack;　//攻撃
    private Collider _attack2;
    [SerializeField] GameObject _effect;　//攻撃時のパーティカル
    [SerializeField] GameObject _effect2;
    [SerializeField] AudioSource _audio;　//攻撃時の音
    [SerializeField] AudioSource _audio2;


    private float walkSpeed = 5f; //歩く速度
    private float _maxHP = 200f;　//HPの最大値
    private float _nowHP = 200f;  //現在のHP
    public Slider slider;　//HP用のスライダー取得
    [SerializeField] Slider _mpSlider;　//SP用のスライダー取得
    private float _maxMP = 100;　//SPの最大値
    private float _nowMP = 0; //最初のSPを0に
    private bool _spAction = false; //必殺技
    public bool _spNo = false; 
    private bool _avoid = false; //回避
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
        _attack = GameObject.Find("playerAttack2").GetComponent<BoxCollider>();　//敵にダメージを与えるコライダー
        _attack2 = GameObject.Find("playerAttack").GetComponent<BoxCollider>();
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


        if (_spAction)　//関連；196
        {
            if (Input.GetKeyDown(KeyCode.C)) //必殺技
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
            float x = Input.GetAxisRaw("Horizontal"); //移動
            float z = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Space))  //回避判定
            {
                _avoid = true;
            }

            if(_avoid)
            {
                _avoidtime += Time.deltaTime;
                if(_avoidtime < 0.7f) //スペースが押された後から0.7経過
                {
                    transform.Translate(Vector3.back * Time.deltaTime * 4.2f);　
                }
                else
                {
                    _avoid = false;
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
                transform.rotation = Quaternion.Euler(0, 0, 0); //上
            }
            if (z == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); //下
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
        slider.value = slider.value - damage;
        if(slider.value == 0)　//HPが0の時
        {
            Destroy(this.gameObject);　
            _canvas.SetActive(true);　//ゲームオーバーのキャンバス表示
        }
    }

    public void Sp(int sp)
    {
        if (_spNo) //必殺技発動中はSPは0
        {
            _mpSlider.value = 0;
        }
        else
        {
            _mpSlider.value = _mpSlider.value + sp;
            if (_mpSlider.value == 100) //SP満タンで必殺技発動可能
            {
                _spAction = true;　//行68反映
            }
        }
    }

    public void Onattack()　//敵にダメージを与えるコライダー//攻撃音　を表示
    { 
        _attack.enabled = true;
        _audio.enabled = true;
    }

    public void Offattack()　//非表示
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

    public void OnAudio()　//必殺技専用の音とパーティカル
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

    public void Offsp() //必殺技
    {
        _spNo = true;
    }
    public void Onsp()
    {
        _spNo = false;
    }
}