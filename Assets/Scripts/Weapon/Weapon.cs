using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public int id;
    [SerializeField] private int damage;
    [SerializeField] private int price;
    [SerializeField] private ParticleSystem particle;

    public int Damage
    {
        get
        {
            return damage;
        }
        protected set
        {
            damage = value;
        }
    }

    public int Price => price;

    public void PlayParticle(Vector3 position)
    {
        Instantiate(particle, position, Quaternion.identity);
    }

    public virtual void Rotate(float angle, Vector3 axis, float speed)
    {
        StartCoroutine(RotateCoroutine(angle, axis, speed));
    }

    protected virtual IEnumerator RotateCoroutine(float angle, Vector3 axis, float speed)
    {
        float d = 0;

        while (d < 0.95f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, axis), Time.deltaTime * speed);

            d = Quaternion.Dot(transform.rotation, Quaternion.AngleAxis(angle, axis));

            yield return null;
        }

        Destroy(gameObject);
    }
}
