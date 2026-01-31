using UnityEngine;

public class BlobboPallo : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BlobbiPalliScreenSpace.instance.AddPallo(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(Camera.main.transform.position);
    }

    public void OnDestroy()
    {
        BlobbiPalliScreenSpace.instance.RemovePallo(this.transform);
    }
}
