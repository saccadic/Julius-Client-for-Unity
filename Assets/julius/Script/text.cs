using UnityEngine;
using System.Collections;

public class text : MonoBehaviour {

	public Julius_Client julius = null;
	private TextMesh textmesh = null;

	private string tmp;
	// Use this for initialization
	void Start () {
			textmesh = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
		if(tmp != julius.Result && julius.Result != ""){
			tmp = julius.Result;
		}
		textmesh.text = tmp;
	}
}
