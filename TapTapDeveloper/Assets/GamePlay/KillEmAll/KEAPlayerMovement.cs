using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KEAPlayerMovement : MonoBehaviour
{
    //up is 0 numbers go clockwise
    int faceDirection;

    public GameObject Bullet;

    public GameObject shotPoint;

    private void Update()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        if (PlayerText.GetCharacterCount() > 0)
        {
            Movement();

            if (PlayerText.CheckForWord(" "))
            {
                Shoot();
                PlayerText.ClearScreen();
            }
        }

        if (Enemymanager.enemyCount <= 0)
        {
            PlayerManagerHandler.ResetAll();
            FindObjectOfType<PlayerManager>().LoadScene("ComputerScreen");
        }
            
    }

    void Shoot()
    {
        if (faceDirection == 0)
        {
            GameObject shot = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.identity);

            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, 5, 0);
        }
        if (faceDirection == 2)
        {
            GameObject shot = Instantiate(Bullet, new Vector3(transform.position.x, transform.position.y - 1.5f, transform.position.z), Quaternion.identity);

            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(0, -5, 0);
        }

        if (faceDirection == 1)
        {
            GameObject shot = Instantiate(Bullet, new Vector3(transform.position.x + 1.5f, transform.position.y , transform.position.z), Quaternion.identity);

            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(5, 0, 0);
        }
        if (faceDirection == 3)
        {
            GameObject shot = Instantiate(Bullet, new Vector3(transform.position.x - 1.5f, transform.position.y, transform.position.z), Quaternion.identity);

            shot.GetComponent<Rigidbody2D>().velocity = new Vector3(-5, 0, 0);
        }
    }

    void Movement()
    {
        if (PlayerText.CheckForWords("W", "I"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z);
            faceDirection = 0;
            PlayerText.ClearScreen();
            shotPoint.transform.position = new Vector2(transform.position.x + 0, transform.position.y + .5f);
        }
        if (PlayerText.CheckForWords("D", "L"))
        {
            transform.position = new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z);
            faceDirection = 1;
            PlayerText.ClearScreen();
            shotPoint.transform.position = new Vector2(transform.position.x + .5f, transform.position.y + 0);
        }
        if (PlayerText.CheckForWords("S", "K"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - .5f, transform.position.z);
            faceDirection = 2;
            PlayerText.ClearScreen();
            shotPoint.transform.position = new Vector2(transform.position.x + 0, transform.position.y + -.5f);
        }
        if (PlayerText.CheckForWords("A", "J"))
        {
            transform.position = new Vector3(transform.position.x - 1f, transform.position.y, transform.position.z);
            faceDirection = 3;
            PlayerText.ClearScreen();
            shotPoint.transform.position = new Vector2(transform.position.x + -.5f, transform.position.y + 0);
        }
    }
}
