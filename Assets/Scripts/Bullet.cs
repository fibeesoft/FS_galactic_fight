using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 vel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= vel * 100f * Time.deltaTime;
    }

    public void Initialize(Vector3 p_vel){
        vel = p_vel;
    }
}
