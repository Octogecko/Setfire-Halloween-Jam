using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractions : MonoBehaviour
{
    private GameObject targetObject;

    private List<GameObject> collidingNPCs = new List<GameObject>();

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started && targetObject)
        {
            targetObject.GetComponent<NPCInteraction>().Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            collidingNPCs.Add(collision.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        targetObject = FindClosestObject(collidingNPCs);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            collidingNPCs.Remove(collision.gameObject);
            targetObject = FindClosestObject(collidingNPCs);
            collision.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            collision.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    private GameObject FindClosestObject(List<GameObject> objects)
    { 
        if (objects.Count == 0) return null;

        GameObject closestObject = objects[0];
        float smallestDistance = (closestObject.transform.position - transform.position).magnitude;

        foreach (GameObject gObject in objects)
        {
            float currentDistance = (gObject.transform.position - transform.position).magnitude;
            if (currentDistance < smallestDistance)
            {
                closestObject = gObject;
                smallestDistance = currentDistance;
            }
        }

        if (targetObject && closestObject != targetObject)
        {
            targetObject.transform.GetChild(0).gameObject.SetActive(false);
            targetObject.transform.GetChild(1).gameObject.SetActive(false);
        }
        closestObject.transform.GetChild(0).gameObject.SetActive(true);
        closestObject.transform.GetChild(1).gameObject.SetActive(true);

        return closestObject;
    }
}
