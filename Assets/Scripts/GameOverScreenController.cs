using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreenController : MonoBehaviour
{
    public Button restartButton;
    public TMP_Text text;


    void updateUI()
    {
        if (GameManager.Get().isWinning)
        {
            text.text = "GAGNÉ";
        }
        else
        {
            text.text = "PERDU";
        }
    }

    void Start()
    {
        restartButton.onClick.AddListener(
        () => {
            GameManager.Get().setGameState(GameState.Setup);
        });
    }

    void OnEnable()
    {
        updateUI();
    }
}
