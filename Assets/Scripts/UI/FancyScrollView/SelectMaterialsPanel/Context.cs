using System;
using FancyScrollView;

namespace SmithScene.SelectMaterial
{
    public class Context : FancyGridViewContext
    {
        public int SelectedIndex = -1;
        public Action<int> OnCellClicked;
    }
}

