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
using JWC;
//using Microsoft.Office.Interop.Excel;
using Excel = Microsoft.Office.Interop.Excel; 
#endregion

namespace JsonExplorer
{
	public partial class FrmProps : UserControl		// Form
	{

		private JObject rubricStyle = null;
		//Excel Application Object 
		private Excel.Application oExcelApp;
		private ZObject m_zObject;		// this is the context we'll use to display properties
		private TVRubric m_tvRubric;
		private ComboBox cboPropertyValue = new ComboBox();
		private bool propertyValueEditCancelled = true;

		public FrmProps( ZRubricSchema rubricSchema )
		{
			InitializeComponent();

			cboPropertyValue.Visible = false;
			btnProperties.Hide();
			dgNodeProperties.Controls.Add(cboPropertyValue);
			dgNodeProperties.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgNodeProperties_CellContentClick);
			
			rubricStyle = rubricSchema.oSchema;
			return;
		}
		public TVRubric tvRubric
		{
			get { return m_tvRubric; }
			set
			{
				m_tvRubric = value;
				
			}
		}
		public ZObject zObject
		{
			get { return m_zObject; }
			set
			{
				m_zObject = value;
				// need to do some stuff, probably add ZProperties
				//Debug.Print("tvJson_AfterSelect:" + m_zObject.name );
				dgNodeProperties.Rows.Clear();
				// load the properties for the selected item
				string tag = m_zObject.rPath;
				dgNodeProperties.Tag = tag;
				// TODO: hookup the schema!!!
				foreach (ZProperty prop in m_zObject.zProperties)
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
		#region myJsonPathStuff
		public string GetJKey(JToken tok)
		{
			return GetJKey(tok, true);
		}
		public string GetJKey(JToken tok, bool useIndexAsKey)
		{
			string jKey = "";
			// if I can get the property, it is the name of the object
			JProperty jProp = tok as JProperty;
			if (jProp != null)
				jKey = jProp.Name;
			else
			{
				// see if the current tok is a child in an array
				JArray aParent = tok.Parent as JArray;
				bool isChildToken = (aParent != null);
				JObject oCurrent = tok as JObject;
				if (oCurrent != null)
				{
					if (isChildToken)
					{
						int childIndex = -1;
						// figure out the index for this node
						for (JToken tPrevSibling = tok; tPrevSibling != null; tPrevSibling = tPrevSibling.Previous)
						{
							++childIndex;
						}
						if (jKey == "" || useIndexAsKey)
							jKey = "[" + childIndex.ToString() + "]";
						else
							jKey = "[" + jKey + "]";
					}
				}
			}

			return jKey;
		}
		public string GetJPath(JToken tok)
		{
			return GetJPath(tok, true);
		}
		// return path for use in rubric
		public string GetRPath(JToken tok)
		{
			string rPath = "";
			string partDelim = "";	// what delimiter to use 
			string part = "";
			// need to walk the hierarchy for this token, backwards
			for (JToken current = tok; current != null; current = current.Parent)
			{
				if ((current as JArray) != null) // treat arrays as anonymous and skip to previous token
					continue;

				// strip off brackets
				part = GetJKey(current, false).Split('[')[0];	// .Replace("[", "").Replace("]", "");
				if (part.Length > 0)
				{
					rPath = part + partDelim + rPath;
					partDelim = ".";	// what delimiter to use, after the first one
				}
			}
			// if we have a path, we will append the delimiter by default
			if (rPath.Length > 0)
				rPath += ".";
			return rPath;
		}

		public string GetJPath(JToken tok, bool useIndexAsKey)
		{
			string jPath = "";
			string partDelim = ".";	// what delimiter to use 
			string part = "";
			// need to walk the hierarchy for this token, backwards
			for (JToken current = tok; current != null; current = current.Parent)
			{
				// partDelim = ".";
				bool isArray = ((current as JArray) != null);
				if (isArray)	// treat arrays as anonymous and skip to previous token
				{
					partDelim = "";		// no delim before array elements
					continue;
				}

				part = GetJKey(current, useIndexAsKey);
				if (part.Length > 0)
				{
					if (jPath.Length == 0)
						jPath = part;
					else
					{
						jPath = part + partDelim + jPath;
						partDelim = ".";	// reset to default delimiter
					}
				}
			}
			return jPath;
		}
		#endregion
		#region prop events
		private void dgProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			PropertySelected(sender, e);
		}

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
				// TODO: based on what's visible, update underlying property value
				if (cboPropertyValue.Visible) // cboPropertyValue.Items.Count > 0)
				{
					string newValue = cboPropertyValue.Text;	// .SelectedItem.ToString(); ;
					// reference the property value cell
					dgNodeProperties.CurrentRow.Cells[1].Value = newValue;
					cboPropertyValue.Hide();
					cboPropertyValue.Items.Clear();
				}
				Debug.Print("endEdit");
			}
			else
				Debug.Print("ediEdit - user cancelled");
		}

		private void dgNodeProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			PropertySelected(sender, e);
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

		private void dgNodeProperties_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			PropertySelected(sender, e);
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

		private void dgNodeProperties_DragDrop(object sender, DragEventArgs e)
		{
			// PasteIt();
		}

		private void dgNodeProperties_Click(object sender, EventArgs e)
		{
			string t = sender.GetType().ToString();
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
		#endregion

		#region TVRubric events
		private void tvJson_AfterSelect(object sender, TreeViewEventArgs e)
		{
			//Debug.Print("tvJson_AfterSelect:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());
			//dgNodeProperties.Rows.Clear();
			//// load the properties for the selected item
			//JToken tok = e.Node.Tag as JToken;
			//if (tok == null)
			//    return;
			//string tag = GetRPath(tok); // +"Properties.";
			//dgNodeProperties.Tag = tag;
			//JObject jObj = tok as JObject;	// tok["Properties"] as JObject;
			//if (jObj != null)
			//{
			//    foreach (JProperty prop in jObj.Properties())
			//    {
			//        int rowIndex = dgNodeProperties.Rows.Add();
			//        DataGridViewRow row = dgNodeProperties.Rows[rowIndex];
			//        string propName = prop.Name;
			//        Debug.Print(propName + " is type:" + prop.Type.ToString());
			//        string propValue = prop.Value.ToString();
			//        row.Cells[0].Value = propName;
			//        row.Cells[1].Value = propValue;
			//        row.Tag = prop;
			//    }
			//}

		}

		#endregion


		#region Excel stuff
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
			Excel.Range rng = null;
			if (oExcelApp.Selection != null)
				rng = oExcelApp.Selection.Cells;
			string sheetName = "";
			Excel.Range range = AM.Interop.Excel.Clipboard.GetRange(out sheetName);

			if (range != null)
			{
				string loc = "Worksheets[" + sheetName + "].Cells[" + range.get_Address(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing) + "]";
				bool hasFormula = (bool)range.HasFormula;
				if (hasFormula)
				{
					string formula = range.Formula.ToString();
					formula = range.FormulaLocal.ToString();
					formula = range.FormulaR1C1.ToString();
					formula = range.FormulaR1C1Local.ToString();
				}
				else
				{
					loc = range.Value.ToString();
					loc = range.Text;
				}
			}

		}

		private void cmnuPaste_Click(object sender, EventArgs e)
		{
			cmnuStep.Hide();
			PasteIt();
		}

		#endregion
	}
}
