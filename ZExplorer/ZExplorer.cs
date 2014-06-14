#region Using directives
using System;
using System.Windows;
using System.Windows.Forms;

using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Xml;

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
#endregion

namespace ZStudio
{
	public partial class ZExplorer : Form
	{
		protected MruStripMenu mruMenu;
		protected string curFileName;
		private int m_curFileNum = 0;
		private ZRubricSchema m_rubricSchema = null;
		private ZRubric m_rubric = null;
#if USE_THIS
		private ZExcel m_rubricExcel = null;
#endif
		private ZExcelToJson m_submissionExcel = null;

		public StatusStrip statusStrip
		{
			get { return zStatus; }
		}
		// TODO: this can be expanded to support settings for the project

		public ZExplorer()
		{
			InitializeComponent();
			// load our default schema
								
			string sf = Properties.Settings.Default.rubricSchemaFilename;

			m_rubricSchema = new ZRubricSchema(sf);
			tvRubric.zProps = ctlZproperties;
			tvRubric.zStep = zStepControl;
			tvRubric.tcDetails = tcDetails;
			
			ctlZproperties.tvRubric = tvRubric;
			ctlZproperties.rubricSchema = m_rubricSchema;			
			int mruMax = Properties.Settings.Default.mruMaxEntries;
			// verify mruMax <= size f mruFiles
			System.Collections.Specialized.StringCollection mruFiles = Properties.Settings.Default.mruFiles;
			if (mruFiles.Count > mruMax)
				mruMax = mruFiles.Count;
			string[] recentFiles = new string[mruMax];
			Properties.Settings.Default.mruFiles.CopyTo(recentFiles, 0);
			
			mruMenu = new MruStripMenu(mnuFileRecentFiles, new MruStripMenu.ClickedHandler(OnMruFile), recentFiles,mruMax);
			IncFilename();

			return;
		}
		#region loadingStuff
		public void NewRubric()
		{
			// open the template file, for now, it's a spreadsheet template
			LoadRubric(Properties.Settings.Default.templateDir + "/rubric.zrst");
			m_rubric.name = "No Name";
		}
		public void LoadRubric(string rubricFileName)
		{
			m_rubric = new ZRubric(m_rubricSchema, rubricFileName);
			tvRubric.zRubric = m_rubric;
			
			zExcelViewer.fileName = m_rubric.project.documentFileName;
			// link the excel viewer to the properties
			ctlZproperties.zExcelViewer = zExcelViewer;
			zStepControl.zExcelViewer = zExcelViewer;
			tcDetails.SelectTab("tabRubric");
			tabRubric.Select();
			txtDescription.Text = m_rubric.project.description;
		}
		public void UpdateStudioTitle()
		{
			// update the name of which rubric is loaded
			this.Text = "z-Grader Studio - " + Path.GetFileNameWithoutExtension(m_rubric.rubricFileName) +
				(m_rubric.isDirty() ? " *" : "");
		}
		private frmExcel mExcelSubmissionViewer = null;
		public void LoadIt(string rubricFileName)
		{
			ZRubric.LoadFunctions();
			ZRubric.LoadPreferences();

			LoadRubric(rubricFileName);
			string rubricDir = Path.GetDirectoryName(rubricFileName);
			string submissionJson = rubricDir + @"\submission.json";
			if (File.Exists(submissionJson))
			{
				m_rubric.LoadSubmission(submissionJson);
			}
			string submissionXlsx = rubricDir + @"\submission.xlsx";
			if (File.Exists(submissionXlsx))
			{
//				m_submissionExcel = new ZExcel(submissionXlsx, 0);
//				m_submissionExcel.SelectRange("C20");
				mExcelSubmissionViewer = new frmExcel();
				zStepControl.zExcelSubmission = mExcelSubmissionViewer.viewer;
				mExcelSubmissionViewer.Open(submissionXlsx);
				mExcelSubmissionViewer.Show(this);
			}

			UpdateStudioTitle();
		}
		#region MenuStuff
		private void OnMruFile(int number, String filename)
		{
			LoadIt(filename);
			mruMenu.SetFirstFile(number);
		}

		private void IncFilename()
		{
			m_curFileNum++;
			// TODO: figure out if I really want to do anything here or not
			//int index = comboCurrentFile.Items.Add("File" + m_curFileNum.ToString());
			//comboCurrentFile.SelectedIndex = index;
		}
		private void mnuFileOpen_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string initialDirectory = Properties.Settings.Default.lastDirUsed;
			openFileDialog.InitialDirectory = initialDirectory;
			openFileDialog.Filter = "rubric files (*.zrst)|*.zrst|txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = false;

			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream inStream;
				if ((inStream = openFileDialog.OpenFile()) != null)
				{
					curFileName = openFileDialog.FileName;
					mruMenu.AddFile(curFileName);
					string lastDir = Path.GetDirectoryName(curFileName);
					Properties.Settings.Default.lastDirUsed = lastDir;
					inStream.Close();
					LoadIt(curFileName);
				}
			}
		}
		private void mnuFileNew_Click(object sender, EventArgs e)
		{
			// open the template file, for now, it's a spreadsheet template
			LoadIt(Properties.Settings.Default.templateDir + "/rubric.zrst");
		}

		private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// rubricJson
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			string initialDirectory = Properties.Settings.Default.lastDirUsed;
			saveFileDialog.InitialDirectory = initialDirectory;
			saveFileDialog.Filter = "rubric files (*.zrsf)|*.zrsf|txt files (*.txt)|*.txt|All files (*.*)|*.*";
			saveFileDialog.AddExtension = true;
			saveFileDialog.DefaultExt = "zrsf";
			saveFileDialog.FilterIndex = 1;
			saveFileDialog.RestoreDirectory = false;

			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				curFileName = saveFileDialog.FileName;
				Stream inStream;
				if ((inStream = saveFileDialog.OpenFile()) != null)
				{
					inStream.Close();
				}
				SaveIt(curFileName);
				string lastDir = Path.GetDirectoryName(curFileName);
				Properties.Settings.Default.lastDirUsed = lastDir;
				mruMenu.AddFile(curFileName);
				// update the name of which rubric is loaded
				this.Text = "z-Grader Studio - " + Path.GetFileNameWithoutExtension(curFileName);
			}
		}
		public void SaveIt(string fileName)
		{
			using (StreamWriter outfile = new StreamWriter(fileName))	// dir + "/rubric.json"))
			{
				// need to update the rubric with the name that has been assigned
				string rubricName = Path.GetFileNameWithoutExtension(fileName);
				m_rubric.name = rubricName;
				outfile.Write(m_rubric.ToString());
				outfile.Close();
				m_rubric.rubricFileName = fileName;
				tvRubric.Refresh();
			}

		}
		private void mnuFileExit_Click(object sender, EventArgs e)
		{
			// save the current list of used files
			Properties.Settings.Default.mruFiles.Clear();
			foreach (string f in mruMenu.GetFilesFullEntrystring())
				Properties.Settings.Default.mruFiles.Add(f.Split(' ').ElementAt(f.Split(' ').Count()-1));
			Properties.Settings.Default.Save();
			if (m_submissionExcel != null)
			{
				m_submissionExcel.Close();
				zStepControl.zExcelSubmission = null;
				m_submissionExcel = null;
			}
			Application.Exit();
		}
		private void JsonExp_FormClosing(object sender, FormClosingEventArgs e)
		{
			Debug.Print("FormClosing");
		}

		#endregion

		private void zExcelViewer_WorkbookLoaded(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			m_rubric.project.documentFileName = this.zExcelViewer.fileName;
			// If we don't have any worksheets loaded yet, do it now
			if (m_rubric.project.worksheets.Count() == 0)
			{
				foreach (Excel.Worksheet ws in zExcelViewer.oExcelApp.ActiveWorkbook.Worksheets)
				{
					m_rubric.project.worksheets.Add(ws.Name);
				}
			}
		}

		private void saveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string fileName = m_rubric.rubricFileName;
			// if this is the default template, send user to to save as
			if (fileName.EndsWith("zrst"))
				saveAsToolStripMenuItem_Click(sender, e);
			else
			{
				// we can just save this as is
				SaveIt(fileName);
			}
		}

		private void mnuProjectImportSteps_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			string initialDirectory = Properties.Settings.Default.lastDirUsed;
			openFileDialog.InitialDirectory = initialDirectory;
			openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;
			openFileDialog.RestoreDirectory = false;
			openFileDialog.Title = "Open project steps text file";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Stream inStream;
				string stepsTextFile = "";
				if ((inStream = openFileDialog.OpenFile()) != null)
				{
					stepsTextFile = openFileDialog.FileName;
					inStream.Close();
					// if we don't have a project yet, automatically create one
					if (m_rubric == null)
					{
						NewRubric();
					}
					int steps = m_rubric.ImportSteps(stepsTextFile);
					tvRubric.zRubric = m_rubric;
					UpdateStudioTitle();
					// MessageBox.Show("Imported " + steps.ToString() + " from " + stepsTextFile);
				}
			}

		}
		public void SelectRange(string address)
		{
			m_submissionExcel.SelectRange(address);
		}

		private void gradeTaskToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// figure out the current task and grade it, then provide feedback
			zStepControl.TestGradeActiveTask();
		}

		private void refreshSubmissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// save whatever is in the associated submission and refresh submission file
			// borrow code from test project
			string submission = m_submissionExcel.SaveSubmission();
			if ( m_rubric != null )
				m_rubric.submission = submission;
			
		}

		private void mniToolsOptionsPreferences_Click(object sender, EventArgs e)
		{
			frmPreferences userPreferences = new frmPreferences();
			DialogResult result = userPreferences.ShowDialog(this);
			// perform some action based on result
			if (result == DialogResult.Cancel)
				return;
			// apply updates
			ZRubric.SavePreferences();
		}

		private void gradeReportToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string currentFolder = "";
			// see if we already have something loaded, if not, we're going to attach to whatever is running
			if ( m_submissionExcel == null )
			{
				m_submissionExcel = new ZExcelToJson();
				currentFolder = m_submissionExcel.currentFolder;
			}
			// if we don't already have a rubric loaded
			if (m_rubric == null && currentFolder.Length > 0)
			{
				m_rubric = new ZRubric(currentFolder);
			}
			m_rubric.LoadSubmission(currentFolder + @"\gradedSubmission.json");
			m_submissionExcel.MarkupSubmission(m_rubric, ZRubric.activeSubmission.taskDeductions);

		}

		private void gradeSubmissionToolStripMenuItem_Click(object sender, EventArgs e)
		{
			string currentFolder = "";
			// see if we already have something loaded, if not, we're going to attach to whatever is running
			// and assume that it has the rubric we're looking for
			if (m_submissionExcel == null)
			{
				m_submissionExcel = new ZExcelToJson();
				currentFolder = m_submissionExcel.currentFolder;
			}
			// if we don't already have a rubric loaded, load it now
			if (m_rubric == null)
			{
				m_rubric = new ZRubric(currentFolder);
			}
			// load whatever keys we have
            DialogResult refreshSubmission = System.Windows.Forms.DialogResult.No;
            refreshSubmission = MessageBox.Show("Refresh submission from current Excel file?", "Refresh submission", MessageBoxButtons.YesNo);
			if (refreshSubmission == System.Windows.Forms.DialogResult.OK)
			{
				if (m_rubric.LoadKeys())
				{
					if (m_submissionExcel != null)
					{
						string doc = m_submissionExcel.SaveSubmission();
						m_rubric.submission = doc;
					}
					else
						m_rubric.LoadSubmission(currentFolder + @"\submission.json");
				}
			}
			else if (m_rubric.LoadKeys())	// make sure keys get loaded
				m_rubric.LoadSubmission(currentFolder + @"\submission.json");

			m_rubric.GradeSubmission();
			m_rubric.SaveGradedSubmission(currentFolder + @"\gradedSubmission.json");
		}

		private void mniProjectStepsInsertBefore_Click(object sender, EventArgs e)
		{

		}

		private void mniProjectStepsInsertAfter_Click(object sender, EventArgs e)
		{
			// figure out the currently active step, then add a new one and go from there

		}

		private void mniProjectStepsRemove_Click(object sender, EventArgs e)
		{

		}
		#region dragDrop
		//private void tvJson_DoubleClick(object sender, EventArgs e)
		//{
		//    Debug.Print("tvJson_DoubleClick:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_DragDrop(object sender, DragEventArgs e)
		//{
		//    Debug.Print("tvJson_DragDrop:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_Click(object sender, EventArgs e)
		//{
		//    Debug.Print("tvJson_Click:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_AfterSelect(object sender, TreeViewEventArgs e)
		//{
		//    //Debug.Print("tvJson_AfterSelect:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());
		//    //dgNodeProperties.Rows.Clear();
		//    //// load the properties for the selected item
		//    //JToken tok = e.Node.Tag as JToken;
		//    //if (tok == null)
		//    //    return;
		//    //string tag = GetRPath(tok); // +"Properties.";
		//    //dgNodeProperties.Tag = tag;
		//    //JObject jObj = tok as JObject;	// tok["Properties"] as JObject;
		//    //if (jObj != null)
		//    //{
		//    //    foreach (JProperty prop in jObj.Properties())
		//    //    {
		//    //        int rowIndex = dgNodeProperties.Rows.Add();
		//    //        DataGridViewRow row = dgNodeProperties.Rows[rowIndex];
		//    //        string propName = prop.Name;
		//    //        Debug.Print(propName + " is type:" + prop.Type.ToString());
		//    //        string propValue = prop.Value.ToString();
		//    //        row.Cells[0].Value = propName;
		//    //        row.Cells[1].Value = propValue;
		//    //        row.Tag = prop;
		//    //    }
		//    //}
		
		//}

		//private void tvJson_ItemDrag(object sender, ItemDragEventArgs e)
		//{
		//    Debug.Print("tvJson_ItemDrag:" + e.Item.GetType().ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		//{
		//    Debug.Print("tvJson_QueryContinueDrag:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		//{
		//    Debug.Print("tvJson_NodeMouseDoubleClick:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
		//{
		//    Debug.Print("tvJson_AfterLabelEdit:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
		//{
		//    Debug.Print("tvJson_BeforeLabelEdit:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		//{
		//    Debug.Print("tvJson_BeforeSelect:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_DragEnter(object sender, DragEventArgs e)
		//{
		//    Debug.Print("tvJson_DragEnter:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_DragLeave(object sender, EventArgs e)
		//{
		//    Debug.Print("tvJson_DragLeave:" + e.ToString() + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		//{
		//    Debug.Print("tvJson_NodeMouseClick:" + e.Node.Name + ":" + sender.GetType().ToString() + ":" + sender.ToString());

		//}

		//private void tvJson_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
		//{
		//    string msg = "tvJson_NodeMouseHover:";
		//    if (e.Node.Name.Length > 0)
		//        msg += e.Node.Name + ":";
		//    if (e.Node.Tag != null )
		//        msg += e.Node.Tag.GetType().ToString() + ":" + e.Node.Tag.ToString() + ":";

		//    Debug.Print( msg + sender.GetType().ToString() + ":" + sender.ToString());
		//}
		//#endregion
		
		//private void dgProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		//{
		//    PropertySelected(sender, e);
		//}

		//private void dgNodeProperties_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		//{
		//    //propertyValueEditCancelled = true;	// by default, the edit was cancelled
		//    //string senderType = sender.GetType().ToString();
		//    //DataGridView cell = sender as DataGridView;
		//    //DataGridViewRow row = cell.CurrentRow;
		//    //if (cboPropertyValue.Visible)
		//    //{
		//    //    row.Cells[0].Selected = false;
		//    //    row.Cells[1].Selected = true;
		//    //}
		//    //else if (btnProperties.Visible)
		//    //{
		//    //    row.Cells[0].Selected = true;
		//    //    row.Cells[1].Selected = false;
		//    //}
		//}
		//// TODO: add code for cancel edit to clear items
		//// also need to update json
		//private void dgNodeProperties_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		//{
		//    //string propName = dgNodeProperties.CurrentRow.Cells[0].Value.ToString();

		//    //if (!propertyValueEditCancelled)
		//    //{
		//    //    // xTODO: based on what's visible, update underlying property value
		//    //    if ( cboPropertyValue.Visible ) // cboPropertyValue.Items.Count > 0)
		//    //    {
		//    //        string newValue = cboPropertyValue.Text;	// .SelectedItem.ToString(); ;
		//    //        // reference the property value cell
		//    //        dgNodeProperties.CurrentRow.Cells[1].Value = newValue;
		//    //        cboPropertyValue.Hide();
		//    //        cboPropertyValue.Items.Clear();
		//    //    }
		//    //    Debug.Print("endEdit");
		//    //}
		//    //else
		//    //    Debug.Print("ediEdit - user cancelled");
		//}

		//private void dgNodeProperties_CellContentClick(object sender, DataGridViewCellEventArgs e)
		//{
		//    PropertySelected(sender, e);
		//}

		//private void dgNodeProperties_Validating(object sender, CancelEventArgs e)
		//{
		//    Debug.Print("Validating");
		//}

		//private void dgNodeProperties_CancelRowEdit(object sender, QuestionEventArgs e)
		//{
		//    Debug.Print("cancelled edit");
		//}

		//private void dgNodeProperties_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
		//{
		//    //Debug.Print("Validating");
		//    //propertyValueEditCancelled = false;
		//    //// only update the value when we are validating, and not escaping
		//    //if (cboPropertyValue.Visible)
		//    //{
		//    //    if (cboPropertyValue.Items.Count > 0)
		//    //    {
		//    //        string newValue = cboPropertyValue.Text;	// .SelectedItem.ToString(); ;
		//    //        dgNodeProperties.CurrentRow.Cells[1].Value = newValue;
		//    //    }
		//    //}
		//}

		//private void btnProperties_Click(object sender, EventArgs e)
		//{
		//    MessageBox.Show("clicked on properties object");
		//}

		//private void dgNodeProperties_CellClick(object sender, DataGridViewCellEventArgs e)
		//{
		//    PropertySelected(sender, e);
		//}
		//#region propertyControlHandling
		//// DataGridViewCellEventArgs
		//private void PropertySelected(object sender, DataGridViewCellEventArgs e)
		//{
		//    //// first thing to do is to hide existing property value controls
		//    //btnProperties.Hide();
		//    //cboPropertyValue.Hide();
		//    //cboPropertyValue.Items.Clear();

		//    //propertyValueEditCancelled = true;	// by default, the edit was cancelled
		//    //string senderType = sender.GetType().ToString();
		//    //DataGridView dgvProperties = sender as DataGridView;
		//    //DataGridViewRow row = dgvProperties.CurrentRow;
		//    //// DataGridViewCell col = dgvProperties.CurrentCell;
		//    //DataGridViewCell propCell = row.Cells[1];
		//    //// xTODO: create json to represent hierarchy of the objects/properties/values,...
		//    //string rPath = dgNodeProperties.Tag.ToString();
		//    //string propName = row.Cells[0].Value.ToString();
		//    //Debug.Print("propRow:" + row.Index.ToString() + ", propPath:" + rPath + ", propName:" + propName);
		//    //if (rPath.Length == 0)
		//    //    return;

		//    //JToken tokProp = rubricStyle.SelectToken(rPath + propName);
		//    //JArray props = tokProp as JArray;
		//    //if (props != null)
		//    //{
		//    //    // load the combo box with the associated properties
		//    //    foreach (JValue val in props)
		//    //    {
		//    //        string theVal = val.ToString();
		//    //        cboPropertyValue.Items.Add(theVal);
		//    //    }
		//    //    // now display it
		//    //    cboPropertyValue.Text = propCell.Value.ToString();
		//    //    // always use the property cell
		//    //    Rectangle rect = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true);
		//    //    cboPropertyValue.Location =  dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true).Location;
		//    //    cboPropertyValue.Width = rect.Width;
		//    //    cboPropertyValue.Show();
		//    //    Debug.Print("doArray" + row.Cells[0].Value + ", " + row.Cells[1].Value);
		//    //    return;
		//    //}
		//    //JObject oProp = tokProp as JObject;
		//    //if (oProp != null)
		//    //{
		//    //    row.Cells[0].Selected = true;
		//    //    row.Cells[1].Selected = false;
		//    //    // if this is location, we need to display the button
		//    //    btnProperties.Location = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true).Location;

		//    //    Rectangle rect = dgvProperties.GetCellDisplayRectangle(1, e.RowIndex, true);
		//    //    // cell.GetCellDisplayRectangle(1, e.RowIndex, true);
		//    //    btnProperties.Width = rect.Height;
		//    //    btnProperties.Height = rect.Height;
		//    //    btnProperties.Left = rect.Right - btnProperties.Width;
		//    //    btnProperties.Top = rect.Top;
		//    //    btnProperties.Parent = dgvProperties;
		//    //    btnProperties.Tag = dgNodeProperties.Tag;
		//    //    btnProperties.Visible = true;
		//    //    btnProperties.Show();
		//    //}
		//}
		//#endregion

		//private void dgNodeProperties_DragDrop(object sender, DragEventArgs e)
		//{
		//    // PasteIt();
		//}
		//private void Excel_DragDrop(object sender, DragEventArgs e)
		//{
		//    string sheetName = "";
		//    Excel.Range range = AM.Interop.Excel.Clipboard.GetRange(e.Data as System.Runtime.InteropServices.ComTypes.IDataObject, out sheetName);
		//    PasteIt();
		//    var data = e.Data;
		//    e.Effect = DragDropEffects.None;
		//}
		//private void PasteIt()
		//{
		//    Excel.Range rng  = null;
		//    if ( oExcelApp.Selection != null )
		//        rng = oExcelApp.Selection.Cells;
		//    string sheetName = "";
		//    Excel.Range range = AM.Interop.Excel.Clipboard.GetRange(out sheetName);

		//    if (range != null)
		//    {
		//        string loc = "Worksheets[" + sheetName + "].Cells[" + range.get_Address(false, false, Excel.XlReferenceStyle.xlA1, Type.Missing, Type.Missing) + "]";
		//        bool hasFormula = (bool)range.HasFormula;
		//        if (hasFormula)
		//        {
		//            string formula = range.Formula.ToString();
		//            formula = range.FormulaLocal.ToString();
		//            formula = range.FormulaR1C1.ToString();
		//            formula = range.FormulaR1C1Local.ToString();
		//        }
		//        else
		//        {
		//            loc = range.Value.ToString();
		//            loc = range.Text;
		//        }
		//    }

		//}

		//private void tvJson_DragOver(object sender, DragEventArgs e)
		//{
		//    dgNodeProperties_DragDrop(sender, e);
		//}

		//private void dgNodeProperties_DragOver(object sender, DragEventArgs e)
		//{
		//    Debug.Print("props-DragOver");
		//    // dgNodeProperties.Cursor = Cursors.Cross;
		//}

		//private void cmnuPaste_Click(object sender, EventArgs e)
		//{
		//    cmnuStep.Hide();
		//    PasteIt();
		//}

		//private void dgNodeProperties_Click(object sender, EventArgs e)
		//{
		//    string t = sender.GetType().ToString();
		//}

		//private void dgNodeProperties_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		//{
		//    //Point loc = dgNodeProperties.GetCellDisplayRectangle(1, dgNodeProperties.CurrentRow.Index,true).Location;
		//    //Debug.Print("loc:" + loc.ToString());
		//    //Debug.Print("e.loc:" + e.Location.ToString());

		//    //if (e.Button == System.Windows.Forms.MouseButtons.Right)
		//    //{
		//    //    cmnuStep.Show(dgNodeProperties,loc);
		//    //}
		//}

		//private void dgNodeProperties_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
		//{
		//    e.Action = DragAction.Drop;
		//}

		//private void dgNodeProperties_DragEnter(object sender, DragEventArgs e)
		//{
			
		//    if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
		//    {
		//        e.Effect = DragDropEffects.All;
		//    }
		//    e.Effect = DragDropEffects.All;
		//}
	}
#endregion
		#endregion

}

