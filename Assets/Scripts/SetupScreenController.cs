using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupScreenController : MonoBehaviour
{
    [Header("Internal")]
    public Button P1Button;
    public Button P2Button;
    public InputField SeedField;
    public Button StartButton;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateUI()
    {
        GameManager gm = GameManager.Get();
    }

}
