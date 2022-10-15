using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
<<<<<<< HEAD
    public float cursePeriod;

    public float timer;
    public bool timerRunning;

=======
>>>>>>> level-manager


    void Start()
    {
        player = GameObject.Find("Player");
    }


<<<<<<< HEAD
    //clocked curseclock, if no need to display things
/*     IEnumerator CurseClock(float period){
        yield return new WaitForSeconds(period);
        switchWorld();
        CurseClock(period);
    } */
=======
    //clocked curseclock
    IEnumerator CurseClock(float period){
        yield return new WaitForSeconds(period);
        switchWorld();
        CurseClock(period);
    }
>>>>>>> level-manager

    void switchWorld(){

    }
    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
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
    
=======
        
    }
>>>>>>> level-manager
}
