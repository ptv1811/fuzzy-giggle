using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerState
{
    Animated, RagdollMode, WaitForStable, RagdollToAnim
}

[System.Serializable]
public class MuscleComponent
{
    public Transform transform;
    public Rigidbody rigidbody;
    public Collider collider;
    public MuscleComponent(Transform t)
    {
        transform = t;
        rigidbody = t.GetComponent<Rigidbody>();
        collider = t.GetComponent<Collider>();
    }
}
public class RagdollSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerState mState = PlayerState.Animated;
    public List<MuscleComponent> mComp = new List<MuscleComponent>();
    public float minHitSpeed = 15f;
    public bool isRagdoll;
    Vector3 hitVelocity;
    Transform mHip;
    Transform mHipParent;
    Animator anim;
    string getUpFromBack, getUpFromBelly;

    void Start()
    {
        anim = GetComponent<Animator>();
        //mHip = anim.GetBoneTransform(HumanBodyBones.Hips);
        //mHipParent = mHip.parent;
        foreach(var trComp in GetComponentsInChildren<Rigidbody>())
        {
            mComp.Add(new MuscleComponent(trComp.transform));
        }
        SetRagdollPart(true, true);
    }

    // Update is called once per frame
    void Update()
    {
        switch (mState)
        {
            case PlayerState.Animated:
                if (hitVelocity.magnitude > minHitSpeed && isRagdoll == true)
                    mState = PlayerState.RagdollMode;
                break;
            case PlayerState.RagdollMode:
                SetRagdollPart(false, true);
                foreach (var comp in mComp)
                {
                    comp.rigidbody.AddForce(-hitVelocity, ForceMode.Impulse);
                }
                hitVelocity = Vector3.zero;
                //mHipParent = null;
                //transform.position = mHip.position;
                if (isRagdoll)
                   mState = PlayerState.WaitForStable;
                break;
            case PlayerState.WaitForStable:
                mHip.parent = mHipParent;
                SetRagdollPart(true, true);
                //anim.Play(getUpFromBack, 0, 0);
                mState = PlayerState.Animated;
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && collision.contacts[0].otherCollider.transform != transform.parent)
        {
            hitVelocity = collision.relativeVelocity;
        }
    }

    void SetRagdollPart(bool isActive, bool gravity)
    {
        anim.enabled = isActive;
        foreach(var trComp in mComp)
        {
            trComp.rigidbody.useGravity = gravity;
            if (trComp.transform == transform) {
                trComp.collider.isTrigger = !isActive;
                trComp.rigidbody.isKinematic = !isActive;
                continue;
            }
               
            trComp.collider.isTrigger = isActive;
            trComp.rigidbody.isKinematic = isActive;
        }
    }
}
