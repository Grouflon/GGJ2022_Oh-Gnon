using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreenController : MonoBehaviour
{
    public Button restartButton;
    public TMP_Text text;

    public void updateScreen()
    {
        if (GameManager.Get().isWinning)
        {
            text.text = "YOU WIN";
        }
        else
        {
            text.text = "YOU LOSE";
        }
    }

    void Start()
    {
        restartButton.onClick.AddListener(
        () => {
            GameManager.Get().setGameState(GameState.Setup);
        });
    }
}
