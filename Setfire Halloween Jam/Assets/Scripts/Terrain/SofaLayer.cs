using UnityEngine;

public class SofaLayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player.transform.position.y >= -2.26)
        {
            spriteRenderer.sortingOrder = 25;
        }
        else
        {
            spriteRenderer.sortingOrder = 1;
        }
    }
}
