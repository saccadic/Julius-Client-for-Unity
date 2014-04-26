Julius-Client-for-Unity ver1.3
進行形>>https://github.com/SavantCat/Julius-Client-for-Unity

使用ライブラリ
Julius v4.3.1

初めに
C#経験1ヶ月、unity経験2ヶ月,github経験1週間の全てにおいて初心者の私がUnityを通して勉強するために書いたコードですのでアドバイスを頂ければ助かります。アドバイスは@Savant_Catに送ってくださると嬉しいです。
スクリプトのライセンスはMIT、Juliusのライセンスはこちらに従います

概要
Julius v4.3.1 のmoduleモードをUnityで扱うためのクライアントスクリプトです(Windows専用)

機能
1.juliusをuntiy側からmoduleモードで起動する
2.TCP/IP経由で結果を取得する
3.終了時にjuliusを終了する

使い方
・prefabファイル内のJulius_clientをシーンに入れれば使えます。初回起動時にネットワークのセキュリティについて聞かれますがTCP/IP経由で結果を取得する関係状必要なので許可してください。Julius_client内のスクリプトではResultというstring変数に常に認識結果を入力しているので別のスクリプトで利用する時はアタッチ？するかして使ってください。サンプルシーンもあるので参考にしてください。
・実際に認識する言葉はAssets\julius\core\model\lang_m内にある"commnd.htkdic"というファイルです。編集方法はこちらを参考にしてください。（言語モデルの章あります）http://shower.human.waseda.ac.jp/~m-kouki/pukiwiki_public/24.html#b5453414　また、"bccwj.60k.htkdic"には多くの語彙が収録されているので参考になると思います。
・windowtype_hiddenを有効にするとウィンドウが表示されず裏で処理してくれます。

最後に
juliusの認識精度を上げるために辞典内の語彙を必要最低限にしています。これはjuliusの本来の実力が発揮できないと思うので現状はjuliusを使ってコマンド入力機能をunityに追加するインターフェースのようなものと考えています。

今後の予定
・認識精度の向上
・結果の受け渡しの方法
・windows専用なので他のOSでも動くようにする