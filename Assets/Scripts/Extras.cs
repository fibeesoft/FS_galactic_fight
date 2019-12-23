using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extras : MonoBehaviour
{
    public GameObject moonObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveTheMoon();
    }

    void MoveTheMoon(){
        if(moonObject != null){
            float speed = 1.1f;
            float maxRotation = 24f;
            moonObject.transform.rotation = Quaternion.Euler(0f, 0f, maxRotation * Mathf.Sin(Time.time * speed) - 3f);
        }
    }
}
