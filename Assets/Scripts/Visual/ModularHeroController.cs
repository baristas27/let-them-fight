using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace GanzSe
{
    public class ModularHeroController : MonoBehaviour
    {
        [Header("Armor Parts")]
        public Transform armorPartsRoot;

        [Header("Face Details Parts")]
        public Transform facePartsRoot;

        [Header("Toggle Helmet")]
        public bool showHelmet = true;

        public void RandomizeArmorParts()
        {
            if (armorPartsRoot == null) return;
            foreach (Transform category in armorPartsRoot)
            {
                SetRandomActiveChild(category);
            }
        }

        public void RandomizeFaceParts()
        {
            if (facePartsRoot == null) return;
            foreach (Transform category in facePartsRoot)
            {
                SetRandomActiveChild(category);
            }
        }

        public void ToggleHelmet()
        {
            Transform heads = armorPartsRoot.Find("HEADS");
            Transform faceParent = facePartsRoot;

            if (heads == null || faceParent == null) return;

            heads.gameObject.SetActive(showHelmet);
            faceParent.gameObject.SetActive(!showHelmet);
        }

        private void SetRandomActiveChild(Transform category)
        {
            if (category.childCount == 0) return;

            foreach (Transform child in category)
            {
                child.gameObject.SetActive(false);
            }

            int rand = Random.Range(0, category.childCount);
            category.GetChild(rand).gameObject.SetActive(true);
        }

        public void RemoveInactiveParts()
        {
            CleanChildren(armorPartsRoot);
            CleanChildren(facePartsRoot);
        }

        private void CleanChildren(Transform root)
        {
            if (root == null) return;
            List<GameObject> toDestroy = new List<GameObject>();
            foreach(Transform category in root)
            {
                foreach (Transform child in category)
                {
                    if(!child.gameObject.activeSelf)
                        toDestroy.Add(child.gameObject);
                }
            }

            foreach (GameObject g in toDestroy)
                Destroy(g);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(ModularHeroController))]
    public class ModularCharacterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            ModularHeroController controller = (ModularHeroController)target;

            GUILayout.Space(10);
            GUILayout.Label("Editor Controls", EditorStyles.boldLabel);

            if (GUILayout.Button("Randomize Armor Parts"))
            {
                controller.RandomizeArmorParts();
            }

            if (GUILayout.Button("Randomize Face Parts"))
            {
                controller.RandomizeFaceParts();
            }

            if (GUILayout.Button(controller.showHelmet ? "Hide Helmet" : "Show Helmet"))
            {
                controller.showHelmet = !controller.showHelmet;
                controller.ToggleHelmet();
            }
        }
    }
#endif
}