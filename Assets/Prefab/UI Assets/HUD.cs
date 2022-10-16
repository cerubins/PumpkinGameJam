using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HUD : MonoBehaviour
{
    public static HUD instance;
    public enum MenuType { GAME_OVER, MAIN_MENU, END, NONE };
    public MenuType startingMenuType = MenuType.NONE;

    void Awake()
    {
        instance = this;
        changeToMenu(startingMenuType);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changeToMenu(MenuType menuType)
    {
        //Turn off all UI panels

        /*
        foreach (Transform childTransform in transform)
        {
            childTransform.gameObject.active = false;
        }*/

        transform.Find("Main Menu").gameObject.active = false;
        transform.Find("Game Over").gameObject.active = false;
        transform.Find("End").gameObject.active = false;


        //Then turn on only the UI panel we want to switch to
        switch (menuType)
        {
            case MenuType.MAIN_MENU:
                transform.Find("Main Menu").gameObject.active = true;
                break;
            case MenuType.GAME_OVER:
                transform.Find("Game Over").gameObject.active = true;
                break;
            case MenuType.END:
                transform.Find("End").gameObject.active = true;
                break;
            default:
                // code block
                break;
        }

    }

    public void ResetScene()
    {
        LevelManager.instance.ResetScene();
        changeToMenu(MenuType.NONE);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Main Menu");

    }

}
