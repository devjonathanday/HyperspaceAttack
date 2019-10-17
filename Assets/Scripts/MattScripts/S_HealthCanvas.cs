using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HealthCanvas : MonoBehaviour
{

    public GameObject[] HealthBars;

    private int CurrentHealthBar = 0;

    // Start is called before the first frame update
    void Start()
    {
       
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
