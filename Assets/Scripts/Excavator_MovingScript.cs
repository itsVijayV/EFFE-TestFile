using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Excavator_MovingScript : MonoBehaviour
{
    [Header("Player Movement")]
    public float horizontal;
    public float vertical;
    public float speed = 5f;
    public float turnSpeed = 100f;
    public bool isbreaking;
    public bool handDown = false;
    public static bool RockBreak = false;

    public enum excavatorAnimations
    {
        idle,
        excavatorHandDown,
        RockBreak
    };
    public excavatorAnimations curretAnimations = excavatorAnimations.idle;

    [Header("Component Reference")]
    public Rigidbody ExcavatorRb;
    public Animator ExcavatorAnim;

    [Header("Paritical Play")]
    public ParticleSystem rockBreakPartical;

    // Start is called before the first frame update
    void Start()
    {
        ExcavatorRb = GetComponent<Rigidbody>();
        ExcavatorAnim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isbreaking = Input.GetKey(KeyCode.Space);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (curretAnimations == excavatorAnimations.idle)
            {
                Debug.Log("Value is Changed");
                curretAnimations = excavatorAnimations.excavatorHandDown;
                handDown = true ;
            }
            else if (curretAnimations == excavatorAnimations.excavatorHandDown)
            {
                Debug.Log("Value is Changed");
                curretAnimations = excavatorAnimations.idle;
                handDown = false ;   
            }
        }

        if (handDown)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (curretAnimations == excavatorAnimations.excavatorHandDown)
                {
                    curretAnimations = excavatorAnimations.RockBreak;
                }
                else if (curretAnimations == excavatorAnimations.RockBreak)
                {
                    curretAnimations = excavatorAnimations.excavatorHandDown;
                }
            }
        }

        ExcavatorAnimation();

    }

    private void FixedUpdate()
    {

        if (isbreaking)
        {
            ExcavatorRb.velocity = Vector3.Lerp(ExcavatorRb.velocity, Vector3.zero, 10f * Time.fixedDeltaTime);
            ExcavatorRb.angularVelocity = Vector3.Lerp(ExcavatorRb.angularVelocity, Vector3.zero, 10f * Time.fixedDeltaTime);
        }
        else 
        {

            Vector3 move = transform.forward * vertical * speed;
            ExcavatorRb.velocity = new Vector3(move.x, ExcavatorRb.velocity.y, move.z);

            // Rotate using MoveRotation (around Y axis)
            Quaternion deltaRotation = Quaternion.Euler(0f, horizontal * turnSpeed * Time.fixedDeltaTime, 0f);
            ExcavatorRb.MoveRotation(ExcavatorRb.rotation * deltaRotation);

        }
    }

    public void ExcavatorAnimation()
    {
        switch (curretAnimations)
        {
            case excavatorAnimations.idle :
                Debug.Log("idel is Running");
                ExcavatorAnim.SetBool("Interact",false);
                rockBreakPartical.Stop();
                break;
            case excavatorAnimations.excavatorHandDown :
                Debug.Log("excavatorHandDown is Running");
                ExcavatorAnim.SetBool("RockBreak", false);
                ExcavatorAnim.SetBool("Interact", true);
                rockBreakPartical.Stop();
                break;
            case excavatorAnimations.RockBreak :
                Debug.Log("RockBreak Running");
                ExcavatorAnim.SetBool("Interact", false);
                ExcavatorAnim.SetBool("RockBreak",true);
                StartCoroutine(AnimationPartical());
                break;
            default :
                Debug.Log("Error in animation");
                break;
        }
    }

    public IEnumerator AnimationPartical()
    {
        yield return new WaitForSeconds(1);

        if (RockBreak && !rockBreakPartical.isPlaying)
        {
            rockBreakPartical.Play();
        }
    }
}
