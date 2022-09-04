using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRot;
    public Collider[] Hitcollider;
    /// <summary>体力</summary>
    [SerializeField]private int playerHp = 100;
    /// <summary>Maxの体力</summary>
    [SerializeField]private int maxPlayerHp = 100;
    /// <summary>hpバー </summary>
    public Slider hpBer;
    float currentTime = 0;
    public static bool heal_flag = false;
    [SerializeField] Button healButton = default;
    [SerializeField] GameObject healObj;
    [SerializeField]float playerSpeed = 5;
    float jumpForce = 200f;
    bool isGround = true;

    UiController uiContoroller;
    SaveManager saveManager;
    Vector3 vel;
    private void Awake()
    {
        targetRot = transform.rotation;
    }
    void Start()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        /*saveManager = GetComponent<SaveManager>();
        playerHp = saveManager.PlayerHp;*/
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        /*if (animator.GetBool("IsGround"))
        {
            if (Input.GetKeyDown("space"))
            {
                animator.SetTrigger("Jump");
                animator.SetBool("IsGround", false);
                rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            }
        }*/
        if (UiController.uiController.enemyCount == 0)
        {
            rb.isKinematic = true;
        }
            float distance = Vector3.Distance(transform.position, healObj.transform.position);
            if (distance <= 1.5)
            {
                heal_flag = true;
                healButton.gameObject.SetActive(true);
            }
            else
            {
                heal_flag = false;
                healButton.gameObject.SetActive(false);
            }
        //前のフレームから経過した秒数を加算
        currentTime += Time.deltaTime;

        //毎秒処理を行う
        if (currentTime >= 1.0f)
        {
            //フラグがtrueの時だけ処理する
            if (heal_flag)
            {
                hpBer.value += 3;
            }
            currentTime = 0;
        }
    }
    private void FixedUpdate()
    {
        Move();
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
        rb.velocity = vel*playerSpeed;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            playerSpeed = 4;
        }
        else
        {
            playerSpeed = 2;
        }       
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = 600 * Time.deltaTime;
        //移動方向を向く
        if (vel.magnitude > 0.5f)
        {
            targetRot = Quaternion.LookRotation(vel, Vector3.up);
        }                                //回転をなめらかに
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        animator.SetFloat("Speed", vel.magnitude * speed, 0.1f, Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // nGUI上をクリックしているので処理をキャンセルする。
                return;
            }
            animator.SetTrigger("Attack");
            
        }
    }
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
    /// <summary>Hpを減らす関数 </summary>
    /// <param name="damage"></param>
    public void TakeHit(float damage)
    {
        playerHp = (int)Mathf.Clamp(playerHp - damage, 0, maxPlayerHp);

        hpBer.value = playerHp;

        if (playerHp <= 0 && !GameState.gameOver)
        {
            GameState.gameOver = true;
        }
    }
}
