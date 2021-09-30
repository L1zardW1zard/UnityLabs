using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sign : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int levelToLoad;
    [SerializeField] private Image LoadingScreen;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.gameObject.GetComponent<Player>();

        if (player != null && player.isScrollFound)
        {
            animator.SetTrigger("Fade");
            Invoke(nameof(LoadNextLevel),2f);
        }
    }

    private void ShowLoadingScreen()
    {
        LoadingScreen.enabled = true;
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene("Level"+levelToLoad);
    }
}
