using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public static bool finishedSpawning = false;

    [SerializeField] private List<Transform> rooms = new List<Transform>();
    [SerializeField] private List<GameObject> npc = new List<GameObject>();
    private GameObject newNpc;

    private List<List<float>> limits = new List<List<float>>()
    {
        new List<float> { -8f, -0.3f, 1.6f, 7.5f }, // Washroom
        new List<float> { 0.3f, 8f, 1.6f, 7.5f }, // Room
        new List<float> { -8f, 8f, -1f, 1.3f }, // Corridor
        new List<float> { -8f, 8f, -6.5f, -1.5f }, // Living room
    };
    private List<List<Vector3>> availableSpawnPoint = new List<List<Vector3>>();
    private Vector3 height = new Vector3(0f, 0.2f);

    private int npcAmount = 100;

    private ContactFilter2D contactFilter = new ContactFilter2D();
    public LayerMask mask = new LayerMask();

    void Awake()
    {
        contactFilter.useTriggers = true;
        contactFilter.layerMask = mask;
        contactFilter.useLayerMask = true;
        for (int r = 0; r < rooms.Count; r++)
        {
            List<Vector3> spawnPoints = new List<Vector3>();
            for (float i = limits[r][0]; i < limits[r][1]; i += 0.3f)
            {
                for (float j = limits[r][2]; j < limits[r][3]; j += 0.3f)
                {
                    Vector3 position = new Vector3(i, j);
                    if (Physics2D.OverlapCircle(position - height, 0.15f, contactFilter).Length == 0)
                    {
                        spawnPoints.Add(position);
                    }
                }
            }
            availableSpawnPoint.Add(spawnPoints);
        }

        for (int i = 0; i < npcAmount; i++)
        {
            int room = Random.Range(0, limits.Count);
            Transform parent = rooms[room];
            GameObject chosenNPC = npc[Random.Range(0, npc.Count)];
            int spawnPointIndex = Random.Range(0, availableSpawnPoint[room].Count);
            newNpc = Instantiate(chosenNPC, parent);
            newNpc.transform.position = availableSpawnPoint[room][spawnPointIndex];
            availableSpawnPoint[room].RemoveAt(spawnPointIndex);
        }

        finishedSpawning = true;
    }
}
