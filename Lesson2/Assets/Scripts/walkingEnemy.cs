using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingEnemy : MonoBehaviour
{
    public float speed = 7f;
    public float speedRotate = -2f;
    public int health = 2;

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Vector3 enemyScale = transform.localScale;
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
            speedRotate *= -1;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Rotate(0,0, speedRotate);
        GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * speed, GetComponent<Rigidbody2D>().velocity.y);
        if(health<=0)
        {
            Destroy(gameObject);
        }
    }
}
