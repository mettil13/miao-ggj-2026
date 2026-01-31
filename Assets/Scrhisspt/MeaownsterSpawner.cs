using UnityEngine;

public class MeaownsterSpawner : MonoBehaviour
{
    public bool lookingLeft;
    public AudioSource audioSource;
    private bool isKitten = false;
    
    private GameObject monster;
    private GameObject instantiatedMonster;
    private bool hasSpawned;
    private float waitTime = 0;

    public void SpawnMonster(GameObject monster, bool isKitten) {
        if (this.monster != null) {
            hasSpawned = false;
            this.monster = monster;
            this.isKitten = isKitten;
        }
    }

    private void Update() {
        if (monster && !hasSpawned && Time.time > waitTime) {
        
            int rand = Random.Range(0, 4);
            if(rand > 2) {
                instantiatedMonster = Instantiate(monster);
                instantiatedMonster.transform.position = transform.position;

                hasSpawned = true;
            }
            else {
                // play sound
                if (isKitten) {
                    audioSource.resource = MeaownsterManager.instance.catsClip;
                }
                else {
                    audioSource.resource = MeaownsterManager.instance.monstersClip;
                }
                audioSource.Play();

                waitTime = Random.Range(0.0f, 3.0f) + Time.time;
            }
            
        }

    }

    public void DespawnMonster() {
        if (instantiatedMonster != null) {
            Destroy(instantiatedMonster);
        }
        hasSpawned = false;
        monster = null;
    }

}
