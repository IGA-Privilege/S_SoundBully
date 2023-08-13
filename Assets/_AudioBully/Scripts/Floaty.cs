using UnityEngine;
using UnityEditor;

public class Floaty : MonoBehaviour
{
	public enum MoveAxis { XAxis, YAxis, ZAxis }
	public MoveAxis moveAxis;
	public float duration = 1f;
	public float height = 1f;
	public AnimationCurve moveCurve;
	public bool reversed = false;

	private float m_Time;
	private float m_OriginalPosition;

	private void Start()
	{
		switch (moveAxis)
		{
			case MoveAxis.XAxis:
				m_OriginalPosition = this.transform.position.x;
				break;
			case MoveAxis.YAxis:
				m_OriginalPosition = this.transform.position.y;
				break;
			case MoveAxis.ZAxis:
				m_OriginalPosition = this.transform.position.z;
				break;
		}

		m_Time = 0f;
	}

	private void Update()
	{
		m_Time += Time.deltaTime;
		Vector3 position = this.transform.position;
		float sign = reversed ? -1f : 1f;
        switch (moveAxis)
        {
            case MoveAxis.XAxis:
				position.x = m_OriginalPosition + Mathf.Lerp(0f, sign * height, moveCurve.Evaluate(Mathf.PingPong(m_Time, duration) / duration));
				break;
            case MoveAxis.YAxis:
				position.y = m_OriginalPosition + Mathf.Lerp(0f, sign * height, moveCurve.Evaluate(Mathf.PingPong(m_Time, duration) / duration));
				break;
            case MoveAxis.ZAxis:
				position.z = m_OriginalPosition + Mathf.Lerp(0f, sign * height, moveCurve.Evaluate(Mathf.PingPong(m_Time, duration) / duration));
				break;
        }
		this.transform.position = position;
	}

	private void OnDrawGizmosSelected()
	{
		Vector3 pos = this.transform.position;
		if (Application.isPlaying)
            switch (moveAxis)
            {
                case MoveAxis.XAxis:
					pos.x = m_OriginalPosition;
					break;
                case MoveAxis.YAxis:
					pos.y = m_OriginalPosition;
					break;
                case MoveAxis.ZAxis:
					pos.z = m_OriginalPosition;
					break;
            }
		float sign = reversed ? -1f : 1f;
		Vector3 center;

		switch (moveAxis)
		{
			case MoveAxis.XAxis:
				Gizmos.color = Color.red;
				center = pos + new Vector3(sign * height / 2, 0f, 0f);
				Gizmos.DrawWireCube(center, new Vector3(height, 1f, 1f));
				break;
			case MoveAxis.YAxis:
				Gizmos.color = Color.green;
				center = pos + new Vector3(0f, sign * height / 2, 0f);
				Gizmos.DrawWireCube(center, new Vector3(1f, height, 1f));
				break;
			case MoveAxis.ZAxis:
				Gizmos.color = Color.blue;
				center = pos + new Vector3(0f, 0f, sign * height / 2);
				Gizmos.DrawWireCube(center, new Vector3(1f, 1f, height));
				break;
		}
	}
}

#if UNITY_EDITOR
[CustomEditor(typeof(Floaty))]
public class LevelTypeEditor : Editor
{
	//private SerializedProperty boolProp;
	//private SerializedProperty paletteProp;
	//private SerializedProperty mapsProp;
	//private SerializedProperty color2DProp;
	//private SerializedProperty color3DProp;

	//private void OnEnable()
	//{
	//	boolProp = serializedObject.FindProperty("is2DLevel");
	//	paletteProp = serializedObject.FindProperty("colorPalette");
	//	mapsProp = serializedObject.FindProperty("maps");
	//	color2DProp = serializedObject.FindProperty("colorArray_2D");
	//	color3DProp = serializedObject.FindProperty("colorArray_3D");
	//}

	//public override void OnInspectorGUI()
	//{
	//	serializedObject.Update();
	//	EditorGUILayout.PropertyField(boolProp);
	//	EditorGUILayout.PropertyField(paletteProp);
	//	EditorGUILayout.PropertyField(mapsProp);
	//	if (boolProp.boolValue) EditorGUILayout.PropertyField(color2DProp);
	//	else EditorGUILayout.PropertyField(color3DProp);
	//	serializedObject.ApplyModifiedProperties();
	//}
}
#endif