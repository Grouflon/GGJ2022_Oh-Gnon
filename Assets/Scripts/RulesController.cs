using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RulesController : MonoBehaviour
{
    public Button jouerButton;

    // Start is called before the first frame update
    void Start()
    {
        jouerButton.onClick.AddListener(
        () => {
            GameManager.Get().setGameState(GameState.Setup);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
