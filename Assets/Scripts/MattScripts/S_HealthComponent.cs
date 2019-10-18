using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_HealthComponent : MonoBehaviour
{
    public int MaximumHealth;

    public Canvas MyCanvas;
    public GameManager MyGameManager;
    private int CurrentHealth;
    public RawImage healthFlash;
    private Color currentFlashColor;
    public Color flashColor;
    public float lerp;

    private S_HealthCanvas MyCanvasHealth;

    void Start()
    {
        CurrentHealth = MaximumHealth;
        MyCanvasHealth = MyCanvas.GetComponent<S_HealthCanvas>();
    }

    public void TakeDamage(int DamageToTake)
    {
        currentFlashColor = flashColor;
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
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(1);

        currentFlashColor = Color.Lerp(currentFlashColor, Color.clear, lerp);
        healthFlash.color = currentFlashColor;
    }
}
