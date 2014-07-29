using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Configuration;

//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel;
using ZLib;
using ZLib.ZRubric;
using ZGUI;
using Excel2Json;

namespace ZGUI.UserControls
{
	public partial class frmZTaskAdd : Form
	{
		private ZExcelToJson m_excelToJson = null;
		public ZExcelToJson excelToJson
		{
			set
			{
				m_excelToJson = value;
			}
		}

		public frmZTaskAdd(ZExcelToJson excelToJson, ZStep zStep, string key, string id)
		{
			InitializeComponent();
			cmdSynchTask.Enabled= false;
			cmdAddTask.Enabled	= false;
			m_excelToJson		= excelToJson;
			m_zStep				= zStep;
			string stepText		= m_zStep.text;
			m_zTask				= zStep.tasks.Add(key, id);
			txtTask.Text		= m_zTask.text;
			this.Refresh();
		}

		private void cmdSynchTask_Click(object sender, EventArgs e)
		{
			// This is where we get the information from Excel's current selection
			// if we don't have anything selected, asta
			if (m_excelToJson == null)
				return;
			txtTargetLocationAddress.Text = m_excelToJson.activeCellAddress;
			txtTargetLocationContext.Text = m_excelToJson.activeSheetName;
			txtAnswerValue.Text = m_excelToJson.activeCellText;
			if (cboProperty.Text == "Formula")
			{
				txtContentExpression.Text = m_excelToJson.activeCellFormula;
				m_zTask.answer.expression = m_excelToJson.activeCellFormula;
			}
			else if (cboProperty.Text == "FormulaR1C1")
			{
				txtContentExpression.Text = m_excelToJson.activeCellFormulaR1C1;
				m_zTask.answer.expression = m_excelToJson.activeCellFormulaR1C1;
			}
			// tagged expression?
			m_zTask.answer.taggedExpression = m_zTask.answer.expression;	// or assume no tags?
			m_zTask.target.type = cboTargetType.Text;
			m_zTask.target.property = cboProperty.Text;
			m_zTask.target.location.type = cboTargetLocationType.Text;
			m_zTask.target.location.address = txtTargetLocationAddress.Text;
			m_zTask.target.location.context = txtTargetLocationContext.Text;
			m_zTask.answer.type = "{^Target.Type}";
			m_zTask.answer.value = m_excelToJson.activeCellText;
		}

		private void cboTarget_SelectionChangeCommitted(object sender, EventArgs e)
		{
			bool ready = cboTargetType.Text.Length > 0 && cboProperty.Text.Length > 0 && cboTargetLocationType.Text.Length > 0;
			cmdSynchTask.Enabled = ready;
			cmdAddTask.Enabled = ready;
		}
		public string targetLocationAddress { get { return txtTargetLocationAddress.Text; } }
		public string targetLocationContext { get { return txtTargetLocationContext.Text; } }
		public string answerValue { get { return txtAnswerValue.Text; } }
		public string contentExpression { get { return txtContentExpression.Text; } }
		private ZStep m_zStep;
		private ZTask m_zTask;
		private void cmdRefreshJson_Click(object sender, EventArgs e)
		{
			string json = m_zStep.ToString();
			txtJson.Text = json;
		}
	}
}
