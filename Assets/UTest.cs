using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMath;
using Extands;

[ExecuteInEditMode]
public class UTest : MonoBehaviour {

	// Use this for initialization
	void Start () 
    {
        //var lookat = UQuaternion.LookRotation(new UVector3(0, 0, -1));  
	}

    void Awake()
    {
        //var lookat = UQuaternion.LookRotation(new UVector3(0, 0, -1));
        mparent = new UTransform();
        mtransfrom = new UTransform();
    }

    [Header("Input")]
    public Vector3 inputVer = Vector3.forward;
    public float angle =0;
    public Vector3 trans;
    public Vector3 scale = Vector3.one;

    [Header("Forward")]
    public Vector3 cForward;
    public UVector3 uForward;

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
    public float ad;
    public float adm;
    public float angleDelta =0f;
    public float angleDeltaM =0f;

    [Header("Identity")]
    public Quaternion id;
    public UQuaternion idm;
    public UMatrix4x4 midM;
    public Matrix4x4 mid;

    private UTransform mparent;//= new UTransform();
    private UTransform  mtransfrom;// = new UTransform();
    private float rotangle =0;

    [Header("Transform")]
    public UVector3 dirU;
    public Vector3 dir;
    public UVector3 pointU;
    public Vector3 point;

    [Header("Determinant")]
    public float mDeterminant;
    public float Determinant;

	// Update is called once per frame
	void Update () 
    {
        //return;
        if (rotangle > 360)
        {
            rotangle = 0;
        }
        rotangle += Time.deltaTime * 5f;

        this.mtransfrom.setParent(mparent);
        this.transform.localRotation = Quaternion.Euler(0, rotangle, 0);

        id = Quaternion.identity;
        idm = UQuaternion.identity;
        midM = UMatrix4x4.identity;
        mid = Matrix4x4.identity;

        cForward = this.transform.forward;
        uForward = new UQuaternion(this.transform.rotation.x,
            this.transform.rotation.y,
            this.transform.rotation.z,
            this.transform.rotation.w) 
            * UVector3.forward;
        
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

        Determinant = m.determinant;
        mDeterminant = mm.Determinant;

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
        lookatMat = Matrix4x4.TRS(
            Vector3.zero,
            Quaternion.LookRotation(inputVer, Vector3.up), 
            Vector3.one);

        var v = this.transform.forward;
        angleY = MathHelper.CalAngleWithAxisY(new UVector3(v.x,v.y,v.z));
        adm = MathHelper.DeltaAngle(angleY, angle);
        ad = Mathf.DeltaAngle(angleY, angle);
        angleDeltaM = MathHelper.MoveTowardsAngle(angleY,angle, adm*Time.deltaTime);
        angleDelta =  Mathf.MoveTowardsAngle(angleY,angle, ad*Time.deltaTime);


        this.mparent.localPosition = this.transform.parent.localPosition.ToUV3();
        this.mparent.localRotation = this.transform.parent.localRotation.ToUQ();
        this.mparent.localScale = this.transform.parent.localScale.ToUV3();

        this.mtransfrom.localPosition = this.transform.localPosition.ToUV3();
        this.mtransfrom.localRotation = this.transform.localRotation.ToUQ();
        this.mtransfrom.localScale = this.transform.localScale.ToUV3();

        dir= this.transform.TransformDirection(Vector3.forward);
        dirU = mtransfrom.TransformDirection(UVector3.forward);

        point = transform.TransformPoint(Vector3.forward);
        pointU = mtransfrom.TransformPoint(UVector3.forward);
	}
}
