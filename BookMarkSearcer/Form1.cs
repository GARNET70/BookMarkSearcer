namespace BookMarkSearcer
{
    public partial class Form1 : Form
    {
        // 読み込んだブックマークJSONと、検索結果をフォームの処理間で共有する。
        private System.Text.Json.JsonDocument? bookmarksData;
        private BookmarkMatch? matchingBookmark;
        // 完了先とダウンロード元のパスを保持する。
        private string compPath = string.Empty;
        private string dlPath = string.Empty;

        // フォームを初期化し、各ボタンのクリックイベントを登録する。
        public Form1()
        {
            InitializeComponent();
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            button3.Click += button3_Click;
            button4.Click += button4_Click;
            button5.Click += button5_Click;
            button6.Click += button6_Click;
            button7.Click += button7_Click;
        }

        #region OnLoad
        // 設定ファイルを読み込み、各コントロールと初期データを設定する。
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var pathsFile = Path.Combine(AppContext.BaseDirectory, "json", "paths.json");
            using var document = System.Text.Json.JsonDocument.Parse(File.ReadAllText(pathsFile));

            textBox1.Text = document.RootElement.GetProperty("BM").GetProperty("path").GetString() ?? string.Empty;
            compPath = document.RootElement.GetProperty("COMP").GetProperty("path").GetString() ?? string.Empty;
            textBox2.Text = document.RootElement.GetProperty("RAR").GetProperty("path").GetString() ?? string.Empty;
            dlPath = document.RootElement.GetProperty("DL").GetProperty("path").GetString() ?? string.Empty;

            button1_Click(this, EventArgs.Empty);
            button2_Click(this, EventArgs.Empty);
        }
        #endregion

        #region buttons
        #region button1
        // BMフォルダにあるブックマークJSONを読み込む。
        private void button1_Click(object? sender, EventArgs e)
        {
            var bookmarksFile = Path.Combine(textBox1.Text, "out_bookmarks.json");

            if (!File.Exists(bookmarksFile))
            {
                return;
            }

            bookmarksData?.Dispose();
            bookmarksData = System.Text.Json.JsonDocument.Parse(File.ReadAllText(bookmarksFile));
        }
        #endregion

        #region button2
        // RARフォルダ直下のサブフォルダ名をコンボボックスへ表示する。
        private void button2_Click(object? sender, EventArgs e)
        {
            comboBox1.Items.Clear();

            if (!Directory.Exists(textBox2.Text))
            {
                return;
            }

            foreach (var directory in Directory.GetDirectories(textBox2.Text))
            {
                comboBox1.Items.Add(Path.GetFileName(directory));
            }

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        #endregion

        #region button3
        // 選択フォルダ名に一致するブックマークのtitleを検索して表示する。
        private void button3_Click(object? sender, EventArgs e)
        {
            textBox3.Clear();
            matchingBookmark = null;

            if (bookmarksData is null || string.IsNullOrEmpty(comboBox1.Text))
            {
                return;
            }

            matchingBookmark = FindMatchingBookmark(bookmarksData.RootElement, comboBox1.Text);
            textBox3.Text = matchingBookmark?.Title ?? string.Empty;
        }
        #endregion

        #region button4
        // 検索結果に対応するブックマークのURLを既定のブラウザで開く。
        private void button4_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(matchingBookmark?.Href))
            {
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = matchingBookmark.Href,
                UseShellExecute = true
            });

            button5_Click(this, EventArgs.Empty);
        }
        #endregion

        #region button5
        // コンボボックスで選択中の作業フォルダをエクスプローラーで開く。
        private void button5_Click(object? sender, EventArgs e)
        {
            var selectedFolder = Path.Combine(textBox2.Text, comboBox1.Text);

            if (!Directory.Exists(selectedFolder))
            {
                return;
            }

            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
            {
                FileName = selectedFolder,
                UseShellExecute = true
            });
        }
        #endregion

        #region button6
        // DLフォルダの画像ファイルを選択中の作業フォルダへ移動する。
        private void button6_Click(object? sender, EventArgs e)
        {
            var selectedFolder = Path.Combine(textBox2.Text, comboBox1.Text);

            if (!Directory.Exists(selectedFolder) || !Directory.Exists(dlPath))
            {
                return;
            }

            foreach (var file in Directory.EnumerateFiles(dlPath))
            {
                var extension = Path.GetExtension(file);
                if (!extension.Equals(".webp", StringComparison.OrdinalIgnoreCase) &&
                    !extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                var destinationFile = Path.Combine(selectedFolder, Path.GetFileName(file));
                if (!File.Exists(destinationFile))
                {
                    File.Move(file, destinationFile);
                }
            }
        }
        #endregion

        #region button7
        // 選択中の作業フォルダをCOMPフォルダへ移動し、一覧を更新する。
        private void button7_Click(object? sender, EventArgs e)
        {
            var selectedIndex = comboBox1.SelectedIndex;
            if (selectedIndex < 0 || string.IsNullOrEmpty(compPath))
            {
                return;
            }

            var folderName = comboBox1.Text;
            var sourceFolder = Path.Combine(textBox2.Text, folderName);
            var destinationFolder = Path.Combine(compPath, folderName);

            if (!Directory.Exists(sourceFolder) || Directory.Exists(destinationFolder))
            {
                return;
            }

            Directory.Move(sourceFolder, destinationFolder);

            textBox3.Clear();
            matchingBookmark = null;
            comboBox1.Items.RemoveAt(selectedIndex);

            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }
        #endregion

        #endregion

        #region Class
        // JSONを再帰的に走査し、titleと同じ要素にあるhrefを取得する。
        private static BookmarkMatch? FindMatchingBookmark(System.Text.Json.JsonElement element, string folderName)
        {
            if (element.ValueKind == System.Text.Json.JsonValueKind.Object)
            {
                foreach (var property in element.EnumerateObject())
                {
                    if (property.NameEquals("title") &&
                        property.Value.ValueKind == System.Text.Json.JsonValueKind.String &&
                        IsTitleMatch(property.Value.GetString()!, folderName))
                    {
                        var href = element.TryGetProperty("href", out var hrefElement) &&
                                   hrefElement.ValueKind == System.Text.Json.JsonValueKind.String
                            ? hrefElement.GetString()
                            : null;

                        return new BookmarkMatch(property.Value.GetString()!, href);
                    }

                    var matchingBookmark = FindMatchingBookmark(property.Value, folderName);
                    if (matchingBookmark is not null)
                    {
                        return matchingBookmark;
                    }
                }
            }
            else if (element.ValueKind == System.Text.Json.JsonValueKind.Array)
            {
                foreach (var item in element.EnumerateArray())
                {
                    var matchingBookmark = FindMatchingBookmark(item, folderName);
                    if (matchingBookmark is not null)
                    {
                        return matchingBookmark;
                    }
                }
            }

            return null;
        }

        // タグの順番を考慮しつつ、titleとフォルダ名が一致するか判定する。
        private static bool IsTitleMatch(string title, string folderName)
        {
            var titleWithoutTags = System.Text.RegularExpressions.Regex.Replace(title, @"\[[^\]]*\]", string.Empty);
            var folderNameWithoutTags = System.Text.RegularExpressions.Regex.Replace(folderName, @"\[[^\]]*\]", string.Empty);

            if (!titleWithoutTags.Contains(folderNameWithoutTags, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            foreach (System.Text.RegularExpressions.Match tag in
                     System.Text.RegularExpressions.Regex.Matches(folderName, @"\[[^\]]*\]"))
            {
                if (!title.Contains(tag.Value, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        // 検索で一致したブックマークのtitleとURLを保持する。
        private sealed record BookmarkMatch(string Title, string? Href);
        #endregion
    }
}
