using UnityEngine;

public class RandomColorizer : MonoBehaviour
{
    [ContextMenu("RandomColor")]
    private void RandomColor()
    {
        var sr = GetComponent<SpriteRenderer>();
        sr.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}