using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ObjectReplacer : MonoBehaviour
{
    [SerializeField] GameObject[] objectsToReplace;

    [SerializeField] GameObject replaceWith;

    [Header("Futher Settings")]


    [SerializeField] string loadScaleOfObject = "NotImplementedYet(bool)";
    [SerializeField] bool deleteOriginal = false;
    [SerializeField] bool InstantiateAsPrefab = true;
    [SerializeField] bool CopyRotation = true;
    [SerializeField] bool OriginalParent = false;
    [SerializeField] Transform parent;
    [ContextMenuItem("ReplaceObjects", "ReplaceObjectLaucher")]
    [ContextMenuItem("Remove Source Objects (RESET)", "ClearSourceObjects")]
    [SerializeField] bool RIGHTCLICKME;
   
    int ReplaceObjects()
    {
        //Error Check
        if(objectsToReplace.Length == 0)
        {
            Debug.LogWarning("No items to replace");
            return 1;
        }

        //Instation of new Obejcts
        for (int i = 0; i < objectsToReplace.Length; i++)
        {
            if(objectsToReplace[i] == null)
            {
                Debug.LogWarning("Object on Index: " + i + " is NULL");
                return 1;
            }

            if(InstantiateAsPrefab)
            {
                GameObject g = PrefabUtility.InstantiatePrefab(replaceWith) as GameObject;
                if(parent != null)
                {
                    g.transform.parent = parent;
                }
                if(OriginalParent)
                {
                    g.transform.parent = objectsToReplace[i].transform.parent;
                }
                if(CopyRotation)
                {
                    g.transform.rotation = objectsToReplace[i].transform.rotation;
                }
                g.transform.position = objectsToReplace[i].transform.position;
            }
            else
            {
                GameObject g = Instantiate(replaceWith, objectsToReplace[i].transform.position, objectsToReplace[i].transform.rotation, parent);
            }

            if(deleteOriginal)
            {
                DestroyImmediate(objectsToReplace[i]);
            }
        }
        return 0;
    }




    [ContextMenu("ReplaceObjects")]
    void ReplaceObjectLaucher()
    {
        Debug.Log("Functon 'Replace Objects' ended with exit code: " + ReplaceObjects().ToString());
    }

    void ClearSourceObjects()
    {
        objectsToReplace = new GameObject[0];
    }

}
