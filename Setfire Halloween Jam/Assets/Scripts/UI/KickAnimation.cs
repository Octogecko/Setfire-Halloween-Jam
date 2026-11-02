using UnityEngine;
using UnityEngine.InputSystem;

public class KickAnimation : MonoBehaviour
{
    public bool finishedKick;

    private void Update()
    {
        GetComponent<Animator>().Update(Time.unscaledDeltaTime);  
        if (finishedKick)
        {
            StopKick();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StopKick();
        }
    }

    public void StopKick()
    {
        transform.parent.gameObject.SetActive(false);
        Time.timeScale = 1f;
        finishedKick = false;
    }
}
