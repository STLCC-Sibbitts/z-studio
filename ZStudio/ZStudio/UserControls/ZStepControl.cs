using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using JWC;
using Excel = Microsoft.Office.Interop.Excel;
using ZLib.ZRubric;
using ZLib;
using ZStudio;

namespace ZStudio.UserControls
{
	public partial class ZStepControl : UserControl
	{
#region Properties
		private ZStep m_zStep;
		private ZExcelViewer m_zExcelViewer = null;
#endregion
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

		public ZStepControl()
		{
			InitializeComponent();
//			DataGridViewColumn col = null;
			dgTasks.Columns["colName"].Tag = ZTask.Tags.Name;
			dgTasks.Columns["colText"].Tag = ZTask.Tags.Text;
			dgTasks.Columns["colPts"].Tag = ZTask.Tags.Pts;
			dgTasks.Columns["colType"].Tag = ZTarget.Tags.Item;
			dgTasks.Columns["colProperty"].Tag = ZTask.Tags.Property;
//			dgTasks.Columns["colLocation"].Tag = ZTask.locationTag;
		}
		public ZStep zStep
		{
			get { return m_zStep; }
			set
			{
				m_zStep = value;
				if (value == null)
					return;
				Program.statusStrip.Items[0].Text = m_zStep.name;
				Program.statusStrip.Update();

				// now populate the control with the values
				txtStep.Text = zStep.step;
				txtName.Text = zStep.name;
				txtPts.Text = zStep.pts.ToString();
				// don't refresh until after rows are assigned
				dgTasks.SuspendLayout();
				dgTasks.Rows.Clear();	// clear out whatever else was there
				dgTasks.Rows.Add(zStep.tasks.Count );
				int rowIdx = 0;
				foreach (ZTask task in zStep.tasks)
				{
					dgTasks.Rows[rowIdx].Tag = task;
					UpdateTaskRow(task, dgTasks.Rows[rowIdx]);
					++rowIdx;
				}
				dgTasks.ResumeLayout();
				dgTasks.Refresh();
			}
		}
		private void UpdateTaskRow(ZTask task, DataGridViewRow row)
		{
			row.Cells["colName"].Value = task.name;
			row.Cells["colText"].Value = task.text;
			row.Cells["colPts"].Value = task.pts.ToString();
			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["colType"];
			cell.Value = task.target;
			cell = (DataGridViewComboBoxCell)row.Cells["colProperty"];
			cell.Value = task.target.property;
//			row.Cells["colLocation"].Value = task.location;
		}
		public string step
		{
			get { return txtStep.Text; }
			set 
			{
 				// TODO: this will be modified to format and setup colored links to the
				// associated tasks
				txtStep.Text = value; 
			}
		}
		public string name
		{
			get { return txtName.Text; }
			set { txtName.Text = value; }
		}
		public string pts
		{
			get { return txtPts.Text; }
			set { txtPts.Text = value; }
		}

		private void scStep_Panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void txtStep_TextChanged(object sender, EventArgs e)
		{

		}

		private void txtName_TextChanged(object sender, EventArgs e)
		{

		}

		private void dgTasks_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
				e.Action = DragAction.Drop;
		}

		private void dgTasks_DragDrop(object sender, DragEventArgs e)
		{
			// Determine which category the item was draged to
			Point p = dgTasks.PointToClient(new Point(e.X, e.Y));
			DataGridView.HitTestInfo hi = dgTasks.HitTest(p.X, p.Y);

			// Need to see if we can figure out which row we're over
			DataGridViewCell currentCell = null;
			DataGridViewRow currentRow = null;
			if (hi.ColumnIndex >= 0)
			{
				currentCell = dgTasks.Rows[hi.RowIndex].Cells[hi.ColumnIndex];
				currentRow = dgTasks.Rows[hi.RowIndex];
			}
			// if we have text, handle that first
			// TODO: need to be smart about this, if it's actually coming from Excel, we need to know
			// foreach( DataFormats.Format fmt in DataFormats )

			string symLink = "";
			foreach (string fmt in e.Data.GetFormats())
				Debug.WriteLine("clipboard has " + fmt + " data on it");
			if (e.Data.GetDataPresent(DataFormats.SymbolicLink))
				symLink = e.Data.GetData(DataFormats.SymbolicLink).ToString();
			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Copy;
				currentCell.Value = e.Data.GetData(DataFormats.Text).ToString();
			}
			else
				PasteIt(currentRow);
		}

		private void PasteIt(DataGridViewRow currentRow)
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
					//"Location":
					//{
					//    "Type":"Cell",
					//    "Worksheets":"Order Form", 
					//    "Cells":"$C$4",
					//    "Path":"Worksheets[Order Form].Cells[$C$4]"
					//},
			// for formulas, save Value and RelativeValue
			if (range != null)
			{
				string loc = "Worksheets[" + sheetName + "].Cells[" + range.get_Address(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing) + "]";
				currentRow.Cells["colLocation"].Value = loc;
				bool hasFormula = (bool)range.HasFormula;
				string formula = "", rFormula = "";
				if (hasFormula)
				{
					currentRow.Cells["colProperty"].Value = "Formula";
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

			// m_dropCell = null;
		}

		private void dgTasks_DragEnter(object sender, DragEventArgs e)
		{

			if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
			{
				e.Effect = DragDropEffects.All;
			}
			e.Effect = DragDropEffects.All;
		}

		private void dgTasks_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgTasks_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
		}

		private void dgTasks_DragOver(object sender, DragEventArgs e)
		{
		}

		private void dgTasks_DragLeave(object sender, EventArgs e)
		{
		}

		private void dgTasks_MouseEnter(object sender, EventArgs e)
		{
		}

		private void dgTasks_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
		}

		private void dgTasks_MouseHover(object sender, EventArgs e)
		{
		}

		private void scStep_Panel1_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		{
			e.Action = DragAction.Drop;
		}

		private void dgTasks_CancelRowEdit(object sender, QuestionEventArgs e)
		{
			string msg = "dgTasks_CancelRowEdit:" + e.ToString() + ":" + e.GetType().ToString();
			Program.statusStrip.Items[0].Text = msg;
			Program.statusStrip.Update();

		}

		private void dgTasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			
			if (row.Tag == null)
				return;
			ZTask zTask = (ZTask)row.Tag;
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			string colName = (string)dgTasks.Columns[cell.ColumnIndex].Tag;
			zTask[colName].Value = cell.Value;
		}

		private void dgTasks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			string msg = e.ToString() + ":" + e.GetType().ToString();
			Program.statusStrip.Items[0].Text = msg;
			Program.statusStrip.Update();
			DataGridViewRow row = e.Row;
			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["colType"];
//			cell.DefaultNewRowValue = "Content";
			cell = (DataGridViewComboBoxCell)row.Cells["colProperty"];
//			cell.DefaultNewRowValue = "Value";

		}

		private void dgTasks_EditModeChanged(object sender, EventArgs e)
		{
			string msg = e.ToString() + ":" + e.GetType().ToString();
			Program.statusStrip.Items[0].Text = msg;
			Program.statusStrip.Update();

		}
		private string selectedText;
		private string selectedStepText;
		private Excel.Range selectedStepRange;
		private ZStepLoc selectedStepLoc;

		private void txtStep_MouseDown(object sender, MouseEventArgs e)
		{
			
			if (selectedText == null)
				return;
			Point p = new Point(e.X, e.Y);
			// if this is a right click, see if we can determine what is under the mouse pointer
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				int charPos = 0;
				selectedStepRange = null;
				selectedStepText = "";
				selectedStepLoc = new ZStepLoc();
				charPos = txtStep.GetCharIndexFromPosition(p);
				charPos = txtStep.GetCharIndexFromPosition(p);
				while (charPos > 0 && Regex.IsMatch(txtStep.Text.Substring(charPos, 1), "[A-Za-z0-9:]"))
				{
					--charPos;
				}
				string cellText = ZExpr.CellRangeText(txtStep.Text.Substring(charPos));
				if (cellText.Length > 0)
				{
					if (m_zExcelViewer.SelectRange(cellText))
						selectedStepRange = m_zExcelViewer.oExcelApp.Selection;
				}
				if (selectedText.Length > 0)
				{
					selectedStepText = selectedText;
					selectedStepLoc.startPos = txtStep.SelectionStart;
					selectedStepLoc.length = txtStep.SelectionLength;
					if ( selectedStepRange == null )
						selectedStepRange = m_zExcelViewer.Find(selectedText);
					if (selectedStepRange != null)
					{
						cellText = selectedStepRange.Address;
						m_zExcelViewer.SelectRange(cellText);
						//selectedStepRange = m_zExcelViewer.oExcelApp.Selection;
					}
				}
				this.cmnuTask.Show(txtStep, p);
			}
			else if (selectedText.Length > 0)
			{
				txtStep.DoDragDrop(selectedText, DragDropEffects.Copy);
			}
			else
			{
				selectedStepRange = null;
				selectedStepText = "";
				selectedStepLoc = null;
			}
			selectedText = null;		// reset
		}

		private void txtStep_MouseUp(object sender, MouseEventArgs e)
		{
			selectedText = "";
			if (txtStep.SelectionLength > 0)
				selectedText = txtStep.SelectedText;
		}

		private void cmnuTaskUpdate_Click(object sender, EventArgs e)
		{
			cmnuTask.Hide();
			// process gathered information
			DataGridViewRow row = dgTasks.CurrentRow;
			ZTask task = (ZTask)row.Tag;
			SynchSelectedTaskRow(row);
		}
		private void SynchSelectedTaskRow(DataGridViewRow row )
		{
			ZTask task = (ZTask)row.Tag;
			int rIndex = row.Index;
			rIndex += (int)'a';
			task.name = Convert.ToChar((int)'a' + row.Index).ToString();
			task.text = this.selectedStepText;
			task.stepLocation = this.selectedStepLoc;
			// take care of other stuff later.
			if (selectedStepRange != null)
			{
				string sheetName = selectedStepRange.Worksheet.Name;
				string loc = "Worksheets[" + sheetName + "].Cells["
						+ selectedStepRange.get_Address(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing) + "]";
				task.target.location.context = sheetName;
				// TODO: will need to consider escaping square brackets for relative cell references
				loc = "Worksheets[" + sheetName + "].Cells["
						+ selectedStepRange.get_Address(false, false, Excel.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing) + "]";
// TODO fix relPath stuff				task.location.relPath = loc;
			}
			UpdateTaskRow(task, row);

		}
		private void cmnuTaskAdd_Click(object sender, EventArgs e)
		{
			cmnuTask.Hide();
			// TODO: confirm that the last row is a "new" row
			// dgTasks.Rows.Add();
			DataGridViewRow row = dgTasks.Rows[dgTasks.Rows.Count - 1];
			// did we already add a task
			ZTask task = (ZTask)row.Tag;
			if (task == null)
			{
				task = new ZTask();
				m_zStep.tasks.Add(task);
				row.Tag = task;
			}
			SynchSelectedTaskRow(row);
		}

		private void dgTasks_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			//if (m_zStep != null)
			//{
			//    ZTask zTask = new ZTask();
			//    m_zStep.zTasks.Add(zTask);
			//    dgTasks.Rows[e.RowIndex].Tag = zTask;
			//}
		}


		private void dgTasks_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		{
			DataGridViewRow row = e.Row;
			if (row.Tag == null)
				return;

		}

		private void dgTasks_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			// was the row edit cancelled
			if (e.Cancel == true)
			{
				dgTasks.Rows.Remove(dgTasks.Rows[e.RowIndex]);
				return;
			}
			// once editing is done, we can validate?
			// this is where I will actually add or update the row
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			string value;
			double dblValue;
			DataGridViewCell valueCell = row.Cells["colPts"];
			if ( ! double.TryParse(valueCell.Value.ToString(),out dblValue) )
			{
				// make sure they don't exceed pts for the step
				e.Cancel = true;	// failed to parse pts
				valueCell.ErrorText = "Pts needs to be numeric";
			}
			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["colType"];
			value  = cell.Value.ToString();
			cell = (DataGridViewComboBoxCell)row.Cells["colProperty"];
			value = cell.Value.ToString();
		}

		private void dgTasks_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			DataGridViewRow row = e.Row;

		}

		private void dgTasks_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			// this is where I will actually add or update the row
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			// adding or updating
			ZTask task = (ZTask)row.Tag;
			if (task == null)
			{
				task = new ZTask();
				m_zStep.tasks.Add(task);
				task.name = row.Cells["colName"].Value.ToString();
				task.text = row.Cells["colText"].Value.ToString(); 
				task.pts = double.Parse( row.Cells["colPts"].Value.ToString());
				DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["colType"];
				task.target.location.address = cell.Value.ToString();
				cell = (DataGridViewComboBoxCell)row.Cells["colProperty"];
				task.target.property = cell.Value.ToString();
			}
		}

		private void dgTasks_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			if (e.StateChanged == DataGridViewElementStates.Visible)
				return;
		}

		private void dgTasks_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
		{
			bool response = e.Response;
			e.Response = true;
		}

		private void dgTasks_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			ZTask task = (ZTask)row.Tag;
			if (task != null)
			{
				UpdateTaskRow(task, row);
			}
		}

		private void dgTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

	}
}
