using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.TDS;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.TDS
{
    public partial class frmDeducteeTaxView : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        public DataView dvDeducteeType = null;
        DeducteeTaxSystem deduteeTaxSytem = new DeducteeTaxSystem();
        GridBand gridBandDefault = new GridBand();
        #endregion

        #region Constructor
        public frmDeducteeTaxView()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void frmDeducteeTaxView_Load(object sender, EventArgs e)
        {
            LoadDeducteeTypes();
        }

        private void glkpDeducteeType_EditValueChanged(object sender, EventArgs e)
        {
            if (glkpDeducteeType.EditValue != null)
            {
                if (dvDeducteeType.Count > 0)
                {
                    dvDeducteeType.RowFilter = String.Format("{0}={1}", deduteeTaxSytem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName, glkpDeducteeType.EditValue.ToString());

                    if (dvDeducteeType.Count > 0)
                    {
                        lblResidentStatus.Text = dvDeducteeType[0][deduteeTaxSytem.AppSchema.DeducteeTypes.RESIDENTIAL_STATUSColumn.ColumnName].ToString();
                        lblDeducteeStatus.Text = dvDeducteeType[0][deduteeTaxSytem.AppSchema.DeducteeTypes.DEDUCTEE_TYPEColumn.ColumnName].ToString();
                        lblStatus.AppearanceItemCaption.ForeColor = Color.Green;
                        lblStatus.Text = dvDeducteeType[0][deduteeTaxSytem.AppSchema.DeducteeTypes.STATUSColumn.ColumnName].ToString();
                    }
                    dvDeducteeType.RowFilter = "";
                }
                LoadTaxDetails();
            }
        }

        private void bandedgvDeducteeType_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            colDelete.OptionsColumn.AllowFocus = false;
            colbandNOP.OptionsColumn.AllowEdit = true;
            colDelete.Width = 25;
        }

        private void bandedgvDeducteeType_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

            colDelete.OptionsColumn.AllowFocus = true;
            colbandNOP.OptionsColumn.AllowEdit = false;
            DataTable dtData = gcDeduteeDetails.DataSource as DataTable;
            DataView dvData = new DataView(dtData);
            dvData.Sort = "NATURE_PAY_ID," + deduteeTaxSytem.AppSchema.DutyTax.APPLICABLE_FROMColumn.ColumnName;
            gcDeduteeDetails.DataSource = dvData.ToTable();
            lblRecordCount.Text = "# " + bandedgvDeducteeType.RowCount.ToString();
        }

        private void bandedgvDeducteeType_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            if (bandedgvDeducteeType != null)
            {
                if (bandedgvDeducteeType.IsNewItemRow(e.RowHandle))
                {
                    string ApplicableFromDate = bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandApplicableFrom) != null ? bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandApplicableFrom).ToString() : string.Empty;
                    string NatureOfPayments = bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandNOP) != null ? bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandNOP).ToString() : string.Empty;
                    int NOPId = bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandPolicyId) != null ? UtilityMember.NumberSet.ToInteger(bandedgvDeducteeType.GetRowCellValue(e.RowHandle, colbandPolicyId).ToString()) : 0;
                    if (!string.IsNullOrEmpty(NatureOfPayments))
                    {
                        if (!string.IsNullOrEmpty(ApplicableFromDate))
                        {
                            DataTable dtData = gcDeduteeDetails.DataSource as DataTable;
                            DataView dvTaxDetails = new DataView(dtData);
                            dvTaxDetails.RowFilter = "CONVERT(Isnull(APPLICABLE_FROM,''), System.String) <> ''";
                            dvTaxDetails.RowFilter = String.Format("APPLICABLE_FROM='{0}' AND NATURE_PAY_ID={1}", ApplicableFromDate, NOPId);
                            DataTable dtDuplicate = dvTaxDetails.ToTable();
                            if (dtDuplicate != null && dtDuplicate.Rows.Count > 0)
                            {
                                ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_DATE_DUPLICATE));
                                gcDeduteeDetails.Select();
                                bandedgvDeducteeType.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
                                bandedgvDeducteeType.FocusedColumn = bandedgvDeducteeType.VisibleColumns[1];
                                bandedgvDeducteeType.ShowEditor();
                                e.Valid = false;
                            }
                        }
                        else
                        {
                            ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_APPLICABLE_FROM_EMPTY));
                            gcDeduteeDetails.Select();
                            bandedgvDeducteeType.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
                            bandedgvDeducteeType.FocusedColumn = bandedgvDeducteeType.VisibleColumns[1];
                            bandedgvDeducteeType.ShowEditor();
                            e.Valid = false;
                        }
                    }
                    else
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_POLICY_NATURE_OF_PAYMENT_EMPTY));
                        gcDeduteeDetails.Select();
                        bandedgvDeducteeType.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
                        bandedgvDeducteeType.FocusedColumn = bandedgvDeducteeType.VisibleColumns[0];
                        bandedgvDeducteeType.ShowEditor();
                        e.Valid = false;
                    }
                }
            }
        }

        private void bandedgvDeducteeType_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = DevExpress.XtraEditors.Controls.ExceptionMode.NoAction;
        }

        private void bandedgvDeducteeType_RowClick(object sender, RowClickEventArgs e)
        {
            if (bandedgvDeducteeType.IsNewItemRow(e.RowHandle))
            {
                colbandNOP.OptionsColumn.AllowEdit = true;
            }
            else
            {
                colbandNOP.OptionsColumn.AllowEdit = false;
            }
        }

        private void rDelete_Click(object sender, EventArgs e)
        {
            int NOPId = bandedgvDeducteeType.GetRowCellValue(bandedgvDeducteeType.FocusedRowHandle, colbandPolicyId) != null ? UtilityMember.NumberSet.ToInteger(bandedgvDeducteeType.GetRowCellValue(bandedgvDeducteeType.FocusedRowHandle, colbandPolicyId).ToString()) : 0;
            if (NOPId > 0)
            {
                if (ShowConfirmationMessage(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_CONFIRM_DELETE_POLICY_DEFINED), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandApplicableFrom, DBNull.Value);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandTDSRate, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandTDSExemptionLimit, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandSurchargeRate, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandSurchargeExcemption, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandEdCessRate, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandEdCessExemptionLimit, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandSecEdCessRate, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandSecEdCessExemption, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandTDSWithoutPan, 0);
                    bandedgvDeducteeType.SetFocusedRowCellValue(colbandTDSExemptionLimitWithoutPan, 0);
                    //For adding of more Taxtype the above functionality can be repeated for the new coloumn
                }
            }
            //else ShowMessageBox("No record is available to Delete");
            else ShowMessageBox(this.GetMessage(MessageCatalog.TDS.NatureofPayments.TDS_NORECORD_AVAILABLE_GRID_INFO));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DeducteeTaxSystem taxSytem = new DeducteeTaxSystem())
                {
                    taxSytem.DeducteeTypeId = glkpDeducteeType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpDeducteeType.EditValue.ToString()) : 0;
                    taxSytem.TaxPolicyId = glkpDeducteeType.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpDeducteeType.EditValue.ToString()) : 0;

                    DataTable dtTaxDetails = gcDeduteeDetails.DataSource as DataTable;
                    if (dtTaxDetails != null && dtTaxDetails.Rows.Count > 0)
                    {
                        DataView dvTaxDetails = new DataView(dtTaxDetails);
                        dvTaxDetails.RowFilter = "CONVERT(Isnull(APPLICABLE_FROM,''), System.String) <> ''";
                        taxSytem.dtTaxDetails = dvTaxDetails.ToTable();
                        ShowWaitDialog();
                        resultArgs = taxSytem.SaveDutyTaxDetails();
                        if (resultArgs != null && resultArgs.Success)
                        {
                            ShowSuccessMessage(GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        }
                        CloseWaitDialog();
                    }
                    else
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        #endregion

        #region Methods
        private void LoadDeducteeTypes()
        {
            using (DeducteeTypeSystem deductee = new DeducteeTypeSystem())
            {
                resultArgs = deductee.FetchActiveDeductTypes();
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dvDeducteeType = resultArgs.DataSource.Table.DefaultView;
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDeducteeType, resultArgs.DataSource.Table, "NAME", "DEDUCTEE_TYPE_ID");
                    glkpDeducteeType.EditValue = glkpDeducteeType.Properties.GetKeyValue(0);
                }
            }
        }

        private void LoadTaxDetails()
        {
            using (DeducteeTaxSystem tax = new DeducteeTaxSystem())
            {
                tax.DeducteeTypeId = glkpDeducteeType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpDeducteeType.EditValue.ToString()) : 0;
                if (resultArgs.Success)
                {
                    ApplyBandedView();
                    resultArgs = tax.FetchDeducteeTaxDetails();
                    gcDeduteeDetails.DataSource = resultArgs.DataSource.Table;
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpNOP, tax.NOP(), "NAME", "NATURE_PAY_ID");
                    lblRecordCount.Text = "# " + bandedgvDeducteeType.RowCount.ToString();
                }
            }
        }

        private void ApplyBandedView()
        {
            if (gridBandDefault.Columns.Count == 0)
            {
                using (DeducteeTaxSystem TaxType = new DeducteeTaxSystem())
                {
                    resultArgs = TaxType.FetchActiveDutyTaxTypes();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        if (resultArgs.DataSource.Table != null)
                        {
                            foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                            {
                                string Name = dr[deduteeTaxSytem.AppSchema.DutyTaxType.TAX_TYPE_NAMEColumn.ColumnName].ToString();
                                string TaxTypeName = Name.ToUpper();
                                switch (TaxTypeName)
                                {
                                    case "TDS WITH PAN":
                                        BandedGridColumn[] TDSBandedColumns = new BandedGridColumn[] { colbandTDSRate, colbandTDSExemptionLimit };
                                        SetBandView(gridBandDefault, Name, TDSBandedColumns);
                                        break;
                                    case "TDS WITHOUT PAN":
                                        GridBand gridBandTDSWithoutPan = new GridBand();
                                        BandedGridColumn[] TDSBandedColumnsWithoutPan = new BandedGridColumn[] { colbandTDSWithoutPan, colbandTDSExemptionLimitWithoutPan };
                                        SetBandView(gridBandTDSWithoutPan, Name, TDSBandedColumnsWithoutPan);
                                        break;
                                    case "SURCHARGE":
                                        GridBand gridBandTDS = new GridBand();
                                        BandedGridColumn[] SurchargeBandedColumns = new BandedGridColumn[] { colbandSurchargeRate, colbandSurchargeExcemption };
                                        SetBandView(gridBandTDS, Name, SurchargeBandedColumns);
                                        break;
                                    case "ED CESS":
                                        GridBand gridBandEdCess = new GridBand();
                                        BandedGridColumn[] EdCessBandedColumns = new BandedGridColumn[] { colbandEdCessRate, colbandEdCessExemptionLimit };
                                        SetBandView(gridBandEdCess, Name, EdCessBandedColumns);
                                        break;
                                    case "SEC ED CESS":
                                        GridBand gridBandSecEdCess = new GridBand();
                                        BandedGridColumn[] bandedColumns = new BandedGridColumn[] { colbandSecEdCessRate, colbandSecEdCessExemption };
                                        SetBandView(gridBandSecEdCess, Name, bandedColumns);
                                        break;
                                }
                            }
                            GridBand gridBandDelete = new GridBand();
                            gridBandDelete.OptionsBand.ShowCaption = false;
                            BandedGridColumn[] bandedColumnsDelete = new BandedGridColumn[] { colDelete };
                            SetBandView(gridBandDelete, "Delete", bandedColumnsDelete);
                        }
                    }
                    else
                        ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_TAX_TYPE_NOT_EXISTS));
                }
            }
        }

        private void SetBandView(GridBand gridBand, string BandCaption, BandedGridColumn[] BandChildColumns)
        {
            gridBand.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            gridBand.Name = BandCaption;
            gridBand.AppearanceHeader.Options.UseTextOptions = true;
            gridBand.AppearanceHeader.Options.UseFont = true;
            gridBand.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridBand.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            int nrOfColumns = BandChildColumns.Length;
            BandedGridColumn[] bandedColumns = new BandedGridColumn[nrOfColumns];
            foreach (BandedGridColumn col in BandChildColumns)
            {
                gridBand.Columns.Add(col);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion




    }
}