
# 「C#でのPDF作成」報告書 <!-- omit in toc -->

* 作成日：2022/12/7
* 作成者：渡邊秀雄(hwatanab4546)

***

## 目次 <!-- omit in toc -->

<!-- markdownlint-disable -->
- [1. 作業報告](#1-作業報告)
  - [1.1 作業内容](#11-作業内容)
  - [1.2 作業環境](#12-作業環境)
  - [1.3 作業期間](#13-作業期間)
  - [1.4 納品物一覧](#14-納品物一覧)
- [2. 各種機能の説明](#2-各種機能の説明)
  - [2.1 図の拡大/縮小について](#21-図の拡大縮小について)
  - [2.2 `calcBeam.DrawFigXXXX()`メソッドによる図の描画について](#22-calcbeamdrawfigxxxxメソッドによる図の描画について)
  - [2.3 `calcBeam.DrawTabXXXX()`メソッドによる表の描画について](#23-calcbeamdrawtabxxxxメソッドによる表の描画について)
  - [2.4 上付き文字と下付き文字について](#24-上付き文字と下付き文字について)
  - [2.5 テキスト描画角度の指定について](#25-テキスト描画角度の指定について)
  - [2.6 多段組みの描画について](#26-多段組みの描画について)
  - [2.7 水平な括線を持つ分数の描画について](#27-水平な括線を持つ分数の描画について)
  - [2.8 矢印の描画について](#28-矢印の描画について)
  - [2.9 破線の描画について](#29-破線の描画について)
  - [2.10 下線付き文字について](#210-下線付き文字について)
  - [2.11 `TextJoiningContext`クラスについて](#211-textjoiningcontextクラスについて)
  - [2.12 `Table`クラスの機能追加について](#212-tableクラスの機能追加について)
- [3. 変更点一覧](#3-変更点一覧)
  - [3.1 作業対象ファイル](#31-作業対象ファイル)
  - [3.2 作業対象クラス/メソッド/型](#32-作業対象クラスメソッド型)
- [4. その他](#4-その他)
  - [4.1 使用したUnicodeの一覧](#41-使用したunicodeの一覧)
  - [4.2 参考リンク](#42-参考リンク)
<!-- markdownlint-restore -->

***

## 1. 作業報告

### 1.1 作業内容

* 「参照用（合成桁の設計例と解説）.pdf」全9ページの内容をPDF出力するための処理を、「GirderGenBrpyServer\PDF_Manager\Printing\Calcrate\calcBeam.cs」に追加しました。
* JSONで指定された設定データは参照していません。

### 1.2 作業環境

* OS：Windows 10 Home 21H2
* 言語：C# 10
* フレームワーク：.NET 6.0
* 開発環境：Microsoft Visual Studio Community 2022

### 1.3 作業期間

* 2022/11/21 ~ 2022/12/7

### 1.4 納品物一覧

* `GirderGenBrpyServer-20221124.zip`
  * ビルド環境一式(中間納品物#1)
  * 一通りの図が出力可能になった段階
* `GirderGenBrpyServer-20221130.zip`
  * ビルド環境一式(中間納品物#2)
  * 一通りの作業が完了した段階
* `GirderGenBrpyServer-20221207.zip`
  * ビルド環境一式(最終納品物)

***

## 2. 各種機能の説明

### 2.1 図の拡大/縮小について

* 図を描画するメソッドで定義されている`scale`変数の設定値を変更すると、図を拡大または縮小できるようになっています。
* 現在の設定値は、等倍を意味する`1.0`に設定されています。
* `scale`の変更により図中のテキスト表示位置は影響を受けますが、テキストのフォントサイズには影響がありません。このため、`scale`を変更する場合はフォントサイズの調整が必要になるかもしれません。

### 2.2 `calcBeam.DrawFigXXXX()`メソッドによる図の描画について

* 図は`mc.currentPos.X`の位置から用紙右側マージン手前までの範囲に中央寄せで描画されます。
* 図を描画するメソッドを呼び出した後の`mc.currentPos.X`は呼び出し前から変更なし、`Y`は図の高さが加算された値となっています。

### 2.3 `calcBeam.DrawTabXXXX()`メソッドによる表の描画について

* 表は`mc.currentPos`の位置を左上角として描画されます。
* 表を描画するメソッドを呼び出した後の`mc.currentPos`は、表の右下角のセルの右側に移動します (`Table.PrintTable()`メソッドの仕様)。

### 2.4 上付き文字と下付き文字について

* 上付き数字と下付き数字については、Unicodeに含まれているものをそのまま使用しています。
* 下付き英字については、全ての英字がUnicodeに含まれているわけではないようですので、小さめのフォントを使用することで対処しています。

### 2.5 テキスト描画角度の指定について

* 既存のメソッド`Text.PrtText()`に描画角度を指定する引数`angle`を追加しました。
* X軸の正方向から時計回りを正とした値(単位は度)を指定してください。
* デフォルトは`0`となっています。

### 2.6 多段組みの描画について

* 新規追加のメソッド`PdfDocumentExtensions.Nup()`メソッドを使用して描画しています。
* このメソッドは`mc.currentPos.X`から用紙右端までの幅を`conponents`で均等分割して多段組みを行います。
* 個々の構成要素を生成するメソッドは、それぞれが生成する領域のサイズ(少なくとも有意な高さが設定されていること)を返却する必要があります。
* このメソッド呼び出し後の`mc.currentPos`には、多段組みされた領域の高さが反映されます。

### 2.7 水平な括線を持つ分数の描画について

* 新規追加のメソッド`Text.PrtFraction()`を使用して描画しています。
* 以下に引数一覧を示します。
    |引数名|型|デフォルト値|説明|
    |--|--|--|--|
    |`numerator`|`string`|なし|分子として表示されるテキスト|
    |`denominator`|`string`|なし|分母として表示されるテキスト|
    |`vinculumPenWidth`|`double`|0.1|括線の太さ|
    |`leftMargin`|`double`|0|括線の左側に挿入する空白の幅|
    |`rightMargin`|`double`|0|括線の右側に挿入する空白の幅|
    |`numeratorLeftMargin`|`double`|6|括線の左端から分子の表示位置の左端までの最低幅|
    |`numeratorRightMargin`|`double`|6|括線の右端から分子の表示位置の右端までの最低幅|
    |`denominatorLeftMargin`|`double`|6|括線の左端から分母の表示位置の左端までの最低幅|
    |`denominatorRightMargin`|`double`|6|括線の右端から分母の表示位置の右端までの最低幅|
    |`numeratorDistance`|`double`|1|分子の下端と括線の間の隙間の幅|
    |`denominatorDistance`|`double`|1|分母の上端と括線の間の隙間の幅|
* 戻り値として、描画したテキストの幅を返却します。

### 2.8 矢印の描画について

* 新規追加のメソッド`Shape.DrawLines()`と`Shape.DrawArc()`の引数`startCap`と`endCap`に、それぞれ始点と終点の形状を指定することで描画しています。
* `startCap`に`LineCap.ArrowAnchor`を指定すると始点が矢印となります。デフォルトは矢印を描画しない`LineCap.NoAnchor`です。
* `endCap`に`LineCap.ArrowAnchor`を指定すると終点が矢印となります。デフォルトは矢印を描画しない`LineCap.NoAnchor`です。

### 2.9 破線の描画について

* 新規追加のメソッド`Shape.DrawLines()`の引数`dashPattern`の指定により描画しています。
* `dashPattern`の指定方法については、[参考リンク](#42-参考リンク)の`Pen.DashPattern`プロパティの説明がわかりやすいと思いますので、そちらをご参照ください。

### 2.10 下線付き文字について

* 新規追加のメソッド`Text.PrtUnderlinedText()`で描画しています。
* 下線付きのフォントは使用せず、テキストの下に横線を描画しています。

### 2.11 `TextJoiningContext`クラスについて

* テキストを描画するごとに`mc.currentPos.X`が自動更新されるコンテキストを生成します。
* `Dispose()`呼び出しにより、コンテキスト生成前の状態を復元します。
* 下線付きテキストや水平な括線を持つ分数を含むテキストを描画する時などは`mc.currentPos.X`を更新しながら複数のメソッドを呼び出す必要がありますが、このコンテキスト内では単純にメソッドを連続して呼び出すだけで済むため、いちいち`mc.currentPos.X`を更新する手間が省けるようになっています。

### 2.12 `Table`クラスの機能追加について

* セルの連結
  * 行方向のセルを連結したい場合は、連結対象の一番上のセルの`Table.RowMerge[,]`に、連結するセルの数-1を設定してください。
  * 列方向のセルを連結したい場合は、連結対象の一番左のセルの`Table.ColMerge[,]`に、連結するセルの数-1を設定してください。
  * 行方向のセル連結および列方向のセル連結は、罫線の描画には影響を与えません (テキストの表示位置にのみ影響を与えます)。
  * 現状は、行方向のセル連結が行われている表に対しては、行削除(`Table.ClearDraft()`メソッドによる)およびテーブル分割(`Table.SplitTable()`メソッドによる)は実行できません (例外が発生します)。
* セル内パッディングの指定
  * 下記の公開インスタンスの設定により、セル内テキストと上下左右のセル境界との最小間隔を指定することができます。
    * `Table.LeftCellPadding[,]` - テキスト左端と左側セル境界の間の最小間隔
    * `Table.TopCellPadding[,]` - テキスト上側と上側セル境界の間の最小間隔
    * `Table.RightCellPadding[,]` - テキスト右端と右側セル境界の間の最小間隔
    * `Table.BottomCellPadding[,]` - テキスト下側と下側セル境界の間の最小間隔

***

## 3. 変更点一覧

### 3.1 作業対象ファイル

* `GirderGenBrpyServer\PDF_Manager\Printing\Calcrate`フォルダ
    |ファイル名|新規/変更|備考|
    |--|--|--|
    |`calcBeam.cs`|変更|5.2節以降の描画処理を追加|
    |`calcBeam_AxisConversionExtensions.cs`|新規||
    |`calcBeam_Fig0502.cs`|新規||
    |`calcBeam_Fig0503.cs`|新規||
    |`calcBeam_Fig0504.cs`|新規||
    |`calcBeam_Fig0505.cs`|新規||
    |`calcBeam_Fig0506.cs`|新規||
    |`calcBeam_Fig0507.cs`|新規||
    |`calcBeam_Tab0504a.cs`|新規||
    |`calcBeam_Tab0504b.cs`|新規||
    |`calcBeam_Tab0506.cs`|新規||

<!-- markdownlint-disable MD033 -->
* `GirderGenBrpyServer\PDF_Manager\Printing\Comon`フォルダ
    |ファイル名|新規/変更|備考|
    |--|--|--|
    |`LineCap.cs`|新規||
    |`PdfDocumentExtensions.cs`|新規||
    |`printManager.cs`|変更|下記プロパティを追加<br>`SectionIndent`<br>`SubsectionIndent`<br>`SubsubsectionIndent`<br>`ParagraphIndent`<br>`BeginningOfParagraphIndent`|
    |`Shape.cs`|変更|下記メソッドを追加<br>`Shape.DrawLines()`<br>`Shape.DrawPolygon()`<br>`Shape.DrawArc()`|
    |`Table.cs`|変更|下記の公開インスタンスを追加<br>`Table.RowMerge[,]`<br>`Table.ColMerge[,]`<br>`Table.LeftCellPadding[,]`<br>`Table.TopCellPadding[,]`<br>`Table.RightCellPading[,]`<br>`Table.BottomCellPadding[,]`<br>罫線の描画処理に線の太さの退避復元処理を追加|
    |`Text.cs`|変更|`Text.PrtText()`メソッドに引数`angle`を追加<br>`Text.PrtUnderlinedText()`メソッドと`Text.PrtFraction()`メソッドを追加<br>`TextJoiningContext`クラス定義を追加|
<!-- markdownlint-restore MD033 -->

### 3.2 作業対象クラス/メソッド/型

* `calcBeam`クラスのメソッド
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`printPDF`|変更|描画処理メイン (5.2節以降の描画処理を追加)|
    |`DrawFig0502`|新規|5.2.1節の図を描画|
    |`DrawFig0503`|新規|5.2.2節の図を描画|
    |`DrawFig0504`|新規|5.3節の図を描画(上側の図は`DrawFig0502`と共通のメソッドを使用)|
    |`DrawFig0505`|新規|5.4.1節の`G1(G4)`の図を描画|
    |`DrawFig0506`|新規|5.4.1節の`G2(G3)`の図を描画(図中のテキストの描画以外は`DrawFig0505`と共通のメソッドを使用)|
    |`DrawFig0507`|新規|5.5節の図を描画|
    |`DrawTab0504a`|新規|5.4.1節の`G1(G4)`の表を描画|
    |`DrawTab0504b`|新規|5.4.1節の`G2(G3)`の表を描画|
    |`DrawTab0506`|新規|5.6.1節の表を描画|
* `AxisConversionExtensions`クラスのメソッド
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`YAxisSymmetry`|新規|y軸に平行な直線に関して線対称な座標に変換|
    |`PointSymmetry`|新規|指定された座標に関して点対称な座標に変換|
* `printManager`クラスのプロパティ
    |プロパティ名|新規/変更|説明|
    |--|--|--|
    |`SectionIndent`|新規|節見出し(X. ・・・)の行インデント|
    |`SubsectionIndent`|新規|小節見出し(X.Y ・・・)の行インデント|
    |`SubsubsectionIndent`|新規|小々節見出し(X.Y.Z ・・・)の行インデント|
    |`ParagraphIndent`|新規|本文段落の行インデント|
    |`BeginningOfParagraphIndent`|新規|本文段落の先頭行の行インデント|
* `Shape`クラスのメソッド
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`DrawLines`|新規|連続した直線の描画。始点と終点に矢印を置くことが可能。破線パターンを指定することで破線を描画することが可能|
    |`DrawPolygon`|新規|多角形の描画(始点と終点の間にも直線を描画)|
    |`DrawArc`|新規|円弧の描画。始点と終点に矢印を置くことが可能。**現状は、真円のみに対応**|
* `Table`クラスの公開インスタンス
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`RowMerge`|新規|指定した個数の下側セルと結合。影響を受けるのはテキストの描画位置のみ。罫線の描画には影響なし。**現状は、行結合を含む表は行削除(ClearDraft)と表分割(SplitTable)が不可**|
    |`ColMerge`|新規|指定した個数の右側セルと結合。影響を受けるのはテキストの描画位置のみ。罫線の描画には影響なし|
    |`LeftCellPadding`|新規|テキスト先頭(左側)とセル境界との間の最小間隔|
    |`TopCellPadding`|新規|テキスト上端とセル境界との間の最小間隔|
    |`RightCellPadding`|新規|テキスト末尾(右側)とセル境界との間の最小間隔|
    |`BottomCellPadding`|新規|テキスト下端とセル境界との間の最小間隔|
* `Shape`クラスのメソッド
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`PrtText`|変更|テキストの傾きを指定する引数`angle`を追加|
    |`PrtUnderlinedText`|新規|下線付きテキストの描画|
    |`PrtFraction`|新規|水平な括線を持つ分数の描画|
* `TextJoiningContext`クラスのメソッド
    |メソッド名|新規/変更|説明|
    |--|--|--|
    |`TextJoiningContext`|新規|`mc.currentPos.X`を自動的に更新することにより連続したテキストの描画が可能なコンテキストを生成。`Dispose()`呼び出しにより、コンテキスト生成前の状態が復元される|
    |`PrtText`|新規|テキストの描画|
    |`PrtUnderlinedText`|新規|下線付きテキストの描画|
    |`PrtFraction`|新規|水平な括線を持つ分数の描画|
    |`Dispose`|新規|コンテキスト生成前の状態を復元|
* enum型
    |型名|新規/変更|説明|
    |--|--|--|
    |`LineCap`|新規|線分または円弧の始点と終点の形状(`NoAnchor`：矢印なし、`ArrowAnchor`：矢印あり)|

***

## 4. その他

### 4.1 使用したUnicodeの一覧

* 下記6種のUnicode文字を使用しています。
    |文字|Unicode|備考|
    |--|--|--|
    |₁|U+2081|下付き1|
    |₂|U+2082|下付き2|
    |₃|U+2083|下付き3|
    |₄|U+2084|下付き4|
    |²|U+00B2|上付き2|
    |℄|U+2104|中心線シンボル|

### 4.2 参考リンク

* [上付き文字と下付き文字: ᵃ ₐ ᴬ ᵇ ᴮ ᶜ - Unicode キャラクター図鑑](https://unicode-table.com/jp/sets/superscript-and-subscript-letters/)
* [破線を描く - .NET Tips (VB.NET,C#...)](https://dobon.net/vb/dotnet/graphics/drawdash.html)
  * `Pen.DashPattern`プロパティの指定方法が記載されています。

***
