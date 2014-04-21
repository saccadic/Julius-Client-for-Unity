/********************************************************* 
*Author : Katuyoshi Hotta
*Twitter : @Savant_Cat
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

public class Julius_Client : MonoBehaviour {
	//--------------------------------------------------
	
	//juliusからの結果用
	public string 	Result;

	//初期設定用
	public			microphone		mic = null;
	public			bool			windowtype_hidden	= false;
	public 			string 			IPAddress 		= "localhost";
	public 			int 			port			= 10500;
	public 			string			command			= "-C main.jconf -C am-gmm.jconf -input mic -48 -module -charconv utf-8 sjis";
	public			float			wait_time 		= 1;
	public			float			timer 			= 0;
		
	//TCP/IP用
	private 		bool 			connect 		= false;
	private 	 	TcpClient 		tcpip 			= null;
	private  		NetworkStream 	net;
	private  		string 			stream;
	
	//XML処理用
	public 	 		string 			tmp_s 			= "HogeHoge";
	private  		byte[] 			data 			= new byte[10000];
	private 		Match 			sampling;
	private  		Regex 			xml_data;
	
	//外部プログラム用
	private System.Diagnostics.Process julius_process;
	
	//マルチスレッド用
	private Thread julius_thread;
	
	//--------------------------------------------------

	//---------------------------------julius.exe-------------------------------------------
	/*外部プログラムjuliusをコマンド付きで起動*/
	private void open_julius_server(){
		System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo();
		info.FileName="julius_server.exe";
		info.WorkingDirectory = @".\Assets\julius\core";
		info.Arguments = command;
		if(windowtype_hidden){
			info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
		}
		//juliusプロセスをjulius_processに登録
		julius_process = System.Diagnostics.Process.Start(info);

		if(julius_process.StartInfo == null)
			Debug.Log(null);
	}
	
	/*外部プログラムjulisのプロセスを強制終了*/
	private void kill_julius_server(){
		//プロセスの強制終了
		julius_process.Kill();
		Debug.Log("Kill julius server.");
	}
	
	/*juliusサーバーへ接続する*/
	private bool initialize_julius_client(){
		//TCP/IPの初期化＆juliusサーバーへ接続
		tcpip = new TcpClient(IPAddress,port);
		//クライアントが取得出来たかどうか
		if (tcpip == null) {
			Debug.Log("Connect Fall.");
			return false;
		} else {
			Debug.Log("Connect Success.");
			//ストリームの取得
			net = tcpip.GetStream ();
			//マルチスレッドへ登録＆開始
			julius_thread = new Thread (new ThreadStart (get_stream));
			julius_thread.Start ();
			return true;
		}
	}
	
	/*juliusサーバーから切断*/
	private void close_julius(){
		//TCP/IPの切断処理
		net.Close();
		//juliusサーバーのプロセスを強制終了
		kill_julius_server ();
	}
	
	/*サーバーが起動するまで時間があるので少し待つ*/
	private IEnumerator start_julius_server(){
		Debug.Log ("Julius Initialize...");
		yield return new WaitForSeconds(wait_time);
		Debug.Log ("START JuliusSystem!");
		connect = initialize_julius_client();
	}
	//--------------------------------------------------------------------
	
	//-----------------------------Stream----------------------------------
	/*juliusサーバーから受信*/
	private void get_stream(){//**マルチスレッド関数**
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
	
	/*ストリーム情報から正規表現を利用して文字列を抽出する*/
	private string XML_search(string stream){
		//正規表現
		xml_data = new Regex("WORD=\"([^。\"]+)\"");
		//初回抽出(NextMatch()を使うため)
		sampling = xml_data.Match(stream);
		while(sampling.Success){//最後まで抽出
			//結合処理
			for(int i = 1;i<sampling.Groups.Count;i++){//なぜかi = 1にしたらうまく行った
				tmp_s += sampling.Groups[i].Value;
			}
			//順次抽出していく
			sampling = sampling.NextMatch();
		}
		//最終的に結合した文字列を返す
		return tmp_s;
	}
	//---------------------------------------------------------------------

	private void timer_reset(){
		timer = 0f;
	}

	//----------------------Main---------------------------
	// Use this for initialization
	void Start() {
		//juliusサーバーを起動
		open_julius_server();
		
		//juliusシステムの起動
		StartCoroutine("start_julius_server");
	}

	private string tmp = "";

	// Update is called once per frame
	void Update () {
		//結果を常に受け取る
		if (connect) {
			if(tmp == tmp_s){
				timer += Time.deltaTime;
				if(timer >= 5f){
					tmp_s = "";
				}
			}else{
				tmp = tmp_s;
				timer_reset();
			}
			Result = tmp_s;
		} else {
			Debug.Log("not conect.");
		}
	}
	
	//終了処理と同時に実行される
	void OnApplicationQuit() {
		if (connect) {
			//juliusサーバーを切断
			close_julius();
			//マルチスレッドの終了
			julius_thread.Abort(); 
		}
	}
	//-----------------------------------------------------
}
