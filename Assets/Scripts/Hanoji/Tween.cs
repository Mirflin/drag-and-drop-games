using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tween {
    public static IEnumerator TweenTo(this float startValue, float endValue, float duration, System.Action<float> onUpdate) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);
            onUpdate.Invoke(currentValue);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        onUpdate.Invoke(endValue);
    }

    public static IEnumerator TweenTo(this Vector2 startValue, Vector2 endValue, float duration, System.Action<Vector2> onUpdate) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            Vector2 currentValue = Vector2.Lerp(startValue, endValue, t);
            onUpdate.Invoke(currentValue);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        onUpdate.Invoke(endValue);
    }

    public static IEnumerator TweenTo(this Vector3 startValue, Vector3 endValue, float duration, System.Action<Vector3> onUpdate) {
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            Vector3 currentValue = Vector3.Lerp(startValue, endValue, t);
            onUpdate.Invoke(currentValue);

            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        onUpdate.Invoke(endValue);
    }
}