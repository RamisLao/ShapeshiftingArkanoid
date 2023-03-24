using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShake : MonoBehaviour
{
	// How long the object should shake for.
	[SerializeField] private float shakeDuration = 0f;
	[SerializeField] private float decreaseFactor = 1.0f;

	private Vector3 originalPos;

	void Start()
	{
		originalPos = transform.localPosition;
	}

	public void Shake(float shakeAmount)
    {
		StartCoroutine(Shake(shakeDuration, shakeAmount, decreaseFactor));
    }

	private IEnumerator Shake(float shakeDuration_, float shakeAmount, float decreaseFactor_)
    {
		float shakeCounter = shakeDuration_;
		while (shakeCounter > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			shakeCounter -= Time.deltaTime * decreaseFactor_;
			yield return null;
		}

		transform.localPosition = originalPos;
	}
}
