using System;
using System.Collections.Generic;
using System.Text;
using Bosco.Utility.Common;

namespace Payroll.Model.UIModel
{
    public class clsEvalExpr
    {
        public string BuildComponentIdFromFormula(string sFormula)
        {
            int i, nPos, nPos1, nStartPos;
            string sCompId = "";
            string sCompId1 = "";
            string sComp = "";
            string sOnlyComponentId = "ê";
            sCompId = sFormula;
            nStartPos = 0;
            try
            {
                for (i = 0; i < sFormula.Length; i++)
                {
                    nPos = sFormula.IndexOf("<", nStartPos);
                    if (nPos >= 0)
                    {
                        nStartPos = nPos + 1;
                        nPos1 = sFormula.IndexOf(">", nStartPos);
                        nStartPos = nPos1 + 1;

                        if (nPos1 > 0)
                        {

                            sComp = sFormula.Substring(nPos, nPos1 - (nPos - 1));

                            sCompId1 = sComp;

                            sComp = sComp.Replace("<", "");
                            sComp = sComp.Replace(">", "");

                            GetComponentId(ref sComp);


                            sCompId = sCompId.Replace(sCompId1, sComp.Trim());	//sCompId=sCompId.Replace(sComp,sCompId1);
                            nStartPos = nPos + sCompId1.Length;

                            sComp = sComp.Replace("<", "");
                            sComp = sComp.Replace(">", "");

                            if (sOnlyComponentId.IndexOf("ê" + sComp.Trim() + "ê") == -1)
                            {
                                sOnlyComponentId += sComp.Trim() + "ê";
                            }
                        }
                        else
                            break;
                    }
                    else
                        break;
                }

            }
            catch (Exception ex)
            {
                string str = ex.Message;
                return sOnlyComponentId;
            }
            if (sOnlyComponentId == "ê") sOnlyComponentId = "";

            return sOnlyComponentId;
        }
        public double EvaluateExpression(string sVal1, int nEvlMethod)
        {
            //	nEvlMethod = 0-Evaluate by using VB Language products
            //	1-Evaluate by using Excel Object
            if (nEvlMethod == 1)
                return e_evalExcel(sVal1);
            return 0;
        }
        private void RemoveWhiteSpace(string sFormula)
        {
            string sText = "";
            sText = sFormula.Trim();
        }
        private double e_evalExcel(string sVal1)
        {
            double dRetnVal;
            //Excel.ApplicationClass objExcel=new Excel.ApplicationClass();

            try
            {
                //object objCheck=objExcel.Evaluate(sVal1);
                object objCheck = (clsGeneral.ObjExcel).Evaluate(sVal1);
                dRetnVal = double.Parse((objCheck.ToString()));
                dRetnVal = double.Parse(dRetnVal.ToString("#,##0.00"));
                return dRetnVal;
            }
            catch //(Exception ex) //commented to avoid warning by pe
            {
                return 0;
            }
        }
        private bool GetComponentId(ref string sComp)
        {
            clsModPay objMod = new clsModPay();
            string strComp = objMod.GetValue("PRComponent", "ComponentId", "Component ='" + sComp + "'");

            sComp = Convert.ToChar(160).ToString() + "<" + strComp + ">" + Convert.ToChar(160).ToString();
            return true;
        }
        public string GetComponentName(string sCompid, string field)
        {
            clsModPay objMod = new clsModPay();
            return objMod.GetValue("PRComponent", field, "ComponentId =" + sCompid);
        }

    }
}
