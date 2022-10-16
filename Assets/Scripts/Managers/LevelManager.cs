using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    GameObject player;
    // Start is called before the first frame update
    public float cursePeriod;

    public float timer=5;
    public bool timerRunning;

    [SerializeField] PlayerController ghost_controller;
    [SerializeField] PlayerController overworld_controller;
    [SerializeField] Cinemachine.CinemachineVirtualCamera ghost_camera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera overworld_camera;



    [SerializeField] AK.Wwise.Event overworld_switch;
    [SerializeField] AK.Wwise.Event spiritworld_switch;
    [SerializeField] AK.Wwise.Event overworldAmbience_play;
    [SerializeField] AK.Wwise.Event overworldAmbience_stop;

    //Level vars
    public bool isOverWorld=true;
    string[] levelSequence = { "Level 1", "Level 2", "Level 3" };
    int currentLevelIndex=2;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ghost_controller = GameObject.FindGameObjectWithTag("Player_Ghost").GetComponent<PlayerController>();
        overworld_controller = GameObject.FindGameObjectWithTag("Player_Overworld").GetComponent<PlayerController>();
        overworld_controller.gameObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().enabled = isOverWorld;
        ghost_controller.gameObject.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>().enabled = !isOverWorld;
        StartTimer(cursePeriod); //Delet later
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

            overworldAmbience_play.Post(gameObject);

            overworld_controller.enabled = true;
            ghost_controller.enabled = false;
        }
        else
        {
            spiritworld_switch.Post(gameObject);

            overworldAmbience_stop.Post(gameObject);

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
                StartTimer(cursePeriod);
            }
            else{
                if(timer - Time.deltaTime <= 0){
                    timer = 0;
                    switchWorld();
                    StartTimer(cursePeriod);
                }
                else{
                    timer -= Time.deltaTime;
                }
            }
        }

    }
    //call this from other functions
    void StartTimer(float period){
        timerRunning = true;
        timer = period;
    }

    //Game Flow Funcs
    public void WonRound() //Called by exit zone when player finishes a round
    {
        Debug.Log("we won bitches");
        //TEMPORARY, SOME SORT OF UI/TRANSITION?
        NextScene();
    }

    public void Death() //Called by ResetOnTouch
    {
        Debug.Log("we died bitches");
        HUD.instance.changeToMenu(HUD.MenuType.GAME_OVER);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        currentLevelIndex = 0;
        SceneManager.LoadScene(levelSequence[currentLevelIndex]);
        StartTimer(10f);
    }

    public void NextScene()
    {
        if (currentLevelIndex < levelSequence.Length - 1) {
            SceneManager.LoadScene(levelSequence[currentLevelIndex + 1]);
            StartTimer(10f);
        }
        else //FINISHES SEQUENCE
        {
            Debug.LogWarning("can't go to the next scene if there's no more!");
            HUD.instance.changeToMenu(HUD.MenuType.END);
        }
        timerRunning = true;
    }

    

}
