using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// 時間で弾を消す
		Destroy(this.gameObject, 1);
	}

	// Update is called once per frame
	void Update()
	{
		// 弾速設定
		transform.position += transform.TransformDirection(Vector3.forward * 1.5f);
	}
	
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy")
		{
			Destroy(this.gameObject);
		}
	}
}
