using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HealthComponent : MonoBehaviour
{

    public int MaximumHealth;


    public Canvas MyCanvas;
    private int CurrentHealth;


    private S_HealthCanvas MyCanvasHealth;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaximumHealth;
        MyCanvasHealth = MyCanvas.GetComponent<S_HealthCanvas>();
    }



    void TakeDamage(int DamageToTake)
    {

        CurrentHealth = CurrentHealth - DamageToTake;
        MyCanvasHealth.PlayerHasTakenDamage();
        if (CurrentHealth <= 0)
            Die();

    }

    void Die()
    {
        MyCanvasHealth.PlayerDied();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDamage(1);

        }
            
    }

}
