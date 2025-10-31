using Microsoft.Maui.Controls;

namespace VaeskeVandladningsSkema.Pages
{
    public partial class SkemaPage : ContentPage
    {
        public SkemaPage()
        {
            InitializeComponent();
        }

        private CheckBox GetTypiskDag()
        {
            return TypiskDag;
        }

        private void OnGemClicked(object sender, EventArgs e)
        {
            bool erUtæt = TypiskDag.IsChecked;
        }
    }
}