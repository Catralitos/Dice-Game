using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{

    public Animator characterAnimator;

    private void Start()
    {
        // Trigger the diceRoll animation
        StartCoroutine(PlayDiceRoll());
    }


    IEnumerator PlayDiceRoll()
    {
        // Trigger the damage animation
        characterAnimator.SetTrigger("DiceRoll");

        // Wait for the duration of the damage animation
        yield return new WaitForSeconds(characterAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Trigger the idle animation
        characterAnimator.SetTrigger("DiceRoll");
    }

}
