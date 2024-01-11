using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public GameObject explosion;
	
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "beam")
		{
			Instantiate(explosion.gameObject, this.transform.position, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
