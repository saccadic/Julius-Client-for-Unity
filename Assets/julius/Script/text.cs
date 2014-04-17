using UnityEngine;
using System.Collections;

public class text : MonoBehaviour {

	public Julius_Client julius = null;
	private TextMesh textmesh = null;

	// Use this for initialization
	void Start () {
			textmesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		textmesh.text = julius.Result;
	}
}
