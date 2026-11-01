using UnityEngine;

public class NPCLayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer _rd;

    public GameObject costume;
    public GameObject pants;
    public GameObject[] clothes;

    private bool inZone;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
        _rd = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (transform.position.y >= player.transform.position.y && inZone || !inZone)
        {
            _rd.sortingOrder = 18;
            SpriteRenderer pantsSprite = pants.GetComponent<SpriteRenderer>();
            SpriteRenderer costumeSprite = costume.GetComponent<SpriteRenderer>();
            if (pantsSprite.sprite) pantsSprite.sortingOrder = 19;
            else if (costumeSprite.sprite) costumeSprite.sortingOrder = 19;
            if (clothes.Length != 0)
            {
                foreach (GameObject clothe in clothes)
                {
                    clothe.GetComponent<SpriteRenderer>().sortingOrder = 20;
                }
            }
        }
        else
        {
            _rd.sortingOrder = 22;
            SpriteRenderer pantsSprite = pants.GetComponent<SpriteRenderer>();
            SpriteRenderer costumeSprite = costume.GetComponent<SpriteRenderer>();  
            if (pantsSprite.sprite) pantsSprite.sortingOrder = 23;
            else if (costumeSprite.sprite) costumeSprite.sortingOrder = 23;
            if (clothes.Length != 0)
            {
                foreach (GameObject clothe in clothes)
                {
                    clothe.GetComponent<SpriteRenderer>().sortingOrder = 24;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inZone = false;
        }
    }
}
