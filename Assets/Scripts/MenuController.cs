using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button jouerButton;
    public Button creditsButton;
    public Button quitterButton;

    public RectTransform title;
    public RectTransform titlePear;
    public RectTransform titleTomato;

    public float titlePeriod = 3.0f;
    public float titleAmplitude = 10.0f;
    public float titlePearPeriod = 3.0f;
    public float titlePearAmplitude = 10.0f;
    public float titleTomatoPeriod = 3.0f;
    public float titleTomatoAmplitude = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        jouerButton.onClick.AddListener(
        () => {
            GameManager.Get().setGameState(GameState.Setup);
        });

        creditsButton.onClick.AddListener(
        () => {

        });

        quitterButton.onClick.AddListener(
        () => {
            Application.Quit();
        });
    }

    // Update is called once per frame
    void Update()
    {
        {
            float titleRatio = Time.time / titlePeriod;
            float titleAngle = Mathf.Sin(titleRatio * Mathf.PI * 2f) * Mathf.Deg2Rad * titleAmplitude;
            title.localRotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * titleAngle);
        }
        
        {
            float pearRatio = Time.time / titlePearPeriod;
            float pearAngle = Mathf.Sin(pearRatio * Mathf.PI * 2f) * Mathf.Deg2Rad * titlePearAmplitude;
            titlePear.localRotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * pearAngle);
        }

        {
            float tomatoRatio = Time.time / titleTomatoPeriod;
            float tomatoAngle = Mathf.Sin(tomatoRatio * Mathf.PI * 2f) * Mathf.Deg2Rad * titleTomatoAmplitude;
            titleTomato.localRotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg * tomatoAngle);
        }
    }
}
