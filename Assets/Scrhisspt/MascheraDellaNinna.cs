using DG.Tweening;
using UnityEngine;

public class MascheraDellaNinna : MonoBehaviour
{
    public bool isMaskDown = false;
    public GameObject mask;
    public float animDuration = 0.5f;

    private Vector3 maskUpPosition;

    private void Start() {
        maskUpPosition = mask.transform.localPosition;
    }

    public void PutMaskDown() {
        isMaskDown = true;
        mask.transform.DOLocalMoveY(0, animDuration);
    }

    public void PutMaskUp() {
        isMaskDown = false;
        mask.transform.DOLocalMoveY(maskUpPosition.y, animDuration);
    }

    public void ToggleMask() {
        if (isMaskDown) {
            PutMaskUp();
        }
        else {
            PutMaskDown();
        }
    }

}
