using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    [SerializeField] float speed = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector3(x, 0, z).normalized*speed;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(Input.GetMouseButtonDown(0))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Mathf.Abs(x) > 0 || Mathf.Abs(z) > 0)
        {
            //falseのとき
            if (!animator.GetBool("Walk"))
            {
                //walkをtrueに変更する処理
                animator.SetBool("Walk", true);
            }
        }
        //trueのとき
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        //前に動いているとき
        if (z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            //falseのとき
            if (!animator.GetBool("Run"))
            {
                //walkをtrueに変更する処理
                animator.SetBool("Run", true);
                speed = 0.25f;
            }
        }
        //trueのとき
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 0.1f;
        }

    }
}
