using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Image timerImage;
    LevelManager levelManager;

    void Start(){
        levelManager = GameObject.FindObjectOfType<LevelManager>();
    }


    void Update() {
        timerImage.fillAmount = levelManager.timer /levelManager.cursePeriod;   
    }


}
