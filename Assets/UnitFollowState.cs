using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;
    public float attackDistance = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       attackController = animator.transform.GetComponent<AttackController>();
       agent = animator.transform.GetComponent<NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(attackController.targetToAttack = null){
        animator.SetBool("isFollowing", false);
       }

       agent.SetDestination(attackController.targetToAttack.position);
       animator.transform.LookAt(attackController.targetToAttack);

       float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
       if(distanceFromTarget > attackDistance){
        animator.SetBool("isAttacking", true);
       }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       agent.SetDestination(animator.transform.position);
    }

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
