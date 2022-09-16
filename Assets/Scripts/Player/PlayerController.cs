using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerUiCanvas playerUiCanvas;
    Rigidbody rb;
    private Animator animator;
    /// <summary>回転</summary>
    Quaternion targetRot;
    /// <summary>当たり判定</summary>
    public Collider[] Hitcollider;
    /// <summary>経過時間</summary>
    float currentTime = 0;
    private bool heal_flag = false;
    /// <summary>死んでいるのか</summary>
    bool isDie = false;
    [SerializeField] GameObject healObj;
    /// <summary>ベクトル</summary>
    Vector3 vel;
    /// <summary>体力</summary>
    [SerializeField]private float _playerHp = 100;
    /// <summary>Maxの体力</summary>
    [SerializeField] private float _playerMaxHp = 100;
    /// <summary>Maxのスタミナ</summary>
    [SerializeField] private float _maxStamina = 100;
    /// <summary>スタミナ</summary>
    [SerializeField] private float _stamina = 100;
    /// <summary>プレイヤーのスピード</summary>
    [SerializeField] private float _playerSpeed = 5;
    public float Hp
    { 
        get { return _playerHp;}
        set { _playerHp = value;}
    }  
    public float MaxHp
    {
        get { return _playerMaxHp; }
        set { _playerMaxHp = value; }
    }
    
    public float Stamina
    {
        get { return _stamina; }
        set { _stamina = value; }
    }
    
    public float MaxStamina
    {
        get { return _maxStamina; }
        set { _maxStamina = value; }
    }
    public float PlayerSpeed
    {
        get { return _playerSpeed; }
        set { _playerSpeed = value; }
    }
    public Animator Animator
    {
        get { return animator; }
        set { animator = value; }
    }
    private void Awake()
    {
        targetRot = transform.rotation;
    }
    void Start()
    {
        _playerHp = _playerMaxHp;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerUiCanvas.Init(this);
        HideColliderWeapon();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
            float distance = Vector3.Distance(transform.position, healObj.transform.position);
            if (distance <= 1.5)
            {
                heal_flag = true;
            }
            else
            {
                heal_flag = false;
            }
        //前のフレームから経過した秒数を加算
        currentTime += Time.deltaTime;

        //毎秒処理を行う
        if (currentTime >= 1.0f)
        {
            Heal();
            currentTime = 0;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        if (Input.GetMouseButton(1))
        {
            GuardNow();
            Debug.Log("Down");
        }
        else if (Input.GetMouseButtonUp(1))
        {
            NoGuard();
            Debug.Log("Up");
        }
    }
    private void FixedUpdate()
    {
        Move();
    }
    void Heal()
    {
        //フラグがtrueの時だけ処理する
        if (heal_flag)
        {
            playerUiCanvas.hpSlider.value += 3;
        }
    }
    private void Move()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //カメラの正面方向に移動するための変数。
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        vel = horizontalRotation * new Vector3(x, 0, z);
        vel.Normalize();
        rb.velocity = vel*_playerSpeed;      
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        if (speed == 2)
        {
            _stamina-=0.5f;
            _playerSpeed = 4;            
            if(_stamina<=20)
            {
                speed = 1;
                _playerSpeed = 2;
            }
            playerUiCanvas.UpdateStamina(_stamina);
        }
        else if(speed == 1)
        {
            _playerSpeed = 2;
            IncreseStamina(1);
        }
        else if(speed <= 0)
        {
            IncreseStamina(2.5f);
        }
        var rotationSpeed = 600 * Time.deltaTime;
        //移動方向を向く
        if (vel.magnitude > 0.5f)
        {
            targetRot = Quaternion.LookRotation(vel, Vector3.up);
        }                                //回転をなめらかに
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        animator.SetFloat("Speed", vel.magnitude * speed, 0.1f, Time.deltaTime);
    }
    void IncreseStamina(float increse)
    {
            //スタミナの自動回復
            _stamina+= increse ;
        if (_stamina >= _maxStamina)
        {
            _stamina = _maxStamina;
        }
        playerUiCanvas.UpdateStamina(_stamina);
    }
    void Attack()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // nGUI上をクリックしているので処理をキャンセルする。
            return;
        }
        animator.SetTrigger("Attack");
        _playerSpeed = 0;
    }
        void GuardNow()
        { animator.SetBool("Guard", true); }
        void NoGuard()
        { animator.SetBool("Guard", false); }
    public void HideColliderWeapon()
    {
        for (int i = 0; i < Hitcollider.Length; i++)
        {
            Hitcollider[i].enabled = false;
        }
    }

    public void ShowColliderWeapon()
    {
        for (int r = 0; r < Hitcollider.Length; r++)
        {
            Hitcollider[r].enabled = true;
        }
    }
    /// <summary>ダメージ </summary>
    /// <param name="damage"></param>
    public  void Damage(int damage)
    {
        if(isDie)
        {
            return;
        }
        _playerHp -= damage;
        playerUiCanvas.UpdateHp(_playerHp);
        if (_playerHp <= 0)
        {
            _playerHp = 0;
            animator.SetTrigger("Die");
            isDie = true;
            //死んだあと動かないように
            HideColliderWeapon();
            rb.velocity = Vector3.zero;
        }
    }
    /*/// <summary>Hpを減らす関数 </summary>
    /// <param name="damage"></param>
    public void TakeHit(float damage)
    {
        playerHp = (int)Mathf.Clamp(playerHp - damage, 0, playerMaxHp);

        playerUiCanvas.hpSlider.value = playerHp;

        if (playerHp <= 0 && !GameState.gameOver)
        {
            GameState.gameOver = true;
        }
    }*/
}
