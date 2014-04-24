<h1>Julius-Client-for-Unity ver1.3</h1>
<h2>まだ未完成なのでDLしないでください</h2>
<br>
<br>
<strong>使用ライブラリ</strong><br>
Julius v4.3.1 <a href = "http://julius.sourceforge.jp/">リンク</a><br>
<br>
<strong>初めに</strong><br>
全てにおいて初心者ですのでアドバイスを頂ければ助かります。アドバイスは@Savant_Catに送ってくださると嬉しいです。<br>
スクリプトのライセンスはフリー<br>
Juliusのライセンスは<a href ="http://julius.sourceforge.jp/index.php?q=license.html">こちらに従います</a><br>
<br>
<strong>概要</strong><br>
Julius v4.3.1 のmoduleモードをUnityで扱うためのクライアント(Windows専用)<br>
<br>
<strong>機能</strong><br>
1.juliusをuntiy側からmoduleモードで起動する<br>
2.TCP/IP経由で結果を取得する<br>
3.終了時にjuliusを終了する<br>
<br>
<strong>使い方</strong><br>
prefabファイル内のJulius_clientをシーンに入れれば使えます。初回起動時にネットワークのセキュリティについて聞かれますがTCP/IP経由で結果を取得する関係状必要なので許可してください。Julius_client内のスクリプトではResultというstring変数に常に認識結果を入力しているので別のスクリプトで利用する時はアタッチするかして使ってください。サンプルシーンもあるので参考にしてください。<br>
<br>
<strong>今後の予定</strong><br>
<ul>
<li>認識精度の向上</li>
<li>windows専用なので他のOSでも動くようにする</li>
</ul>
<br>



