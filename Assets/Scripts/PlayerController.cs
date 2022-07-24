using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    Animator animator;
    [SerializeField] float speed = 10.0f;
    private Vector3 latestPos;
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
        rb.velocity = new Vector3(x, 0, z);
        Vector3 diff = transform.position - latestPos;   //�O�񂩂�ǂ��ɐi�񂾂����x�N�g���Ŏ擾
        latestPos = transform.position;  //�O���Position�̍X�V
        //�x�N�g���̑傫����0.01�ȏ�̎��Ɍ�����ς��鏈��������
        if (diff.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(diff); //������ύX����
        }
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
            //false�̂Ƃ�
            if (!animator.GetBool("Walk"))
            {
                //walk��true�ɕύX���鏈��
                animator.SetBool("Walk", true);
            }
        }
        //true�̂Ƃ�
        else if (animator.GetBool("Walk"))
        {
            animator.SetBool("Walk", false);
        }

        //�O�ɓ����Ă���Ƃ�
        if (z > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            //false�̂Ƃ�
            if (!animator.GetBool("Run"))
            {
                //walk��true�ɕύX���鏈��
                animator.SetBool("Run", true);
                speed = 12.5f;
                Debug.Log(speed);
            }
        }
        //true�̂Ƃ�
        else if (animator.GetBool("Run"))
        {
            animator.SetBool("Run", false);
            speed = 10.0f;
            Debug.Log(speed);
        }

    }
    private void FixedUpdate()
    {
    }
}
