using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
	public float speed = 3f;


    // Update is called once per frame
    void Update()
    {
        
        transform.Rotate(new Vector3(0, 0, Time.deltaTime * speed));
        //transform.position = new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, 0);
        //transform.SetPositionAndRotation(new Vector3(Time.deltaTime * speed, Time.deltaTime * speed, 0), new Quaternion(0, 0, Time.deltaTime * speed, 0));
    }
}

