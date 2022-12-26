using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace FileManager.Cell
{
    public abstract class LocalCell : ViewCell
    {
        internal object OriginalBindingContext;

        public bool IsInitialized { get; private set; }

        public void PrepareCell()
        {
            InitializeCell();
            if (base.BindingContext != null)
            {
                SetupCell(isRecycled: false);
            }

            IsInitialized = true;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            if (IsInitialized)
            {
                SetupCell(isRecycled: true);
            }
        }

        protected abstract void InitializeCell();

        protected abstract void SetupCell(bool isRecycled);
    }
}
