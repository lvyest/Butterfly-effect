using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 2;

    Vector3 spinZ = new Vector3(0, 0, 90);

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += transform.up * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.DownArrow))
            transform.position -= transform.up * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow))
            //transform.position += transform.right * speed * Time.deltaTime;
            transform.eulerAngles -= spinZ * Time.deltaTime;
        else if (Input.GetKey(KeyCode.LeftArrow))
            //transform.position -= transform.right * speed * Time.deltaTime;
            transform.eulerAngles += spinZ * Time.deltaTime;

    }
}
