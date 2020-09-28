using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMat : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material = PassMat.material;
        Debug.Log(PassMat.index);
    }

   
}
