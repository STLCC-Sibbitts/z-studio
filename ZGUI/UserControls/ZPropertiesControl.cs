#region Using directives
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using System.Configuration;
using Excel = Microsoft.Office.Interop.Excel;
using ZLib.ZRubric;
using ZLib;
#endregion


namespace ZGUI
{
	public partial class ZPropertiesControl : UserControl
	{
		private ZRubricSchema m_rubricSchema = null;
		private ZExcelViewer m_zExcelViewer = null;
		//private Excel.Application m_oExcelApp;	// currently active excel app
		private ZToken m_zToken;		// this is the context we'll use to display properties
		private TVRubric m_tvRubric;
		private bool propertyValueEditCancelled = true;

		// grab the excel app from the associated viewer
		public Excel.Application oExcelApp
		{
			get { return m_zExcelViewer.oExcelApp; }
		}
		public ZExcelViewer zExcelViewer
		{
			get { return m_zExcelViewer; }
			set { m_zExcelViewer = value; }
		}
		public ZPropertiesControl()
		{
			InitializeComponent();

			cboPropertyValue.Hide();
			btnProperties.Hide();
		}
		private JObject rubricStyle
		{
			get
			{
				JObject jObj = null;
				if (m_rubricSchema != null)
					jObj = m_rubricSchema.oSchema;
				return jObj;
			}
			set { ; }
		}
		public ZRubricSchema rubricSchema
		{
			set
			{
				m_rubricSchema = value;
			}
			
		}
		public TVRubric tvRubric
		{
			get { return m_tvRubric; }
			set
			{
				m_tvRubric = value;

			}
		}
		public ZToken zToken
		{
			get { return m_zToken; }
			set
			{
				m_zToken = value;
				// need to do some stuff, probably add ZProperties
				dgNodeProperties.Rows.Clear();
				if (m_zToken == null)
					return;
				// load the properties for the selected item
				string tag = m_zToken.rPath;
				dgNodeProperties.Tag = tag;
				// TODO: hookup the schema!!!
				foreach (ZProperty prop in m_zToken.zProperties)
				{
					int rowIndex = dgNodeProperties.Rows.Add();
					DataGridViewRow row = dgNodeProperties.Rows[rowIndex];
					string propName = prop.name;
					Debug.Print(propName + " is type:" + prop.Type.ToString());
					string propValue = prop.value;
					row.Cells[0].Value = propName;
					row.Cells[1].Value = propValue;
					row.Tag = prop;
				}
				dgNodeProperties.Refresh();
				this.ResumeLayout();
			}

		}
		#region prop editing

		private void dgNodeProperties_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			propertyValueEditCancelled = true;	// by default, the edit was cancelled
			string senderType = sender.GetType().ToString();
			DataGridView cell = sender as DataGridView;
			DataGridViewRow row = cell.CurrentRow;
			if (cboPropertyValue.Visible)
			{
				row.Cells[0].Selected = false;
				row.Cells[1].Selected = true;
				cboPropertyValue.Tag = (sender as DataGridView).CurrentRow;
			}
			else if (btnProperties.Visible)
			{
				row.Cells[0].Selected = true;
				row.Cells[1].Selected = false;
			}
			// Debug.Print("beginEdit" + row.Cells[0].Value + ", " + row.Cells[1].Value);
		}
		// TODO: add code for cancel edit to clear items
		// also need to update json
		private void dgNodeProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			string propName = dgNodeProperties.CurrentRow.Cells[0].Value.ToString();

			if (!propertyValueEditCancelled)
			{
				string newValue = "";
				DataGridViewRow row = (sender as DataGridView).CurrentRow;
				ZProperty prop = row.Tag as ZProperty;
				// TODO: based on what's visible, update underlying property value
				if (cboPropertyValue.Visible) // cboPropertyValue.Items.Count > 0)
				{
					newValue = cboPropertyValue.SelectedItem.ToString();	// .SelectedItem.ToString(); ;
					prop.value = newValue;
					// reference the property value cell
					dgNodeProperties.CurrentRow.Cells[1].Value = newValue;
					cboPropertyValue.Hide();
					cboPropertyValue.Items.Clear();
				}
				else
				{
					prop.value = row.Cells[1].Value.ToString();
				}
				Debug.Print("endEdit");
			}
			else
				Debug.Print("ediEdit - user cancelled");
		}

		private void dgNodeProperties_Validating(object sender, CancelEventArgs e)
		{
			Debug.Print("Validating");
		}

		private void dgNodeProperties_CancelRowEdit(object sender, QuestionEventArgs e)
		{
			Debug.Print("cancelled edit");
		}

		private void dgNodeProperties_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		{
			Debug.Print("Validating");
			propertyValueEditCancelled = false;
			// only update the value when we are validating, and not escaping
			if (cboPropertyValue.Visible)
			{
				if (cboPropertyValue.Items.Count > 0)
				{
					string newValue = cboPropertyValue.Text;	// .SelectedItem.ToString(); ;
					dgNodeProperties.CurrentRow.Cells[1].Value = newValue;
				}
			}
		}

		private void btnProperties_Click(object sender, EventArgs e)
		{
			MessageBox.Show("clicked on properties object");
		}

		#endregion

		#region propertyControlHandling
		// DataGridViewCellEventArgs
		private void PropertySelected(object sender, DataGridViewCellEventArgs e)
		{
			// first thing to do is to hide existing property value controls
			btnProperties.Hide();
			cboPropertyValue.Hide();
			cboPropertyValue.Items.Clear();

			propertyValueEditCancelled = true;	// by default, the edit was cancelled
			string senderType = sender.GetType().ToString();
			DataGridView dgvProperties = sender as DataGridView;
			DataGridViewRow row = dgvProperties.CurrentRow;
			// DataGridViewCell col = dgvProperties.CurrentCell;
			DataGridViewCell propCell = row.Cells[1];
			// TODO: create json to represent hierarchy of the objects/properties/values,...
			string rPath = dgNodeProperties.Tag.ToString();
			string propName = row.Cells[0].Value.ToString();
			Debug.Print("propRow:" + row.Index.ToString() + ", propPath:" + rPath + ", propName:" + propName);
			if (rPath.Length == 0)
				return;

			JToken tokProp = rubricStyle.SelectToken(rPath + propName);
			JArray props = tokProp as JArray;
			if (props != null)
			{
				// load the combo box with the associated properties
				foreach (JValue val in props)
				{
					string theVal = val.ToString();
					cboPropertyValue.Items.Add(theVal);
				}
				// now display it
				cboPropertyValue.Text = propCell.Value.ToString();
				// always use the property cell
				Rectangle rect = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true);
				cboPropertyValue.Location = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true).Location;
				cboPropertyValue.Width = rect.Width;
				cboPropertyValue.Show();
				Debug.Print("doArray" + row.Cells[0].Value + ", " + row.Cells[1].Value);
				return;
			}
			JObject oProp = tokProp as JObject;
			if (oProp != null)
			{
				row.Cells[0].Selected = true;
				row.Cells[1].Selected = false;
				// if this is location, we need to display the button
				btnProperties.Location = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true).Location;

				Rectangle rect = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true);
				// cell.GetCellDisplayRectangle(1, e.RowIndex, true);
				btnProperties.Width = rect.Height;
				btnProperties.Height = rect.Height;
				btnProperties.Left = rect.Right - btnProperties.Width;
				btnProperties.Top = rect.Top;
				btnProperties.Parent = dgvProperties;
				btnProperties.Tag = dgNodeProperties.Tag;
				btnProperties.Visible = true;
				btnProperties.Show();
			}
		}
		#endregion
		#region drag-n-drop
		private void dgNodeProperties_DragDrop(object sender, DragEventArgs e)
		{
			PasteIt();
		}

		private void dgNodeProperties_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			Point loc = dgNodeProperties.GetCellDisplayRectangle(1, dgNodeProperties.CurrentRow.Index, true).Location;
			Debug.Print("loc:" + loc.ToString());
			Debug.Print("e.loc:" + e.Location.ToString());

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				cmnuStep.Show(dgNodeProperties, loc);
			}
		}

		private void dgNodeProperties_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			e.Action = DragAction.Drop;
		}

		private void dgNodeProperties_DragEnter(object sender, DragEventArgs e)
		{

			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
			{
				e.Effect = DragDropEffects.All;
			}
			e.Effect = DragDropEffects.All;
		}

		private void cmnuPaste_Click(object sender, EventArgs e)
		{
			cmnuStep.Hide();
			PasteIt();
		}
		private void Excel_DragDrop(object sender, DragEventArgs e)
		{
			string sheetName = "";
			Excel.Range range = AM.Interop.Excel.Clipboard.GetRange(e.Data as System.Runtime.InteropServices.ComTypes.IDataObject, out sheetName);
			PasteIt();
			var data = e.Data;
			e.Effect = DragDropEffects.None;
		}
		private void PasteIt()
		{
			// if we're dragging, we're doing it from the current app
			string sheetName = "";
			Excel.Range range = null;
			if (oExcelApp.Selection != null)
			{
				sheetName = oExcelApp.ActiveSheet.Name;
				range = oExcelApp.Selection.Cells;
			}
			else
				range = AM.Interop.Excel.Clipboard.GetRange(out sheetName);

			if (range != null)
			{
				string loc = "Worksheets[" + sheetName + "].Cells[" + range.get_Address(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing) + "]";
				bool hasFormula = (bool)range.HasFormula;
				string formula = "", rFormula = "";
				if (hasFormula)
				{
					formula = range.Formula.ToString();
					formula = range.FormulaLocal.ToString();
					rFormula = range.FormulaR1C1.ToString();
					// formula = range.FormulaR1C1Local.ToString();
				}
				else
				{
					loc = range.Value.ToString();
					loc = range.Text;
				}
				MessageBox.Show("Got something from Excel: " + loc
					+ ", value[" + range.Value.ToString() + "]\n" 
					+ "formula[" + formula + "]\n"
					+ "rformula[" + rFormula + "]");
			}

		}


		#endregion

		private void cboPropertyValue_SelectionChangeCommitted(object sender, EventArgs e)
		{
			DataGridViewRow row = cboPropertyValue.Tag as DataGridViewRow;
			dgNodeProperties_CellEndEdit(dgNodeProperties, null);
		}

	}
}
