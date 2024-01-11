using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageClear : MonoBehaviour
{
	public string _nextScene;
	public void ChangeScene(string nextScene)
	{
		SceneManager.LoadScene(nextScene);
	}
	public GameObject ClearText;
	// Use this for initialization
	void Start()
	{
		ClearText.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{
		if (transform.position.z > 850f)
		{
			//ClearText.SetActive(true);
			ChangeScene(_nextScene);
		}
	}
}
