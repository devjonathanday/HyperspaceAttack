using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class S_HealthCanvas : MonoBehaviour
{

    public GameObject[] HealthBars;
    public GameObject FailureScreen;

    private int CurrentHealthBar = 0;

    public EventSystem eventSystem;
    public GameObject restartButton;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        
    }


    public void PlayerHasTakenDamage()
    {
        if (CurrentHealthBar < 3)
        {
            HealthBars[CurrentHealthBar].SetActive(false);
            CurrentHealthBar++;
        }
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FailureScreen.SetActive(true);
        eventSystem.SetSelectedGameObject(restartButton);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
