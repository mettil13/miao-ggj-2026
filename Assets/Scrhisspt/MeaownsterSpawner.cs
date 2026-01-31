using UnityEngine;

public class MeaownsterSpawner : MonoBehaviour
{
    public bool lookingLeft;
    public AudioSource audioSource;
    private bool isKitten = false;
    private float kittenDespawnTime = float.MaxValue;
    
    [SerializeField] private GameObject monster;
    private GameObject instantiatedMonster;
    private bool hasSpawned;
    private float spawnTime = float.MaxValue;
    private float waitTime = 0;
    public Animation anim;
    public Transform pivot;

    public void SpawnMonster(GameObject monster, bool isKitten) {
        if (this.monster == null) {
            hasSpawned = false;
            this.monster = monster;
            this.isKitten = isKitten;
        }
    }

    private void Update() {
        if (monster && Time.time > waitTime) {
        
            int rand = Random.Range(0, 4);
            if(rand > 2 && !hasSpawned) {
                instantiatedMonster = Instantiate(monster);
                instantiatedMonster.transform.position = transform.position;
                instantiatedMonster.transform.parent = transform;
                instantiatedMonster.GetComponentInChildren<CameraTriggrrrrr>().onLASERSTRONZO.AddListener(DespawnMonsterWithSound);
                instantiatedMonster.transform.parent = this.pivot;
                instantiatedMonster.transform.localPosition = Vector3.zero;
                anim.Stop();
                anim.Play();
                spawnTime = Time.time;
                kittenDespawnTime = Random.Range(5.0f, 15.0f) + Time.time;
                hasSpawned = true;
            }
            
            // play sound
            if (isKitten) {
                audioSource.resource = MeaownsterManager.instance.catsClip;
            }
            else {
                audioSource.resource = MeaownsterManager.instance.monstersClip;
            }

            if (hasSpawned) {
                audioSource.volume = 1f;
            }
            else {
                audioSource.volume = 0.25f;
            }
                    
            audioSource.Play();
            
            waitTime = Random.Range(0.0f, 6.0f) + Time.time;
        }

        if (monster != null && !isKitten && hasSpawned && spawnTime + GameManager.instance.loseTime < Time.time) {
            GameManager.instance.LoseGame();
        }

        if(isKitten && hasSpawned && Time.time > kittenDespawnTime) {
            DespawnMonster();
        }
    }

    public void DespawnMonster() {
        if (instantiatedMonster != null) {
            Destroy(instantiatedMonster);
        }
        hasSpawned = false;
        monster = null;
        spawnTime = float.MaxValue;
    }

    public void DespawnMonsterWithSound() {
        if (isKitten) {
            audioSource.resource = MeaownsterManager.instance.catsDeathClip;
        }
        else {
            audioSource.resource = MeaownsterManager.instance.monstersDeathClip;
        }
        audioSource.volume = 1;
        audioSource.Play();
        DespawnMonster();
    }

}
