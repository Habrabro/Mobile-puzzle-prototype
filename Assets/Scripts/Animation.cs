using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour
{
    GameObject gObject;
    Animation nextAnimation;
    Vector2 originPosition, targetPosition;
    float delay, deltaTime;

    IEnumerator smoothlyMove()
    {
        gObject.transform.position = originPosition;
        while (Mathf.Abs(gameObject.transform.position.sqrMagnitude - targetPosition.sqrMagnitude) > 0)
        {
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                targetPosition, Time.deltaTime * deltaTime);
            yield return new WaitForEndOfFrame();
        }
        nextAnimation.StartAnimation();
        StopAllCoroutines();
    }

    IEnumerator timer(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(smoothlyMove());
    }

    public void StartAnimation()
    {
        StartCoroutine(timer(delay));
    }

    public void initializeAnimation(GameObject gameObject, Vector2 originPosition, Vector2 targetPosition,
        float delay, float deltaTime, Animation nextAnimation)
    {
        this.gObject = gameObject;
        this.originPosition = originPosition;
        this.targetPosition = targetPosition;
        this.delay = delay;
        this.deltaTime = deltaTime;
        this.nextAnimation = nextAnimation;
    }
}
