using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTriggerScript : MonoBehaviour
{
    private bool player1InZone = false;
    private bool player2InZone = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        // �berpr�fen, welcher Spieler den Trigger betreten hat
        if (other.CompareTag("Player1"))
        {
            player1InZone = true;
        }
        else if (other.CompareTag("Player2"))
        {
            player2InZone = true;
        }

        // Starten der Coroutine, wenn beide Spieler im Collider sind
        if (player1InZone && player2InZone)
        {
            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // �berpr�fen, welcher Spieler den Trigger verlassen hat
        if (other.CompareTag("Player1"))
        {
            player1InZone = false;
        }
        else if (other.CompareTag("Player2"))
        {
            player2InZone = false;
        }

        // Abbrechen der Coroutine, falls einer der Spieler den Collider verl�sst
        if (!player1InZone || !player2InZone)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(3f); // Wartezeit von 3 Sekunden

        // Berechne den Index der n�chsten Szene
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // �berpr�fen, ob der Index g�ltig ist
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.LogWarning("Keine weitere Szene verf�gbar!");
        }
    }
}
