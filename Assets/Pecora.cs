
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Pecora : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private float distanceFromCamera;
    private Vector3 offset;

    public float maxLaunchMagnitude = 1;


    Vector3 dragVector;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = PecoraMinigame.instance.mainCamera;
        PecoraMinigame.instance.OnNewPecora(this);
    }


    bool dragging=false;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                    OnMouseDowno();
            }
            */

            Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);
           Vector3 mousePos = mainCamera.ScreenToViewportPoint(GetMousePos()) ;


            if(Vector2.Distance(screenPos, mousePos) < 0.07f)
                OnMouseDowno();
        
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            if(dragging)
                    OnMouseUpo();
        }

        if(dragging)
            OnMouseDraggo();
        else
        {
            Vector3 trPos = PecoraMinigame.instance.transform.InverseTransformPoint(transform.position);
            if(trPos.x > 0f && trPos.y < 1f)
                GetComponent<Rigidbody>().AddForce(Vector3.right* 10);

            if(trPos.x > 14f)
            {
                PecoraMinigame.instance.OnPecoraExit(this);
            }
        }

    }


    void OnMouseDowno()
    {
        dragVector = Vector3.zero;
        dragging=true;
        
        distanceFromCamera = Vector3.Distance(
            mainCamera.transform.position,
            transform.position
        );

        Vector3 mouseWorldPos = GetMouseWorldPosition();
        offset = transform.position - mouseWorldPos;

        // Evita che la fisica interferisca durante il drag
        rb.isKinematic = true;
    }

Vector3 lastPos;
    void OnMouseDraggo()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();

        Vector3 newPos = mouseWorldPos + offset;

        Vector3 delta = newPos - lastPos;
        dragVector = delta / Time.deltaTime;
        dragVector.z=0;


        transform.position = newPos;
        lastPos = newPos;
    }

    void OnMouseUpo()
    {
        rb.isKinematic = false;
        dragging=false;

        rb.linearVelocity = dragVector.normalized * Mathf.Min(dragVector.magnitude, maxLaunchMagnitude);
    }

    private Vector3 GetMousePos()
    {
        if(Camera.main == null)
            return Input.mousePosition;

        Vector3 p = Input.mousePosition;

        Vector3 vP = Camera.main.ScreenToViewportPoint(p);

        return PecoraMinigame.instance.mainCamera.ViewportToScreenPoint(vP);
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = GetMousePos();
        mousePos.z = distanceFromCamera;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
