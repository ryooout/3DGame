using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRot;
    public Collider[] Hitcollider;
    /// <summary>�̗�</summary>
    public int playerHp = 100;
    /// <summary>Max�̗̑�</summary>
    int maxPlayerHp = 100;
    /// <summary>hp�o�[ </summary>
    public Slider hpBer;
    float currentTime = 0;
    public static bool heal_flag = false;
    [SerializeField] Button healButton = default;
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
        if (UiContoroller.enemyCount == 0)
        {
            rb.isKinematic = true;
        }
    }
    private void FixedUpdate()
    {
    }
    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        //�J�����̐��ʕ����Ɉړ����邽�߂̕ϐ��B
        var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
        var velocity = horizontalRotation * new Vector3(x, 0, z).normalized;
        var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
        var rotationSpeed = 600 * Time.deltaTime;
        //�ړ�����������
        if (velocity.magnitude > 0.5f)
        {
            targetRot = Quaternion.LookRotation(velocity, Vector3.up);
        }                                //��]���Ȃ߂炩��
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
        animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");
        }
        //�O�̃t���[������o�߂����b�������Z
        currentTime += Time.deltaTime;

        //���b�������s��
        if (currentTime >= 1.0f)
        {
            //�t���O��true�̎�������������
            if (heal_flag == true)
            {
                hpBer.value += 10;
            }
            currentTime = 0;
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
    /// <summary>Hp�����炷�֐� </summary>
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Heal")
        {
            healButton.gameObject.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        healButton.gameObject.SetActive(false);
    }
    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
    }
}
