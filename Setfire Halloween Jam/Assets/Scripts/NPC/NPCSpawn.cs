using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class NPCSpawn : MonoBehaviour
{
    public static NPCSpawn Instance;

    public static bool finishedSpawning = false;

    [SerializeField] private List<Transform> rooms = new List<Transform>();
    [SerializeField] private List<GameObject> npc = new List<GameObject>();
    [SerializeField] private List<Sprite> costumes;
    [SerializeField] private List<Sprite> hats;
    [SerializeField] private List<Sprite> shirts;
    [SerializeField] private List<Sprite> pants;
    public TextAsset file;
    private string[] fileLines;
    private GameObject newNpc;

    private List<List<float>> limits = new List<List<float>>()
    {
        new List<float> { -8f, -0.3f, 1.6f, 7.5f }, // Washroom
        new List<float> { 0.3f, 8f, 1.6f, 7.5f }, // Room
        new List<float> { -8f, 8f, -1f, 1.3f }, // Corridor
        new List<float> { -8f, 8f, -6.5f, -1.5f }, // Living room
    };
    private List<List<Vector3>> availableSpawnPoint = new List<List<Vector3>>();

    [SerializeField] private int npcAmount = 70;
    public int impostorAmount = 7;
    public int currentImpostors = 0;

    private ContactFilter2D contactFilter = new ContactFilter2D();
    public LayerMask mask = new LayerMask();

    void Start()
    {
        Instance = this;

        fileLines = File.ReadAllLines("cringe_sentences.txt");

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
                    if (Physics2D.OverlapCircle(position, 0.1f, contactFilter).Length == 0)
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
            Vector3 spawnPoint = availableSpawnPoint[room][spawnPointIndex];
            newNpc = Instantiate(chosenNPC, parent);
            newNpc.transform.position = spawnPoint;
            newNpc.GetComponent<NPCInteraction>().fileDialogue = fileLines[Random.Range(0, fileLines.Count())];
            availableSpawnPoint[room].RemoveAt(spawnPointIndex);
            availableSpawnPoint[room].RemoveAll(spawn => (spawnPoint - spawn).magnitude <= 0.8f);

            // Giving the NPC clothes
            ref bool hasThemedClothes = ref newNpc.GetComponent<NPCInteraction>().hasThemedClothes;
            ref bool impostor = ref newNpc.GetComponent<NPCInteraction>().imposter;
            if (currentImpostors < impostorAmount) impostor = Random.Range(0, 10) == 8;
            else impostor = false;

            if (impostor) currentImpostors++; 
            bool themedClotheNow = Random.Range(0, 2) == 0;
            if (!impostor)
            {
                costumes.AddRange(Resources.LoadAll<Sprite>("clothes/Costume/" + GameStateManager.Instance.theme)); // Loading the clothes in theme if the npc isn't an impostor
            }
            else
            {
                costumes.AddRange(Resources.LoadAll<Sprite>("clothes/Costume/" + GameStateManager.Instance.outOfTheme.ToString()));
                costumes.AddRange(Resources.LoadAll<Sprite>("clothes/Costume/Other"));
            }
            themedClotheNow = Random.Range(0, 2) == 0;
            hasThemedClothes = hasThemedClothes || themedClotheNow;
            if (themedClotheNow && !impostor)
            {
                hats.AddRange(Resources.LoadAll<Sprite>("clothes/Hat/" + GameStateManager.Instance.theme));
            }
            else
            {
                hats.AddRange(Resources.LoadAll<Sprite>("clothes/Hat/" + GameStateManager.Instance.outOfTheme));
                hats.AddRange(Resources.LoadAll<Sprite>("clothes/Hat/Other"));
            }
            themedClotheNow = Random.Range(0, 2) == 0;
            hasThemedClothes = hasThemedClothes || themedClotheNow;
            if (themedClotheNow && !impostor)
            {
                shirts.AddRange(Resources.LoadAll<Sprite>("clothes/Shirt/" + GameStateManager.Instance.theme));
            }
            else
            {
                shirts.AddRange(Resources.LoadAll<Sprite>("clothes/Shirt/" + GameStateManager.Instance.outOfTheme));
                shirts.AddRange(Resources.LoadAll<Sprite>("clothes/Shirt/Other"));
            }
            themedClotheNow = Random.Range(0, 2) == 0 || !hasThemedClothes;
            if (themedClotheNow && !impostor)
            {
                pants.AddRange(Resources.LoadAll<Sprite>("clothes/Pants/" + GameStateManager.Instance.theme));
            }
            else
            {
                pants.AddRange(Resources.LoadAll<Sprite>("clothes/Pants/" + GameStateManager.Instance.outOfTheme));
                pants.AddRange(Resources.LoadAll<Sprite>("clothes/Pants/Other"));
            }

            SpriteRenderer[] clothes = newNpc.transform.GetComponentsInChildren<SpriteRenderer>();
            if (Random.Range(0f, 10f) > 8 && impostor) // 2/10 chance of giving the npc a costume. If not, gives it clothes
            {
                clothes[1].sprite = costumes[Random.Range(0, costumes.Count)];
            }
            else
            {
                clothes[2].sprite = hats[Random.Range(0, hats.Count)];
                clothes[3].sprite = shirts[Random.Range(0, shirts.Count)];
                clothes[4].sprite = pants[Random.Range(0, pants.Count)];
            }
            costumes.Clear();
            hats.Clear();
            shirts.Clear();
            pants.Clear();

            if (availableSpawnPoint[room].Count == 0) availableSpawnPoint.Remove(availableSpawnPoint[room]);
            if (availableSpawnPoint.Count == 0) break;
        }

        finishedSpawning = true;
    }
}
