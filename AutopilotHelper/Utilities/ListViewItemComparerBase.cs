using System.Collections;

namespace AutopilotHelper.Utilities
{
    public abstract class ListViewItemComparerBase : IComparer
    {
        protected int column;
        protected bool ascending = true;  // Default to ascending

        public ListViewItemComparerBase()
        {
            column = 0;
        }

        public ListViewItemComparerBase(int column)
        {
            this.column = column;
        }

        public void SetSortDirection(bool direction)
        {
            ascending = direction;
        }

        protected virtual int CompareDateTime(object x, object y)
        {
            var item1 = (ListViewItem)x;
            var item2 = (ListViewItem)y;

            string text1 = item1.SubItems[column].Text;
            string text2 = item2.SubItems[column].Text;

            try
            {
                DateTime dateTime1 = DateTime.Parse(text1, System.Globalization.CultureInfo.InvariantCulture);
                DateTime dateTime2 = DateTime.Parse(text2, System.Globalization.CultureInfo.InvariantCulture);

                int result = DateTime.Compare(dateTime1, dateTime2);
                return ascending ? result : -result;
            }
            catch (FormatException)
            {
                // If the string is not a valid DateTime, fall back to string comparison
                return String.Compare(text1, text2);
            }
        }

        protected virtual int CompareInt(object x, object y)
        {
            var item1 = (ListViewItem)x;
            var item2 = (ListViewItem)y;

            string text1 = item1.SubItems[column].Text;
            string text2 = item2.SubItems[column].Text;

            try
            {
                int value1 = int.Parse(text1);
                int value2 = int.Parse(text2);

                int result = 0;
                if (value1 < value2)
                    result = -1;
                if (value1 > value2)
                    result = 1;

                return ascending ? result : -result;
            }
            catch (FormatException)
            {
                // If the string is not a valid Int, fall back to string comparison
                return String.Compare(text1, text2);
            }
        }

        public virtual int Compare(object x, object y)
        {
            var item1 = (ListViewItem)x;
            var item2 = (ListViewItem)y;

            string text1 = item1.SubItems[column].Text;
            string text2 = item2.SubItems[column].Text;

            var result = String.Compare(text1, text2);
            return ascending ? result : -result;
        }
    }

    public class ListViewItemDateTimeComparer : ListViewItemComparerBase
    {
        public ListViewItemDateTimeComparer(int column) : base(column)
        {
        }

        public override int Compare(object x, object y)
        {
            return base.CompareDateTime(x, y);
        }
    }

    public class ListViewItemStringComparer : ListViewItemComparerBase
    {
        public ListViewItemStringComparer(int column) : base(column)
        {
        }

        public override int Compare(object x, object y)
        {
            return base.Compare(x, y);
        }
    }

    public class ListViewItemIntComparer : ListViewItemComparerBase
    {
        public ListViewItemIntComparer(int column) : base(column)
        {
        }

        public override int Compare(object x, object y)
        {
            return base.CompareInt(x, y);
        }
    }
}
