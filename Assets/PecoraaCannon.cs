using UnityEngine;

public class PecoraaCannon : MonoBehaviour
{
    public GameObject pecoraPrefab;

    public float spawnForce;
    public float spawnTime = 5f;

    float remainingTime=0;
    
    void Update()
    {
        remainingTime-=Time.deltaTime;

        if(remainingTime <= 0f)
        {
            GameObject pecoraObj = Instantiate(pecoraPrefab);
            pecoraObj.transform.position = transform.position;

            Vector3 dir = Quaternion.Euler(0,0, Random.Range(0f, 60f)) * Vector3.right;

            pecoraObj.GetComponent<Rigidbody>().AddForce(dir * spawnForce);

            remainingTime = spawnTime;
        }
    }

}
