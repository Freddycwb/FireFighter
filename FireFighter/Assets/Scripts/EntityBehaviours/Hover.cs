using UnityEngine;

public class Hover : MonoBehaviour
{
	[SerializeField] private Rigidbody rb;
	[SerializeField] private Vector3 raySourceOffset;
	[SerializeField] private Vector3 direction = Vector3.down;

	[SerializeField] private float hoverHeight;
	[SerializeField] private float overshootDistance;
	[SerializeField] private float regroundHeight;
	
	[SerializeField] private LayerMask layerMask;
	[SerializeField] private QueryTriggerInteraction shouldHitTrigger;

	private bool grounded = true;

	[SerializeField] private bool updateDebug;
	[SerializeField] private LineRenderer debugGroundCheck;
	[SerializeField] private LineRenderer debugMainRay;
	[SerializeField] private LineRenderer debugOvershoot;

	public bool GetGrounded()
	{
		return grounded;
	}

	private void Update() {
		// - debug -
		if (!updateDebug) return;

		debugGroundCheck.enabled = !grounded;
		debugMainRay.enabled = grounded;
		debugOvershoot.enabled = grounded;

		if (!grounded) {
			debugGroundCheck.SetPosition(0, transform.position + raySourceOffset);
			debugGroundCheck.SetPosition(1, transform.position + raySourceOffset + direction * regroundHeight);
			debugMainRay.SetPosition(0, transform.position + raySourceOffset + direction * regroundHeight);
		} else {
			debugMainRay.SetPosition(0, transform.position + raySourceOffset);
		}

		debugMainRay.SetPosition(1, transform.position + raySourceOffset + direction * hoverHeight);
		debugOvershoot.SetPosition(0, transform.position + raySourceOffset + direction * hoverHeight);
		debugOvershoot.SetPosition(1, transform.position + raySourceOffset + direction * (hoverHeight + overshootDistance));
		// ---
	}

	private void FixedUpdate() {
		if (!grounded) {
			if (!(grounded = Physics.Raycast(transform.position + raySourceOffset, direction, regroundHeight, layerMask, shouldHitTrigger))) return;
		}

		RaycastHit hit;
		if (!(grounded = Physics.Raycast(transform.position + raySourceOffset, direction, out hit, hoverHeight + overshootDistance, layerMask, shouldHitTrigger))) return;

		// zeroing out velocity along ray by subtracting the velocity projected to direction
		Vector3 proj = Vector3.Dot(rb.linearVelocity, direction) * direction;
		rb.linearVelocity -= proj;

		rb.position += direction * (hit.distance - hoverHeight);
	}

	public void DeattachFromGround() {
		grounded = false;
	}
}
