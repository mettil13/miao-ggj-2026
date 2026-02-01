using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraTriggrrrrr : MonoBehaviour
{
    public UnityEvent onLASERSTRONZO;
    public float tempoToWatch = 1;
    public SpriteRenderer monsterRenderer;
    public Tween glowTween;
    public bool isKitten = false;

    private void Awake()
    {
        Material newMat = new Material(monsterRenderer.material);
        monsterRenderer.material = newMat;
    }

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        StartCoroutine(STRONZOROUTINE());
        Debug.Log("CAIO");

       if(!isKitten) BlobbiPalliScreenSpace.instance.AddViewed(this.transform);
    }
    private void OnTriggerExit(Collider other)
    {
        SetMatPropertyBase();
        StopAllCoroutines();
        BlobbiPalliScreenSpace.instance.RemoveViewed(this.transform);
    }

    IEnumerator STRONZOROUTINE()
    {
        glowTween?.Kill();
        glowTween = DOVirtual.Float(0, 1, tempoToWatch, (f) => SetMatProperty(f)).SetEase(Ease.InQuart);
        yield return new WaitForSecondsRealtime(tempoToWatch);
        onLASERSTRONZO.Invoke();
    }

    public void SetMatPropertyBase()
    {
        glowTween?.Kill();
        SetMatProperty(0);
    }
    public void SetMatProperty(float value01)
    {
        monsterRenderer.material.SetFloat("_value", value01);
    }
}
