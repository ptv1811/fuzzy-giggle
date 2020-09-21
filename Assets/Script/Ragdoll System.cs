using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody[] mRigidbodies = GetComponentsInChildren<Rigidbody>();
        Collider[] mCollider = GetComponentsInChildren<Collider>();
        foreach (var rigid in mRigidbodies)
        {
            if (rigid.transform == transform)
                continue;
            rigid.isKinematic = true;
        }
        foreach (var col in mCollider)
        {
            if (col.transform == transform)
                continue;
            col.isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
