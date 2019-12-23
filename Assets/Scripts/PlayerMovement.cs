using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [Range(2f,20f)] public float moveSpeed;
    Vector3 pos;
    void Start()
    {
        moveSpeed = 6;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){
        pos = transform.position;
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(moveX, moveY,0f);
        pos += move.normalized*Time.deltaTime*moveSpeed;
        pos = new Vector3(Mathf.Clamp(pos.x, -8f, 8f), Mathf.Clamp(pos.y, -4f, 4f), 0f);
        transform.position = pos;

        if ((Vector2)move != Vector2.zero) {
            float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

}
