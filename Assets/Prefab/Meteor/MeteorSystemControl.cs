using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSystemControl : MonoBehaviour
{

    public GameObject m_target;

    public GameObject m_meteorPrefab;

    public GameObject m_bigExplosionPrefab;

    public GameObject m_targetCirclePrefab;

    [Range(0.0f, 10.0f)]
    public float m_timeSpawn = 1.0f;

    [Range(0.0f, 5.0f)]
    public float m_CountSpawn = 1.0f;

    private float m_timeRandom = 0;
    private float m_countRandom = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_timeRandom = Random.Range(1.0f, m_timeSpawn);
        m_countRandom = Random.Range(1.0f, m_CountSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timeSpawn > m_timeRandom)
        {
            m_countRandom = Random.Range(1.0f, m_CountSpawn);
            Vector3 offset = new Vector3(-15.0f, 20.0f, -5.0f);
            for (int i = 0; i < m_countRandom; i++)
            {
                GameObject instance = m_meteorPrefab.Spawn(m_target.transform.position + offset, Quaternion.identity, m_meteorPrefab.transform.localScale, this.transform);
                MeteorController meteorController = instance.GetComponent<MeteorController>();

                meteorController.m_target = new Vector3(m_target.transform.position.x + Random.Range(0, 10), m_target.transform.position.y, m_target.transform.position.z + Random.Range(0, 10));
                meteorController.m_bigExplosionPrefab = m_bigExplosionPrefab;
                meteorController.m_targetCirclePrefab = m_targetCirclePrefab;
            }

            m_timeRandom = Random.Range(1.0f, m_CountSpawn);
            m_timeSpawn = 0.0f;
        }

        m_timeSpawn += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }
}
