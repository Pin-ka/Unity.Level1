using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rigidBody;
    public float speed = 5f;
    bool isFasingRight = true;
    bool isGrounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    Animator anim;
    public GameObject prefBullet;
    public Transform GunPos;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        GunPos = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground",isGrounded);
        anim.SetFloat("vSpeed",rigidBody.velocity.y);
        if (!isGrounded)
            return;
        else if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Ground",false);
            rigidBody.AddForce(new Vector2(0,500));
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }


    public void Run()
    {
        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);
        if (move > 0 && !isFasingRight)
            Flip();
        else if (move < 0 && isFasingRight)
            Flip();
    }

    void Flip()
    {
        isFasingRight = !isFasingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Shoot()
    {
        GameObject temp = Instantiate(prefBullet,GunPos.position,Quaternion.identity);
        temp.name = "bullet";
        temp.GetComponent<Bullet>().direction = (isFasingRight) ? 1 : -1;
        GetComponent<AudioSource>().Play();
    }
}
