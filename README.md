# BookMarkSearcer

Windows Formsで作成した、フォルダ名とブックマークを対応付けて整理するためのデスクトップアプリケーションです。

## 動作環境

- Windows
- .NET 10
- Visual Studio 2026以降を推奨

## 主な機能

- 設定ファイルから各フォルダのパスを読み込み
- ブックマークJSONの読み込み
- 対象フォルダ一覧の表示
- フォルダ名に一致するブックマークタイトルの検索
- 対応するブックマークURLをブラウザで開く
- 対象フォルダをエクスプローラーで開く
- ダウンロードフォルダから画像ファイルを対象フォルダへ移動
- 処理済みフォルダを完了フォルダへ移動

## プロジェクト構成

```text
BookMarkSearcer/
├─ Form1.cs
├─ Form1.Designer.cs
├─ Form1.resx
├─ Program.cs
├─ BookMarkSearcer.csproj
├─ json/
│  └─ paths.json
├─ AGENTS.md
└─ README.md
```

## 設定

`json/paths.json`に各フォルダのパスを設定します。

```json
{
  "COMP": {
	"path": "D:\\VM\\fwork\\G\\03_Completed"
  },
  "BM": {
	"path": "D:\\VM\\fwork\\G\\04_BM"
  },
  "RAR": {
	"path": "D:\\VM\\fwork\\G\\05_rar"
  },
  "DL": {
	"path": "D:\\DL"
  }
}
```

各設定の用途は次のとおりです。

| キー | 用途 |
|---|---|
| `BM.path` | `out_bookmarks.json`が保存されているフォルダ |
| `COMP.path` | 処理完了後のフォルダ移動先 |
| `RAR.path` | 処理対象フォルダが保存されているフォルダ |
| `DL.path` | `.webp`および`.jpg`ファイルの取得元フォルダ |

`paths.json`はビルド時に出力フォルダの`json`ディレクトリへコピーされます。実行時はアプリケーションの出力フォルダにある設定ファイルが使用されます。

## 使用方法

1. `paths.json`の各パスを環境に合わせて設定します。
2. `out_bookmarks.json`を`BM.path`で指定したフォルダに配置します。
3. アプリケーションを起動します。
4. 必要に応じて以下のボタンを使用します。

| ボタン | 処理 |
|---|---|
| button1 | `out_bookmarks.json`を再読み込み |
| button2 | `RAR.path`直下のフォルダ一覧を`comboBox1`へ読み込み |
| button3 | 選択フォルダ名とブックマークの`title`を照合し、結果を`textBox3`へ表示 |
| button4 | button3で一致したブックマークの`href`を既定のブラウザで開く |
| button5 | 選択中のフォルダをエクスプローラーで開く |
| button6 | `DL.path`直下の`.webp`または`.jpg`を選択フォルダへ移動 |
| button7 | 選択フォルダを`COMP.path`へ移動し、一覧から削除 |

フォーム起動時には、設定ファイル、ブックマークJSON、対象フォルダ一覧が自動的に読み込まれます。

## ブックマーク検索の仕様

ブックマークJSONは`System.Text.Json.JsonDocument`として読み込まれ、JSON内のオブジェクトと配列を再帰的に検索します。

タイトル照合では次のルールを使用します。

- 大文字と小文字を区別しない
- `[]`で囲まれたタグの順番を区別しない
- タグ以外のタイトル部分を部分一致で比較
- 一致した`title`と、同じ要素にある`href`を取得

## ビルド

Visual Studioで`BookMarkSearcer.slnx`を開き、通常どおりビルドしてください。

コマンドラインでビルドする場合は、プロジェクトディレクトリで次を実行します。

```powershell
dotnet build
```

## 注意事項

- 設定したフォルダが存在しない場合、一部の処理は実行されません。
- 移動先に同名フォルダが存在する場合、フォルダ移動は実行されません。
- 画像移動時、移動先に同名ファイルが存在する場合は上書きせずスキップします。
- `paths.json`のキー名を変更する場合は、`Form1.cs`の読み込み処理も変更してください。
- AIエージェント向けの詳細な開発情報は`AGENTS.md`を参照してください。
