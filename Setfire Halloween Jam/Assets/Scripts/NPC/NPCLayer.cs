using UnityEngine;

public class NPCLayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer _rd;

    [SerializeField] private GameObject pants;
    [SerializeField] private GameObject[] clothes;

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
            if (pants) pants.GetComponent<SpriteRenderer>().sortingOrder = 19;
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
            if (pants) pants.GetComponent<SpriteRenderer>().sortingOrder = 23;
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
