using UnityEngine;

public class CamMouseLook: MonoBehaviour
{
	[SerializeField]
	private float sensitivity = 5.0f;
	[SerializeField]
	public float smoothing = 2.0f;

	private Vector2 mouseLook;
	private Vector2 smoothV;
	private GameObject character;

	private void Start()
	{
		character = transform.parent.gameObject;
	}

	private void Update()
	{
		Vector2 md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV;
		mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
		character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
	}
}
