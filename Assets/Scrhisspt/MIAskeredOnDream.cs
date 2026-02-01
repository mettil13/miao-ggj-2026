using System.Collections;
using UnityEngine;

public class MIAskeredOnDream : MonoBehaviour
{
    [SerializeField] private Collider laser;
    [SerializeField] private bool onDisable = false;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playerController.mask.isMaskDown && !onDisable)
        {
            StopAllCoroutines();
            StartCoroutine(DisableLaser());
        }
        else if (!GameManager.instance.playerController.mask.isMaskDown && onDisable)
        {
            StopAllCoroutines();
            StartCoroutine(EnableLaser());
        }
    }

    IEnumerator EnableLaser()
    {
        onDisable = false;
        yield return new WaitForSecondsRealtime(0.5f);
        laser.enabled = true;
        laser.gameObject.SetActive(true);
    }

    IEnumerator DisableLaser()
    {
        onDisable = true;
        //yield return new WaitForSecondsRealtime()
        laser.enabled = false;
        laser.gameObject.SetActive(false);
        yield return null;
    }
}
