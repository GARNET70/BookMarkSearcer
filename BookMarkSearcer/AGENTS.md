# BookMarkSearcer プロジェクト概要

## 目的

Windows Formsで作成された、フォルダとブックマークを対応付けて処理するデスクトップアプリケーションです。

## プロジェクト情報

- プロジェクト: `BookMarkSearcer.csproj`
- ターゲットフレームワーク: `.NET 10` (`net10.0-windows`)
- UI: Windows Forms
- Nullable参照型: 有効
- Implicit usings: 有効
- ソリューション: `BookMarkSearcer.slnx`

## 主要ファイル

- `Form1.cs`: フォーム起動処理、ボタン処理、JSON検索、ファイル操作
- `Form1.Designer.cs`: Windows Formsコントロールの定義とレイアウト
- `Program.cs`: アプリケーションエントリーポイント
- `json/paths.json`: 各種フォルダパスの設定
- `Form1.resx`: フォームのリソース

## paths.json

`paths.json`はビルド出力先の`json/paths.json`へ常にコピーされます。実行時は`AppContext.BaseDirectory`を基準に読み込まれます。

現在使用している設定キー:

- `BM.path`: `out_bookmarks.json`が存在するブックマークフォルダ
- `COMP.path`: 完了したフォルダの移動先
- `RAR.path`: 処理対象フォルダが置かれているフォルダ
- `DL.path`: ダウンロード画像ファイルの取得元フォルダ

パスを変更する場合は、JSONの構造とキー名を維持してください。

## 起動時の処理

`Form1.OnLoad`で以下を実行します。

1. `paths.json`を読み込む
2. `BM.path`を`textBox1`に設定
3. `COMP.path`を内部フィールドへ保持
4. `RAR.path`を`textBox2`に設定
5. `DL.path`を内部フィールドへ保持
6. `BM.path/out_bookmarks.json`を読み込み、`bookmarksData`へ保持
7. `RAR.path`直下のフォルダ名を`comboBox1`へ読み込み、先頭項目を選択

## ボタン処理

- `button1`: `textBox1`のフォルダから`out_bookmarks.json`を再読み込み
- `button2`: `textBox2`直下のフォルダ名を`comboBox1`へ再読み込み
- `button3`: `comboBox1`のフォルダ名と`bookmarksData`内の`title`を比較し、一致したtitleを`textBox3`へ表示
- `button4`: button3で一致したブックマークの`href`を既定のWebブラウザで開く
- `button5`: `textBox2`と`comboBox1`の選択項目から対象フォルダをエクスプローラーで開く
- `button6`: `DL.path`直下の`.webp`または`.jpg`ファイルを、`comboBox1`で選択中のフォルダへ移動
- `button7`: 選択中のフォルダを`COMP.path`へ移動し、`textBox3`と選択状態を初期化

イベントは現在、`Form1`コンストラクター内で各ボタンに明示的に登録されています。

## ブックマーク検索

`bookmarksData`は`System.Text.Json.JsonDocument`として保持されます。JSONのオブジェクトおよび配列を再帰的に走査し、`title`プロパティを検索します。

title比較では、次の点を考慮します。

- 大文字・小文字を区別しない
- `[]`内のタグの順番を区別しない
- タグ以外の本文が部分一致する

一致したブックマークの`title`と、同じJSONオブジェクトにある`href`を保持します。

## 変更時の注意

- `Form1.Designer.cs`はデザイナー管理ファイルのため、UIレイアウト変更以外では直接編集しない
- ボタンイベントのシグネチャは`object? sender, EventArgs e`を使用する
- ファイルやフォルダ操作の前に存在確認を行う
- `paths.json`のキー名を変更する場合は`Form1.cs`の`GetProperty`呼び出しも同時に変更する
- JSONを再読み込みする際は、既存の`bookmarksData`を破棄してから置き換える
- 変更後は`get_errors`相当の確認とソリューション全体のビルドを実行する
