using System;
using System.Collections.Generic;
using UnityEngine;

public class PecoraMinigame : MonoBehaviour
{
    public Camera mainCamera;

    public static PecoraMinigame instance;

    public Action onPecoraExit;

    List<Pecora> pecore =new List<Pecora>();

    public AudioSource audioSource;

    public PecoraaCannon cannon;

    public int pecoreCount
    {
        get
        {
            return pecore.Count;
        }
    }

    void Awake()
    {
        instance = this;

        Disable();
    }

    void Update()
    {
        if(Camera.main != null)
            audioSource.transform.position = Camera.main.transform.position;

        audioSource.volume = ((float)pecoreCount-3f) / 10f; 
    }

    public void OnNewPecora(Pecora pecora)
    {
        pecore.Add(pecora);
    }

    public void OnPecoraExit(Pecora pecora)
    {
        pecore.Remove(pecora);
        GameObject.Destroy(pecora.gameObject);

        onPecoraExit?.Invoke();
    }

    void Reset()
    {
        foreach(var pecora in pecore)
            GameObject.Destroy(pecora.gameObject);

        pecore.Clear();
    }

    public void Enable()
    {
        Reset();

        audioSource.enabled=true; 
        cannon.enabled=true;
    }

    public void Disable()
    {
        Reset();

        audioSource.enabled=false;
        cannon.enabled=false;
    }
}
