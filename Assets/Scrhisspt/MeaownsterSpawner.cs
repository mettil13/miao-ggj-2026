using UnityEngine;

public class MeaownsterSpawner : MonoBehaviour
{
    public bool lookingLeft;
    public AudioSource audioSource;
    private bool isKitten = false;
    
    [SerializeField] private GameObject monster;
    private GameObject instantiatedMonster;
    private bool hasSpawned;
    private float spawnTime = float.MaxValue;
    private float waitTime = 0;
    public Animator animator;
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
                instantiatedMonster.GetComponentInChildren<CameraTriggrrrrr>().onLASERSTRONZO.AddListener(DespawnMonster);
                instantiatedMonster.transform.parent = this.pivot;
                animator.Play("DEFAULT");
                spawnTime = Time.time;
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

            }
            waitTime = Random.Range(0.0f, 6.0f) + Time.time;
            
        }

        if (monster != null && hasSpawned && spawnTime + GameManager.instance.loseTime < Time.time) {
            GameManager.instance.LoseGame();
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

}
