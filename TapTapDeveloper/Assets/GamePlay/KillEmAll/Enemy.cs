using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    public Sprite[] AliveAndDeadSprites;

    bool isDead = false;

    private void Start()
    {
        Enemymanager.enemyCount += 1;
    }

    private void Update()
    {
        GetComponent<SpriteRenderer>().sprite = (isDead)? AliveAndDeadSprites[1] : AliveAndDeadSprites[0];
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            isDead = true;
            Enemymanager.enemyCount -= 1;
        }
    }

}
