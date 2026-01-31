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
    public Vector3[] ssPoints = new Vector3[10];
    public Material material;
    ComputeBuffer buffer;
    public float pecoroCameraFade = 1;
    public bool  _mode = true;
    public float _modeL = 0;
    #endregion

    #region METHODS
    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {

        for (int i = 0; i < ssPoints.Length; i++)
        {
            ssPoints[i] = new Vector3(i, i * 2, 0);
        }

        CreateBuffer();
    }

    public void AddPallo(Transform transform)
    {
        _palli.Add(transform);
    }
    public void RemovePallo(Transform transform)
    {
        _palli.Remove(transform);
    }

    private void CreateBuffer()
    {
        if (buffer != null) return;

        buffer = new ComputeBuffer(ssPoints.Length, sizeof(float) * 3);
        buffer.SetData(ssPoints);
        material.SetBuffer("_SSPoints", buffer);
    }
    // Update is called once per frame
    void Update()
    {
        CreateBuffer();
        int i = 0;
        foreach (Transform t in _palli)
        {
            Vector3 ssPos = Camera.main.WorldToViewportPoint(t.position);
            ssPoints[i] = ssPos;
            i++;
        }

        buffer.SetData(ssPoints);
        material.SetBuffer("_SSPoints", buffer);

        _modeL = _mode ? Mathf.Clamp01(_modeL + Time.deltaTime*0.1f) : Mathf.Clamp01(_modeL - Time.deltaTime);
        pecoroCameraFade = _modeL;
        Shader.SetGlobalFloat("_PecoroCameraFade", pecoroCameraFade);
    }

    public void SetMode(bool mode)
    {
        _mode=mode;
    }
    #endregion
}
