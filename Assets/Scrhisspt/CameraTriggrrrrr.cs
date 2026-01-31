using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraTriggrrrrr : MonoBehaviour
{
    public UnityEvent onLASERSTRONZO;
    public float tempoToWatch = 1;

    private void OnTriggerEnter(Collider other)
    {
        StopAllCoroutines();
        StartCoroutine(STRONZOROUTINE());
        Debug.Log("CAIO");
    }
    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
    }

    IEnumerator STRONZOROUTINE()
    {
        yield return new WaitForSecondsRealtime(tempoToWatch);
        onLASERSTRONZO.Invoke();
    }

}
