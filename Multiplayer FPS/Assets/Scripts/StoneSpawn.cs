using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneSpawn : MonoBehaviour {

	public Transform stone;

	// Use this for initialization
	void Start () {
		meshrenderer = GetComponent<MeshRenderer>();
		for (int i = 0; i < 1; i++)
		{
			Transform s = Instantiate(stone);
			s.localPosition = new Vector3(Random.Range(115, 120), Random.Range(35, 40), Random.Range(40, 45));
		}
	}
	
	// Update is called once per frame
	void Update () {
		meshrenderer.material.color = new Color(((int)Time.time % 2) * Random.Range(0, 7) * 255f, 0f, 0f);
	}
	MeshRenderer meshrenderer;
}
