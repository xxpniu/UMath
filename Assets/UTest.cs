using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMath;

[ExecuteInEditMode]
public class UTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
	}

    [Header("Input")]
    public Vector3 cForward;
    public Vector3 inputVer = Vector3.forward;
    public float angle =0;
    public Vector3 trans;
    public Vector3 scale = Vector3.one;


    [Header("AngleAxis")]
    public Quaternion q;
    public UQuaternion mq;

    [Header("Euler")]
    public Quaternion qEuler;
    public Vector3 qToEuler;
    public UQuaternion mqEuler;
    public UVector3 mqToEuler;

  
    [Header("STR Matrix")]
    public Matrix4x4 m;
    public UMatrix4x4 mm;

    [Header("Decompose matrix")]
    public UVector3 exTrans;
    public UVector3 exScale;
    public UQuaternion exRotation;

    [Header("Look At")]
    public Quaternion lookat;
    public UQuaternion mlookat;
    public UVector3 mforwardLookat;
    public Vector3 forwardLookat;

    [Header("Look at to euler")]
    public UVector3 mangle;
    public Vector3 rangle;
    public Matrix4x4 lookatMat;
    public UMatrix4x4 mlookatMat;

    [Header("Delta Angle")]
    public float angleY = 0f;
    public float angleDelta =0f;
    public float angleDeltaM =0f;

	
	// Update is called once per frame
	void Update () 
    {
        cForward = this.transform.forward;
        var inputUv = new UVector3(inputVer.x, inputVer.y, inputVer.z);
        var s = new UVector3(scale.x, scale.y, scale.z);
        var t = new UVector3(trans.x, trans.y, trans.z);

        q = Quaternion.AngleAxis(angle, inputVer);
        mq = UQuaternion.AngleAxis(angle, inputUv);

        qEuler = Quaternion.Euler(inputVer);
        qToEuler = qEuler.eulerAngles;
        mqEuler = UQuaternion.Euler(inputUv);
        mqToEuler = mqEuler.eulerAngles;
       
        m = Matrix4x4.TRS(trans, q, scale);
        mm = UMatrix4x4.TRS(t, mq, s);
        exScale = mm.Scale;
        exTrans = mm.Translate;
        exRotation = mm.Rotation;

        lookat = Quaternion.LookRotation(inputVer);
        mlookat = UQuaternion.LookRotation(inputUv);

        mforwardLookat = mlookat*UVector3.forward  ;
        forwardLookat = lookat*Vector3.forward ;

        mangle = UQuaternion.LookRotation(inputUv).eulerAngles;
        rangle = Quaternion.LookRotation(inputVer).eulerAngles;


        mlookatMat = UMatrix4x4.LookAt(UVector3.zero, inputUv,UVector3.up);
        lookatMat = Matrix4x4.zero;//need


        angleY = MathHelper.CalAngleWithAxisY(inputUv);
        angleDeltaM = MathHelper.DeltaAngle(angleY, angle);
        angleDelta = Mathf.DeltaAngle(angleY, angle);
	}
}
