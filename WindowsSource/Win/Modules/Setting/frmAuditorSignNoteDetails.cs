using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;


using Bosco.Utility;
using Bosco.Model.Setting;
using ACPP.Modules.Transaction;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using ACPP.Modules.Data_Utility;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using DevExpress.XtraEditors;
using DevExpress.Utils.Menu;
using System.Reflection;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Utils;



namespace ACPP.Modules
{
    public partial class frmAuditorSignNoteDetails : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        DataTable dtEnumAuditorSignNote = new DataTable();
        DataTable dtAuditorNoteSignDetails = new DataTable();
        private frmFinanceBaseAdd parentfrm;
        #endregion

        #region Property
       
        #endregion

        #region Constructors
        public frmAuditorSignNoteDetails(frmFinanceBaseAdd fromParent)
        {
            InitializeComponent();
            parentfrm = fromParent;

            AuditorSignNote auditornotesign = new AuditorSignNote();
            DataView dvEnumAuditorNoteSign = this.UtilityMember.EnumSet.GetEnumDataSource(auditornotesign, Sorting.Ascending);
            dtEnumAuditorSignNote = dvEnumAuditorNoteSign.ToTable();

            lgFYAuditorNoteSignDetails.Text = "Auditor Note Sign Details for " + UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false).ToString("MMM yyyy") + " - " +
                                                                     UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false).ToString("MMM yyyy");
        }

        #endregion

        #region Methods

        private ResultArgs SaveAuditorSignNote()
        {
            ResultArgs resultarg = new ResultArgs();
            
            using (UISetting uisetting = new UISetting())
            {
                if (dtEnumAuditorSignNote != null)
                {
                    dtAuditorNoteSignDetails.Clear();
                    for (int i = 0; i < dtEnumAuditorSignNote.Rows.Count; i++)
                    {
                        AuditorSignNote SettingName = (AuditorSignNote)Enum.Parse(typeof(AuditorSignNote), dtEnumAuditorSignNote.Rows[i][1].ToString());
                        string Value = "";
                        switch (SettingName)
                        {

                            case AuditorSignNote.AuditorNote:
                                {
                                    Value = txtmeAuditorNotes.Text.Trim();
                                    break;
                                }
                            case AuditorSignNote.ShowDate:
                                {
                                    if (dtAuditorDate.DateTime != DateTime.MinValue)
                                    {
                                        Value = UtilityMember.DateSet.ToDate(dtAuditorDate.DateTime.ToShortDateString(), false).ToShortDateString();
                                    }
                                    break;
                                }
                            case AuditorSignNote.Place:
                                {
                                    Value = txtAuditorPlace.Text.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign1:
                                {
                                    Value = txtmeAuditorSign1.Text.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign2:
                                {
                                    Value = txtmeAuditorSign2.Text.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign3:
                                {
                                    Value = txtmeAuditorSign3.Text.Trim();
                                    break;
                                }
                        }
                        if (!string.IsNullOrEmpty(SettingName.ToString()))
                        {
                            DataRow dr = dtAuditorNoteSignDetails.NewRow();
                            dr[uisetting.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTINGColumn.ColumnName] = SettingName.ToString();
                            dr[uisetting.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTING_VALUEColumn.ColumnName] = Value.ToString();
                            dtAuditorNoteSignDetails.Rows.Add(dr);
                        }
                    }

                    resultarg = uisetting.SaveAuditorNoteSign(dtAuditorNoteSignDetails);

                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
                }
            }
            return resultarg;
        }

        private void AssignAuditorNoteSignDetails()
        {
            ResultArgs resultarg = new ResultArgs();
                        
            using (UISetting uisetting = new UISetting())
            {
                dtAuditorNoteSignDetails = uisetting.AppSchema.AuditorNoteSign.DefaultView.ToTable();
                resultarg = uisetting.FetchAuditorSignNote();
                if (resultarg.Success && resultarg.DataSource.Table != null)
                {
                    dtAuditorNoteSignDetails = resultarg.DataSource.Table;
                    foreach (DataRow dr in dtAuditorNoteSignDetails.Rows)
                    {
                        AuditorSignNote SettingName = (AuditorSignNote)Enum.Parse(typeof(AuditorSignNote), dr[uisetting.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTINGColumn.ColumnName].ToString());
                        string value = dr[uisetting.AppSchema.AuditorNoteSign.AUDITOR_NOTE_SETTING_VALUEColumn.ColumnName].ToString();
                        
                        switch(SettingName)
                        {
                           case AuditorSignNote.AuditorNote:
                                {
                                     txtmeAuditorNotes.Text = value.Trim();
                                    break;
                                }
                            case AuditorSignNote.ShowDate:
                                {
                                    if (!string.IsNullOrEmpty(value))
                                    {
                                        if (UtilityMember.DateSet.IsDate(value) && UtilityMember.DateSet.ToDate(value, false).Date != DateTime.MinValue)
                                        {
                                            dtAuditorDate.DateTime = UtilityMember.DateSet.ToDate(value, false).Date;
                                        }
                                    }
                                    break;
                                }
                            case AuditorSignNote.Place:
                                {
                                    txtAuditorPlace.Text = value.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign1:
                                {
                                    txtmeAuditorSign1.Text = value.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign2:
                                {
                                    txtmeAuditorSign2.Text = value.Trim();
                                    break;
                                }
                            case AuditorSignNote.Sign3:
                                {
                                    txtmeAuditorSign3.Text = value.Trim();
                                    break;
                                }
                        }
                      
                    }
                }

            }
        }

        #endregion

        #region Events

        private void frmFinanceSetting_Load(object sender, EventArgs e)
        {
            AssignAuditorNoteSignDetails();
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txtmeAuditorNotes.Text) || !string.IsNullOrEmpty(txtAuditorPlace.Text) ||
                    !string.IsNullOrEmpty(txtmeAuditorSign1.Text) || !string.IsNullOrEmpty(txtmeAuditorSign2.Text) || !string.IsNullOrEmpty(txtmeAuditorSign3.Text)
                    )
                {
                    using (UISetting uisetting = new UISetting())
                    {
                        resultArgs = SaveAuditorSignNote();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                            AssignAuditorNoteSignDetails();
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox("Auditor Sign Note detail(s) are empty");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void richEditControl_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Return)
            //{
            //    e.SuppressKeyPress = true;
            //}

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      
        #endregion

        

        

    }
}