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
            //false‚Ì‚Æ‚«
            if (!animator.GetBool("Walk"))
            {
                //walk‚ğtrue‚É•ÏX‚·‚éˆ—
                animator.SetBool("Walk", true);
            }
        }
        //true‚Ì‚Æ‚«
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        //‘O‚É“®‚¢‚Ä‚¢‚é‚Æ‚«
        if (z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            //false‚Ì‚Æ‚«
            if (!animator.GetBool("Run"))
            {
                //walk‚ğtrue‚É•ÏX‚·‚éˆ—
                animator.SetBool("Run", true);
                speed = 0.25f;
            }
        }
        //true‚Ì‚Æ‚«
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 0.1f;
        }

    }
}
