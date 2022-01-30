using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class SetupScreenController : MonoBehaviour
{
    public Color normalColor = Color.white;
    public Color selectedColor = Color.red;

    [Header("Internal")]
    public Button p1Button;
    public Button p2Button;
    public TMP_InputField seedField;
    public Button startButton;
    public TMP_Text messageText;
    public Button creditsButton;

    // Start is called before the first frame update
    void Start()
    {
        p1Button.onClick.AddListener(
            () =>
            {
                GameManager.Get().localPlayer = 0;
                updateUI();
            });

        p2Button.onClick.AddListener(
            () =>
            {
                GameManager.Get().localPlayer = 1;
                updateUI();
            });

        seedField.onValueChanged.AddListener(
            (string _text) =>
            {
                updateUI();
            });

        startButton.onClick.AddListener(
            () =>
            {
                GameManager.Get().setGameState(GameState.Game);
            });

        updateUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        seedField.text = "";
        updateUI();
    }

    void updateUI()
    {
        GameManager gm = GameManager.Get();

        messageText.text = "";

        // Player buttons
        {
            ColorBlock p1Colors = p1Button.colors;
            ColorBlock p2Colors = p2Button.colors;

            p1Colors.normalColor = normalColor;
            p1Colors.highlightedColor = normalColor;
            p1Colors.selectedColor = normalColor;
            p2Colors.normalColor = normalColor;
            p2Colors.highlightedColor = normalColor;
            p2Colors.selectedColor = normalColor;

            if (gm.localPlayer == 0)
            {
                p1Colors.normalColor = selectedColor;
                p1Colors.highlightedColor = selectedColor;
                p1Colors.selectedColor = selectedColor;
            }
            else if (gm.localPlayer == 1)
            {
                p2Colors.normalColor = selectedColor;
                p2Colors.highlightedColor = selectedColor;
                p2Colors.selectedColor = selectedColor;
            }

            p1Button.colors = p1Colors;
            p2Button.colors = p2Colors;
        }

        // Seed
        bool isSeedValid = false;
        {
            if (seedField.text.Length > 0)
            {
                int seed;
                if (int.TryParse(seedField.text, out seed))
                {
                    gm.seed = seed;
                    isSeedValid = true;
                }
                else
                {
                    messageText.text = "Vous devez rentrer un nombre (max 10 chiffres)";
                }
            }
        }

        // Start Button
        startButton.interactable = (gm.localPlayer == 0 || gm.localPlayer == 1) && isSeedValid;
    }
}