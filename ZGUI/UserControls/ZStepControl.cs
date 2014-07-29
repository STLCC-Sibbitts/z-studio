using System;
using System.Reflection;
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
using Excel = Microsoft.Office.Interop.Excel;
using ZLib.ZRubric;
using ZLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using Excel2Json;
namespace ZGUI
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
		public ZExcelViewer zExcelSubmission = null;
		public ZExcel zExcelRubric = null;

		public ZStepControl()
		{
			InitializeComponent();
//			DataGridViewColumn col = null;
//			dgTasks.Columns["colName"].Tag = ZTask.Tags.Name;
//			dgTasks.Columns["colText"].Tag = ZTask.Tags.Text;
//			dgTasks.Columns["colPts"].Tag = ZTask.Tags.Pts;
//			dgTasks.Columns["colType"].Tag = ZTarget.Tags.Item;
//			dgTasks.Columns["colProperty"].Tag = ZTask.Tags.Property;
//			dgTasks.Columns["colLocation"].Tag = ZTask.locationTag;
		}
		
		public ZStep zStep
		{
			get { return m_zStep; }
			set
			{
				dgTasks.VirtualMode = false;
				m_zStep = value;
				if (value == null)
					return;
				//TODO: redo status strip updating
				//Program.statusStrip.Items[0].Text = m_zStep.id;
				//Program.statusStrip.Update();
				// now populate the control with the values
				step = zStep.text;

				txtName.Text = zStep.key;
				txtPts.Text = zStep.pts.ToString();
				// don't refresh until after rows are assigned
				Debug.Print(MethodInfo.GetCurrentMethod().Name + ", calling dgTasks.SuspendLayout()");
				dgTasks.SuspendLayout();
				Debug.Print(MethodInfo.GetCurrentMethod().Name + ", calling dgTasks.Rows.Clear()");
				dgTasks.Rows.Clear();	// clear out whatever else was there
				int rowIdx = 0;
				foreach (ZTask task in zStep.tasks)
				{
					// just going to check tagged address
//					ZExpr addr = new ZExpr(task.target.location.address, task); 
//					string theTag = addr.rootNode.tag;
					rowIdx = dgTasks.Rows.Add();
					DataGridViewRow row = dgTasks.Rows[rowIdx++];
					row.Tag = task;
					Debug.Print(MethodInfo.GetCurrentMethod().Name + ", calling UpdateTaskRow for row: " + row.Index);
					// for the first row, we will make sure it is selected
					UpdateTaskRow(row, rowIdx == 1);
					// now autoadjust the height of the row
					int rowHeight = dgTasks.CurrentRow.GetPreferredHeight(rowIdx, DataGridViewAutoSizeRowMode.AllCellsExceptHeader,false);
					row.Height = rowHeight;
				}
				Debug.Print(MethodInfo.GetCurrentMethod().Name + ", calling dgTasks.ResumeLayout()");
				dgTasks.ResumeLayout();
				Debug.Print(MethodInfo.GetCurrentMethod().Name + ", selecting first row in dgTasks");
//				dgTasks.Rows[0].Selected = true;
				Debug.Print(MethodInfo.GetCurrentMethod().Name + ", calling dgTasks.Refresh()");
				dgTasks.Refresh();
//				dgTasks.Rows[0].Selected = true;
			}
		}
		private void UpdateTaskRow(DataGridViewRow row, bool selected = false)
		{
			if ( row == null || row.Tag == null )
				return;
			Debug.Print(MethodInfo.GetCurrentMethod().Name + ", row: " + row.Index + ", selected: " + selected.ToString());
			ZTask task = row.Tag as ZTask;
			row.Selected = selected;
			Debug.Print(MethodInfo.GetCurrentMethod().Name + ", row: " + row.Index + ", setting column values");
			// need to look at this and see if it really needs to be done
			string taskText = task.text;
			dgTasks.VirtualMode = false;
			row.Cells["colText"].Value = taskText;
			DataGridViewComboBoxCell cell = null;
			
			cell = (DataGridViewComboBoxCell)row.Cells["colCategory"];
			cell.Value = task.mapping.category;

			cell = (DataGridViewComboBoxCell)row.Cells["colDifficulty"];
			cell.Value = task.mapping.difficulty;

			row.Cells["colPts"].Value = task.pts.ToString();

			cell = (DataGridViewComboBoxCell)row.Cells["colAction"];
			cell.Value = task.mapping.action;
			if ( selected )
				UpdateTaskDetails(row);
		}
		public string step
		{
			get 
			{
				// TODO: this will need to be modified to deal with the tagged text?
				return txtStep.Text; 
			}
			set 
			{
				if (m_zStep == null || value.Length == 0)
					return;
				// associated tasks
				txtStep.Text = value;
				// color code the step text, just like Excel, also grey or bold the text for this task
				Font taggedFont = new Font(txtStep.Font, FontStyle.Bold | FontStyle.Underline);
				foreach (ZStepLoc stepLoc in m_zStep.stepLocs.Values)
				{
					// only color the tags
					if (stepLoc.locType == ZStepLoc.LocType.tag)
					{
						txtStep.Select(stepLoc.startPos, stepLoc.length);
						txtStep.SelectionFont = taggedFont;
						txtStep.SelectionColor = stepLoc.color;
						txtStep.DeselectAll();
					}
				}
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
		#region dragDrop
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
			// see if this is coming from our step text
			if ( sender.Equals(txtStep) )
			{
				// this is where we can get the information needed to generate the taskLoc information for this task

			}

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
		#endregion
		private void dgTasks_CancelRowEdit(object sender, QuestionEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			string msg = "dgTasks_CancelRowEdit:" + e.ToString() + ":" + e.GetType().ToString();
			//TODO: redo status strip updating
			//Program.statusStrip.Items[0].Text = msg;
			//Program.statusStrip.Update();

		}

		private void dgTasks_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			
			if (row.Tag == null)
				return;
			ZTask zTask = (ZTask)row.Tag;
			DataGridViewCell cell = row.Cells[e.ColumnIndex];
			// TODO_now: when row changes, set column tags to individual elements, tokens, or update zTask[colName] deal
			string colName = (string)dgTasks.Columns[cell.ColumnIndex].Tag;
			//TODO: serious fix needed here
			//zTask[colName].Value = cell.Value;
		}

		private void dgTasks_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			string msg = e.ToString() + ":" + e.GetType().ToString();
			//TODO: redo status strip updating
			//Program.statusStrip.Items[0].Text = msg;
			//Program.statusStrip.Update();
			DataGridViewRow row = e.Row;
			DataGridViewComboBoxCell cell = (DataGridViewComboBoxCell)row.Cells["colType"];
//			cell.DefaultNewRowValue = "Content";
			cell = (DataGridViewComboBoxCell)row.Cells["colProperty"];
//			cell.DefaultNewRowValue = "Value";
			Debug.Print(MethodInfo.GetCurrentMethod().Name + " - end");

		}
		public void TestGradeActiveTask()
		{
			DataGridViewRow row = dgTasks.CurrentRow;
			if (row.Tag == null)
				return;
			// make sure our submission has been loaded

			ZTask zTask = (ZTask)row.Tag;
			zTask.GradeSubmission(zTask.parentStep);
			int	deductionsFound = 0;
			// now see if there were any deductions for this task
			foreach (ZTaskDeduction taskDed in ZRubric.activeSubmission.taskDeductions)
			{
				if (taskDed.task.name == zTask.name)
				{
					string deductionComment = string.Format("Task[{0}] {1}\nName[{2}], Type[{3}], Points[{4}]",
						taskDed.task.name, taskDed.scenario.remediation.feedback, taskDed.scenario.name, taskDed.scenario.deduction.type, taskDed.pointsDeducted);
					zExcelSubmission.DeductionComment(zTask.target.location.context, zTask.target.location.address, deductionComment);
					MessageBox.Show(deductionComment);
					++deductionsFound;
					break;
				}
			}
			if ( deductionsFound == 0 )
				MessageBox.Show("No errors found for current task.");

		}
		private void dgTasks_EditModeChanged(object sender, EventArgs e)
		{
			string msg = e.ToString() + ":" + e.GetType().ToString();
			//TODO: redo status strip updating
			//Program.statusStrip.Items[0].Text = msg;
			//Program.statusStrip.Update();

		}
		private string selectedText;
		private string selectedStepText;
		private Excel.Range selectedStepRange;
//		private ZStepLoc selectedStepLoc;

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
//				selectedStepLoc = new ZStepLoc();
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
//					selectedStepLoc.startPos = txtStep.SelectionStart;
//					selectedStepLoc.length = txtStep.SelectionLength;
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
				// this is where I need to figure out what has been selected from the control
				txtStep.DoDragDrop(selectedText, DragDropEffects.Copy);
			}
			else
			{
				selectedStepRange = null;
				selectedStepText = "";
//				selectedStepLoc = null;
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
			Debug.Print(MethodInfo.GetCurrentMethod().Name + ", row: " + row.Index );
			ZTask task = (ZTask)row.Tag;
			int rIndex = row.Index;
			rIndex += (int)'a';
			task.name = Convert.ToChar((int)'a' + row.Index).ToString();
			task.text = this.selectedStepText;
//			task.stepLocation = this.selectedStepLoc;
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
			UpdateTaskRow(row);
			Debug.Print(MethodInfo.GetCurrentMethod().Name +  " - end");

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
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			//if (m_zStep != null)
			//{
			//    ZTask zTask = new ZTask();
			//    m_zStep.zTasks.Add(zTask);
			//    dgTasks.Rows[e.RowIndex].Tag = zTask;
			//}
		}


		private void dgTasks_NewRowNeeded(object sender, DataGridViewRowEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			DataGridViewRow row = e.Row;
			if (row.Tag == null)
				return;

		}

		private void dgTasks_RowValidating(object sender, DataGridViewCellCancelEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			// was the row edit cancelled
			if (e.Cancel == true)
			{
				dgTasks.Rows.Remove(dgTasks.Rows[e.RowIndex]);
				return;
			}
			return;
#if USE_THIS
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
#endif
		}

		private void dgTasks_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			DataGridViewRow row = e.Row;

		}

		private void dgTasks_RowValidated(object sender, DataGridViewCellEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			// this is where I will actually add or update the row
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			return;
			// adding or updating
#if DO_THIS
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
#endif
		}

		private void dgTasks_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			if (e.StateChanged == DataGridViewElementStates.Visible)
				return;
		}

		private void dgTasks_RowDirtyStateNeeded(object sender, QuestionEventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			bool response = e.Response;
			e.Response = false;		// TODO: verify?!?
		}

		private void dgTasks_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			Debug.Print(MethodInfo.GetCurrentMethod().Name + ", row: " + row.Index);
			if ( row.Selected )
				Debug.Print("isSelected");
			// UpdateTaskRow(row, row.Selected);
		}

		private void dgTasks_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{

		}

		private void txtStep_MouseHover(object sender, EventArgs e)
		{
		}

		private void txtStep_MouseLeave(object sender, EventArgs e)
		{
			txtStep.SelectAll();
			txtStep.SelectionBackColor = txtStep.BackColor;
			txtStep.DeselectAll();
		}

		private void txtStep_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_zStep == null)
				return;
			// see if mouse is over any of the text for a task
			// just turn background to light-gray
			int charPos = txtStep.GetCharIndexFromPosition( new Point(e.X, e.Y));
			//if ( charPos > 0 )
			//	charPos++;
			Color activeTaskBackColor = Color.LightGray;
			Color defaultBackColor = txtStep.BackColor;
			bool selectedTask = false;
			foreach (ZStepLoc stepLoc in m_zStep.stepLocs.Values)
			{
				if (stepLoc.locType == ZStepLoc.LocType.task)
				{
					string	stuff = "charPos: " + charPos + ", stepLoc: " + stepLoc.key + ", pos: " + stepLoc.startPos + ", len: " + stepLoc.length;
					if ((charPos >= stepLoc.startPos) && (charPos - stepLoc.startPos) < stepLoc.length)
					{
						stuff = "hit - " + stuff;
						txtStep.Select(stepLoc.startPos, stepLoc.length);
						txtStep.SelectionBackColor = activeTaskBackColor;
						txtStep.DeselectAll();
						selectedTask = true;
					}
					else
					{
						stuff = "miss - " + stuff;
						txtStep.Select(stepLoc.startPos, stepLoc.length);
						txtStep.SelectionBackColor = defaultBackColor;
						txtStep.DeselectAll();
					}
					Debug.Print(stuff);
				}
			}
			if (!selectedTask && m_zStep.stepLocs.Values.Count > 0)
			{
				txtStep_MouseLeave(sender, e);
			}

		}
		private void UpdateTaskDetails(DataGridViewRow row)
		{
			if ((row == null) || (!(row.Tag is ZTask)))
			{
				Debug.Print(MethodInfo.GetCurrentMethod().Name + " - resetting group settings");
				ResetGroupSettings(scTaskDetails.Panel1.Controls);
				ResetGroupSettings(scTaskDetails.Panel2.Controls);
			}
			else
			{
				// always reset before loading
				ResetGroupSettings(scTaskDetails.Panel1.Controls);
				ResetGroupSettings(scTaskDetails.Panel2.Controls);
				ResetGroupSettings(tabContent.Controls);
				Debug.Print(MethodInfo.GetCurrentMethod().Name + " - loading group settings");
				ZTask task = dgTasks.CurrentRow.Tag as ZTask;
				LoadGroupSettings(scTaskDetails.Panel1.Controls, task);
				LoadGroupSettings(scTaskDetails.Panel2.Controls, task);
				if (task.target.type == ZTarget.Types.Content)
				{
					LoadGroupSettings(tabContent.Controls, task);
				}
			}
		}
		private void dgTasks_SelectionChanged(object sender, EventArgs e)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name);
			// this is where I will actually add or update the row
			DataGridViewRow row = dgTasks.CurrentRow;
			UpdateTaskDetails(row);
		}
		private void LoadGroupSettings(Control.ControlCollection controlCollection, ZTask task)
		{

			Debug.Print(MethodInfo.GetCurrentMethod().Name + " for: " + controlCollection.Owner.Name);
			object taskValue;
			//Debug.Print("Group control:" + groupControl.Name);
			foreach (Control control in controlCollection)
			{
				if (control.Tag == null)
				{
					Debug.Print("Skipping: " + control.Name);
					continue;
				}
				ComboBox cboControl = null;
				if (control.Name.StartsWith("cbo"))
					cboControl = control as ComboBox;
				// the tag will either be the name of the preference, or it will be the preference object itself
				string tag = "";
				taskValue = null;
//				object tagValue = null;
				if (control.Tag is string)
				{
					tag = control.Tag.ToString();
					if (tag.Length == 0 || tag.StartsWith("#"))
						continue;
					JToken taskToken = task.SelectToken(tag);
					if (taskToken == null)
					{
//						if (tag == "Category" || tag == "Deduction")
//							tag = tag.ToString();
						// the containing group is linked to the token that contains these tags
						if ( controlCollection.Owner.Tag is string)
							controlCollection.Owner.Tag = task.SelectToken(controlCollection.Owner.Tag.ToString());

						if (controlCollection.Owner.Tag is JToken)
						{
							JToken ownerToken = controlCollection.Owner.Tag as JToken;
							if (ownerToken != null)
							{
								if (ownerToken is JValue || ownerToken is JProperty)
									taskToken = ownerToken.Value<JValue>(tag);
								else if (ownerToken is JObject)
									taskToken = ownerToken.SelectToken(tag);
								// could we find the task token through the owner?
							}
						}

					}
					if (taskToken == null)
					{
						string controlTag = control.Tag.ToString();
						if (!controlTag.StartsWith("#"))
							controlTag = "#" + controlTag;
						control.Tag = controlTag;
						taskValue = null;
					}
					else
					{
						control.Tag = taskToken;
						taskValue = taskToken;
					}
				}
				else if (control.Tag is JToken)
				{
					taskValue = control.Tag as object;
				}
				if ( taskValue == null )
				{
					// disable anything that isn't found
					if ( control is GroupBox )
						control.Enabled = false;
					else
						control.ResetText();
					continue;
				}
				control.Enabled = true;
				// see if there is a tag associated with this control

				Debug.Print("Text control:" + control.Name);
				string taskValueText = taskValue.ToString();
				if (taskValue is JArray)
				{
					taskValueText = "";
					foreach (JToken taskTok in (taskValue as JArray))
					{
						taskValueText += taskTok.ToString();
					}
				}
				ZTaskLocs taskLocs = new ZTaskLocs(task.parentStep.stepLocs, taskValueText);
				string	valueString = "";
				Color	foreColor = control.ForeColor;
				if (taskLocs == null  || taskLocs.locs.Count == 0)
				{
					valueString = taskValue.ToString();
				}
				else
				{
					valueString = taskLocs.text;
					if ( taskLocs.locs[0].locType == ZTaskLoc.LocType.value)
						foreColor = taskLocs.locs[0].color;
				}
				Debug.Print(control.Name + ", value: '" + valueString + "'");
				// based on the type, grab a particular value
				if (control is CheckBox)
				{
					bool value = bool.Parse(taskValue.ToString());
					(control as CheckBox).Checked = value;
					control.ForeColor = foreColor;
					EnableGroupSettings(controlCollection, value);
				}
				else if (control is MaskedTextBox)
				{
					MaskedTextBox mtb = (control as MaskedTextBox);
					// if this is a numeric mask, which it should be, convert accordingly
					if (mtb.Mask.Contains('0'))
					{
						double value = double.Parse(taskValue.ToString());
						mtb.Text = value.ToString(mtb.Mask.ToString());
					}
					control.ForeColor = foreColor;
				}
				else if (control is TextBox)
				{
					(control as TextBox).Text = valueString;
					control.ForeColor = foreColor;
				}
				else if (control is RichTextBox)
				{
					RichTextBox txtValue = control as RichTextBox;
					txtValue.Text = valueString;
					// colorize it
					if (taskLocs != null && taskLocs.Values.Count > 0)
					{
						txtValue.Text = taskLocs.text;
						Font taggedFont = new Font(txtValue.Font, FontStyle.Bold | FontStyle.Underline);
						foreach (ZTaskLoc taskLoc in taskLocs.locs)
						{
							txtValue.Select(taskLoc.startPos, taskLoc.length);
							txtValue.SelectionFont = taggedFont;
							txtValue.SelectionColor = taskLoc.color;
							txtValue.DeselectAll();
						}
					}

				}
				else if (control is ComboBox)
				{
					ComboBox cbo = control as ComboBox;
					cbo.SelectedItem = valueString;
					control.ForeColor = foreColor;
					cbo.Update();
					cbo.Refresh();
				}
				else if (control is NumericUpDown)
				{
					NumericUpDown nud = (control as NumericUpDown);
					(control as NumericUpDown).Value = int.Parse(valueString);
					control.ForeColor = foreColor;
					control.Text = taskValue.ToString();
					nud.Refresh();
				}
				else if (control is GroupBox)
				{
					LoadGroupSettings((control as GroupBox).Controls, task);
				}
				//else if (control is Panel)
				//{
				//	LoadGroupSettings((control as Panel).Controls, taskValue);
				//}
			}

		}
		private void ResetGroupSettings(Control.ControlCollection controls)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name + " for: " + controls.Owner.Name);
			string tag;
			foreach (Control control in controls)
			{
				if (control.Name.StartsWith("cbo"))
				{
					ComboBox cbo = control as ComboBox;
				}
				if (control.Tag == null)
					continue;
				// original tag is preserved with leading #
				if (control.Tag is string)
				{
					tag = control.Tag as string;
					if ( tag.StartsWith("#") )
						control.Tag = tag.Substring(1);
					continue;
				}
				// the tag will either be the name of the preference, or it will be the preference object itself
				tag = ZRubric.OName(control.Tag as JToken);
				if (tag.Length == 0)
					tag = ZRubric.OID(control.Tag as JToken);
				if (control is GroupBox)
				{
					ResetGroupSettings((control as GroupBox).Controls);
				}
				else if (control is Panel)
				{
					ResetGroupSettings((control as Panel).Controls);
				}
				else if (control is CheckBox)
				{
					(control as CheckBox).Checked = false;
				}
				else if (control is ComboBox)
				{
					(control as ComboBox).SelectedIndex = 0;
				}
				else
				{
					control.ResetText();
				}
				control.Refresh();
				// do this last
				control.Tag = tag;
			}
			EnableGroupSettings(controls, true);
			if (!(controls.Owner.Tag is string))
			{
				tag = ZRubric.OName(controls.Owner.Tag as JToken);
				controls.Owner.Tag = ZRubric.OName(controls.Owner.Tag as JToken);
			}
		}
		private void EnableGroupSettings(Control.ControlCollection controlCollection, bool isEnabled)
		{
			Debug.Print(MethodInfo.GetCurrentMethod().Name + " for: " + controlCollection.Owner.Name);
			// disable or enable child controls
			foreach (Control control in controlCollection)
			{
				if (control.Tag == null)
					continue;
				// the tag will either be the name of the preference, or it will be the preference object itself
				if (control is MaskedTextBox)
				{
					control.Enabled = isEnabled;
				}
				else if (control is TextBox)
				{
					control.Enabled = isEnabled;
				}
				else if (!(control is Label))
					control.Enabled = isEnabled;

			}
		}

		private void dgTasks_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			DataGridViewRow row = dgTasks.Rows[e.RowIndex];
			Debug.Print(MethodInfo.GetCurrentMethod().Name + ", row: " + row.Index);
			if (row.Selected)
				UpdateTaskDetails(row);

		}

		private void mniSubmissionSelect_Click(object sender, EventArgs e)
		{
			cmnuTarget.Hide();
			DataGridViewRow row = dgTasks.CurrentRow;
			if ((row != null) && (row.Tag is ZTask))
			{
				ZTask task = dgTasks.CurrentRow.Tag as ZTask;
				string taskLocation = task.target.location.address;
				string sheet = task.target.location.context;
				if (zExcelSubmission != null)
					zExcelSubmission.SelectRange(sheet,taskLocation);
				
			}


		}

		private void txtTargetLocationAddress_MouseDown(object sender, MouseEventArgs e)
		{

			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Point p = new Point(e.X, e.Y);
				cmnuTarget.Show(txtTargetLocationAddress, p);
			}

		}

		private void txtStep_MouseDoubleClick(object sender, MouseEventArgs e)
		{

		}

		private void txtStep_MouseClick(object sender, MouseEventArgs e)
		{
			// if not right-click and selected text, asta
			// if selected text is subset of a task
			//		if is already a taskLoc 
			//			modify content
			// if selected text is not a task location, or subset of a task location, we have a new task
			//		popup dialog box to gather task information and insert new task if desired

		}

		// TODO: add feacture to detect when options have changed or form is dirty
		//private void SaveGroupSettings(Control.ControlCollection controls)
		//{
		//	// go through each of the controls in this group
		//	JValue preferenceValue = null;
		//	//			ZPreference zPreference = ZRubric.activePreferences[controls.Owner.Tag.ToString()];
		//	Debug.Print("Group control:" + controls.Owner.Name);
		//	foreach (Control control in controls)
		//	{
		//		if (control.Name.StartsWith("cbo"))
		//		{
		//			ComboBox cbo = control as ComboBox;
		//		}
		//		if (control.Tag == null)
		//			continue;
		//		// the tag will either be the name of the preference, or it will be the preference object itself
		//		preferenceValue = null;
		//		if (control.Tag is JValue)
		//		{
		//			preferenceValue = control.Tag as JValue;
		//		}
		//		Debug.Print("Text control:" + control.Name);
		//		// based on the type, grab a particular value
		//		if (control is CheckBox)
		//		{
		//			preferenceValue.Value = (control as CheckBox).Checked;
		//		}
		//		else if (control is MaskedTextBox)
		//		{
		//			MaskedTextBox mtb = (control as MaskedTextBox);
		//			// if this is a numeric mask, which it should be, convert accordingly
		//			if (mtb.Mask.Contains('0'))
		//			{
		//				double value = double.Parse(mtb.Text);
		//				preferenceValue.Value = value;
		//			}
		//			else
		//				preferenceValue.Value = mtb.Text;
		//		}
		//		else if (control is TextBox)
		//		{
		//			preferenceValue.Value = (control as TextBox).Text;
		//		}
		//		else if (control is RichTextBox)
		//		{
		//			preferenceValue.Value = (control as RichTextBox).Text;
		//		}
		//		else if (control is ComboBox)
		//		{
		//			ComboBox cbo = control as ComboBox;
		//			preferenceValue.Value = cbo.SelectedItem;
		//		}
		//		else if (control is NumericUpDown)
		//		{
		//			NumericUpDown nud = (control as NumericUpDown);
		//			// check the value, if it is a whole number, save as an int
		//			int nudInt;
		//			if (int.TryParse(nud.Value.ToString(), out nudInt))
		//				preferenceValue.Value = nudInt;
		//			else
		//				preferenceValue.Value = nud.Value;
		//		}
		//		else if (control is GroupBox)
		//		{
		//			SaveGroupSettings((control as GroupBox).Controls);
		//		}
		//		else if (control is Panel)
		//		{
		//			SaveGroupSettings((control as Panel).Controls);
		//		}
		//	}
		//}

	}
}
