using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomFog : MonoBehaviour
{
    [SerializeField] private GameObject room;

    private void Start()
    {
        room.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            room.SetActive(true);
            GetComponent<TilemapRenderer>().enabled = false; // à transformer en fondu
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            room.SetActive(false);
            GetComponent<TilemapRenderer>().enabled = true;
        }
    }
}
