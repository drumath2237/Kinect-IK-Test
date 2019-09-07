using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(GameObject))]
public class AvatarIKController : MonoBehaviour
{
    public Transform LeftHand, RightHand;
    public Transform LeftFoot, RightFoot;
    public Transform SpineBase;

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
        transform.position = new Vector3(
            SpineBase.position.x,
            transform.position.y,
            SpineBase.position.z
        );

        transform.rotation = Quaternion.Euler(0, SpineBase.transform.rotation.y, 0) * Quaternion.AngleAxis(180, new Vector3(0, 1, 0));
    }
}
