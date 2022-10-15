using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update


    void Start()
    {
        player = GameObject.Find("Player");
    }


    //clocked curseclock
    IEnumerator CurseClock(float period){
        yield return new WaitForSeconds(period);
        switchWorld();
        CurseClock(period);
    }

    void switchWorld(){

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
