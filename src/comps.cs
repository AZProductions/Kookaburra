using System.ComponentModel;

namespace KookaburraShell
{
    public partial class comps : Component
    {
        public comps()
        {
            InitializeComponent();
        }

        public comps(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
