using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HeartSystem : MonoBehaviour
{
    public GameObject[] hearts;
    public int life;
    private bool dead;
    public Text diceResultText;
    public Animator characterAnimator;

    private void Start()
    {
        life = hearts.Length;
        RollDice();
        // Trigger the idle animation
        characterAnimator.SetTrigger("Idle");
    }

    void Update()
    {
        if (dead == true)
        {
            // Show dice roll results in the Text component
            diceResultText.text = "Player 1 died!";

            // Start a coroutine to hide the text after 5 seconds
            StartCoroutine(GameOverAfterDelay(5f));
        }
    }

    public void TakeDamage(int d)
    {
        if (life >= 1)
        {
            life -= d;

            Destroy(hearts[life].gameObject);
            if (life < 1)
            {
                dead = true;
            }
            
            StartCoroutine(PlayDamageAnimation());
        }
    }

    IEnumerator PlayDamageAnimation()
    {
        // Trigger the damage animation
        characterAnimator.SetTrigger("TakeDamage");

        // Wait for the duration of the damage animation
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Trigger the idle animation
        characterAnimator.SetTrigger("Idle");
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
        int result2 = Random.Range(1, 7); // Generate a random number between 1 and 6

        // Show dice roll results in the Text component
        diceResultText.text = "Player 1's first dice rolled: " + result + "\n" +
                              "Player 1's second dice rolled: " + result2;

        // Start a coroutine to hide the text after 5 seconds
        StartCoroutine(HideTextAfterDelay(5f));
    }

    IEnumerator HideTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        diceResultText.text = ""; // Clear the text
    }

    IEnumerator GameOverAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("GameOver");
    }
}
