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

    public float timer;
    public bool timerRunning;

    [SerializeField] PlayerController ghost_controller;
    [SerializeField] PlayerController overworld_controller;
    [SerializeField] Cinemachine.CinemachineVirtualCamera ghost_camera;
    [SerializeField] Cinemachine.CinemachineVirtualCamera overworld_camera;



    [SerializeField] AK.Wwise.Event overworld_switch;
    [SerializeField] AK.Wwise.Event spiritworld_switch;
    [SerializeField] AK.Wwise.Event overworldAmbience_play;
    [SerializeField] AK.Wwise.Event overworldAmbience_stop;
    [SerializeField] AK.Wwise.Event overworldMusic_play;
    [SerializeField] AK.Wwise.Event overworldMusic_pause;
    [SerializeField] AK.Wwise.Event overworldMusic_resume;
    [SerializeField] AK.Wwise.Event spiritWorldMusic_play;
    [SerializeField] AK.Wwise.Event spiritWorldMusic_pause;
    [SerializeField] AK.Wwise.Event spiritWorldMusic_resume;
    [SerializeField] AK.Wwise.Event stopAll;

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
        StartTimer(cursePeriod);
        overworldMusic_play.Post(gameObject);
        spiritWorldMusic_play.Post(gameObject);
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

            spiritWorldMusic_pause.Post(gameObject);
            overworldMusic_resume.Post(gameObject);

            overworld_controller.enabled = true;
            ghost_controller.enabled = false;
        }
        else
        {
            spiritworld_switch.Post(gameObject);
            overworldAmbience_stop.Post(gameObject);

            spiritWorldMusic_resume.Post(gameObject);
            overworldMusic_pause.Post(gameObject);

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
                    switchWorld();
                    StartTimer(cursePeriod);
                }
                else
                {
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
        //lol we won the game 
        HUD.instance.changeToMenu(HUD.MenuType.END);
    }

    public void Death() //Called by ResetOnTouch
    {
        stopAll.Post(gameObject);
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
    }

    public void NextScene()
    {
        if (currentLevelIndex < levelSequence.Length - 1) {
            SceneManager.LoadScene(levelSequence[currentLevelIndex + 1]);
        }
        else //FINISHES SEQUENCE
        {
            Debug.LogWarning("can't go to the next scene if there's no more!");
            HUD.instance.changeToMenu(HUD.MenuType.END);
        }
    }

    

}
