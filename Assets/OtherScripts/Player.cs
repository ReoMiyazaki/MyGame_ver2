using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	
	// 移動制御
	public float moveSpeed;
	public float moveForceMultiplier;

	// 水平移動時に機首を左右に向けるトルク
	public float yawTorqueMagnitude = 30.0f;

	// 垂直移動時に機首を上下に向けるトルク
	public float pitchTorqueMagnitude = 60.0f;

	// 水平移動時に機体を左右に傾けるトルク
	public float rollTorqueMagnitude = 30.0f;

	// バネのように姿勢を元に戻すトルク
	public float restoringTorqueMagnitude = 100.0f;

	// playerのHP
	public int playerHP;
	int Hp;
	public Slider slider;

	// 弾射出のクールタイム
	private int coolTimer;
	
	// レーザー
	public GameObject beam;
	
	// 爆発
	public GameObject Explosion;

	// 移動用 Rigidbody
	private new Rigidbody rigidbody;

	private Vector3 Player_pos;

	// Startよりも前に呼び出し
	void Awake()
	{
		rigidbody = GetComponent<Rigidbody>();

		// バネ復元力でゆらゆら揺れ続けるのを防ぐため、angularDragを大きめにしておく
		rigidbody.angularDrag = 20.0f;
	}

	void Start()
	{
		slider.value = 1;
		Hp = playerHP;
	}

	// 毎フレーム呼び出し
	void Update()
	{
		// 攻撃関係の処理
		if(Input.GetKey(KeyCode.Space))
		{
			if(coolTimer < 1)
			{
				Instantiate(beam.gameObject, this.transform.position, this.transform.rotation);
				coolTimer = 75;
			}
			coolTimer--;
		}

		// debugLog確認用
		if(Input.GetKeyDown(KeyCode.P))
		{
			Debug.Log($"speed : {rigidbody.transform.position.z}");
		}
	}

	// 一定間隔で呼ばれる
	void FixedUpdate()
	{
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");

		// xとyにspeedを掛ける
		rigidbody.AddForce(x * moveSpeed, y * moveSpeed, 0);

		Vector3 moveVector = Vector3.zero;

		rigidbody.AddForce(moveForceMultiplier * (moveVector - rigidbody.velocity));

		var root_speed = Vector3.zero;
		root_speed.z = 0.5f;
		rigidbody.transform.Translate(root_speed);

		// プレイヤーの入力に応じて姿勢をひねろうとするトルク
		Vector3 rotationTorque = new Vector3(-y * pitchTorqueMagnitude, x * yawTorqueMagnitude, -x * rollTorqueMagnitude);

		// 現在の姿勢のずれに比例した大きさで逆方向にひねろうとするトルク
		Vector3 right = transform.right;
		Vector3 up = transform.up;
		Vector3 forward = transform.forward;
		Vector3 restoringTorque = new Vector3(forward.y - up.z, right.z - forward.x, up.x - right.y) * restoringTorqueMagnitude;

		// 機体にトルクを加える
		rigidbody.AddTorque(rotationTorque + restoringTorque);
	}

	// 当たり判定
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Enemy" || col.gameObject.tag == "Stage")
		{
			Instantiate(Explosion.gameObject, this.transform.position, Quaternion.identity);
			Hp--;
			slider.value = (float)Hp / (float)playerHP;
			//playerHP--;
			Debug.Log($"playerHP : {playerHP}");
		}
	}
}
