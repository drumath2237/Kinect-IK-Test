using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Animator))]

public class VRMIKController : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public Transform rightHandObj = null;

    public bool tmpSwitch = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // IK を計算するためのコールバック
    void OnAnimatorIK()
    {
        if (animator)
        {

            // IK が有効ならば、位置と回転を直接設定します
            if (ikActive)
            {

                // 指定されている場合は、右手のターゲット位置と回転を設定します
                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }

            }

            //IK が有効でなければ、手と頭の位置と回転を元の位置に戻します
            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetLookAtWeight(0);
            }
        }
    }
}