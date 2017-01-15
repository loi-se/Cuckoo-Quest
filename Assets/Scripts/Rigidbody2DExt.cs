using UnityEngine;

public static class Rigidbody2DExt {

	public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force) {
		var explosionDir = rb.position - explosionPosition;
		var explosionDistance = explosionDir.magnitude;

		// Normalize without computing magnitude again
		if (upwardsModifier == 0)
			explosionDir /= explosionDistance;
		else {
			// From Rigidbody.AddExplosionForce doc:
			// If you pass a non-zero value for the upwardsModifier parameter, the direction
			// will be modified by subtracting that value from the Y component of the centre point.
			explosionDir.y += upwardsModifier;
			explosionDir.Normalize();
		}
			

		Debug.Log ("ExplosionForce: " + explosionForce.ToString());
		Debug.Log("explosionDistance: " + explosionDistance.ToString());
		Debug.Log ("explosionDir: " + explosionDir.ToString ());

		rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
		Debug.Log ("force added");
	}

	public static void CastAddExplosionForce(float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force) {
		Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, explosionRadius);
		Debug.Log ("number of colliders:" + colliders.Length.ToString());
		foreach (Collider2D hit in colliders) {
			//Debug.Log ("number of colliders:" + colliders.Length.ToString());
			var rb = hit.attachedRigidbody;
			if (rb != null)
				rb.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, mode);
		}

	}
}
