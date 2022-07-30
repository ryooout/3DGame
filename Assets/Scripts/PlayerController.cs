using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    Quaternion targetRot;
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
        rb = GetComponent<Rigidbody>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            //ƒJƒƒ‰‚Ì³–Ê•ûŒü‚ÉˆÚ“®‚·‚é‚½‚ß‚Ì•Ï”B
            var horizontalRotation = Quaternion.AngleAxis(Camera.main.transform.eulerAngles.y, Vector3.up);
            var velocity = horizontalRotation * new Vector3(x, 0, z).normalized;
            var speed = Input.GetKey(KeyCode.LeftShift) ? 2 : 1;
            var rotationSpeed = 600 * Time.deltaTime;
            //ˆÚ“®•ûŒü‚ðŒü‚­
            if (velocity.magnitude > 0.5f)
            {
                targetRot = Quaternion.LookRotation(velocity, Vector3.up);
            }                                //‰ñ“]‚ð‚È‚ß‚ç‚©‚É
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotationSpeed);
            animator.SetFloat("Speed", velocity.magnitude * speed, 0.1f, Time.deltaTime);
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
    public void AttackButton()
    {
        animator.SetTrigger("Attack");
    }
}
