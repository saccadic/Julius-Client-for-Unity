using UnityEngine;
using System.Collections;


public class Spectrum : MonoBehaviour {
	public GameObject mic;
	public GameObject obj = null;
	public float[] spectrum;
	private int 		num 		=  1024;
	public float 		margin 		=  1;
	public float		offset_size = 10;

	private AudioSource audio_data;
	private GameObject[] clone;
	private Vector3 size;
	//private int count = 0;
	
	// Use this for initialization
	void Start () {
		audio_data = mic.GetComponent<AudioSource>();

		size = obj.transform.localScale;
		spectrum = new float[num];
		clone = new GameObject[num];
		for(int i = 0; i < num; i++){
			clone[i] = (GameObject)GameObject.Instantiate(obj);
			clone[i].transform.position = new Vector3(margin*i+transform.position.x,transform.position.y,transform.position.z);
			clone[i].transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		spectrum = audio_data.GetSpectrumData(1024, 0, FFTWindow.BlackmanHarris);

		for(int i=0;i<num;i++){
			clone[i].transform.localScale = new Vector3(1,spectrum[i]*offset_size,1);
		}
	}
}
