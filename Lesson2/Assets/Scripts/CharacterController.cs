using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject prefMine;
    public Transform GunPos;
    public int health;
    float GUIX;
    float GUIY;
    Camera MainCam;
    float OffX=3.82f;
    float OffY=0.33f;
    float X;
    float Y;
    int CamSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();
        GunPos = transform.GetChild(1).transform;
        health = 10;
        MainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        X = transform.position.x;
        Y = transform.position.y;
        MainCam.transform.position = Vector3.Lerp(MainCam.transform.position,new Vector3(X+OffX,Y+OffY,MainCam.transform.position.z),Time.deltaTime*CamSpeed);
        GUIX = Camera.main.WorldToScreenPoint(transform.position).x;
        GUIY = Camera.main.WorldToScreenPoint(transform.position).y;
        Run();
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", rigidBody.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            GetMine();
        }
        if (!isGrounded)
            return;
        else if (isGrounded && Input.GetKeyDown(KeyCode.UpArrow))
        {
            anim.SetBool("Ground", false);
            rigidBody.AddForce(new Vector2(0, 500));
        }
        if (health <= 0)
        {
            SceneManager.LoadScene(0);
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
        GameObject temp = Instantiate(prefBullet, GunPos.position, Quaternion.identity);
        temp.name = "bullet";
        temp.GetComponent<Bullet>().direction = (isFasingRight) ? 1 : -1;
    }

    void GetMine()
    {
        GameObject temp = Instantiate(prefMine, GunPos.position, Quaternion.identity);
        temp.name = "mine";
        temp.GetComponent<Mine>().direction = (isFasingRight) ? 1 : -1;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(GUIX-10,Screen.height-GUIY-70,30,20),health.ToString());
    }
}
