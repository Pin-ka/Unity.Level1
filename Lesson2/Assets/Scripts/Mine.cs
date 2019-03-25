using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    float speed = 0.05f;
    public int direction = 1;

    private void OnBecameInvisible()
    {
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.position += new Vector3(Vector3.right.x * direction * speed, Vector3.right.y * direction * speed, transform.position.z);
    }
}
