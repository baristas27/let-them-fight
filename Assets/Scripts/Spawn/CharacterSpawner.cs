using GanzSe;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    [Header("Prefab & Spawn Settings")]
    public GameObject characterPrefab;           // Modular karakter prefabi
    public Transform[] spawnPoints;              // 2 nokta (Transform array)

    [Header("Randomization Options")]
    public bool randomizeArmor = true;
    public bool randomizeFace = true;
    public bool randomHelmet = true;             // Rastgele kask göster/gizle

    public CharacterDefinition[] candidateDefs;

    void Start()
    {
        SpawnCharacters();
    }

    [ContextMenu("Spawn Characters (Editor)")]
    public void SpawnCharacters()
    {
        if (characterPrefab == null)
        {
            Debug.LogError("Character Prefab atanmamýþ!");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn noktalarý belirtilmemiþ!");
            return;
        }

        foreach (Transform spawnPoint in spawnPoints)
        {
            // Karakteri spawn et
            GameObject character = Instantiate(characterPrefab, spawnPoint.position, spawnPoint.rotation);
            

            var rc = character.GetComponent<RuntimeCharacter>();
            if (rc !=null && candidateDefs != null && candidateDefs.Length > 0)
            {
                var chosen = candidateDefs[Random.Range(0, candidateDefs.Length)];
                rc.ApplyDefinition(chosen);
            }

            // ModularHeroController bul ve randomize et
            ModularHeroController controller = character.GetComponent<ModularHeroController>();
            if (controller != null)
            {
                if (randomizeArmor) controller.RandomizeArmorParts();
                if (randomizeFace) controller.RandomizeFaceParts();

                if (randomHelmet)
                {
                    controller.showHelmet = Random.value > 0.5f;
                    controller.ToggleHelmet();
                }
            }
            else
            {
                Debug.LogWarning("Spawn edilen karakterde ModularHeroController yok: " + character.name);
            }
        }
    }

    // Editor'da test etmek için
    [ContextMenu("Clear Spawned Characters")]
    private void ClearSpawnedCharacters()
    {
        ModularHeroController[] all = FindObjectsOfType<ModularHeroController>();
        foreach (var ctrl in all)
        {
            if (Application.isPlaying)
                Destroy(ctrl.gameObject);
            else
                DestroyImmediate(ctrl.gameObject);
        }
    }

 

}