using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectsManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectToEnable = new List<GameObject>();

    private void Awake()
    {
        StartCoroutine(enableObjects());
    }

    private IEnumerator enableObjects()
    {
        yield return new WaitWhile(() => !NPCSpawn.finishedSpawning);
        foreach (GameObject obj in objectToEnable)
        {
            obj.SetActive(true);
        }
    }
}
