using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    int width, height;

    void Move(FieldPosition originPosition, FieldPosition targetPosition)
    {
        int addend = 10;
        Vector2 direction = (Vector2)originPosition - targetPosition;
        StopAllCoroutines();
        if (targetPosition.jump)
        {
            StartCoroutine(moveSmoothly(transform.position, targetPosition + direction * addend, 5));
            StartCoroutine(timer(0.2f, (bool countdownIsOver) => { if (countdownIsOver)
            {
                StopAllCoroutines();
                StartCoroutine(moveSmoothly(targetPosition - direction * addend, targetPosition, 20));
            }}));
        }
        else
        {
            StartCoroutine(moveSmoothly(transform.position, targetPosition, 20));
        }
    }

    IEnumerator moveSmoothly(Vector2 originPosition, Vector2 targetPosition, float deltaTime)
    {
        transform.position = originPosition;
        while (Mathf.Abs(gameObject.transform.position.sqrMagnitude - targetPosition.sqrMagnitude) > 0)
        {
            gameObject.transform.position = Vector2.Lerp(gameObject.transform.position,
                targetPosition, Time.deltaTime * deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator timer(float time, System.Action<bool> callBack)
    {
        callBack(false);
        yield return new WaitForSeconds(time);
        callBack(true);
    }

    // Use this for initialization
    void Start () {
        gameObject.GetComponent<IMovable>().OnPositionChange += Move;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
