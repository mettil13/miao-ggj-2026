using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 maxRotation, minRotation;
    public float sensibility = 0.5f;
    public GameObject gO;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLookAround(InputValue value) {
        Vector2 delta = value.Get<Vector2>();
        Vector3 rotation = gO.transform.eulerAngles;
        rotation += new Vector3(-delta.y * sensibility, delta.x * sensibility, 0);
        //rotation = new Vector3(Mathf.Clamp(rotation.x, minRotation.x, maxRotation.x), Mathf.Clamp(rotation.y, minRotation.y, maxRotation.y), 0);

        if (rotation.y < 180) {
            rotation.y = Mathf.Min(rotation.y, maxRotation.y);
            //rotation.y = Mathf.Clamp(rotation.y, -1, maxRotation.y);
        }
        else {
            rotation.y = Mathf.Max(rotation.y, 360 - minRotation.y);
            //rotation.y = Mathf.Clamp(rotation.y, 360 - minRotation.y, 361);
        }

        if (rotation.x < 180) {
            rotation.x = Mathf.Min(rotation.x, maxRotation.x);
            //rotation.x = Mathf.Clamp(rotation.x, 0, maxRotation.x);
        }
        else {
            rotation.x = Mathf.Max(rotation.x, 360 - minRotation.x);
            //rotation.x = Mathf.Clamp(rotation.x, 360 - minRotation.x, 360);
        }
        Debug.Log(rotation);
        gO.transform.eulerAngles = rotation;
    }
}
