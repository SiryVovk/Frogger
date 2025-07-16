using System.Collections;
using UnityEngine;

public class Dive : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D[] allChildColliders;
    [SerializeField] private SpriteRenderer[] allChildSprites;

    [Header("Dive Timing")]
    [SerializeField] private float diveCheckInterval = 4f;
    [SerializeField] private float diveChance = 0.2f;

    [SerializeField] private float partStepDelay = 0.5f;
    [SerializeField] private float underwaterDuration = 2f;

    private Coroutine diveRoutine;
    private Coroutine diveCheckRoutine;

    private void OnEnable()
    {
        SetAlpha(1f);
        SetCollidersActive(true);
        diveCheckRoutine = StartCoroutine(TryDiveCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator TryDiveCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(diveCheckInterval);

            if (Random.value < diveChance)
            {
                if (diveRoutine == null)
                {
                    diveRoutine = StartCoroutine(DiveSequence());
                }
            }
        }
    }

    private IEnumerator DiveSequence()
    {
        for (int i = 0; i < allChildColliders.Length; i++)
        {
            if (allChildColliders[i] != null)
                allChildColliders[i].enabled = false;

            if (allChildSprites[i] != null)
                yield return StartCoroutine(FadeSprite(allChildSprites[i], 1f, 0f, partStepDelay));

            yield return new WaitForSeconds(partStepDelay);
        }

        yield return new WaitForSeconds(underwaterDuration);

        // Поступове винирювання
        for (int i = 0; i < allChildColliders.Length; i++)
        {
            if (allChildSprites[i] != null)
                yield return StartCoroutine(FadeSprite(allChildSprites[i], 0f, 1f, partStepDelay));

            if (allChildColliders[i] != null)
                allChildColliders[i].enabled = true;

            yield return new WaitForSeconds(partStepDelay);
        }

        diveRoutine = null;
    }

    private IEnumerator FadeSprite(SpriteRenderer sr, float fromAlpha, float toAlpha, float duration)
    {
        float time = 0f;
        Color color = sr.color;

        while (time < duration)
        {
            float t = time / duration;
            float alpha = Mathf.Lerp(fromAlpha, toAlpha, t);
            sr.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        sr.color = new Color(color.r, color.g, color.b, toAlpha);
    }

    private void SetAlpha(float alpha)
    {
        foreach (var sr in allChildSprites)
        {
            if (sr != null)
            {
                Color c = sr.color;
                sr.color = new Color(c.r, c.g, c.b, alpha);
            }
        }
    }

    private void SetCollidersActive(bool active)
    {
        foreach (var col in allChildColliders)
        {
            if (col != null)
                col.enabled = active;
        }
    }
}
