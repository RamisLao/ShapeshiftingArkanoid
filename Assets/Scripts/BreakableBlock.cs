using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private float boingInSpeed;
    [SerializeField] private Color fadeOutStartingColor;

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }

    public void FadeOut()
    {
        StartCoroutine(FadeOut(0f, 5f));
    }

    public void BoingIn(Vector3 endScale)
    {
        StartCoroutine(BoingIn(endScale, boingInSpeed));
    }

    private IEnumerator BoingIn(Vector3 endScale, float boingInSpeed_)
    {
        Vector3 currentLocalScale = transform.localScale;

        for (float i = 0; i <= 1f; i += 0.1f * Time.deltaTime * boingInSpeed_)
        {
            float value = EasingFunction.BounceBezier3(i);
            transform.localScale = Vector3.Lerp(currentLocalScale, endScale, value);
            yield return null;
        }

        transform.localScale = Vector3.Lerp(currentLocalScale, endScale, 1);
    }

    private IEnumerator FadeOut(float endValue, float length)
    {
        Renderer renderer = GetComponent<Renderer>();
        float alpha = renderer.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / length)
        {
            if (t > 0.75f)
            {
                GetComponent<BoxCollider>().enabled = false;
            }

            Color newColor = fadeOutStartingColor;
            newColor.a = Mathf.Lerp(alpha, endValue, t);
            renderer.material.color = newColor;
            yield return null;
        }

        Destroy(gameObject);
    }
}
