using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasTransitionManager : MonoBehaviour
{
    public CanvasGroup[] canvasPanels; // Assign your panels (set each with CanvasGroup)
    public float transitionDuration = 0.5f;

    private int currentIndex = 0;
    private bool isTransitioning = false;

    void Start()
    {
        // Initialize all panels: show only the first one
        for (int i = 0; i < canvasPanels.Length; i++)
        {
            canvasPanels[i].alpha = (i == currentIndex) ? 1 : 0;
            canvasPanels[i].interactable = (i == currentIndex);
            canvasPanels[i].blocksRaycasts = (i == currentIndex);
        }
    }

    public void Next()
    {
        if (!isTransitioning && currentIndex < canvasPanels.Length - 1)
        {
            StartCoroutine(TransitionTo(currentIndex + 1));
        }
    }

    public void Previous()
    {
        if (!isTransitioning && currentIndex > 0)
        {
            StartCoroutine(TransitionTo(currentIndex - 1));
        }
    }

    private IEnumerator TransitionTo(int newIndex)
    {
        isTransitioning = true;

        CanvasGroup current = canvasPanels[currentIndex];
        CanvasGroup next = canvasPanels[newIndex];

        // Fade out current
        float time = 0;
        while (time < transitionDuration)
        {
            current.alpha = Mathf.Lerp(1, 0, time / transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }
        current.alpha = 0;
        current.interactable = false;
        current.blocksRaycasts = false;

        // Fade in new
        time = 0;
        while (time < transitionDuration)
        {
            next.alpha = Mathf.Lerp(0, 1, time / transitionDuration);
            time += Time.deltaTime;
            yield return null;
        }
        next.alpha = 1;
        next.interactable = true;
        next.blocksRaycasts = true;

        currentIndex = newIndex;
        isTransitioning = false;
    }
}
