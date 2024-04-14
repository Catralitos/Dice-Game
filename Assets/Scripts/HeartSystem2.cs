using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class HeartSystem2 : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;
    private bool dead;
    public Text diceResultText;

    private void Start()
    {
        life = hearts.Length;
    }

    void Update2()
    {
        if (dead == true)
        {
            Debug.Log("Player 2 died!");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void TakeDamage2 (int d)
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
    public void Defend2(int d)
    {
        life += d;

        // Check if the life index is within the bounds of the array
        if (life < hearts.Length)
        {
            // Check if the GameObject reference is not null and not destroyed
            if (hearts[life] != null && !hearts[life].gameObject.Equals(null))
            {
                hearts[life].SetActive(true); // Activate the GameObject corresponding to the new life
            }
        }

        // Update the "dead" flag if necessary
        if (life >= 1)
        {
            dead = false; // Player is not dead if life is greater than or equal to 1
        }
    }
    public void RollDice2()
    {
        int result = Random.Range(1, 7); // Generate a random number between 1 and 6
        int result2 = Random.Range(1, 7); // Generate a random number between 1 and 6

        // Show dice roll results in the Text component
        diceResultText.text = "Player 2's first dice rolled: " + result + "\n" +
                              "Player 2's second dice rolled: " + result2;

        // Start a coroutine to hide the text after 5 seconds
        StartCoroutine(HideTextAfterDelay(5f));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        diceResultText.text = ""; // Clear the text
    }

}