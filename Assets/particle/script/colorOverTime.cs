using UnityEngine;
using System.Collections;
using UnityEditor;

public class colorOverTime : MonoBehaviour {


    ParticleSystem m_ps;
    public Gradient m_gradient;
    public int m_duration; 
	// Use this for initialization
	void Start () {
        m_ps = this.particleSystem;

   //     ParticleSystem.Particle[] ps = new ParticleSystem.Particle[0];
  //      int n1 = m_ps.GetParticles(ps);
    //    Debug.Log(ps.Length);
	}	
	// Update is called once per frame
	void Update () {
        if(m_duration==0)
            return;
        ParticleSystem.Particle[] ps = new ParticleSystem.Particle[100];
        int n1=m_ps.GetParticles(ps);

//         Debug.Log(n1);
//         Debug.Log(ps.Length);
        for (int i = 0; i < n1;i++ )
        {
            ParticleSystem.Particle p1 =ps[i];
            float timeAlive = p1.startLifetime - p1.lifetime;
            int nalive = (int)(timeAlive * 1000);
            float pos = (nalive % m_duration) / (float)m_duration;
            Color c1 = m_gradient.Evaluate(pos);

            Debug.Log(p1.color);
            p1.color = c1;
            p1.color = Color.black;
            Debug.Log(p1.color);
        }
        m_ps.SetParticles(ps,n1); 
        
	}
}
