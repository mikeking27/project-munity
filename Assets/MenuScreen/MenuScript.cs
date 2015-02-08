using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{

    public void OnClickLevel1()
    {
        Application.LoadLevel("level_one");
    }

    public void OnClickExit()
    {
        Application.Quit();
    }
}