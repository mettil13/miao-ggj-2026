using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 maxRotation, minRotation;
    public float sensibility = 0.5f;
    public GameObject gO;
    public MascheraDellaNinna mask;

    public GameManager manager;
    public Material movingMaterial;
    public float baseMovement = 0.02f;
    public float intensity = .2f;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    

    public void OnLookAround(InputValue value) {
        Vector2 delta = value.Get<Vector2>();


        Shader.SetGlobalFloat("_Intensity",( Mathf.Clamp(delta.sqrMagnitude, 0, 1) * intensity + baseMovement)*0.1666f);

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

        BlobbiPalliScreenSpace.instance.SetMode(mask.isMaskDown);
        if (mask.isMaskDown)
        {
            PecoraMinigame.instance.Enable();
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            PecoraMinigame.instance.Disable();
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void LateUpdate() {
        if (Cursor.lockState == CursorLockMode.Locked)
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));
    }
}
