using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitFollowState : StateMachineBehaviour
{
    AttackController attackController;
    NavMeshAgent agent;
    UnitMovement unitMovement;
    public float attackDistance = .7f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       attackController = animator.transform.GetComponent<AttackController>();
       agent = animator.transform.GetComponent<NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(attackController.targetToAttack == null){
        animator.SetBool("isFollowing", false);
       }else{
         if(animator.transform.GetComponent<UnitMovement>().isCommandedToMove == false){
           agent.SetDestination(attackController.targetToAttack.position);
           animator.transform.LookAt(attackController.targetToAttack);
         }
       }



      //  float distanceFromTarget = Vector3.Distance(attackController.targetToAttack.position, animator.transform.position);
      //  if(distanceFromTarget < attackDistance){
      //   agent.SetDestination(animator.transform.position);
      //   animator.SetBool("isAttacking", true);
      //  }
    }
}
