using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Kinect = Windows.Kinect;
using System.Linq;

public class CalibrationManager : MonoBehaviour
{
    [SerializeField] Text CountText;
    [SerializeField] GameObject BodySourceManager;

    BodySourceManager _source;
    [SerializeField] GameObject jointObj;

    // Start is called before the first frame update
    void Start()
    {
        _source = BodySourceManager.GetComponent<BodySourceManager>();
        IEnumerator coru = CalibrateAfterCountdown();
        StartCoroutine(coru);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CalibrateAfterCountdown()
    {
        for(int i=0; i<3; i++)
        {
            yield return new WaitForSeconds(1f);
            CountText.text = (3 - i).ToString();
        }

        var _body = _source.GetData().FirstOrDefault(b => b.IsTracked);

        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.SpineShoulder, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.ElbowLeft, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.HandLeft, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.ElbowRight, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.HandRight, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.SpineBase, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.KneeLeft, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.AnkleLeft, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.KneeRight, _body);
        SetBoneTransform(Instantiate(jointObj), Kinect.JointType.AnkleRight, _body);
    }

    Vector3 KPos2Vec3(Kinect.JointType type, Kinect.Body _body)
    {
        return new Vector3(
            _body.Joints[type].Position.X * 10f,
            _body.Joints[type].Position.Y * 10f,
            _body.Joints[type].Position.Z
        );
    }

    Quaternion Vec4ToQ(Kinect.JointType type, Kinect.Body _body)
    {
        return new Quaternion(
               _body.JointOrientations[type].Orientation.X,
               _body.JointOrientations[type].Orientation.Y,
               _body.JointOrientations[type].Orientation.Z,
               _body.JointOrientations[type].Orientation.W
        );
    }

    void SetBoneTransform(GameObject obj, Kinect.JointType type, Kinect.Body _body)
    {
        obj.transform.position = KPos2Vec3(type, _body);
        obj.transform.rotation = Vec4ToQ(type, _body);

        obj.name = type.ToString();
    }

}
