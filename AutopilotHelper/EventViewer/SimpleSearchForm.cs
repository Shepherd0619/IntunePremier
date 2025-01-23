using DarkModeForms;

namespace AutopilotHelper.EventViewer
{
    public partial class SimpleSearchForm : Form
    {
        private ListView listView;

        public SimpleSearchForm()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        public SimpleSearchForm(ListView parent)
        {
            listView = parent;
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;
            var form = listView.FindForm();
            form.Activate();
            listView.Select();
            FindNextKeyword(textBox1.Text, radioButton2.Checked);
        }

        private void FindNextKeyword(string keyword, bool searchDown)
        {
            var caseSensitive = checkBox1.Checked;
            StringComparison comparison;

            if (caseSensitive)
            {
                comparison = StringComparison.Ordinal;
            }
            else
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            // 获取当前已选中的项（如果存在）
            ListView.SelectedListViewItemCollection selectedItems = listView.SelectedItems;

            int startIndex = 0;

            if (selectedItems.Count > 0 && searchDown)
            {
                // 如果向下搜索且有选择，则从最后一个已选中项的下一项开始
                startIndex = selectedItems[selectedItems.Count - 1].Index + 1;
            }
            else
            {
                // 否则，从列表顶部或底部开始，具体取决于搜索方向
                if (searchDown)
                    startIndex = 0; // 从顶部开始
                else if (selectedItems.Count > 0)
                {
                    startIndex = selectedItems[selectedItems.Count - 1].Index - 1;
                }
                else
                {
                    startIndex = Math.Max(0, listView.Items.Count - 1); // 从底部（向下）开始
                }
            }

            if (searchDown)
            {
                for (int i = startIndex; i < listView.Items.Count; i++)
                {
                    int indexToCheck;
                    if (searchDown && i >= listView.Items.Count)
                        indexToCheck = 0; // 循环到底部后回到顶部
                    else if (!searchDown && i < 0)
                        indexToCheck = listView.Items.Count - 1; // 循环到顶部后回到底部
                    else
                        indexToCheck = i;

                    if (listView.Items[indexToCheck].SubItems[3].Text.Contains(keyword, comparison))
                    {
                        // 找到了匹配的项，选择它并退出循环
                        var item = listView.Items[indexToCheck];
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        return;
                    }
                }
            }
            else
            {
                for (int i = startIndex; i > -1; i--)
                {
                    int indexToCheck = i;

                    if (listView.Items[indexToCheck].SubItems[3].Text.Contains(keyword, comparison))
                    {
                        // 找到了匹配的项，选择它并退出循环
                        var item = listView.Items[indexToCheck];
                        item.Selected = true;
                        item.Focused = true;
                        item.EnsureVisible();
                        return;
                    }
                }
            }

            MessageBox.Show("No more matches found!\n\n" +
                "If you currently selected item in log view, it means there is no match according to given direction.", 
                "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
