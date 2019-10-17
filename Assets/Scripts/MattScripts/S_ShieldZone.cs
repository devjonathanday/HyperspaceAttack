using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{

    private bool CharacterInside;

    // Start is called before the first frame update
    void Start()
    {

    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
            CharacterInside = true;
        
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
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
