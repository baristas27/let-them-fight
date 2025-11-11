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
    public bool randomHelmet = true;             // Rastgele kask g�ster/gizle

    public CharacterDefinition[] candidateDefs;

    [Header("Runtime References")]
    public GameObject fighterA;
    public GameObject fighterB;

    void Start()
    {
        SpawnCharacters();
    }

    [ContextMenu("Spawn Characters (Editor)")]
    public void SpawnCharacters()
    {
        if (characterPrefab == null)
        {
            Debug.LogError("Character Prefab atanmamis");
            return;
        }

        if (spawnPoints == null || spawnPoints.Length < 2)
        {
            Debug.LogError("Spawn noktalari belirtilmemis!");
            return;
        }

        fighterA = Instantiate(characterPrefab, spawnPoints[0].position, spawnPoints[0].rotation);


        fighterB = Instantiate(characterPrefab, spawnPoints[1].position, spawnPoints[1].rotation);

        SetupSpawnedCharacter(fighterA);
        SetupSpawnedCharacter(fighterB);

        fighterA.GetComponent<FighterAI>().SetTarget(fighterB.transform);
        fighterB.GetComponent<FighterAI>().SetTarget(fighterA.transform);



    }

    private void SetupSpawnedCharacter(GameObject character)
    {
        if (character == null) return;
         
        var rc = character.GetComponent<RuntimeCharacter>();
        if(rc !=null && candidateDefs != null && candidateDefs.Length > 0)
        {
            var chosen = candidateDefs[Random.Range(0, candidateDefs.Length)];
            rc.ApplyDefinition(chosen);
        }



        ModularHeroController controller = character.GetComponent<ModularHeroController>();
        if (controller != null)
        {
            if (randomizeArmor) controller.RandomizeArmorParts();
            if (randomizeFace) controller.RandomizeFaceParts();

            if(randomHelmet)
            {
                controller.showHelmet = Random.value > 0.5f;
                controller.ToggleHelmet();
            }
            controller.RemoveInactiveParts();
        }
        else
        {
            Debug.LogWarning("Spawn edilen karakterde ModularHeroController yok: " + character.name);
        }
    }

    // Editor'da test etmek i�in
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