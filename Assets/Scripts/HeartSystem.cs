using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;
    private bool dead;

    private void Start()
    {
        life = hearts.Length;
    }

    void Update()
    {
        if (dead == true)
        {
            Debug.Log("Player died!");
        }
    }

    public void TakeDamage (int d)
    {
        if (life >= 1)
        {
            life  -= d;
            Destroy(hearts[life].gameObject);
            if(life < 1)
            {
                dead = true;
            }
        }

    }

    public void Defend(int d)
    {
        life += d; // Increase life by the given amount

        // Check if the life index is within the bounds of the array
        if (life < hearts.Length)
        {
            // Check if the GameObject reference is not null and not destroyed
            if (hearts[life] != null && !hearts[life].gameObject.Equals(null))
            {
                hearts[life].SetActive(true); // Activate the GameObject corresponding to the new life
            }
        }


        if (life >= 1)
        {
            dead = false; // Player is not dead if life is greater than or equal to 1
        }
    }
        public void RollDice()
    {
        int result = Random.Range(1, 7); // Generate a random number between 1 and 6
        Debug.Log("Player's first dice rolled: " + result);
    }
        public void RollDice2()
    {
        int result = Random.Range(1, 7); // Generate a random number between 1 and 6
        Debug.Log("Player's second dice rolled: " + result);
    }

}