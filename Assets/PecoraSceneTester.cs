using UnityEngine;

public class PecoraSceneTester : MonoBehaviour
{
    void Start()
    {
       PecoraMinigame.instance.Enable(); 
       PecoraMinigame.instance.mainCamera.gameObject.AddComponent<AudioListener>();
    }

}
