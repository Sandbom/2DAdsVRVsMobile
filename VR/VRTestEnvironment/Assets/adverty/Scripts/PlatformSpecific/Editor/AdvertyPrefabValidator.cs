using UnityEditor;
using UnityEngine;
using System.Linq;

namespace Adverty.Editor
{
    internal class AdvertyPrefabValidator : UnityEditor.Editor
    {
        private const string DEFAULT_GAMEOBJECT_NAME = "Unit";
        private const string DEFAULT_MESH_LOCATION = "Meshes/UnitMesh";
        private const string DEFAULT_MATERIAL_LOCATION = "Materials/UnitMaterial";

        internal static void ValidatePrefab(string assetPath, bool isMainUnit)
        {
#if UNITY_2018_3_OR_NEWER
            Fit(assetPath, isMainUnit);
#else
            Object prefab = AssetDatabase.LoadAssetAtPath<Object>(assetPath);
            Fit(prefab as GameObject, isMainUnit);
#endif
        }

        private static void Fit<T>(T inputObject, bool isMainUnit)
        {
            GameObject instatiatedPrefab = GetInstantiatedUnitPrefab(inputObject);

            if (isMainUnit)
            {
                if (IsSomethingMissedInPrefab(instatiatedPrefab))
                {
                    GameObject newPrefabInstance = CreateNewUnit();
                    SavePrefab(newPrefabInstance, inputObject);
                    DestroyImmediate(newPrefabInstance);
                }
            }
            else
            {
                bool isPrefabChanged = false;
                FitParentPrefab(ref instatiatedPrefab, out isPrefabChanged);

                if (isPrefabChanged)
                {
                    SavePrefab(instatiatedPrefab, inputObject);
                }
            }

            DestroyPrefabInstance(instatiatedPrefab);
        }

        private static void FitParentPrefab(ref GameObject instatiatedPrefab, out bool isPrefabChanged)
        {
            isPrefabChanged = false;
            for (int childIndex = 0; childIndex < instatiatedPrefab.transform.childCount; childIndex++)
            {
                GameObject child = instatiatedPrefab.transform.GetChild(childIndex).gameObject;
                if (IsSomethingMissedInPrefab(child))
                {
                    GameObject unit = CreateNewUnit();
                    unit.transform.parent = child.transform.parent;
                    unit.transform.localPosition = child.transform.localPosition;
                    unit.transform.localRotation = child.transform.localRotation;
                    unit.transform.localScale = child.transform.localScale;
                    DestroyImmediate(child);
                    childIndex--;
                    isPrefabChanged = true;
                }
            }
        }

        private static void DestroyPrefabInstance(GameObject instantiatedPrefab)
        {
#if UNITY_2018_3_OR_NEWER
            PrefabUtility.UnloadPrefabContents(instantiatedPrefab);
#else
            DestroyImmediate(instantiatedPrefab);
#endif
        }

        private static void SavePrefab<T>(GameObject instance, T targetPrefab)
        {
#if UNITY_2018_3_OR_NEWER

            PrefabUtility.SaveAsPrefabAsset(instance, targetPrefab as string);
#else
            PrefabUtility.ReplacePrefab(instance, targetPrefab as GameObject, ReplacePrefabOptions.ConnectToPrefab);
#endif
        }

        private static GameObject GetInstantiatedUnitPrefab<T>(T prefab)
        {
#if UNITY_2018_3_OR_NEWER
            return PrefabUtility.LoadPrefabContents(prefab as string);
#else
            return (GameObject)PrefabUtility.InstantiatePrefab(prefab as GameObject);
#endif
        }

        private static GameObject CreateNewUnit()
        {
            GameObject unit = new GameObject(DEFAULT_GAMEOBJECT_NAME);
            unit.AddComponent<MeshFilter>().mesh = (Mesh)Resources.Load(DEFAULT_MESH_LOCATION);
            unit.AddComponent<MeshRenderer>().material = (Material)Resources.Load(DEFAULT_MATERIAL_LOCATION);
            unit.AddComponent<Unit>();
            return unit;
        }

        private static bool IsContainsMissedComponent(GameObject childObject)
        {
            Component[] components = childObject.GetComponents<Component>();
            return components.Any(component => !component);
        }

        private static bool IsSomethingMissedInPrefab(GameObject childObject)
        {
            if(IsContainsMissedComponent(childObject))
            {
                return true;
            }

            if(!childObject.GetComponent<Unit>())
            {
                return false;
            }

            if(!childObject.GetComponent<MeshRenderer>().sharedMaterial || !childObject.GetComponent<MeshFilter>().sharedMesh)
            {
                return true;
            }

            return false;
        }
    }
}
