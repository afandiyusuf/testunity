using UnityEngine;
using System.Collections;
using UnityEngine.UI;
[RequireComponent(typeof(CanvasGroup))]
public class SimpleUiAnimation : MonoBehaviour {
    
    public enum AnimationType 
    {
        FadeIn
    }
    public enum Direction
    {
        Bot_to_Top,
        Still
    }

    public AnimationType animationType = AnimationType.FadeIn;
    public Direction direction = Direction.Bot_to_Top;

    public float speed;
    public float delay;

    public bool isStartNow;
    private RectTransform thisRectTransform;
    public float tolerance = 10;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private CanvasGroup thisCanvasGroup;
    private float currentTime;

    void Start()
    {

        thisCanvasGroup = GetComponent<CanvasGroup>();
        thisRectTransform = GetComponent<RectTransform>();
        targetPosition = new Vector2(thisRectTransform.anchoredPosition.x,thisRectTransform.anchoredPosition.y);
        startPosition = new Vector2(thisRectTransform.anchoredPosition.x, thisRectTransform.anchoredPosition.y - tolerance);

        ApplyInitAnimation();
        StartCoroutine("StartAnimation");
        Invoke("DestroyThis", delay + speed +((delay + speed)/10));
    }
    void DestroyThis()
    {
        isStartNow = false;


        if (animationType == AnimationType.FadeIn)
            thisCanvasGroup.alpha = 1;

        if (direction == Direction.Bot_to_Top)
            thisRectTransform.anchoredPosition = targetPosition;

        Destroy(this);
    }
    void ApplyInitAnimation()
    {
        if (animationType == AnimationType.FadeIn)
        {
            thisCanvasGroup.alpha = 0;
        }

        if (direction == Direction.Bot_to_Top)
        {
            thisRectTransform.anchoredPosition = startPosition;
        }
    }

    IEnumerator StartAnimation()
    {
        yield return new WaitForSeconds(delay);
        isStartNow = true;
        yield return null;
    }

    void Update()
    {
        if (!isStartNow)
            return;

        currentTime += Time.deltaTime;
        if (currentTime > speed + delay)
            return;

        if (animationType == AnimationType.FadeIn)
        {
            if(thisCanvasGroup.alpha < 1.5f)
                thisCanvasGroup.alpha += Time.deltaTime/speed;
        }

        if (direction == Direction.Bot_to_Top)
        {
            if(thisRectTransform.anchoredPosition.y <= targetPosition.y)
                thisRectTransform.anchoredPosition = new Vector2(thisRectTransform.anchoredPosition.x, thisRectTransform.anchoredPosition.y + ((Time.deltaTime * tolerance)/speed));
        }
    }
    
}
