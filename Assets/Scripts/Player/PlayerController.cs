using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController :HumanManager,IDamageInterFace
{
    [SerializeField] private AudioManager audioManager; 
    [SerializeField] private PlayerUiCanvas playerUiCanvas;
    [SerializeField] private Damager textSenyouDamager;
    Rigidbody rb;
    /// <summary>回転</summary>
    Quaternion targetRot;
    /// <summary>経過時間</summary>
    private float  currentTime = 0;
    private bool heal_flag = false;
    [SerializeField,Header("回復オブジェクト")] GameObject healObj;
    [SerializeField, Header("ザコ敵")] GameObject zakotekiCollider;
    /// <summary>ベクトル</summary>
    Vector3 vel;
    /// <summary>Maxのスタミナ</summary>
    [SerializeField,Header("Maxのスタミナ")] protected float _maxStamina = 100;
    /// <summary>スタミナ</summary>
    [SerializeField,Header("スタミナ")] protected float _stamina = 100;
    /// <summary>プレイヤーのスピード</summary>
    [SerializeField,Header("プレーヤーのスピード")] private float _playerSpeed = 5;
    [SerializeField, Header("ガードのコライダー")] Collider _guardCollider;
    /// <summary>アニメーションのブレンドツリーで切り替える際のスピード</summary>
    private int speed;
    public int Speed => speed;
    private PauseManager _pauseManager;
    private bool _isStop;
    public float Stamina => _stamina;

    public float MaxStamina => _maxStamina;
    public float PlayerSpeed { get => _playerSpeed; set => _playerSpeed = value; }
    private void Awake()
    {
        targetRot = transform.rotation;
        Cursor.lockState = CursorLockMode.Locked;
        _pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }
    void OnEnable()
    {
        // 呼んで欲しいメソッドを登録する。
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable ではメソッドの登録を解除すること。さもないとオブジェクトが無効にされたり破棄されたりした後にエラーになってしまう。
        _pauseManager.OnPauseResume -= PauseResume;
    }

    void PauseResume(bool isPause)
    {
        if (isPause)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }
    /// <summary>停止</summary>
    public void Pause()
    {
        _isStop = true;
        Cursor.lockState = CursorLockMode.None;
        // カーソル表示
        Cursor.visible = true;
    }
    /// <summary>再生</summary>
    public void Resume()
    {
        _isStop = false;
        Cursor.lockState = CursorLockMode.Locked;
        // カーソル非表示
        Cursor.visible = false;
    }
    void Start()
    {
       // Hp = MaxHp;
        _isStop = false;
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        playerUiCanvas.Init(this);
        HideColliderWeapon();
       // saveManager = GetComponent<SaveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // nGUI上をクリックしているので処理をキャンセルする。
            return;
        }
        if(_isStop&&(Input.GetButtonDown("Cancel")))
        { Cursor.lockState = CursorLockMode.None; }
        
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
        if (Input.GetMouseButtonDown(0)&&!_isStop)
        {
            Attack(); 
        }
        if (Input.GetMouseButton(1) && !_isStop)
        {
            GuardNow();
           // Debug.Log("Down");
        }
        else if (Input.GetMouseButtonUp(1) && !_isStop)
        {
            NoGuard();
           // Debug.Log("Up");
        }
    }
    private void FixedUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // nGUI上をクリックしているので処理をキャンセルする。
            return;
        }
        if(!_isStop)
        { Move(); }
        
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
        if (_animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            return;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //カメラの正面方向に移動するための変数。
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        vel = horizontalRotation * new Vector3(x, 0, z).normalized;
        rb.velocity = vel* _playerSpeed;      
        speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        if (speed == 2)
        {
            _stamina-=0.5f;
            PlayerSettingSpeed(5);
            if(_stamina<=20)
            {
                speed = 1;
                PlayerSettingSpeed(3);
            }
            playerUiCanvas.UpdateStamina(_stamina);
        }
        else if(speed == 1)
        {
            PlayerSettingSpeed(3);
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
        }                                
        //回転をなめらかに
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        _animator.SetFloat("Speed", vel.magnitude * speed, 0.1f, Time.deltaTime);
    }
    void IncreseStamina(float increse)
    {        //スタミナの自動回復
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
        _animator.SetTrigger("Attack");
        PlayerSettingSpeed(0);
    }
    /// <summary>あるアニメーションのタイミングでSE再生</summary>
   public void SEplay() {audioManager.PlaySound(0);}
    /// <summary>ガード中</summary>
   void GuardNow()
   {
        _animator.SetBool("Guard", true);
        ShowGuardCollider();
   } 
    /// <summary>ガード解除</summary>
   void NoGuard()
   { 
        _animator.SetBool("Guard", false);
        HideGuardCollider();
   }
   public override void HideColliderWeapon(){ base.HideColliderWeapon(); }
   protected override void ShowColliderWeapon(){ base.ShowColliderWeapon(); }
    /// <summary>インターフェースのダメージ関数 </summary>
    /// <param name="damage"></param>
    public void AddDamage(float damage)
    {
        if (_isDie)
        {
            return;
        }
        _hp -= damage;
        playerUiCanvas.UpdateHp(_hp);
        if (_hp <= 0)
        {
            _hp = 0;
            _animator.SetTrigger("Die");
            _isDie = true;
            //死んだあと動かないように
            HideColliderWeapon();
        }
        else
        {
            _animator.SetTrigger("Hurt");
        }
    }
    /// <summary>プレイヤーのスピード設定</summary>
    /// <param name="speed"></param>
   public void PlayerSettingSpeed(float speed)
   {
        _playerSpeed = speed;
   }
    /// <summary>ガード解除時にコライダーを出す</summary>
    public void ShowGuardCollider()
    {
        _guardCollider.enabled = true;
    }
    /// <summary>ガード時にコライダーを隠す</summary>
    public  void HideGuardCollider()
    {
        _guardCollider.enabled = false;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        var damager = other.GetComponent<Damager>();
        if (damager != null)
        {
            _animator.SetTrigger("Hurt");
        }
    }
}
