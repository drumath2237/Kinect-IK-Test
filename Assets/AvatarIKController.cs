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

    private Dictionary<AvatarIKGoal, Pose> ema = new Dictionary<AvatarIKGoal, Pose>()
    {
        {AvatarIKGoal.LeftHand, Pose.identity},
        {AvatarIKGoal.RightHand, Pose.identity},
        {AvatarIKGoal.LeftFoot, Pose.identity},
        {AvatarIKGoal.RightFoot, Pose.identity}
    };

    private Dictionary<AvatarIKGoal, Pose> ema2;
    private Dictionary<AvatarIKGoal, Pose> dema;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        ema2 = new Dictionary<AvatarIKGoal, Pose>(ema);
        dema = new Dictionary<AvatarIKGoal, Pose>(ema);
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

    void IKGoalSetting(AvatarIKGoal goal, Pose pose)
    {
        
    }

    void SetIKTransform(AvatarIKGoal goal, Pose pose)
    {
        var N = 20;
        var alpha = 2.0f / (N + 1.0f);

        var ema_pos = alpha * pose.position + (1 - alpha) * ema[goal].position;
//        var ema2_pose =  
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
