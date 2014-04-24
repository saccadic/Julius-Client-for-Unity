<h1>Julius-Client-for-Unity ver1.3</h1>
<br>
<br>
パッケージデータ（240MB以上あるので注意）-> <a href="https://www.dropbox.com/s/akqozkkjjmcg4nu/Julius-Client-for-Unity.unitypackage" >Julius-Client-for-Unity.unitypackage</a>
<br>
<br>
<strong>使用ライブラリ</strong><br>
Julius v4.3.1 <a href = "http://julius.sourceforge.jp/">リンク</a><br>
<br>
<strong>初めに</strong><br>
C#経験1ヶ月、unity経験2ヶ月,github経験1週間の全てにおいて初心者の私がUnityを通して勉強するために書いたコードですのでアドバイスを頂ければ助かります。アドバイスは@Savant_Catに送ってくださると嬉しいです。
スクリプトのライセンスは<a href = "https://github.com/SavantCat/Julius-Client-for-Unity/blob/master/Assets/julius/Script/LICENSE.txt">MIT</a>、
Juliusのライセンスは<a href ="http://julius.sourceforge.jp/index.php?q=license.html">こちらに従います</a><br>
<br>
<strong>概要</strong><br>
Julius v4.3.1 のmoduleモードをUnityで扱うためのクライアントスクリプト(Windows専用)<br>
<br>
<strong>機能</strong><br>
1.juliusをuntiy側からmoduleモードで起動する<br>
2.TCP/IP経由で結果を取得する<br>
3.終了時にjuliusを終了する<br>
<br>
<strong>使い方</strong><br>
<ul>
<li>prefabファイル内のJulius_clientをシーンに入れれば使えます。初回起動時にネットワークのセキュリティについて聞かれますがTCP/IP経由で結果を取得する関係上必要なので許可してください。Julius_client内のスクリプトではResultというstring変数に常に認識結果を入力しているので別のスクリプトで利用する時はアタッチするかして使ってください。サンプルシーンもあるので参考にしてください。</li>
<li>実際に認識する言葉はAssets\julius\core\model\lang_m内にある"commnd.htkdic"というファイルです。編集方法は<a href = "http://shower.human.waseda.ac.jp/~m-kouki/pukiwiki_public/24.html#b5453414">こちら</a>を参考にしてください。（言語モデルの章あります）また、"bccwj.60k.htkdic"には多くの語彙が収録されているので参考になると思います。</li>
<li>windowtype_hiddenを有効にするとウィンドウが表示されず裏で処理してくれます。</li>
</ul>
<br>
<strong>注意点</strong><br>
終了時にjuliusを終了しますが何故か上手く行かない時があります。その時はタスクマネージャで確認してみてください。多分あります。終了してください。
<br>
<br>
<strong>最後に</strong><br>
juliusの認識精度を上げるために辞典内の語彙を必要最低限にしています。これはjuliusの本来の実力が発揮できないと思うので現状はjuliusを使ってコマンド入力機能をunityに追加するインターフェースのようなものと考えています。<br>
<br>
<strong>今後の予定</strong><br>
<ul>
<li>認識精度の向上</li>
<li>結果の受け渡しの方法</li>
<li>プロセスの重複起動の問題</li>
<li>windows専用なので他のOSでも動くようにする</li>
</ul>
<br>



