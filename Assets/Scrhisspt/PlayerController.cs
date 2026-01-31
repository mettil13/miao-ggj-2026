using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 maxRotation, minRotation;
    public float sensibility = 0.5f;
    public GameObject gO;
    public MascheraDellaNinna mask;

    public GameManager manager;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    

    public void OnLookAround(InputValue value) {
        Vector2 delta = value.Get<Vector2>();
        Vector3 rotation = gO.transform.eulerAngles;
        if(rotation.x > 180) {
            rotation.x -= 360;
        }
        if (rotation.y > 180) {
            rotation.y -= 360;
        }
        rotation += new Vector3(-delta.y * sensibility, delta.x * sensibility, 0);
        rotation = new Vector3(Mathf.Clamp(rotation.x, minRotation.x, maxRotation.x), Mathf.Clamp(rotation.y, minRotation.y, maxRotation.y), 0);

        //Debug.Log(rotation);
        gO.transform.eulerAngles = rotation;
    }

    void OnToggleMask(InputValue value) {
        mask.ToggleMask();
    }
}
