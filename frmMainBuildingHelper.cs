using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.Editors;
using System;
using System.Linq;

namespace FieldTool.UI
{
    public class frmMainBuildingHelper
    {
        #region Private member variables

        #region Registered controls

        private static ListViewEx _lstBuildingList;

        private static MetroTilePanel _pnlBuildingEditButtons;
        private static ItemContainer _itmBuildingEditButtons;
        private static MetroTileItem _btnBuildingAdd;
        private static MetroTileItem _btnBuildingCopy;
        private static MetroTileItem _btnBuildingEdit;
        private static MetroTileItem _btnBuildingDelete;

        private static LabelX _lblNew;
        private static LabelX _lblEdit;
        private static LabelX _lblCopy;

        private static TabControl _tabBuildingInfo;

        private static TabControlPanel _pnlBuildingGeneral;

        private static LabelX _lblBuildingCategory;
        private static ComboBoxEx _cboBuildingCategory;
        private static LabelX _lblBuildingType;
        private static ComboBoxEx _cboBuildingType;

        private static LabelX _lblBuildingName;
        private static TextBoxX _txtBuildingName;
        private static LabelX _lblBuildingAddress;
        private static TextBoxX _txtBuildingAddress1;
        private static TextBoxX _txtBuildingAddress2;

        private static MetroTilePanel _pnlCopyAddressButton;
        private static ItemContainer _itmCopyAddressButtons;
        private static MetroTileItem _btnCopyAddress;

        private static LabelX _lblBuildingCity;
        private static TextBoxX _txtBuildingCity;
        private static LabelX _lblBuildingState;
        private static ComboBoxEx _cboBuildingState;
        private static LabelX _lblBuildingZip;
        private static TextBoxX _txtBuildingZip;
        private static TextBoxX _txtBuildingZipExt;

        private static LabelX _lblBuildingElectricAccountNumber;
        private static TextBoxX _txtBuildingElectricAccountNumber;
        private static LabelX _lblBuildingElectricRateCode;
        private static TextBoxX _txtBuildingElectricRateCode;
        private static LabelX _lblBuildingGasAccountNumber;
        private static TextBoxX _txtBuildingGasAccountNumber;
        private static LabelX _lblBuildingGasRateCode;
        private static TextBoxX _txtBuildingGasRateCode;

        private static TabControlPanel _pnlBuildingDetails;

        private static LabelX _lblBuildingNumUnits;
        private static TextBoxX _txtBuildingNumUnits;
        private static LabelX _lblBuildingYearBuilt;
        private static TextBoxX _txtBuildingYearBuilt;
        private static LabelX _lblBuildingRoofType;
        private static TextBoxX _txtBuildingRoofType;
        private static LabelX _lblBuildingSafetyConcerns;
        private static TextBoxX _txtBuildingSafetyConcerns;
        private static LabelX _lblBuildingWallType;
        private static TextBoxX _txtBuildingWallType;
        private static LabelX _lblBuildingSpecialEquipment;
        private static TextBoxX _txtBuildingSpecialEquipment;
        private static LabelX _lblBuildingFoundation;
        private static TextBoxX _txtBuildingFoundation;
        private static LabelX _lblBuildingComments;
        private static TextBoxX _txtBuildingComments;

        private static LabelX _lblBuildingOccupants;
        private static IntegerInput _numBuildingOccupants;
        private static LabelX _lblBuildingFloorsAbove;
        private static IntegerInput _numBuildingFloorsAbove;
        private static LabelX _lblBuildingFloorsBelow;
        private static IntegerInput _numBuildingFloorsBelow;
        private static LabelX _lblBuildingGrossFloor;
        private static IntegerInput _numBuildingGrossFloor;
        private static LabelX _lblBuildingOccupiedFloor;
        private static IntegerInput _numBuildingOccupiedFloor;

        private static MetroTilePanel _pnlBuildingSaveButtons;
        private static ItemContainer _itmBuildingSaveButtons;
        private static MetroTileItem _btnBuildingCancel;
        private static MetroTileItem _btnBuildingSave;

        private static TextBoxX _txtBuildingHoursEquivalent;
        private static TextBoxX _txtBuildingId;

        #endregion Registered controls

        #endregion Private member variables

        #region Exposed class methods (static)

        #region Control methods

        internal static void RegisterControls(ListViewEx lstBuildingList, MetroTilePanel pnlBuildingEditButtons, ItemContainer itmBuildingEditButtons, MetroTileItem btnBuildingAdd, MetroTileItem btnBuildingCopy,
            MetroTileItem btnBuildingEdit, MetroTileItem btnBuildingDelete, LabelX lblNew, LabelX lblEdit, LabelX lblCopy, TabControl tabBuildingInfo, TabControlPanel pnlBuildingGeneral,
            LabelX lblBuildingCategory, ComboBoxEx cboBuildingCategory, LabelX lblBuildingType, ComboBoxEx cboBuildingType, LabelX lblBuildingName, TextBoxX txtBuildingName, LabelX lblBuildingAddress,
            TextBoxX txtBuildingAddress1, TextBoxX txtBuildingAddress2, MetroTilePanel pnlCopyAddressButton, ItemContainer itmCopyAddressButtons, MetroTileItem btnCopyAddress, LabelX lblBuildingCity,
            TextBoxX txtBuildingCity, LabelX lblBuildingState, ComboBoxEx cboBuildingState, LabelX lblBuildingZip, TextBoxX txtBuildingZip, TextBoxX txtBuildingZipExt, LabelX lblBuildingElectricAccountNumber,
            TextBoxX txtBuildingElectricAccountNumber, LabelX lblBuildingElectricRateCode, TextBoxX txtBuildingElectricRateCode, LabelX lblBuildingGasAccountNumber, TextBoxX txtBuildingGasAccountNumber,
            LabelX lblBuildingGasRateCode, TextBoxX txtBuildingGasRateCode, TabControlPanel pnlBuildingDetails, LabelX lblBuildingNumUnits, TextBoxX txtBuildingNumUnits, LabelX lblBuildingYearBuilt,
            TextBoxX txtBuildingYearBuilt, LabelX lblBuildingRoofType, TextBoxX txtBuildingRoofType, LabelX lblBuildingSafetyConcerns, TextBoxX txtBuildingSafetyConcerns, LabelX lblBuildingWallType,
            TextBoxX txtBuildingWallType, LabelX lblBuildingSpecialEquipment, TextBoxX txtBuildingSpecialEquipment, LabelX lblBuildingFoundation, TextBoxX txtBuildingFoundation, LabelX lblBuildingComments,
            TextBoxX txtBuildingComments, LabelX lblBuildingOccupants, IntegerInput numBuildingOccupants, LabelX lblBuildingFloorsAbove, IntegerInput numBuildingFloorsAbove, LabelX lblBuildingFloorsBelow,
            IntegerInput numBuildingFloorsBelow, LabelX lblBuildingGrossFloor, IntegerInput numBuildingGrossFloor, LabelX lblBuildingOccupiedFloor, IntegerInput numBuildingOccupiedFloor, MetroTilePanel pnlBuildingSaveButtons,
            ItemContainer itmBuildingSaveButtons, MetroTileItem btnBuildingCancel, MetroTileItem btnBuildingSave, TextBoxX txtBuildingHoursEquivalent, TextBoxX txtBuildingId)
        {
            _lstBuildingList = lstBuildingList;

            _pnlBuildingEditButtons = pnlBuildingEditButtons;
            _itmBuildingEditButtons = itmBuildingEditButtons;
            _btnBuildingAdd = btnBuildingAdd;
            _btnBuildingCopy = btnBuildingCopy;
            _btnBuildingEdit = btnBuildingEdit;
            _btnBuildingDelete = btnBuildingDelete;

            _lblNew = lblNew;
            _lblEdit = lblEdit;
            _lblCopy = lblCopy;

            _tabBuildingInfo = tabBuildingInfo;

            _pnlBuildingGeneral = pnlBuildingGeneral;

            _lblBuildingCategory = lblBuildingCategory;
            _cboBuildingCategory = cboBuildingCategory;
            _lblBuildingType = lblBuildingType;
            _cboBuildingType = cboBuildingType;

            _lblBuildingName = lblBuildingName;
            _txtBuildingName = txtBuildingName;
            _lblBuildingAddress = lblBuildingAddress;
            _txtBuildingAddress1 = txtBuildingAddress1;
            _txtBuildingAddress2 = txtBuildingAddress2;

            _pnlCopyAddressButton = pnlCopyAddressButton;
            _itmCopyAddressButtons = itmCopyAddressButtons;
            _btnCopyAddress = btnCopyAddress;

            _lblBuildingCity = lblBuildingCity;
            _txtBuildingCity = txtBuildingCity;
            _lblBuildingState = lblBuildingState;
            _cboBuildingState = cboBuildingState;
            _lblBuildingZip = lblBuildingZip;
            _txtBuildingZip = txtBuildingZip;
            _txtBuildingZipExt = txtBuildingZipExt;

            _lblBuildingElectricAccountNumber = lblBuildingElectricAccountNumber;
            _txtBuildingElectricAccountNumber = txtBuildingElectricAccountNumber;
            _lblBuildingElectricRateCode = lblBuildingElectricRateCode;
            _txtBuildingElectricRateCode = txtBuildingElectricRateCode;
            _lblBuildingGasAccountNumber = lblBuildingGasAccountNumber;
            _txtBuildingGasAccountNumber = txtBuildingGasAccountNumber;
            _lblBuildingGasRateCode = lblBuildingGasRateCode;
            _txtBuildingGasRateCode = txtBuildingGasRateCode;

            _pnlBuildingDetails = pnlBuildingDetails;

            _lblBuildingNumUnits = lblBuildingNumUnits;
            _txtBuildingNumUnits = txtBuildingNumUnits;
            _lblBuildingYearBuilt = lblBuildingYearBuilt;
            _txtBuildingYearBuilt = txtBuildingYearBuilt;
            _lblBuildingRoofType = lblBuildingRoofType;
            _txtBuildingRoofType = txtBuildingRoofType;
            _lblBuildingSafetyConcerns = lblBuildingSafetyConcerns;
            _txtBuildingSafetyConcerns = txtBuildingSafetyConcerns;
            _lblBuildingWallType = lblBuildingWallType;
            _txtBuildingWallType = txtBuildingWallType;
            _lblBuildingSpecialEquipment = lblBuildingSpecialEquipment;
            _txtBuildingSpecialEquipment = txtBuildingSpecialEquipment;
            _lblBuildingFoundation = lblBuildingFoundation;
            _txtBuildingFoundation = txtBuildingFoundation;
            _lblBuildingComments = lblBuildingComments;
            _txtBuildingComments = txtBuildingComments;

            _lblBuildingOccupants = lblBuildingOccupants;
            _numBuildingOccupants = numBuildingOccupants;
            _lblBuildingFloorsAbove = lblBuildingFloorsAbove;
            _numBuildingFloorsAbove = numBuildingFloorsAbove;
            _lblBuildingFloorsBelow = lblBuildingFloorsBelow;
            _numBuildingFloorsBelow = numBuildingFloorsBelow;
            _lblBuildingGrossFloor = lblBuildingGrossFloor;
            _numBuildingGrossFloor = numBuildingGrossFloor;
            _lblBuildingOccupiedFloor = lblBuildingOccupiedFloor;
            _numBuildingOccupiedFloor = numBuildingOccupiedFloor;

            _pnlBuildingSaveButtons = pnlBuildingSaveButtons;
            _itmBuildingSaveButtons = itmBuildingSaveButtons;
            _btnBuildingCancel = btnBuildingCancel;
            _btnBuildingSave = btnBuildingSave;

            _txtBuildingHoursEquivalent = txtBuildingHoursEquivalent;
            _txtBuildingId = txtBuildingId;
        }

        internal static bool ValidateControls()
        {
            return (
                _lstBuildingList != null &&
                _pnlBuildingEditButtons != null &&
                _itmBuildingEditButtons != null &&
                _btnBuildingAdd != null &&
                _btnBuildingCopy != null &&
                _btnBuildingEdit != null &&
                _btnBuildingDelete != null &&
                _lblNew != null &&
                _lblEdit != null &&
                _lblCopy != null &&
                _tabBuildingInfo != null &&
                _pnlBuildingGeneral != null &&
                _lblBuildingCategory != null &&
                _cboBuildingCategory != null &&
                _lblBuildingType != null &&
                _cboBuildingType != null &&
                _lblBuildingName != null &&
                _txtBuildingName != null &&
                _lblBuildingAddress != null &&
                _txtBuildingAddress1 != null &&
                _txtBuildingAddress2 != null &&
                _pnlCopyAddressButton != null &&
                _itmCopyAddressButtons != null &&
                _btnCopyAddress != null &&
                _lblBuildingCity != null &&
                _txtBuildingCity != null &&
                _lblBuildingState != null &&
                _cboBuildingState != null &&
                _lblBuildingZip != null &&
                _txtBuildingZip != null &&
                _txtBuildingZipExt != null &&
                _lblBuildingElectricAccountNumber != null &&
                _txtBuildingElectricAccountNumber != null &&
                _lblBuildingElectricRateCode != null &&
                _txtBuildingElectricRateCode != null &&
                _lblBuildingGasAccountNumber != null &&
                _txtBuildingGasAccountNumber != null &&
                _lblBuildingGasRateCode != null &&
                _txtBuildingGasRateCode != null &&
                _pnlBuildingDetails != null &&
                _lblBuildingNumUnits != null &&
                _txtBuildingNumUnits != null &&
                _lblBuildingYearBuilt != null &&
                _txtBuildingYearBuilt != null &&
                _lblBuildingRoofType != null &&
                _txtBuildingRoofType != null &&
                _lblBuildingSafetyConcerns != null &&
                _txtBuildingSafetyConcerns != null &&
                _lblBuildingWallType != null &&
                _txtBuildingWallType != null &&
                _lblBuildingSpecialEquipment != null &&
                _txtBuildingSpecialEquipment != null &&
                _lblBuildingFoundation != null &&
                _txtBuildingFoundation != null &&
                _lblBuildingComments != null &&
                _txtBuildingComments != null &&
                _lblBuildingOccupants != null &&
                _numBuildingOccupants != null &&
                _lblBuildingFloorsAbove != null &&
                _numBuildingFloorsAbove != null &&
                _lblBuildingFloorsBelow != null &&
                _numBuildingFloorsBelow != null &&
                _lblBuildingGrossFloor != null &&
                _numBuildingGrossFloor != null &&
                _lblBuildingOccupiedFloor != null &&
                _numBuildingOccupiedFloor != null &&
                _pnlBuildingSaveButtons != null &&
                _itmBuildingSaveButtons != null &&
                _btnBuildingCancel != null &&
                _btnBuildingSave != null &&
                _txtBuildingHoursEquivalent != null &&
                _txtBuildingId != null
            );
        }

        #endregion Control methods

        #endregion Exposed class methods (static)
    }
}