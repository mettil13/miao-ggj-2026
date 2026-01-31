
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class Pecora : MonoBehaviour
{
    private Rigidbody rb;
    private Camera mainCamera;
    private float distanceFromCamera;
    private Vector3 offset;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }


    bool dragging=false;
    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                    OnMouseDown();
            }
        
        }
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                    OnMouseUp();
            }
        
        }

        if(dragging)
            OnMouseDrag();

    }


    void OnMouseDown()
    {
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

    void OnMouseDrag()
    {
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        transform.position = mouseWorldPos + offset;
    }

    void OnMouseUp()
    {
        rb.isKinematic = false;
        dragging=false;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = distanceFromCamera;
        return mainCamera.ScreenToWorldPoint(mousePos);
    }
}
