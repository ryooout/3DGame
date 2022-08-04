using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRot;
    public Collider[] Hitcollider;
    /// <summary>体力</summary>
    int playerHp = 100;
    /// <summary>Maxの体力</summary>
    int maxPlayerHp = 100;
    /// <summary>hpバー </summary>
    Slider hpBer;
    enum Action
    {
        MOVE,
        ATTACK,
        DAMAGED
    }
    Action action = Action.MOVE;
    private void Awake()
    {
        targetRot = transform.rotation;
    }
    void Start()
    {
        TryGetComponent(out animator);
        TryGetComponent(out rb);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void FixedUpdate()
    {
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //カメラの正面方向に移動するための変数。
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * new Vector3(x, 0, z).normalized;
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = 600 * Time.deltaTime;
        //移動方向を向く
        if (velocity.magnitude > 0.5f)
        {
            targetRot = Quaternion.LookRotation(velocity, Vector3.up);
        }                                //回転をなめらかに
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
        if(Input.GetMouseButtonDown(0))
        {
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
        playerHp = (int)Mathf.Clamp(playerHp - damage, 0, playerHp);

        hpBer.value = playerHp;

        if (playerHp <= 0 && !GameState.gameOver)
        {
            GameState.gameOver = true;
        }
    }
}
