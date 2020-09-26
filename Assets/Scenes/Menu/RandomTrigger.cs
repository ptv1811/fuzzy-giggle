using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTrigger : StateMachineBehaviour
{
    readonly float dancemintime = 1;
    readonly float dancemaxtime = 5;
    float dancetimer=0;
    string [] dancetrigger = { "Gangnam", "Hiphop", "Swing" };
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (dancetimer<= 0)
        {
            DanceRandom(animator);
            dancetimer = Random.Range(dancemintime, dancemaxtime);
        }
        else
        {
            dancetimer -= Time.deltaTime;
        }
    }
    void DanceRandom(Animator animator)
    {
        System.Random rnd = new System.Random();
        int dance = rnd.Next(dancetrigger.Length);
        string dances = dancetrigger[dance];
        Debug.Log(dances);
        animator.SetTrigger(dances);

    }
    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
