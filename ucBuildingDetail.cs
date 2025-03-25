using FieldTool.Constants;
using System;
using System.Linq;
using System.Windows.Forms;

namespace FieldTool.UI
{
    public partial class ucBuildingDetail : UserControl
    {
        #region Constructors

        public ucBuildingDetail()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Private helper methods

        private void SetFormState(Enumerations.PanelDisplayMode mode)
        {
            switch (mode)
            {
                case Enumerations.PanelDisplayMode.Add:
                    break;

                case Enumerations.PanelDisplayMode.Edit:
                    break;

                case Enumerations.PanelDisplayMode.ReadOnly:
                    break;

                default:
                    throw new ArgumentException("Invalid building mode: " + mode.ToString());
            }
        }

        #endregion Private helper methods
    }
}