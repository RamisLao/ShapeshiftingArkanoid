using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EasingFunction
{
	public static float Linear(float t)
	{
		return t;
	}

	public static float Flip(float t)
	{
		return 1 - t;
	}

	public static float Scale(float scale, float t)
	{
		return scale * t;
	}

	public static float Mix(float t1, float t2, float weight)
	{
		return (1 - weight) * t1 + weight * t2;
	}

	public static float BounceClampBottom(float t)
	{
		return Mathf.Abs(t);
	}

	public static float BounceClampTop(float t)
	{
		return Flip(Mathf.Abs(Flip(t)));
	}

	public static float BounceClampBottomTop(float t)
	{
		return BounceClampBottom(BounceClampTop(t));
	}

	// Normalized cubic (3rd) Bezier A, B, C, D where A start, D end, are 0 and 1 respectively
	public static float Bezier3(float b, float c, float t)
	{
		float s = 1 - t;
		float t2 = t * t;
		float s2 = s * s;
		float t3 = t * t2;
		return (3.0f * b * s2 * t) + (3.0f * c * s * t2) + t3;
	}

	public static float SmoothStep2(float t)
	{
		return Mix(SmoothStart2(t), SmoothStop2(t), t);
	}

	public static float Arch2(float t)
	{
		return Scale(4, Scale(t, Flip(t)));
	}

	public static float SmoothStart26(float t)
	{
		return Mix(SmoothStart2(t), SmoothStart3(t), 0.6f); // x^2.6
	}

	public static float SmoothStart2(float t)
	{
		return t * t;
	}

	public static float SmoothStart3(float t)
	{
		return t * t * t;
	}

	public static float SmoothStart4(float t)
	{
		return t * t * t * t;
	}

	public static float SmoothStop2(float t)
	{
		return Flip(SmoothStart2(Flip(t))); // 1 - (1-t)^2
	}

	public static float SmoothStop3(float t)
	{
		return Flip(SmoothStart3(Flip(t)));
	}

	public static float SmoothStop4(float t)
	{
		return Flip(SmoothStart4(Flip(t)));
	}

	public static float Bezier3a(float t)
	{
		return Bezier3(2, -1, t);
	}

	public static float BounceBezier3(float t)
	{
		return BounceClampTop(Bezier3(2.6f, 0.5f, t));
	}
}
