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


    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(m_target);
        Quaternion rot = Quaternion.AngleAxis(90.0f, new Vector3(1.0f, 0.0f, 0.0f));
        m_targetCircle = Instantiate(m_targetCirclePrefab, m_target + new Vector3(0.0f, 0.1f, 0.0f), rot);

    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            //rb.position += transform.forward * (speed * Time.deltaTime);

            //transform.position = Vector3.Lerp(transform.position, meteorGround.transform.position, 0.025f);

            transform.position = Vector3.MoveTowards(transform.position, m_target, speed * Time.deltaTime);
            if (transform.position == m_target)
            {
                Explosion();
            }
        }
    }

    void Explosion()
    {
        PoolManager.Kill(gameObject);
        GameObject explosion = Instantiate(m_bigExplosionPrefab, m_target, Quaternion.identity);
        Destroy(explosion, 3);
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
            Destroy(explosion, 3);
            Destroy(m_targetCircle);
        }
    }
}
