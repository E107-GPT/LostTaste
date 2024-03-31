using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFallEffect : MonoBehaviour
{
    public GameObject m_gameObjectMain;
    GameObject m_makedObject;

    public float maxLength;
    public bool isDestroy;
    public float ObjectDestroyTime;
    public float TailDestroyTime;
    public float HitObjectDestroyTime;
    public float maxTime = 1;
    public float MoveSpeed = 10;
    public bool isCheckHitTag;
    public string mtag;
    public bool isShieldActive = false;
    public bool isHitMake = true;

    float time;
    float m_scalefactor;

    void Start()
    {
        m_scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;//transform.parent.localScale.x;
        time = Time.time;
    }


    void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed * m_scalefactor);

        //if (isDestroy)
        //{
        //    time += Time.deltaTime;
        //    if (ObjectDestroyTime < time)
        //    {
        //        MakeHitObject(transform);
        //    }
        //}
    }


    void HitObject()
    {

    }

    void MakeHitObject(Transform point)
    {
    }
}
