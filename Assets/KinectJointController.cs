using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Kinect = Windows.Kinect;
using System.Linq;

public class KinectJointController : MonoBehaviour
{
    [Header("Manager")]
    public GameObject _bsmng;
    private BodySourceManager bsManager;

    [Header("Body Source")]
    public GameObject j_head;
    public GameObject j_lefthand;
    public GameObject j_righthand;
    public GameObject j_leftfoot;
    public GameObject j_rightfoot;
    public GameObject j_SpineBase;

    Kinect.Body _body;

    // Start is called before the first frame update
    void Start()
    {
        bsManager = _bsmng.GetComponent<BodySourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _body = bsManager.GetData().FirstOrDefault(b => b.IsTracked);

        if (_body.IsTracked)
        {
            SetBoneTransform(j_head, Kinect.JointType.Head);
            SetBoneTransform(j_lefthand, Kinect.JointType.HandLeft);
            SetBoneTransform(j_righthand, Kinect.JointType.HandRight);
            SetBoneTransform(j_leftfoot, Kinect.JointType.AnkleLeft);
            SetBoneTransform(j_rightfoot, Kinect.JointType.AnkleRight);
            SetBoneTransform(j_SpineBase, Kinect.JointType.SpineBase);
        }
    }

    Vector3 KPos2Vec3(Kinect.JointType type)
    {
        return new Vector3(
            _body.Joints[type].Position.X * 4f,
            _body.Joints[type].Position.Y * 4f,
            _body.Joints[type].Position.Z
        );
    }

    Quaternion Vec4ToQ(Kinect.JointType type)
    {
        return new Quaternion(
               _body.JointOrientations[type].Orientation.X,
               _body.JointOrientations[type].Orientation.Y,
               _body.JointOrientations[type].Orientation.Z,
               _body.JointOrientations[type].Orientation.W
        );
    }

    void SetBoneTransform(GameObject obj, Kinect.JointType type)
    {
        obj.transform.position = KPos2Vec3(type);
        obj.transform.rotation = Vec4ToQ(type);

        obj.name = type.ToString();
    }
}
