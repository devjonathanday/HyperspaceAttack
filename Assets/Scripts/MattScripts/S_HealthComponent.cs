﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_HealthComponent : MonoBehaviour
{
    public int MaximumHealth;

    public Canvas MyCanvas;
    public GameManager MyGameManager;
    private int CurrentHealth;

    private S_HealthCanvas MyCanvasHealth;

    void Start()
    {
        CurrentHealth = MaximumHealth;
        MyCanvasHealth = MyCanvas.GetComponent<S_HealthCanvas>();
    }

    public void TakeDamage(int DamageToTake)
    {
        CurrentHealth = CurrentHealth - DamageToTake;
        MyCanvasHealth.PlayerHasTakenDamage();
        if (CurrentHealth <= 0)
            Die();
    }

    void Die()
    {
        MyCanvasHealth.PlayerDied();
        //MyGameManager.colorGrading.saturation.value = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(1);
    }
}
