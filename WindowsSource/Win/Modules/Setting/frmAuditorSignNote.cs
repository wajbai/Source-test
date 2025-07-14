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
    public partial class frmAuditorSignNote : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        private frmFinanceBaseAdd parentfrm;
        #endregion

        #region Property
       
        #endregion

        #region Constructors
        public frmAuditorSignNote(frmFinanceBaseAdd fromParent)
        {
            InitializeComponent();
            parentfrm = fromParent;
        }

        #endregion

        #region Methods

      
        #endregion

        #region Events

        private void frmFinanceSetting_Load(object sender, EventArgs e)
        {
            AssignAuditorNoteSignDetails();
            //rchAuditorSignNote.AddHandler(KeyDownEvent, new System.Windows.Input.KeyEventHandler(RichEditControl_KeyDown), true);  
            //rtxtAuditorNote.Options.DocumentCapabilities.Paragraphs = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            //string txt = @"{\rtf1\deff0{\fonttbl{\f0 Calibri;}}{\colortbl\red0\green0\blue0 ;\red0\green0\blue255 ;}{\*\defchp \fs22}{\*\listoverridetable}{\stylesheet {\ql\fs22 Normal;}{\*\cs1\fs22 Default Paragraph Font;}{\*\cs2\sbasedon1\fs22 Line Number;}{\*\cs3\ul\fs22\cf1 Hyperlink;}{\*\ts4\tsrowd\fs22\ql\trautofit1\tscellpaddfl3\tscellpaddl108\tscellpaddfr3\tscellpaddr108\tsvertalt\cltxlrtb Normal Table;}{\*\ts5\tsrowd\sbasedon4\fs22\ql\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trautofit1\tscellpaddfl3\tscellpaddl108\tscellpaddfr3\tscellpaddr108\tsvertalt\cltxlrtb Table Simple 1;}}\nouicompat\splytwnine\htmautsp\sectd\pard\plain\qc{\lang1033\langfe1033\b\fs22\cf0 EXAMINED AND FOUND CORRECT}\lang1033\langfe1033\b\fs22\par\pard\plain\qc{\lang1033\langfe1033\b\fs22\cf0 FOR S.RAMESH BABU & CO.}\lang1033\langfe1033\b\fs22\par\pard\plain\qc{\lang1033\langfe1033\b\fs22\cf0 CHARTERED ACCOUNTS}\lang1033\langfe1033\b\fs22\cf0\par\pard\plain\qc\lang1033\langfe1033\b\fs22\cf0\par\pard\plain\qc\lang1033\langfe1033\b\fs22\par\trowd\irow0\irowband0\ts5\trgaph108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trleft-108\trftsWidth2\trwWidth5000\trautofit1\trpaddfl3\trpaddl108\trpaddfr3\trpaddr108\tblindtype3\tblind0\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4350\clpadfr3\clpadr108\clpadft3\clpadt108\cellx690\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth5340\clpadfr3\clpadr108\clpadft3\clpadt108\cellx6255\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4845\clpadfr3\clpadr108\clpadft3\clpadt108\cellx11325\pard\plain\ql\intbl\yts5{\lang1033\langfe1033\b\fs22\cf0 Date : }\lang1033\langfe1033\b\fs22\cell\pard\plain\qc\intbl\yts5\lang1033\langfe1033\b\fs22\cell\pard\plain\qc\intbl\yts5\lang1033\langfe1033\b\fs22\cell\trowd\irow0\irowband0\ts5\trgaph108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trleft-108\trftsWidth2\trwWidth5000\trautofit1\trpaddfl3\trpaddl108\trpaddfr3\trpaddr108\tblindtype3\tblind0\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4350\clpadfr3\clpadr108\clpadft3\clpadt108\cellx690\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth5340\clpadfr3\clpadr108\clpadft3\clpadt108\cellx6255\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4845\clpadfr3\clpadr108\clpadft3\clpadt108\cellx11325\row\pard\plain\ql\intbl\yts5{\lang1033\langfe1033\b\fs22\cf0 Place: }\lang1033\langfe1033\b\fs22\cell\pard\plain\qc\intbl\yts5{\lang1033\langfe1033\b\fs22\cf0 PARTENER }\lang1033\langfe1033\b\fs22\cell\pard\plain\qc\intbl\yts5{\lang1033\langfe1033\b\fs22\cf0 SISTER IN CHARGE}\lang1033\langfe1033\b\fs22\cell\trowd\irow1\irowband1\lastrow\ts5\trgaph108\trbrdrt\brdrs\brdrw10\trbrdrl\brdrs\brdrw10\trbrdrb\brdrs\brdrw10\trbrdrr\brdrs\brdrw10\trbrdrh\brdrs\brdrw10\trbrdrv\brdrs\brdrw10\trleft-108\trftsWidth2\trwWidth5000\trautofit1\trpaddfl3\trpaddl108\trpaddfr3\trpaddr108\tblindtype3\tblind0\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4350\clpadfr3\clpadr108\clpadft3\clpadt108\cellx690\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth5340\clpadfr3\clpadr108\clpadft3\clpadt108\cellx6255\clvertalt\clbrdrt\brdrnone\clbrdrl\brdrnone\clbrdrb\brdrnone\clbrdrr\brdrnone\cltxlrtb\clftsWidth3\clwWidth4845\clpadfr3\clpadr108\clpadft3\clpadt108\cellx11325\row\pard\plain\qj{\lang1033\langfe1033\b\fs22\cf0 \tab \tab \tab \tab }\lang1033\langfe1033\b\fs22\par}";
            //rtxtAuditorNote.RtfText = txt;
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string auditornoterrtf = rtxtAuditorNote.RtfText;
                string auditornoter = rtxtAuditorNote.Text;
                
                if (!string.IsNullOrEmpty(auditornoter))
                {
                    using (UISetting uisetting = new UISetting())
                    {
                        /*Document document = rtxtAuditorNote.Document;

                        // Create a new table style.
                        TableStyle tStyleMain = document.TableStyles.CreateNew();

                        // Specify table style options.
                        tStyleMain.TableBorders.InsideHorizontalBorder.LineThickness = 0;
                        tStyleMain.TableBorders.InsideHorizontalBorder.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableBorders.InsideVerticalBorder.LineThickness = 0;
                        tStyleMain.TableBorders.InsideVerticalBorder.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableBorders.Top.LineThickness = 0; 
                        tStyleMain.TableBorders.Top.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableBorders.Bottom.LineThickness = 0;
                        tStyleMain.TableBorders.Bottom.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableBorders.Left.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableBorders.Right.LineStyle = TableBorderLineStyle.None;
                        tStyleMain.TableLayout = TableLayoutType.Fixed;
                        tStyleMain.Name = "MyTableStyle";

                        // Add the style to the collection of styles.
                        document.TableStyles.Add(tStyleMain);
                        document.Tables[0].Style = tStyleMain;*/

                        
                        /*
                        ResultArgs resultarg = uisetting.SaveAuditorNoteSign(auditornoterrtf);
                                                
                        if (resultarg.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                        }
                        AssignAuditorNoteSignDetails();*/
                    }
                }
                else
                {
                    this.ShowMessageBox("Auditor Note is empty");
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

        private void AssignAuditorNoteSignDetails()
        {
            using (UISetting uisetting = new UISetting())
            {
                ResultArgs resultarg = uisetting.FetchAuditorSignNote();

                this.rtxtAuditorNote.RtfText = string.Empty;
                if (resultarg.Success && resultarg.ReturnValue!=null)
                {
                    this.rtxtAuditorNote.RtfText = resultarg.ReturnValue.ToString();
                }
            }
        }
        #endregion

        

        

    }
}