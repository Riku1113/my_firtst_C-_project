using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehavior : StateMachineBehaviour
{
    //アニメーション開始時に実行される
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Hurt");
        //攻撃中に速度を0にする
        animator.GetComponent<PlayerManager>().moveSpeed = 0.4f;
        //ダメージを受けると当たり判定を消す
        animator.GetComponent<PlayerManager>().HideColliderWeapon();
    }

    //アニメーション中に実行される
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    //アニメーションの遷移が行われるときに実行される
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Hurt");
        //速度を元に戻す
        animator.GetComponent<PlayerManager>().moveSpeed = 3;
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
