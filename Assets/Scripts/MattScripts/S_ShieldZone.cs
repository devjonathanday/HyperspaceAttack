using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ShieldZone : MonoBehaviour
{
    private bool CharacterInside;
    public float shrinkSpeed;

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
            transform.localScale -= Vector3.one * Time.deltaTime * shrinkSpeed;
            if (transform.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    void Update()
    {
        ShrinkZone();
    }
}
