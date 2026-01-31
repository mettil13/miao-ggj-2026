using UnityEngine;
using UnityEngine.Audio;

public class MeaownsterManager : MonoBehaviour
{
    public bool playRandomAudio = true;
    public AudioSource[] audioSources;
    public MeaownsterSpawner[] monsterSpawners;

    public GameObject[] monsters;
    public GameObject[] kittens;

    public AudioResource genericClip;
    public AudioResource catsClip;
    public AudioResource monstersClip;

    private float randomAudioWaitTime = 0;
    private float monsterSpawnWaitTime = 0;

    public static MeaownsterManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playRandomAudio && randomAudioWaitTime < Time.time) {
            int randomAudioIndex = Random.Range(0, audioSources.Length);
            // play audio
            audioSources[randomAudioIndex].resource = genericClip;
            audioSources[randomAudioIndex].Play();

            randomAudioWaitTime = Random.Range(0.0f, 10.0f) + Time.time;
        }

        if (monsterSpawnWaitTime < Time.time) {
            int monsterSpawnerIndex = Random.Range(0, monsterSpawners.Length);
            GameObject go;
            if(Random.Range(0, 2) > 0) {
                go = monsters[Random.Range(0, monsterSpawners.Length)];
                monsterSpawners[monsterSpawnerIndex].SpawnMonster(go, false);
            }
            else {
                go = kittens[Random.Range(0, kittens.Length)];
                monsterSpawners[monsterSpawnerIndex].SpawnMonster(go, true);
            }

            monsterSpawnWaitTime = Random.Range(0.0f, 5.0f) + Time.time;
        }
    }
}
