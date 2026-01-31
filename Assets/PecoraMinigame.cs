using System;
using System.Collections.Generic;
using UnityEngine;

public class PecoraMinigame : MonoBehaviour
{
    public Camera mainCamera;

    public static PecoraMinigame instance;

    public Action onPecoraExit;

    List<Pecora> pecore =new List<Pecora>();

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

    public void Reset()
    {
        foreach(var pecora in pecore)
            GameObject.Destroy(pecora.gameObject);

        pecore.Clear();
    }
}
