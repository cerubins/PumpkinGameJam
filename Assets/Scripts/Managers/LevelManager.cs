using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    public float cursePeriod;

    public float timer;
    public bool timerRunning;

    [SerializeField] GhostController ghost_controller;
    [SerializeField] OverworldController overworld_controller;

    //Level vars
    public bool isOverWorld;

    void Start()
    {
        ghost_controller = GameObject.FindGameObjectWithTag("Player_Ghost").GetComponent<GhostController>();
        overworld_controller = GameObject.FindGameObjectWithTag("Player_Overworld").GetComponent<OverworldController>();
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
            overworld_controller.enabled = true;
            ghost_controller.enabled = false;
        }
        else
        {
            overworld_controller.enabled = false;
            ghost_controller.enabled = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
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
    
}
