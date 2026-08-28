using UnityEngine;
using System.Collections;

public class GlitchEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    [Header("Glitch Timing")]
    public float minInterval = 1.5f;
    public float maxInterval = 4f;
    public float glitchDuration = 0.15f;

    [Header("Glitch Visuals")]
    public float positionJitterAmount = 0.08f;
    public Color[] glitchColors = new Color[] { Color.cyan, Color.magenta, new Color(1f, 1f, 1f, 0.4f) };

    private Vector3 originalLocalPosition;
    private Color originalColor;

    void Start()
    {
        if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        originalLocalPosition = transform.localPosition;
        originalColor = spriteRenderer.color;
        StartCoroutine(GlitchLoop());
    }

    IEnumerator GlitchLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(minInterval, maxInterval));
            yield return StartCoroutine(GlitchBurst());
        }
    }

    IEnumerator GlitchBurst()
    {
        float elapsed = 0f;
        while(elapsed < glitchDuration)
        {
            transform.localPosition = originalLocalPosition + (Vector3)(Random.insideUnitCircle * positionJitterAmount);
            spriteRenderer.color = glitchColors[Random.Range(0, glitchColors.Length)];
            spriteRenderer.enabled = Random.value > 0.15f; // ara sira tamamen kaybolsun

            elapsed += 0.03f;
            yield return new WaitForSeconds(0.03f);
        }

        transform.localPosition = originalLocalPosition;
        spriteRenderer.color = originalColor;
        spriteRenderer.enabled = true;
    }
}