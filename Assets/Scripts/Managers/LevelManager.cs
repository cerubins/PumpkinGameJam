using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    GameObject player;
    // Start is called before the first frame update
    public float cursePeriod;

    public float timer;
    public bool timerRunning;

    [SerializeField] PlayerController ghost_controller;
    [SerializeField] PlayerController overworld_controller;
    [SerializeField] Cinemachine.CinemachineVirtualCamera ghost_camera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera overworld_camera;
    [SerializeField] AK.Wwise.Event overworld_switch;
    [SerializeField] AK.Wwise.Event spiritworld_switch;

    //Level vars
    public bool isOverWorld;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        ghost_controller = GameObject.FindGameObjectWithTag("Player_Ghost").GetComponent<PlayerController>();
        overworld_controller = GameObject.FindGameObjectWithTag("Player_Overworld").GetComponent<PlayerController>();
    }


    //clocked curseclock, if no need to display things
/*     IEnumerator CurseClock(float period){
        yield return new WaitForSeconds(period);
        switchWorld();
        CurseClock(period);
    } */

    void switchWorld(){
        isOverWorld = !isOverWorld;
        if (isOverWorld)
        {
            overworld_switch.Post(gameObject);

            overworld_controller.enabled = true;
            ghost_controller.enabled = false;
        }
        else
        {
            spiritworld_switch.Post(gameObject);

            overworld_controller.enabled = false;
            ghost_controller.enabled = true;
        }
        overworld_controller.gameObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().enabled = isOverWorld;
        ghost_controller.gameObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().enabled = !isOverWorld;

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switchWorld();
        }

        UpdateTimer();

    }

    void UpdateTimer(){
        if(timerRunning){

            if(timer == 0){
                switchWorld();
                timerRunning = false; 
            }
            else{
                if(timer - Time.deltaTime <= 0){
                    timer = 0;
                    timerRunning = false;
                }
                else{
                    timer -= Time.deltaTime;
                }
            }
        }

    }
    //call this from other functions
    void ResetTimer(float period){
        timerRunning = true;
        timer = period;
    }

    //Game Flow Funcs
    public void WonRound() //Called by exit zone when player finishes a round
    {
        Debug.Log("we won bitches");
    }

    public void Death() //Called by ResetOnTouch
    {
        Debug.Log("we died bitches");
    }

}
