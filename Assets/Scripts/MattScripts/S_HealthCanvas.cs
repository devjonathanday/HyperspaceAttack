using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HealthCanvas : MonoBehaviour
{

    public GameObject[] HealthBars;
    public GameObject FailureScreen;

    private int CurrentHealthBar = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        
    }


    public void PlayerHasTakenDamage()
    {
        HealthBars[CurrentHealthBar].SetActive(false);
        CurrentHealthBar++;
        if(CurrentHealthBar == 3)
        {
            Debug.Log("Dead");
        }
    }

    public void PlayerDied()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FailureScreen.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
