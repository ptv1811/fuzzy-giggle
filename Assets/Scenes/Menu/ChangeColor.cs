using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Material mat;
    public GameObject obj;
   public void SetMaterial()
    {
        obj.GetComponent<Renderer>().material = mat;
    }

}
