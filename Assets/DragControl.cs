using UnityEngine;
using UnityEngine.EventSystems;

public class DragControl : MonoBehaviour, IDragHandler {

	public Transform target;

	public void OnDrag (PointerEventData eventData) {

		Vector2 delta = eventData.delta;
		target.Rotate(new Vector3(delta.y, -delta.x, 0),Space.World);
	}
}
