using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBreakCollider : MonoBehaviour
{
    [Header("Rock Destroy and spawn")]
    public Transform spawnPoint;
    public GameObject[] rockPices;

    public GameObject animcomponet;
    public ExcavatorAnimationEvent excavatorAnimationEvent;

    private void Start()
    {
        excavatorAnimationEvent = animcomponet.GetComponent<ExcavatorAnimationEvent>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            Debug.Log("Collided With Rock");
            excavatorAnimationEvent.GetDestroyGameObject(other.gameObject);
            // Take Excavator Reffrence Directly
            Excavator_MovingScript.RockBreak = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            Excavator_MovingScript.RockBreak = false;
        }
    }
}
