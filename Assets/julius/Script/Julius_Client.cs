/********************************************************* 
*Katuyoshi Hotta
*
*
*             Julius client for Unity v1.2
* 
* 
*                                                2014/4/15
***********************************************************/

using UnityEngine;
using System.Collections;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;


[RequireComponent(typeof(AudioClip))]
public class Julius_Client : MonoBehaviour {
	//--------------------------------------------------

	//juliusからの結果用
	public string 	Result;

	//初期設定用
	public			bool			windowtype_hidden	= false;
	public 			string 			IPAddress 		= "localhost";
	public 			int 			port			= 10500;
	public 			string			command			= "-C main.jconf -C am-gmm.jconf -input mic -48 -module -charconv utf-8 sjis";
	public			float			vol					= 0f;

	//TCP/IP用
	private 	   	bool 			connect 		= false;
	private static 		TcpClient 		tcpip 			= null;
	private static 		NetworkStream 		net;
	private static 		string 			stream;

	//XML処理用
	public 	static 		string 			tmp_s 			= "HogeHoge";
	private static 		byte[] 			data 			= new byte[10000];
	private static		Match 			sampling;
	private static 		Regex 			xml_data;

	//外部プログラム用
	private System.Diagnostics.Process ps;

	//マルチスレッド用
	private Thread julius_client;

	//--------------------------------------------------
	
	/*juliusサーバーへ接続する*/
	private bool start_julius_client(){
		//TCP/IPの初期化＆juliusサーバーへ接続
		tcpip = new TcpClient(IPAddress,port);
		//tcpopが取得出来たかどうか
		if (tcpip == null) {
			Debug.Log("Connect Fall.");
			return false;
		} else {
			Debug.Log("Connect Success.");
			//ストリームの取得
			net = tcpip.GetStream ();
			//マルチスレッドへ登録＆開始
			julius_client = new Thread (new ThreadStart (get_stream));
			julius_client.Start ();
			return true;
		}
	}

	private IEnumerator start_julius(){
		Debug.Log ("Julius");
		yield return new WaitForSeconds(1);	//juliusサーバーが起動するまで時間があるので少し待つ
		Debug.Log ("START:Julius");
		connect = start_julius_client();
	}

	private void run_julius_server(){//外部プログラムjulisuをコマンド付きで起動
		System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
		info.FileName="julius_server.exe";
		info.WorkingDirectory = @".\Assets\julius\core";
		info.Arguments = command;
		if(windowtype_hidden){
			info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		}
		ps = System.Diagnostics.Process.Start(info);
	}

	private void start_recording(){
		audio.clip = Microphone.Start (null,true,999,44100);
		audio.loop = true;
		audio.mute = true;
		while (!(Microphone.GetPosition("") > 0)){}             // マイクが取れるまで待つ。空文字でデフォルトのマイクを探してくれる
		audio.Play();  
	}

	private float GetAveragedVolume()
	{ 
		float[] data = new float[256];
		float a = 0;
		audio.GetOutputData(data,0);
		foreach(float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a/256.0f;
	}
	
	private void kill_julius_server(){//外部プログラムjulisのプロセスを強制終了
		ps.Kill ();
		Debug.Log("Kill julius server.");
	}
	
	private static string XML_search(string stream){
		xml_data = new Regex("WORD=\"([^\"]+)\"");//正規表現
		sampling = xml_data.Match(stream);//第一抽出

		while(sampling.Success){//最後まで抽出
			//Debug.Log(sampling.Groups.Count);

			//結合処理
			for(int i = 1;i<sampling.Groups.Count;i++){//なぜi = 1にしたらうまく行った
				tmp_s += sampling.Groups[i].Value;
				//Debug.Log(sampling.Groups[i].Value);
			}

			sampling = sampling.NextMatch();//順次抽出していく
		}
		return tmp_s;//最終的に結合した文字列を返す
	}

	/*juliusサーバーから受信*/
	private static void get_stream(){//**マルチスレッド関数**
		while(true){
			//マルチスレッドの速度？
			Thread.Sleep(0);
			//ストリームの受信
			net.Read(data, 0, data.Length);
			stream = System.Text.Encoding.Default.GetString(data);
			//Debug.Log (stream);

			Debug.Log ("tmp_s : "+tmp_s);
			tmp_s = "";//初期化

			//XMLデータから文字列の抽出
			tmp_s = XML_search(stream);
		}
	}

	/*juliusサーバーへ送信*/
	private void send_stream(string msg){
		//net = tcpip.GetStream ();
		byte[] send_byte = Encoding.UTF8.GetBytes(msg);
		//ストリームの送信
		net.Write(send_byte,0,send_byte.Length);
		Debug.Log ("Send Message -> "+msg);
	}

	//juliusサーバーから切断
	private void close_julius(){
		net.Close();//tcp/ipの切断処理
		kill_julius_server ();//juliusサーバーのプロセスを強制終了
	}
	
	//終了処理と同時に実行される
	void OnApplicationQuit() {
		if (connect) {
			close_julius();//サーバーの切断
			julius_client.Abort(); 
		}//マルチスレッドの終了
	}
	
	// Use this for initialization
	void Start() {
		start_recording ();

		//juliusサーバーを起動
		run_julius_server ();

		//juliusシステムの起動
		StartCoroutine("start_julius");
	}
	
	// Update is called once per frame
	void Update () {
		//結果を常に受け取る
		if (connect) {
			vol = GetAveragedVolume();
			Result = tmp_s;
		} else {
			Debug.Log("not conect.");
		}
	}
}
