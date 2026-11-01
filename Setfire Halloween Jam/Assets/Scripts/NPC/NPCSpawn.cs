using System.Collections.Generic;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public static bool finishedSpawning = false;

    [SerializeField] private List<Transform> rooms = new List<Transform>();
    [SerializeField] private List<GameObject> npc = new List<GameObject>();
    [SerializeField] private Sprite[] costumes;
    [SerializeField] private Sprite[] hats;
    [SerializeField] private Sprite[] shirts;
    [SerializeField] private Sprite[] pants;
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
        costumes = Resources.LoadAll<Sprite>("clothes/Costume");
        hats = Resources.LoadAll<Sprite>("clothes/Hat");
        shirts = Resources.LoadAll<Sprite>("clothes/Shirt");
        pants = Resources.LoadAll<Sprite>("clothes/Pants");

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
                    if (Physics2D.OverlapCircle(position - height, 0.1f, contactFilter).Length == 0)
                    {
                        spawnPoints.Add(position);
                    }
                }
            }
            availableSpawnPoint.Add(spawnPoints);
        }

        for (int i = 0; i < npcAmount; i++) // spawning npcAmount NPCs
        {
            // Spawning the NPC at the right position
            int room = Random.Range(0, limits.Count);
            Transform parent = rooms[room];
            GameObject chosenNPC = npc[Random.Range(0, npc.Count)];
            int spawnPointIndex = Random.Range(0, availableSpawnPoint[room].Count);
            newNpc = Instantiate(chosenNPC, parent);
            newNpc.transform.position = availableSpawnPoint[room][spawnPointIndex];
            availableSpawnPoint[room].RemoveAt(spawnPointIndex);

            // Giving the NPC clothes
            SpriteRenderer[] clothes = newNpc.transform.GetComponentsInChildren<SpriteRenderer>();
            if (Random.Range(0f, 10f) > 8) // 2/10 chance of giving the npc a costume. If not, gives it clothes
            {
                clothes[1].sprite = costumes[Random.Range(0, costumes.Length)];
            }
            else
            {
                clothes[2].sprite = hats[Random.Range(0, hats.Length)];
                clothes[3].sprite = shirts[Random.Range(0, shirts.Length)];
                clothes[4].sprite = pants[Random.Range(0, pants.Length)];
            }
        }

        finishedSpawning = true;
    }
}
