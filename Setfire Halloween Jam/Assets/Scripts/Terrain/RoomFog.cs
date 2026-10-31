using UnityEngine;
using UnityEngine.Tilemaps;

public class RoomFog : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TilemapRenderer>().enabled = false; // à transformer en fondu
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GetComponent<TilemapRenderer>().enabled = true;
        }
    }
}
