using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMath;

[ExecuteInEditMode]
public class UTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}

    public Vector3 inputVer = Vector3.forward;
    public float angle =0;
    public Vector3 trans;
    public Vector3 scale = Vector3.one;



    public Quaternion q;
    public UQuaternion mq;

    public Quaternion qEuler;
    public Vector3 qToEuler;
    public UQuaternion mqEuler;
    public UVector3 mqToEuler;

    public Quaternion eq;
    public UQuaternion emq;

    public Matrix4x4 m;
    public UMatrix4x4 mm;

    public UVector3 exTrans;
    public UVector3 exScale;
    public UQuaternion exRotation;

    public Quaternion lookat;
    public UQuaternion mlookat;

	
	// Update is called once per frame
	void Update () 
    {
        var inputUv = new UVector3(inputVer.x, inputVer.y, inputVer.z);
        var s = new UVector3(scale.x, scale.y, scale.z);
        var t = new UVector3(trans.x, trans.y, trans.z);

        q = Quaternion.AngleAxis(angle, inputVer);
        mq = UQuaternion.AngleAxis(angle, inputUv);

        qEuler = Quaternion.Euler(inputVer);
        qToEuler = qEuler.eulerAngles;
        mqEuler = UQuaternion.Euler(inputUv);
        mqToEuler = mqEuler.eulerAngles;

        eq = Quaternion.Euler(inputVer);
        emq = UQuaternion.Euler(inputUv);

        m = Matrix4x4.TRS(trans, q, scale);
        mm = UMatrix4x4.TRS(t, mq, s);
        exScale = mm.Scale;
        exTrans = mm.Translate;
        exRotation = mm.Rotation;

        lookat = Quaternion.LookRotation(inputVer);
        mlookat = UQuaternion.LookRotation(inputUv);
	}
}
