using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{

    public float TimeToDecrease;//Time until zone disappears
    public Vector3 ShrinkSpeed; //How much to shrink the scale every tick



    private bool CharacterInside = true;
    private float CurrentTime;



    // Start is called before the first frame update
    void Start()
    {
        CurrentTime = TimeToDecrease;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.other.tag == "PlayerCharacter")
            CharacterInside = true;
        
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.other.tag == "PlayerCharacter")
            CharacterInside = false; 
    }


    void ShrinkZone()
    {
        if (CharacterInside)
        {
            transform.localScale -= new Vector3(Time.deltaTime,0,Time.deltaTime);
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        ShrinkZone();
    }

    


    
}
