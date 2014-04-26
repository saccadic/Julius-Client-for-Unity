using UnityEngine;
using System.Collections;

public class volume : MonoBehaviour {
	public GameObject mic;
	public GameObject obj = null;
	public int 		num 	=  1;
	public float 	margin 	=  0;
	public int 		max 	=  1;
	public int		min 	= -1;
	public float	offset_size = 0;

	private AudioSource audio_data;
	private GameObject[] clone;
	private Vector3 size  = Vector3.zero;
	private float vol = 0;
	private float result = 0;
	private int count = 0;


	private float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio_data.GetOutputData(data,0);
		
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		
		result = a / 256;
				
		return result;
	}

	// Use this for initialization
	void Start () {
		audio_data = mic.GetComponent<AudioSource>();

		size = obj.transform.localScale;
		clone = new GameObject[num];
		for(int i = 0; i < num; i++){
			clone[i] = (GameObject)GameObject.Instantiate(obj);
			clone[i].transform.position = new Vector3(margin*i+transform.position.x,transform.position.y,transform.position.z);
			clone[i].transform.parent = transform;
		}
	}
	
	// Update is called once per frame
	void Update () {
		vol = GetAveragedVolume();

		if(count >= num){
			count = 0;
		}
		size = new Vector3(1f,vol*offset_size,1f);
		clone[count].transform.localScale = size;
		for(int i=0;i<num;i++){
			if(i < count + min || count + max < i){
				clone[i].transform.localScale = new Vector3(1,1,1);
			}
		}
		count++;
	}
}
