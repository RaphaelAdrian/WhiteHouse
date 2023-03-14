using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform playerPos;
    public Transform[] path;

    Animator anim;


    [HideInInspector]
    public int reversePath = 0;
    [HideInInspector]
    public int pathNum = 0;

    public float accuracy = 1f;
    float speed = 2f;
    public bool isDetected;
    [Header("TIMER")]
    public float timerIdle = 2f;
    public float timeChase = 2f;
    public Light detectLight;



    void Start()
    {
        detectLight.enabled = false;
        anim = GetComponent<Animator>();

    }


    private void OnTriggerStay(Collider collider)
    {
            Debug.Log("Collided");
            if (collider.tag == "Player")
            {
                isDetected = true;
            }
    }

    private void OnTriggerExit(Collider other)
    {

        isDetected = false;

    }
    // Update is called once per frame
    void Update()
    {
        Timer();

        if (!isDetected)
        {
            if (Vector3.Distance(transform.position, playerPos.position) < 3f)
            {
                isDetected = true;
            }
            else
            {
                isDetected = false;
                detectLight.enabled = false;
            }
        }

        if (isDetected)
        {
            detectLight.enabled = true;
            OnChase();
        }
        else
        {
            if (timerIdle <= 0)

                OnPath();
        }

/*        while(GameManager.instance.isDialog)
        {
            this.transform.Translate(0, 0, 0);
        }
        while(!GameManager.instance.isDialog)
        
        {
            if (!isDetected)
            {
                OnPath();
            }
            
        }*/
    }

    void OnChase()
    {
        Vector3 lookAtGoal = new Vector3(playerPos.position.x, playerPos.transform.position.y, playerPos.position.z);
        this.transform.LookAt(lookAtGoal);

        if (Vector3.Distance(transform.position, playerPos.position) > 1f)
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
            SettingMovement();
        }

    }


    void OnPath()
    {
        Vector3 lookAtGoal = new Vector3(path[pathNum].position.x, path[pathNum].position.y, path[pathNum].position.z);
        this.transform.LookAt(lookAtGoal);
        if (Vector3.Distance(transform.position, path[pathNum].position) > 1f) //While the Distance of the Player and the AI is greater than 1
        {
            this.transform.Translate(0, 0, speed * Time.deltaTime);
            SettingMovement();
        }
        else
        {
/*            anim.SetFloat("Velocity", 0, 0f, Time.deltaTime);
            anim.SetFloat("SlopeNormal", 0, 0f, Time.deltaTime);*/
            Timer();
            if (timerIdle <= 0)
            {


                if (reversePath == 0)
                {
                    pathNum++;
                    timerIdle = 2;
                }
                if (pathNum == path.Length || reversePath != 0)
                {

                    pathNum--;
                    timerIdle = 2;
                    reversePath++;

                }
                if (reversePath == path.Length)
                {
                    reversePath = 0;
                }

            }

        }
        


    }

    void Timer()
    {
        timerIdle -= (1f * Time.deltaTime);


    }
    void SettingMovement()
    {

/*        anim.SetFloat("Velocity", 1, 0.2f, Time.deltaTime);
        anim.SetFloat("SlopeNormal", 1, 0.2f, Time.deltaTime);
*/
    }
}
