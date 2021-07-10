using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    public float speed = 10;

    public Vector3 m_target;

    public GameObject m_bigExplosionPrefab;

    public GameObject m_targetCirclePrefab;

    private GameObject m_targetCircle;

    private Vector3 m_direction;

    private Rigidbody m_rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        m_direction = m_target - transform.position;
        m_rigidbody = GetComponent<Rigidbody>();
        transform.LookAt(m_target);

        Quaternion rot = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));
        m_targetCircle = Instantiate(m_targetCirclePrefab, m_target + new Vector3(0.0f, 0.1f, 0.0f), rot);

    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            m_rigidbody.velocity = m_direction * speed;

            if (transform.position.y < -1.0f)
            {
                Explosion();
            }
        }
    }

    void Explosion()
    {
        PoolManager.Kill(gameObject);
        GameObject explosion = Instantiate(m_bigExplosionPrefab, m_target, Quaternion.identity);
        Destroy(explosion, 2);
        Destroy(m_targetCircle);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject objCollision = collision.collider.gameObject;
        if (!objCollision.CompareTag("Meteor"))
        {
            ContactPoint contact = collision.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
            Vector3 pos = contact.point;

            PoolManager.Kill(gameObject);
            GameObject explosion = Instantiate(m_bigExplosionPrefab, pos, rot);
            Destroy(explosion, 2);
            Destroy(m_targetCircle);
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponentInChildren<SphereCollider>(), collision.collider, true);
        }
    }
}
