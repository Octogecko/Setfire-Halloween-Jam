using UnityEngine;

public class WallLayer : MonoBehaviour
{
    private GameObject player;
    private SpriteRenderer _sr;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.y - transform.localScale.y / 2 > player.transform.position.y - 0.5)
        {
            _sr.sortingOrder = 14;
        }
        else
        {
            _sr.sortingOrder = 16;
        }
    }
}
