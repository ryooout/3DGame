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
    /// <summary>��]</summary>
    Quaternion targetRot;
    /// <summary>�o�ߎ���</summary>
    private float  currentTime = 0;
    private bool heal_flag = false;
    [SerializeField,Header("�񕜃I�u�W�F�N�g")] GameObject healObj;
    [SerializeField, Header("�U�R�G")] GameObject zakotekiCollider;
    /// <summary>�x�N�g��</summary>
    Vector3 vel;
    /// <summary>Max�̃X�^�~�i</summary>
    [SerializeField,Header("Max�̃X�^�~�i")] protected float _maxStamina = 100;
    /// <summary>�X�^�~�i</summary>
    [SerializeField,Header("�X�^�~�i")] protected float _stamina = 100;
    /// <summary>�v���C���[�̃X�s�[�h</summary>
    [SerializeField,Header("�v���[���[�̃X�s�[�h")] private float _playerSpeed = 5;
    [SerializeField, Header("�K�[�h�̃R���C�_�[")] Collider _guardCollider;
    /// <summary>�A�j���[�V�����̃u�����h�c���[�Ő؂�ւ���ۂ̃X�s�[�h</summary>
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
        // �Ă�ŗ~�������\�b�h��o�^����B
        _pauseManager.OnPauseResume += PauseResume;
    }

    void OnDisable()
    {
        // OnDisable �ł̓��\�b�h�̓o�^���������邱�ƁB�����Ȃ��ƃI�u�W�F�N�g�������ɂ��ꂽ��j�����ꂽ�肵����ɃG���[�ɂȂ��Ă��܂��B
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
    /// <summary>��~</summary>
    public void Pause()
    {
        _isStop = true;
        Cursor.lockState = CursorLockMode.None;
        // �J�[�\���\��
        Cursor.visible = true;
    }
    /// <summary>�Đ�</summary>
    public void Resume()
    {
        _isStop = false;
        Cursor.lockState = CursorLockMode.Locked;
        // �J�[�\����\��
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
            // nGUI����N���b�N���Ă���̂ŏ������L�����Z������B
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
        //�O�̃t���[������o�߂����b�������Z
        currentTime += Time.deltaTime;

        //���b�������s��
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
            // nGUI����N���b�N���Ă���̂ŏ������L�����Z������B
            return;
        }
        if(!_isStop)
        { Move(); }
        
    }
    void Heal()
    {
        //�t���O��true�̎�������������
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
        //�J�����̐��ʕ����Ɉړ����邽�߂̕ϐ��B
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
        //�ړ�����������
        if (vel.magnitude > 0.5f)
        {
            targetRot = Quaternion.LookRotation(vel, Vector3.up);
        }                                
        //��]���Ȃ߂炩��
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        _animator.SetFloat("Speed", vel.magnitude * speed, 0.1f, Time.deltaTime);
    }
    void IncreseStamina(float increse)
    {        //�X�^�~�i�̎�����
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
            // nGUI����N���b�N���Ă���̂ŏ������L�����Z������B
            return;
        }
        _animator.SetTrigger("Attack");
        PlayerSettingSpeed(0);
    }
    /// <summary>����A�j���[�V�����̃^�C�~���O��SE�Đ�</summary>
   public void SEplay() {audioManager.PlaySound(0);}
    /// <summary>�K�[�h��</summary>
   void GuardNow()
   {
        _animator.SetBool("Guard", true);
        ShowGuardCollider();
   } 
    /// <summary>�K�[�h����</summary>
   void NoGuard()
   { 
        _animator.SetBool("Guard", false);
        HideGuardCollider();
   }
   public override void HideColliderWeapon(){ base.HideColliderWeapon(); }
   protected override void ShowColliderWeapon(){ base.ShowColliderWeapon(); }
    /// <summary>�C���^�[�t�F�[�X�̃_���[�W�֐� </summary>
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
            //���񂾂��Ɠ����Ȃ��悤��
            HideColliderWeapon();
        }
        else
        {
            _animator.SetTrigger("Hurt");
        }
    }
    /// <summary>�v���C���[�̃X�s�[�h�ݒ�</summary>
    /// <param name="speed"></param>
   public void PlayerSettingSpeed(float speed)
   {
        _playerSpeed = speed;
   }
    /// <summary>�K�[�h�������ɃR���C�_�[���o��</summary>
    public void ShowGuardCollider()
    {
        _guardCollider.enabled = true;
    }
    /// <summary>�K�[�h���ɃR���C�_�[���B��</summary>
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
