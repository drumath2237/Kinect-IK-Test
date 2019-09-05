using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GameObject))]
public class AvatarIKController : MonoBehaviour
{
    public Transform LeftHand, RightHand;
    public Transform LeftFoot, RightFoot;

    private Animator animator;

    //public GameObject avatar;

    public bool isIKActive;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK(int layerIndex)
    {
        if (isIKActive)
        {
            IKGoalSetting(AvatarIKGoal.LeftHand, LeftHand);
            IKGoalSetting(AvatarIKGoal.RightHand, RightHand);
            IKGoalSetting(AvatarIKGoal.LeftFoot, LeftFoot);
            IKGoalSetting(AvatarIKGoal.RightFoot, RightFoot);
        }
    }

    void IKGoalSetting(AvatarIKGoal ik, Transform t)
    {
        animator.SetIKPositionWeight(ik, 1);
        animator.SetIKRotationWeight(ik, 1);

        animator.SetIKPosition(ik, t.position);
        animator.SetIKRotation(ik, t.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
