using UnityEngine;
using System.Collections;

public class objRotate : MonoBehaviour { 


    public Vector3 m_axis;
    public Vector3 m_orginPosition;
    public float m_radio=10;
    public int m_degreePerSec=90;
    public float m_timeCost;
	// Use this for initialization
	void Start () {
        m_axis.x = 0;
        m_axis.y = 1;
        m_axis.z = 0;
        m_orginPosition = transform.position;


     
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = m_orginPosition;
        transform.up = Vector3.up;
        transform.right = Vector3.right;
        transform.forward = Vector3.forward;

        m_timeCost += Time.deltaTime;
        float angle = m_degreePerSec * m_timeCost;
        transform.Rotate(Vector3.up,angle);
        transform.Translate(new Vector3(m_radio,0,0));
	}
}
