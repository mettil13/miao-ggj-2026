using System.Collections.Generic;
using System;
using UnityEngine;

[ExecuteInEditMode]
public class BlobbiPalliScreenSpace : MonoBehaviour
{
    #region SINGLETON
    static private BlobbiPalliScreenSpace _instance;
    static public BlobbiPalliScreenSpace  instance { get{  return _instance; } }
    #endregion

    #region VARS
    private List<Transform> _palli = new List<Transform>();
    private List<Transform> _viewed = new List<Transform>();
    public Vector3[] ssPoints = new Vector3[10];
    public Vector3[] ssViewed = new Vector3[10];
    public Material material;
    ComputeBuffer buffer;
    ComputeBuffer buffer2;
    public float pecoroCameraFade = 1;
    private bool  _mode = false;
    public float modeL = 0;
    #endregion

    #region METHODS
    private void Awake()
    {
        _instance = this;
    }
    private void OnDestroy()
    {
        _instance = null;
    }

    private void Start()
    {
        _palli = new List<Transform>();
        for (int i = 0; i < ssPoints.Length; i++)
        {
            ssPoints[i] = new Vector3(i, i * 2, 0);
        }
        _viewed = new List<Transform>();
        for (int i = 0; i < ssViewed.Length; i++)
        {
            ssViewed[i] = new Vector3(i, i * 2, 0);
        }

        CreateBuffer();
    }

    public void AddPallo(Transform transform)
    {
        _palli.Add(transform);
    }
    public void AddViewed(Transform transform)
    {
        _viewed.Add(transform);
    }
    public void RemoveViewed(Transform transform)
    {
       if(_viewed.Contains(transform)) _viewed.Remove(transform);
    }

    public void RemovePallo(Transform transform)
    {
        _palli.Remove(transform);
        _viewed.Remove(transform);
    }

    private void CreateBuffer()
    {
        if (buffer != null) return;

        buffer = new ComputeBuffer(ssPoints.Length, sizeof(float) * 3);
        buffer.SetData(ssPoints);
        material.SetBuffer("_SSPoints", buffer);

        buffer2 = new ComputeBuffer(ssViewed.Length, sizeof(float) * 3);
        buffer2.SetData(ssViewed);
        material.SetBuffer("_SSViewed", buffer);
    }
    // Update is called once per frame
    void Update()
    {
        CreateBuffer();
        int i = 0;
        foreach (Transform t in _palli)
        {
            if(t==null) continue;

            Vector3 ssPos = Camera.main.WorldToViewportPoint(t.position);
            ssPoints[i] = ssPos;
            i++;
        }
        i = 0;
        foreach (Transform t in _viewed)
        {
            if (t == null) continue;

            Vector3 ssPos = Camera.main.WorldToViewportPoint(t.position);
            ssViewed[i] = ssPos;
            i++;
        }


        buffer.SetData(ssPoints);
        material.SetFloat("_SSPointsCount", _palli.Count);
        material.SetBuffer("_SSPoints", buffer);

        buffer2.SetData(ssViewed);
        material.SetFloat("_SSViewedCount", i);
        material.SetBuffer("_SSViewed", buffer2);

        modeL = _mode ? Mathf.Clamp01(modeL + Time.deltaTime*0.5f) : Mathf.Clamp01(modeL - Time.deltaTime);
        pecoroCameraFade = modeL;
        Shader.SetGlobalFloat("_PecoroCameraFade", pecoroCameraFade);
    }

    public void SetMode(bool mode)
    {
        _mode=mode;
    }
    #endregion
}
