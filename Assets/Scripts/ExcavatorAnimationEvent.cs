using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExcavatorAnimationEvent : MonoBehaviour
{
    [Header("Rock Destroy and spawn")]
    public Transform spawnPoint;
    public GameObject[] rockPices;
    public GameObject destroyGameobject;
    public int rockPicesCount = 0;

    [Header("RockCount Convert To UI")]
    public RockCountUI rockCountUI;

    private void Start()
    {
        rockCountUI = GameObject.Find("ScoreUICounter").GetComponent<RockCountUI>();
    }
    public void GetDestroyGameObject(GameObject destroyObject)
    {
        destroyGameobject = destroyObject;
    }
    public void RockSpawnEventAndDestroy()
    {
        Instantiate(rockPices[0], spawnPoint.transform.position, Quaternion.identity);
        Instantiate(rockPices[1], spawnPoint.transform.position, Quaternion.identity);
        Instantiate(rockPices[2], spawnPoint.transform.position, Quaternion.identity);
        Excavator_MovingScript.RockBreak = false;
        Destroy(destroyGameobject, 1);

        foreach (GameObject gameObject in rockPices)
        {
            rockPicesCount++;
        }

        rockCountUI.ScoreDisplay(rockPicesCount);

    }
}
