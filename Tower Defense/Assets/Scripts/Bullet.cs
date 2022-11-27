using System;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;

	public int damage;

	public float explosionRadius = 0f;

	private Rigidbody _rigidbody;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody>();
		_rigidbody.velocity = transform.forward * speed;
		
	}

	private void Update()
	{
		transform.position = Vector3.MoveTowards(transform.position, transform.forward, Time.deltaTime * 5);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Enemy"))
		{
			gameObject.SetActive(false);
		}
		HitTarget(other.transform);
	}

	void HitTarget (Transform target)
	{
		if (explosionRadius > 0f)
		{
			Explode();
		} else
		{
			Damage(target);
		}

		gameObject.SetActive(false);
	}

	void Explode ()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
		foreach (Collider collider in colliders)
		{
			if (collider.CompareTag("Enemy"))
			{
				Damage(collider.transform);
			}
		}
	}

	void Damage (Transform enemy)
	{
		EnemyController e = enemy.GetComponent<EnemyController>();

		if (e != null)
		{
			e.Damaged(damage);
		}
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, explosionRadius);
	}
}
