using System;
using FancyScrollView;

namespace CommonUI
{
    public class Context : FancyGridViewContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
}

