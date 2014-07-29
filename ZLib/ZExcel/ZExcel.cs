using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.IO;
//using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office;
using Microsoft.Office.Interop;
using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Core;
using Newtonsoft.Json;
using ZLib;
using ZLib.ZRubric;
// adding comments to a cell
	//Range("A1").AddComment
	//Range("A1").Comment.Visible = True
	//Application.Left = 88.75
	//Application.Top = 16
	//Range("A1").Comment.Text Text:="Gary E. Sibbitts:" & Chr(10) & "This is a bold comment."
//	Range("E26").Comment.Shape.Select True
    
//	Range("E26").Comment.Text Text:= _
//		"Step 14: Select E26 and type Highest Salary." & Chr(10) & "-: Select "
//	Range("E26").Comment.Text Text:= _
//		"E26 and type Highest SalaryCreate.Value.Text - Partial credit awarded, detected minor discrepancy between value entered, 'Highest Salry' and valu", Start:=79
//	Range("E26").Comment.Text Text:= _
//		"e expected 'Highest Salary'. " & Chr(10) & " deducted: 0.25 pts", Start:=200
//	Dim theFont As Font
//	With Range("E26").Comment.Shape.TextFrame.Characters(Start:=79, Length:=27).Font
//		.Name = "Gill Sans MT"
//		.FontStyle = "Regular"
//		.Size = 12
//		.Strikethrough = False
//		.Superscript = False
//		.Subscript = False
//		.OutlineFont = False
//		.Shadow = False
//		.Underline = xlUnderlineStyleNone
//		.Color = ColorConstants.vbGreen
//		.Color = -16776961
//	End With
//theend:
//	With Range("E26").Comment.Shape.TextFrame.Characters(Start:=79, Length:=27).Font
//		.Name = "Gill Sans MT"
//		.FontStyle = "Bold"
//		.Size = 14
//		.Strikethrough = False
//		.Superscript = False
//		.Subscript = False
//		.OutlineFont = False
//		.Shadow = False
//		.Underline = xlUnderlineStyleSingle
//		.Bold = True
//	End With
        

namespace Excel2Json
{
	public class ZExcel
	{
		public Application mApp;
		public bool usingExistingInstance;
		public Workbook mWorkbook;
		public string currentFolder;
		public ZExcel()
		{
			try
			{
				//Try to reuse an excel application object
				mApp = Marshal.GetActiveObject("Excel.Application") as Microsoft.Office.Interop.Excel.Application;
				usingExistingInstance = true;
				mWorkbook = mApp.ActiveWorkbook;
				Worksheet wksht = mApp.ActiveSheet;
				//tvJson.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvJson_AfterSelect);
//GS - add this back in for building intermediate submission results				mApp.SheetChange += mApp_SheetChange;
				// mWorkbook.SheetSelectionChange - use this to force a recalculation of the spreadsheet
				// containing all of the non-event inducing changes

				currentFolder = mWorkbook.Path;
			}
			catch(Exception)
			{
				//Create one
				mApp = new Application();
				usingExistingInstance = false;
			}

			return;
#region old stuff
			//Microsoft.Office.Core.MsoThemeColorSchemeIndex tcs = mWorkbook.Theme.ThemeColorScheme.;
			//int tcsCount = 0;

			//string ts = tcs.ToString();
			//System.ConsoleColor cc = System.ConsoleColor.DarkBlue;
			//ts = cc.ToString();
			//double wc;
			//// rgbDarkBlue = 9109504
			//r = RGB2String(9109504);
			//Debug.Print(r);
			//for (tcsCount = 1; tcsCount < tcs.Count; ++tcsCount)
			//{
			//    ThemeColor tcx = tcs.Colors((MsoThemeColorSchemeIndex) tcsCount);
			//      wc = (double)mWorkbook.Colors[tcx.ThemeColorSchemeIndex];
			//      var vwc = tcs.Colors(tcx.ThemeColorSchemeIndex).RGB;
			//    Debug.Print(" idx '{0}', rgb {1}, wkbk {2}, vwc {3} ", tcx.ThemeColorSchemeIndex, tcx.RGB, wc, RGB2String((long) vwc));
			//}
			//Debug.Print( "\n Character count: {0}", tcsCount );

			//var th0 = mWorkbook.Theme;
			//var clrs = mWorkbook.Colors;
			////var color = mWorkbook.Colors(24);
			//mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
			//mApp.Quit();
			//mApp = null;

			//Workbook2Json(mWorkbook);
			//jsonfn = filename.Replace("xlsx", "json");
			//using (StreamWriter outfile = new StreamWriter(jsonfn))
			//{
			//    outfile.Write(sw.ToString());
			//}
			
			//Debug.WriteLine(sw.ToString());
			//mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
			//mApp.Quit();
			//mApp = null;
#endregion
		}
		~ZExcel()
		{
			Close();
			//mApp.Quit();
		}
		public void Close()
		{
			if (mApp != null)
			{
				try
				{
					if (mApp.ActiveWorkbook != null)
					{
						mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
					}
					mApp.Quit();

				}
				catch (Exception ex)
				{
					Debug.Print("Encountered error trying to close Excel: " + ex.Message);
				}
			}
			mApp = null;

		}
		void mApp_SheetChange(object sht, Range target)
		{
			// prompt to see if this change should be recorded
			string filename = mApp.ActiveWorkbook.FullName;
			
		}
		
		public ZExcel(string filename, int ex34)
		{
			mApp = new  Application();
			mWorkbook = mApp.Workbooks.Open(filename);
			mApp.Visible = true;
//			mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
//			mApp.Quit();
//			mApp = null;
		}
		public double stepDeduction(string stepID, ZTaskDeductions taskDeductions)
		{
			double deduction = 0;
			foreach (ZTaskDeduction taskDeduction in taskDeductions)
			{
				if ( taskDeduction.stepID == stepID )
					deduction += taskDeduction.pointsDeducted;
			}
			return deduction;
		}
		private string WrapIt(string text, int wrapLen = 70)
		{
			// if contains embedded newline characters, leave alone
			if ( text.Contains('\n') || text.Length <= wrapLen )
				return text;

			int breakPos = text.IndexOf(" ", wrapLen);
			// include a little margin
			while (breakPos > 0 && ((breakPos + 10) < text.Length) )
			{
				text = text.Substring(0, breakPos) + '\n' + text.Substring(++breakPos);
				breakPos += wrapLen;
				if (breakPos >= text.Length)
					break;
				breakPos = text.IndexOf(" ", breakPos);
			}

			return text;
		}
		private static bool isNewLine(char ch)
		{
			return ch == '\n';
		}
		public void MarkupSubmission(ZRubric rubric, ZTaskDeductions taskDeductions)
		{
			// loop through each of the task deductions and add comments to the excel file
			// report basics in output window
			// TODO: provide option to export
			string ontologyName = ZRubric.activeProject.ontology;
			ZOntology ontology = ZRubric.activeOntologies[ontologyName];
			ZResources resources = ZRubric.activePreferences.resources;
			ZResourceProviders rps = ontology.resourceProviders;
			// lets see what we have for this objective
			ZResourceProvider rp = null;
			ZObjectiveMappings oms = null;

			string t;
			// let's see if we can figure out how much needs to be deducted
			double grade = ZRubric.activeProject.totalPts;
			foreach (ZTaskDeduction taskDeduction in ZRubric.activeSubmission.taskDeductions)
			{
				grade -= taskDeduction.pointsDeducted;
			}

			// add a new sheet for the report
			int reportSheetRow = 1;	// second row after header

			Worksheet reportSheet = mApp.ActiveWorkbook.Worksheets.Add();	// mApp.ActiveWorkbook.Worksheets[0]);
			reportSheet.Name = "ZReport";
			// set initial column widths
			Range rng = reportSheet.Columns[1][1];
			rng.ColumnWidth = 10;
			rng = reportSheet.Columns[1][2];
			rng.ColumnWidth = 4;
			rng = reportSheet.Columns[1][3];
			rng.ColumnWidth = 4;
			rng = reportSheet.Columns[1][4];
			rng.ColumnWidth = 90;
			rng = reportSheet.Columns[1][10];
			rng.ColumnWidth = 30;

			rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 1], reportSheet.Cells[reportSheetRow, 3]];
			rng.Merge();
			rng.Value = "Project:";
			rng.Font.Bold = true;

			rng = reportSheet.Cells[reportSheetRow, 4];
			rng.Value = ZRubric.activeProject.name;

			++reportSheetRow;
			rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 1], reportSheet.Cells[reportSheetRow, 3]];
			rng.Merge();
			rng.Value = "Grade:";
			rng.Font.Bold = true;

			rng = reportSheet.Cells[reportSheetRow, 4];
			rng.Value = grade.ToString("N2") + " out of " + ZRubric.activeProject.totalPts.ToString("N2");
			rng.WrapText = true;

			reportSheetRow += 2;

			int firstResourceRow = reportSheetRow;
			foreach (ZResourceProvider zrp in rps)
			{
				string name = zrp.id;
				// for now, just grab the first one
				ZObjectiveResource zres = zrp.Resources(0)[0];
				{
					name += ": Type - " + zres.id;
					rng = reportSheet.Cells[reportSheetRow++, 4];
					rng.Value = name;
					rng.WrapText = true;
				}
			}
			rng = reportSheet.Range[reportSheet.Cells[firstResourceRow, 1], reportSheet.Cells[reportSheetRow-1, 3]];
			rng.Merge();
			rng.Value = "Resources:";
			rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
			rng.Font.Bold = true;

			++reportSheetRow;
			rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 1], reportSheet.Cells[reportSheetRow + 2, 3]];
			rng.Merge();
			rng.Value = "Allocations:";
			rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
			rng.Font.Bold = true;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Non-Critical-Errors:       {0:N2} pts => {1:P2}"
				, ZRubric.activeSubmission.allocations.NCE.max
				, ZRubric.activeSubmission.allocations.NCE.pct);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Execution-Errors:          {0:N2} pts => {1:P2}"
				, ZRubric.activeSubmission.allocations.EE.max
				, ZRubric.activeSubmission.allocations.EE.pct);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Learning-Objective-Errors: {0:N2} pts => {1:P2}"
				, ZRubric.activeSubmission.allocations.LO.max
				, ZRubric.activeSubmission.allocations.LO.pct);
			rng.Value = t;

			rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 1], reportSheet.Cells[reportSheetRow+2, 3]];
			rng.Merge();
			rng.Font.Bold = true;
			rng.Value = "Points/error:";
			rng.VerticalAlignment = XlVAlign.xlVAlignCenter;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Non-Critical-Errors:       {0:N2} pts"
				, ZRubric.activeSubmission.allocations.NCE.ppe);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Execution-Errors:          {0:N2} pts"
				, ZRubric.activeSubmission.allocations.EE.ppe);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Learning-Objective-Errors: {0:N2} pts"
				, ZRubric.activeSubmission.allocations.LO.ppe);
			rng.Value = t;

			rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 1], reportSheet.Cells[reportSheetRow+2, 3]];
			rng.Merge();
			rng.Value = "Points Deducted:";
			rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
			rng.Font.Bold = true;
			rng.WrapText = true;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Non-Critical-Errors:       actual: {0:N2}, total {1:N2}"
				, ZRubric.activeSubmission.allocations.NCE.actual
				, ZRubric.activeSubmission.allocations.NCE.total);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Execution-Errors:          actual: {0:N2}, total {1:N2}"
				, ZRubric.activeSubmission.allocations.EE.actual
				, ZRubric.activeSubmission.allocations.EE.total);
			rng.Value = t;

			rng = reportSheet.Cells[reportSheetRow++, 4];
			t = string.Format("Learning-Objective-Errors: actual: {0:N2}, total {1:N2}"
				, ZRubric.activeSubmission.allocations.LO.actual
				, ZRubric.activeSubmission.allocations.LO.total);
			rng.Value = t;

			ZStep currentStep = rubric.steps[0];	// first step
			++reportSheetRow;	// second row after header

			int objectiveCol = 5;
			int	nextStepIndexToDisplay = 0;
			foreach (ZTaskDeduction taskDeduction in ZRubric.activeSubmission.taskDeductions)
			{
				currentStep = rubric.steps[0];
				// grab the step for which this deduction applies
				ZStep theStep = rubric.steps[taskDeduction.stepID];
				// output the information for this step and all preceding steps
				#region outputPrecedingSteps
				while (nextStepIndexToDisplay < (theStep.index - 1))
				{
					currentStep = rubric.steps[nextStepIndexToDisplay++];
					rng = reportSheet.Cells[reportSheetRow, 1];
					rng.Value = currentStep.name;
					rng.VerticalAlignment = XlVAlign.xlVAlignCenter;

					rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 2], reportSheet.Cells[reportSheetRow, 4]];
					rng.Merge();
					rng.Value =currentStep.text;

					rng.Select();
					// color code the step text, just like Excel, also grey or bold the text for this task
					foreach (ZStepLoc stepLoc in theStep.stepLocs.Values)
					{
						Characters characters = mApp.ActiveCell.Characters[stepLoc.startPos, stepLoc.length+1];
						if (theStep.tasks.Count > 1 && stepLoc.locType == ZStepLoc.LocType.task)
						{
							characters.Font.Bold = true;
							characters.Font.Underline = XlUnderlineStyle.xlUnderlineStyleSingle;
						}
						else if (stepLoc.locType == ZStepLoc.LocType.tag)
						{
							characters.Font.Bold = true;
							characters.Font.Color = stepLoc.color;
						}
					}
					rng.WrapText = true;
					double rh = rng.RowHeight;
					string stxt = currentStep.text;
					if (stxt.Contains('\n'))
					{
						// see how many
						int lines = stxt.Count<char>(isNewLine) + 1;
						// if we didn't expand the height of the row after wrapping, we need to do it now
						if (rh == rng.RowHeight)
						{
							rh *= lines;
							// make sure we don't get too big
							if (rh < 1000)
								rng.RowHeight = rh;
						}
					}


					rng.WrapText = true;
					rng = reportSheet.Cells[reportSheetRow, 5];
					// nothing was deducted
					rng.Value = string.Format("0/{0:f2}", currentStep.pts );
					reportSheetRow++;
					// now dump task stuff
					foreach (ZTask task in currentStep.tasks)
					{
						rng = reportSheet.Cells[reportSheetRow, 2];
						rng.Value = task.id;
						rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
						rng = reportSheet.Cells[reportSheetRow, 3];
						rng.Value = "OK";
						rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
						rng = reportSheet.Cells[reportSheetRow, 4];
						t = task.text;
						rng.Value = task.text;
						rng.Select();
						ZTaskLocs taskLocs = new ZTaskLocs(task.parentStep.stepLocs, task.text);
						if (taskLocs != null  && taskLocs.locs.Count > 0)
						{
							foreach (ZTaskLoc taskLoc in taskLocs.locs)
							{
								Characters characters = mApp.ActiveCell.Characters[taskLoc.startPos, taskLoc.length+1];
								characters.Font.Bold = true;
								characters.Font.Color = taskLoc.color;
							}
						}

						rng.WrapText = true;
						// output mapping information, category, difficulty, action, objective, ...
						objectiveCol = 6;
						rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
						rng.Value = task.mapping.category;
						rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
						rng.Value = task.mapping.difficulty;
						rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
						rng.Value = task.mapping.action;
						rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
						rng.Value = task.mapping.objective;
						rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
						rng.Value = ontology.objectives[ task.mapping.objective].text;
						reportSheetRow++;
					}
				}
				#endregion
				// if we haven't output this step already
				if (nextStepIndexToDisplay <= theStep.index)
				{
					currentStep = theStep;
					nextStepIndexToDisplay = currentStep.index + 1;
					rng = reportSheet.Cells[reportSheetRow, 1];
					rng.Value = currentStep.name;
					rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
					rng = reportSheet.Range[reportSheet.Cells[reportSheetRow, 2], reportSheet.Cells[reportSheetRow, 4]];
					rng.Merge();
					rng.Value =currentStep.text;
					rng.Select();
					// color code the step text, just like Excel, also grey or bold the text for this task
					foreach (ZStepLoc stepLoc in theStep.stepLocs.Values)
					{
						// only color the tags, but underline the individual tasks if more than one present
						Characters characters = mApp.ActiveCell.Characters[stepLoc.startPos, stepLoc.length+1];
						if (theStep.tasks.Count > 1 && stepLoc.locType == ZStepLoc.LocType.task)
						{
							characters.Font.Bold = true;
							characters.Font.Underline = XlUnderlineStyle.xlUnderlineStyleSingle;
						}
						else if (stepLoc.locType == ZStepLoc.LocType.tag)
						{
							characters.Font.Bold = true;
							characters.Font.Color = stepLoc.color;
						}
					}

					double rowHeight = rng.RowHeight;
					rng.WrapText = true;
					// need to see if the row should be made taller
					string txt = currentStep.text;
					if (txt.Contains('\n'))
					{
						// see how many
						int lines = txt.Count<char>(isNewLine) + 1;
						// if we didn't expand the height of the row after wrapping, we need to do it now
						if (rowHeight == rng.RowHeight)
						{
							rowHeight *= lines;
							// make sure we don't get too big
							if (rowHeight < 1000 )
								rng.RowHeight = rowHeight;
						}
					}
					rng = reportSheet.Cells[reportSheetRow, 5];
					double deduction = stepDeduction(taskDeduction.stepID, taskDeductions);
					rng.Value = string.Format("{0:f2}/{1:f2}", deduction, currentStep.pts);
					reportSheetRow++;
				}
				// now display any preceding tasks
				string taskID = taskDeduction.origTaskID;
				ZTask dTask = theStep.tasks[taskID];
				foreach (ZTask task in theStep.tasks)
				{
					if ( task.index >= dTask.index )
						break;
					rng = reportSheet.Cells[reportSheetRow, 2];
					rng.Value = task.id;
					rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
					rng = reportSheet.Cells[reportSheetRow, 3];
					rng.Value = "OK";
					rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
					rng = reportSheet.Cells[reportSheetRow, 4];
					rng.Value = task.text;
					rng.Select();
					ZTaskLocs taskLocs = new ZTaskLocs(task.parentStep.stepLocs, task);
					if (taskLocs != null  && taskLocs.locs.Count > 0)
					{
						foreach (ZTaskLoc taskLoc in taskLocs.locs)
						{
							Characters characters = mApp.ActiveCell.Characters[taskLoc.startPos, taskLoc.length+1];
							characters.Font.Bold = true;
							characters.Font.Color = taskLoc.color;
						}
					}

					rng.WrapText = true;
					// output mapping information, category, difficulty, action, objective, ...
					objectiveCol = 6;
					rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
					rng.Value = task.mapping.category;
					rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
					rng.Value = task.mapping.difficulty;
					rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
					rng.Value = task.mapping.action;
					rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
					rng.Value = task.mapping.objective;
					rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
					rng.Value = ontology.objectives[task.mapping.objective].text;
					reportSheetRow++;
				}
				// now we can display the deduction
				//TODO: add comment
				rng = reportSheet.Cells[reportSheetRow, 2];
				rng.Value = dTask.id;
				rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
				rng = reportSheet.Cells[reportSheetRow, 3];
				rng.Value = "ERR";
				rng.VerticalAlignment = XlVAlign.xlVAlignCenter;
				rng = reportSheet.Cells[reportSheetRow, 4];
				rng.Value = dTask.text;
				rng.Select();
				ZTaskLocs dTaskLocs = new ZTaskLocs(dTask.parentStep.stepLocs, dTask);
				if (dTaskLocs != null  && dTaskLocs.locs.Count > 0)
				{
					foreach (ZTaskLoc taskLoc in dTaskLocs.locs)
					{
						Characters characters = mApp.ActiveCell.Characters[taskLoc.startPos, taskLoc.length+1];
						characters.Font.Bold = true;
						characters.Font.Color = taskLoc.color;
					}
				}

				rng.WrapText = true;
				// add comment if the target is a cell
				if (dTask.target.location.type == "Cell")
				{
					Worksheet errSheet = mApp.Worksheets[dTask.target.location.context];
					Range errCell = errSheet.Range[dTask.target.location.address];
					errCell.ClearComments();
					Comment comment = errCell.AddComment();
					string stepText = theStep.name + ": ";
					int stepOffset = stepText.Length;
					stepText = WrapIt(stepText + theStep.text);
					string taskText = ""; 
					string scenarioText = WrapIt( "Scenario: " + taskDeduction.scenario.name + " - " + taskDeduction.scenario.remediation.feedback);
					int taskOffset = 0;
					if ( theStep.tasks.Count > 1 ) // dTask.text.LevenshteinDistance(theStep.text) > 4)
					{
						taskText = "Task " + dTask.id + ": ";
						taskOffset = taskText.Length + stepText.Length;
						taskText += dTask.text;
						taskText = WrapIt(taskText);
						stepText  += "\n" + taskText;
					}
					else
						taskText = "";
					stepText += "\n\n" + scenarioText + "\n\nDeducted: " + taskDeduction.pointsDeducted.ToString("N2") + " pts";

					comment.Text(stepText);
					foreach (ZStepLoc stepLoc in theStep.stepLocs.Values)
					{
						// only color the tags
//						if (stepLoc.locType == ZStepLoc.LocType.task)
						{
							Characters characters = comment.Shape.TextFrame.Characters(stepOffset + stepLoc.startPos, stepLoc.length+1);
							characters.Font.Bold = true;
							characters.Font.Underline = XlUnderlineStyle.xlUnderlineStyleSingle;
							if (stepLoc.locType == ZStepLoc.LocType.tag)
								characters.Font.Color = stepLoc.color;
						}
					}

					if (taskText.Length > 0 && dTaskLocs != null  && dTaskLocs.locs.Count > 0)
					{
						foreach (ZTaskLoc taskLoc in dTaskLocs.locs)
						{
							Characters characters = comment.Shape.TextFrame.Characters(taskOffset + taskLoc.startPos, taskLoc.length+1);
							characters.Font.Bold = true;
							characters.Font.Color = taskLoc.color;
						}
					}
					comment.Shape.TextFrame.AutoSize = true;
				}

				// output mapping information, category, difficulty, action, objective, ...
				objectiveCol = 6;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = dTask.mapping.category;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = dTask.mapping.difficulty;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = dTask.mapping.action;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = dTask.mapping.objective;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = ontology.objectives[dTask.mapping.objective].text;
				reportSheetRow++;

				objectiveCol = 4;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				t = "Scenario: " + taskDeduction.scenario.name + " - " + taskDeduction.scenario.remediation.feedback;
				rng.Value = t;
				rng.WrapText = true;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = taskDeduction.scenario.deduction.pointsDeducted;

//				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
//				rng.Value = taskDeduction.scenario.remediation.category;
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				// use task category if no deduction category present
				rng.Value = taskDeduction.scenario.deduction.category.Length > 0 ? taskDeduction.scenario.deduction.category : dTask.mapping.category;
				
				rng = reportSheet.Cells[reportSheetRow, objectiveCol++];
				rng.Value = taskDeduction.scenario.deduction.type;
				reportSheetRow++;
				// output resources for this objective/error, this will change once we
				// have error specific feedback
				foreach (ZResource res in resources)
				{
					string name = res.id;
					rp = rps[name];
					if (rp != null)
					{
						oms = rp.objectiveMappings;
						// TODO: handle multiple mappings for same id, like EX281
						List<ZObjectiveMapping> omList = oms[dTask.mapping.objective];
						foreach (ZObjectiveMapping om in omList)
						{
							ZObjectiveResources ors = rp.Resources(om.type);
							string omName = om.name;
							ZObjectiveResource or = ors[om.name];
							objectiveCol = 4;
							rng = reportSheet.Cells[reportSheetRow, objectiveCol];
							rng.Value = name + " - Type[" + om.type + "], ID[" + or.id + "], Desc:" + or.text;
							rng.WrapText = true;
							reportSheetRow++;
						}
					}
				}
				t = "Workbook: '" + taskDeduction.task.target.location.context + ", address: '" + taskDeduction.task.target.location.address + "\n";
				string testString = theStep.text;
				grade -= taskDeduction.pointsDeducted;

			}
			//txtOut.Text += string.Format("Grade is {0:N2} out of {1:N2}\n", grade, ZRubric.activeProject.totalPts);
			//txtOut.Text += "==============================================================\n";


		}
		public void SaveMarkedupSubmission()
		{
			string markupFilename = mWorkbook.FullName.Replace(".xlsx", "MarkedUp.xlsx");
			if (File.Exists(markupFilename))
				File.Delete(markupFilename);
			mWorkbook.SaveAs(markupFilename);
		}
		public bool SelectRange(string rangeText)
		{
			bool rangeSelected = false;
			try
			{
				Worksheet ws = mApp.ActiveSheet;
				ws.Range[rangeText].Select();
				rangeSelected = true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Trying to select[" + rangeText + "] - " + ex.ToString());
			}
			return rangeSelected;
		}
	}
	public class ZExcelToJson : JsonTextWriter
	{
		private ZExcel zExcel;
		private Application   mApp
		{
			get
			{
				return zExcel.mApp;
			}
		}
		private bool usingExistingInstance
		{
			get { return zExcel.usingExistingInstance; }
			set { zExcel.usingExistingInstance = value; }
		}
		private Workbook mWorkbook
		{
			get { return zExcel.mWorkbook; }
			set { zExcel.mWorkbook = value; }
		}
		private bool mIncludeFormatting;
		public string currentFolder
		{
			get { return zExcel.currentFolder; }
			set { zExcel.currentFolder = value; }
		}
		public string activeSheetName
		{
			get 
			{
				Worksheet wkSheet = zExcel.mWorkbook.ActiveSheet as Worksheet;
				return wkSheet.Name;
			}
		}
		public string selectedAddress
		{
			get
			{
				Worksheet wkSheet = zExcel.mWorkbook.ActiveSheet as Worksheet;
				string value = "";
				// TODO: this will need to change once we start working with other stuff
				object selectedObject = zExcel.mApp.Selection;
				Type selectedObjectType = selectedObject.GetType();
				if (selectedObjectType == typeof( Range) )
				{
					Range selectedRange = zExcel.mApp.Selection as Range;
					value = selectedRange.Address.ToString();		 
				}
				return value;
			}
		}
		public string activeCellAddress
		{
			get
			{
				string value = zExcel.mApp.ActiveCell.Address.ToString();
				return value;
			}
		}
		public string activeCellText
		{
			get
			{
				string value = "";
				value = zExcel.mApp.ActiveCell.Value.ToString();
				value = zExcel.mApp.ActiveCell.Value2.ToString();
				return value;
			}
		}
		public string activeCellFormula
		{
			get
			{
				string value = "";
				if (zExcel.mApp.ActiveCell.HasFormula)
				{
					value = zExcel.mApp.ActiveCell.Formula.ToString();
				}
				return value;
			}
		}
		public string activeCellFormulaR1C1
		{
			get
			{
				string value = "";
				if (zExcel.mApp.ActiveCell.HasFormula)
				{
					value = zExcel.mApp.ActiveCell.FormulaR1C1.ToString();
				}
				return value;
			}
		}
		public int stepTaskCounter;
		public void ToJson()
		{
			if ( !usingExistingInstance )
				return;
			mIncludeFormatting = true;
			// just grab the first workbook
			var th = mWorkbook.Theme.ToString();
			Workbook2Json(mWorkbook);
		}
		public string SelectionToJson()
		{
			string	json = "";
			if (!usingExistingInstance)
				return json;
			mIncludeFormatting = true;
			// just grab the first workbook
			Range2Json(mWorkbook.ActiveSheet,mApp.Selection);
			json = m_writer.ToString();
			CloseOutput = true;
			Close();
			m_writer = new StringWriter(new StringBuilder());
			return json;
		}
		public void MarkupSubmission(ZRubric rubric, ZTaskDeductions taskDeductions) 
		{
			zExcel.MarkupSubmission(rubric, taskDeductions); 
		}
		public void SaveMarkedupSubmission() { zExcel.SaveMarkedupSubmission(); }
		public string SaveSubmission(bool refreshRubricSubmission = false)
		{
			ToJson();
			string jsonfn = currentFolder + @"\submission.json";
			string submission = m_writer.ToString();
			using (StreamWriter outfile = new StreamWriter(jsonfn))
			{
				outfile.Write(m_writer.ToString());
			}
			CloseOutput = true;
			Close();
			m_writer = new StringWriter(new StringBuilder());

			return submission;
		}
		public void ToJson(string filename)
		{
			mIncludeFormatting = false;
			mWorkbook = mApp.Workbooks.Open(filename);
			var th = mWorkbook.Theme.ToString();
			Workbook2Json( mWorkbook );
			mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
		}
		~ZExcelToJson()
		{
			//mApp.Quit();
			zExcel = null;
		}
		public void Reset()
		{
			//_writer = null;
			//_writer = new StringWriter(new StringBuilder());
		}
		
		public ZExcelToJson()
			: base(new StringWriter(new StringBuilder()))
		{
			Formatting = Formatting.Indented;
			stepTaskCounter = 0;
			zExcel = new ZExcel();
			//Try to reuse an excel application object
			Worksheet wksht = mApp.ActiveSheet;
			//tvJson.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvJson_AfterSelect);
			mApp.SheetChange += mApp_SheetChange;

			return;
#region old stuff
			//Microsoft.Office.Core.MsoThemeColorSchemeIndex tcs = mWorkbook.Theme.ThemeColorScheme.;
			//int tcsCount = 0;

			//string ts = tcs.ToString();
			//System.ConsoleColor cc = System.ConsoleColor.DarkBlue;
			//ts = cc.ToString();
			//double wc;
			//// rgbDarkBlue = 9109504
			//r = RGB2String(9109504);
			//Debug.Print(r);
			//for (tcsCount = 1; tcsCount < tcs.Count; ++tcsCount)
			//{
			//    ThemeColor tcx = tcs.Colors((MsoThemeColorSchemeIndex) tcsCount);
			//      wc = (double)mWorkbook.Colors[tcx.ThemeColorSchemeIndex];
			//      var vwc = tcs.Colors(tcx.ThemeColorSchemeIndex).RGB;
			//    Debug.Print(" idx '{0}', rgb {1}, wkbk {2}, vwc {3} ", tcx.ThemeColorSchemeIndex, tcx.RGB, wc, RGB2String((long) vwc));
			//}
			//Debug.Print( "\n Character count: {0}", tcsCount );

			//var th0 = mWorkbook.Theme;
			//var clrs = mWorkbook.Colors;
			////var color = mWorkbook.Colors(24);
			//mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
			//mApp.Quit();
			//mApp = null;

			//Workbook2Json(mWorkbook);
			//jsonfn = filename.Replace("xlsx", "json");
			//using (StreamWriter outfile = new StreamWriter(jsonfn))
			//{
			//    outfile.Write(sw.ToString());
			//}
			
			//Debug.WriteLine(sw.ToString());
			//mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
			//mApp.Quit();
			//mApp = null;
#endregion
		}

		public bool SelectRange(string rangeText)
		{
			return zExcel.SelectRange(rangeText);
		}
		void mApp_SheetChange(object sht, Range target)
		{
			// prompt to see if this change should be recorded
			string filename = mApp.ActiveWorkbook.FullName;
			++stepTaskCounter;
			string jsonfn = string.Format("{0}_{1}.json", filename.Replace(".xlsx", ""),stepTaskCounter.ToString("000"));
			Range2Json(sht as Worksheet, target);
			
			using (StreamWriter outfile = new StreamWriter(jsonfn))
			{
			    outfile.Write(m_writer.ToString());
			}
			CloseOutput = true;
			Close();
			m_writer = new StringWriter(new StringBuilder());
		}
		
		public ZExcelToJson(string filename, int ex34)
			: base(new StringWriter(new StringBuilder()))
		{
			zExcel = new ZExcel(filename, ex34);
			StringBuilder sb = new StringBuilder();
			StringWriter sw = new StringWriter(sb);
			JsonWriter jsonWriter = new JsonTextWriter(sw);
			Formatting = Formatting.Indented;
			Workbook2Json(mWorkbook);
			WriteStartObject();
			WritePropertyName("Name");
			WriteValue(mWorkbook.Name);
			WritePropertyName("Type");
			WriteValue("Workbook");

			WritePropertyName("Worksheets");
			WriteStartArray();

			foreach (Worksheet sht in mWorkbook.Worksheets)
			{
				Debug.Print(sht.Name);
				WriteStartObject();
				WritePropertyName("Name");
				WriteValue(sht.Name);
				WritePropertyName("Type");
				WriteValue("Sheet");
				if (sht.Name == "Documentation")
				{
					WritePropertyName("Cells");
					WriteStartArray();
					// dump the cells values
					foreach (Range cell in sht.UsedRange)
					{
						string val = "";
						if (cell.Value != null)
							val = cell.Value.ToString();
						else if (cell.Value2 != null)
							val = cell.Value2.ToString();
						WriteStartObject();
						WritePropertyName("Name");
						WriteValue(cell.Address.ToString());
						WritePropertyName("Type");
						WriteValue("Cell");
						WritePropertyName("Value");
						WriteValue(val);
						WriteEndObject();
						Debug.Write(cell.Row.ToString() + "," + cell.Column.ToString() + ":" + val);
					}
					WriteEnd();
					WriteEndObject();
				}
				else if (sht.Name == "Stock History")
				{
					// dump chart properties
					// 2a	In the Stock Values worksheet, select the range A3:F33, and then 
					//		insert a Volume-Open-High-Low-Close stock chart. ( Hint: Click the 
					//		Other Charts button in the Charts group on the Insert tab to locate the stock 
					//		charts.) 
					// 2b	Move the embedded chart to the chart sheet named Stock History
					// 3a	Insert the chart title Mitchell Oil above the chart area, 
					// 3b	set the font size of the chart title to 18 points
					// 3c	Remove the chart legend
					// 4a	Add the title Date to the primary horizontal axis
					// 4b	set its font size to 14 points
					// 4c	Add the title Shares Traded to the primary vertical axis
					// 4d	set its font size to 14 points
					// 4e	rotate the title 270°
					// 5a	Add the title Stock Value to the secondary vertical axis
					// 5b	set its font size to 14 points, and rotate the title 90°. 
					//		( Hint: Open the Format Axis Title dialog box for the second-ary vertical axis and use the Text direction button found in the Alignment category.) 
					// 6	Set the font size of all axis values to 12 points
					// 7a	Display the horizontal and vertical gridlines using a dashed line style
					// 7b	Set the interval between major tick marks on the primary horizontal axis to 7 days. 
					// 8a	For the primary vertical axis, display the values in units of one million
					// 8b	change the number format to two decimal place accuracy
					// 8c	set the maximum value of the axis scale to 2,000,000. 
					// 9	For the secondary vertical axis, set the minimum value of the scale to 10
					// 10a	Decrease the gap width between the columns in the plot to 30%
					// 10b	change the fill color to light blue
					// 11	Change the fill color of the plot area to light yellow.
					// 12	In a stock market chart, the daily chart values will show either an increase or 
					//		a decrease from the previous day. Increases are shown with an up bar displayed 
					//		in white and decreases are shown with a down bar displayed in black. 
					// 12a	Select the data series for the up bars and change their fill colors to light green
					// 12b	Select the data series for the down bars and change their fill colors to red.
					// 

					//// dump the charts in this workbook
					//WritePropertyName("Charts");
					//WriteStartArray();

					//foreach (Chart chart in mWorkbook.Charts)
					//{
					//    Chart2Json(chart);
					//}
					//WriteEnd();

					WritePropertyName("Charts");
					WriteStartArray();
					ChartObject chart = sht.ChartObjects(0);
					{
						string val = "";
						string val2 = "";
						WriteStartObject();
						WritePropertyName("Name");
						WriteValue(chart.Name);
						WritePropertyName("Type");
						WriteValue("Chart");
						WritePropertyName("Value");
						WriteValue(val);
						WritePropertyName("Value2");
						WriteValue(val2);
						WriteEndObject();

					}
					WriteEnd();
					WriteEndObject();

				}
				else if (sht.Name == "Stock Values")
				{
					// dump difference column (G)
					// 13	Go to the Stock Values worksheet. 
					// 13a	Add a column sparkline to cell B2 to display the Shares Traded values 
					//		in the range B4:B33
					// 14	Add a line sparkline to cell F2 to display the closing values of the 
					//		stock over the range F4:F33
					// 15a	In the range G3:G33, create a new column of values named Difference that 
					// 15b	is equal to the difference between the stock’s closing value and its 
					//		opening value on each day. 
					// 15c	Display the difference values using the same format as in column F
					// 16	Add a win/loss sparkline to cell G2 that indicates the days that the stock 
					//		gained value and the days that the stock lost value.
					// 
					WritePropertyName("Cells");
					WriteStartArray();
					// dump the cells values
					foreach (Range cell in sht.UsedRange)
					{
						string val = "";
						string val2 = "";
						string formula = "";
						if (cell.Value != null)
							val = cell.Value.ToString();
						if (cell.Value2 != null)
							val2 = cell.Value2.ToString();
						WriteStartObject();
						WritePropertyName("Name");
						WriteValue(cell.Address.ToString());
						WritePropertyName("Type");
						WriteValue("Cell");
						WritePropertyName("Value");
						WriteValue(val);
						WritePropertyName("Value2");
						WriteValue(val2);
						if (cell.HasFormula)
						{
							formula = cell.Formula.ToString();
							WritePropertyName("Formula");
							WriteValue(formula);
						}
						WriteEndObject();
						Debug.Write(cell.Row.ToString() + "," + cell.Column.ToString() + ":" + val);
					}
					WriteEnd();
					// charts on




					WriteEndObject();

				}
				else
				{
					Debug.WriteLine(sht.Name);

				}
			}
			WriteEnd();
			WriteEndObject();
			Debug.WriteLine(sw.ToString());
			mApp.ActiveWorkbook.Close(XlSaveAction.xlDoNotSaveChanges);
			mApp.Quit();
			zExcel = null;
		}
		private void Workbook2Json( Workbook wkbk)
		{
			WriteStartObject();
			WritePropertyName("Name");
			WriteValue(mWorkbook.Name);
			WritePropertyName("Type");
			WriteValue("Workbook");
			WorkbookDefaults2Json( mWorkbook);

			WritePropertyName("LCAs");
			WriteStartArray();
			LCAs2Json();
			WriteEnd();

			WritePropertyName("Worksheets");
			WriteStartArray();

			foreach (Worksheet sht in mWorkbook.Worksheets)
			{
				Debug.Print(sht.Name);
				Sheet2Json(sht);
				Debug.WriteLine(sht.Name);
			}
			WriteEnd();
			WriteEndObject();
		}
		private void WorkbookDefaults2Json(Workbook wkbk)
		{
			if (mIncludeFormatting)
			{
				WritePropertyName("Defaults");
				WriteStartObject();
				// based on theme
				WritePropertyName("Font");
				WriteStartObject();
				Style style = wkbk.Styles["Normal"];
				Font font = style.Font;
				WritePropertyName("Name");
				WriteValue(font.Name);
				WritePropertyName("size");
				WriteValue(font.Size);
				// figure out default column width
				
				WriteEndObject();
				WriteEndObject();
			}
		}
		private void ColumnWidths2Json(Worksheet sht)
		{
			if (!mIncludeFormatting)
				return;
			WritePropertyName("ColumnWidths");
			WriteStartArray();
			int	firstUnusedColumn = sht.UsedRange.Columns.Columns.Count + 1;
			for (int colOffset = 1; colOffset <= (sht.UsedRange.Columns.Columns.Count + 1); ++colOffset)
			{
				string address = sht.UsedRange.Cells[colOffset][1].Address;
				var cols = address.Split('$');

				string colName =  cols[1];	// address.Split( ['$'] )[0];
				if ( colOffset == firstUnusedColumn )
					colName = "Default";
				double colWidth = sht.UsedRange[colOffset][1].ColumnWidth;
				WriteStartObject();
				WritePropertyName("Column");
				WriteValue(colName);
				WritePropertyName("Width");
				WriteValue(colWidth.ToString());
				WriteEndObject();
			}	
			WriteEnd();
		}

		private void Range2Json(Worksheet sht, Range range )
		{
			WriteStartObject();
			WritePropertyName(sht.Name);
			WriteStartObject();
			WritePropertyName("Name");
			WriteValue(sht.Name);
			WritePropertyName("Type");
			WriteValue("Sheet");

			WritePropertyName("Cells");
			WriteStartArray();
			// dump the cells values
			foreach (Range cell in range)
			{
				// TODO: ignore empty cells? As long as they don't have any special formatting
				// TODO: borders?
				Cell2Json(cell);
			}
			WriteEnd();
			WriteEndObject();
			WriteEndObject();
		}

		private void Sheet2Json(Worksheet sht)
		{
			WriteStartObject();
				WritePropertyName(sht.Name);
				WriteStartObject( );
					WritePropertyName("Name");
					WriteValue(sht.Name);
					WritePropertyName("Type");
					WriteValue("Sheet");
					ColumnWidths2Json(sht);

					WritePropertyName("Ranges");
					WriteStartArray();
					BorderRanges2Json(sht);
					FillRanges2Json(sht);
					WriteEnd();

					WritePropertyName("Cells");
					WriteStartArray();
					// dump the cells values
					foreach (Range cell in sht.UsedRange)
					{
						// TODO: ignore empty cells? As long as they don't have any special formatting
						// TODO: borders?
						Cell2Json(cell);
					}
					WriteEnd();
				WriteEndObject();
			WriteEndObject();
		}
#region LCAs
		private void LCAs2Json()
		{
//			Range	namedRange;
			foreach(Name name in mWorkbook.Names)
			{
				string theName = "" + name.Name;
				if (theName.StartsWith("_"))
				{
					WriteStartObject();
					WritePropertyName("LCA");
					WriteValue(theName);
					WritePropertyName("Address");
					WriteValue(name.RefersToRange.Address);
					WriteEndObject();
				}
			}
		}
		// TODO: handle unnamed ranges for borders and other unnamed regions
		private void FillRanges2Json(Worksheet sht)
		{
			Range range = sht.UsedRange;
			Range nextCell, leftSideCell, rightSideCell;
			// differentiate between formulas, value2, value and text and formats, at least figure out what default
			// values are, and aren't
			// 
			// dump unnamed ranges, which need to be found first
			int rangeSeqNum = 0;
			long i;
			Range ul, ll, ur, lr;	// upperleft and lower-right 
			ul = ll = ur = lr = null;
			// string str = "";
			int lastCol = sht.UsedRange.Columns.Count + sht.UsedRange.Column - 1;
			int lastRow = sht.UsedRange.Rows.Count + sht.UsedRange.Row - 1;
//			int noFillColor = (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone;
			foreach (Range chkCell in sht.UsedRange)
			{
				Debug.Print("cell:" + chkCell.Address);
				//if (chkCell.Address == "$A$3")
				//	ul = ul;
				// if we haven't found the ul corner yet
				if (ul == null)
				{
					// check for a multi-cell box first, then check for a border applied to
					// a contiguous "row" of cells
					// look for border around first, then look for all borders and other excel options
					if (IsTopLeftCornerFill(sht, chkCell))
					{
						ul = chkCell;
						// see if this cell is the topleft corner of a fill range, either fill color or pattern
						int theCellCI = (int)chkCell.Interior.ColorIndex;
						// check this row
						for (i = chkCell.Column + 1; i <= lastCol; ++i)
						{
							nextCell = sht.Cells[chkCell.Row, i];
							Debug.Print("nextCell:" + nextCell.Address);
							// make sure the next cell has the same fill, if not, that's a problem
							if ((nextCell.Interior.ColorIndex == chkCell.Interior.ColorIndex)
									&& IsTopRightCornerFill(sht, nextCell))
							{
								ur = nextCell;
								break;
							}
						}
					}
					else
					{
						ul = null;
					}

					// if we have an upper left and an upper right check for lower left
					if ((ul != null) && (ur != null))
					{
						// check left edge
						for (i = chkCell.Row + 1; i <= lastRow; ++i)
						{
							leftSideCell = sht.Cells[i, ul.Column];
							rightSideCell = sht.Cells[i, ur.Column];
							// make sure the next cell has the same borders, if not, that's a problem
							if (leftSideCell.Interior.ColorIndex == ul.Interior.ColorIndex
									&& (rightSideCell.Interior.ColorIndex == ur.Interior.ColorIndex)
									&& IsBottomLeftCornerFill(sht, leftSideCell) && IsBottomRightCornerFill(sht, rightSideCell) )
							{
								ll = leftSideCell;
								lr = rightSideCell;
								++rangeSeqNum;

								Debug.Print("Found fill in: " + ul.Address + ":" + lr.Address);
								WriteStartObject();
								WritePropertyName("Range");
								WriteValue(ul.Address + ":" + lr.Address);
								WritePropertyName("Type");
								WriteValue("Fill");
								CellFill2Json(ul);
								WriteEndObject();
								ul = ur = ll = lr = null;
								break;
							}
						}
					}
					else
					{
						ul = null;
						ur = null;
					}
				}
			}
		}
		private void BorderRanges2Json(Worksheet sht)
		{
			Range range = sht.UsedRange;
			Range nextCell, leftSideCell, rightSideCell;
			// differentiate between formulas, value2, value and text and formats, at least figure out what default
			// values are, and aren't
			// 
			// dump unnamed ranges, which need to be found first
			int rangeSeqNum = 0;
			long i;
			Range ul, ll, ur, lr;	// upperleft and lower-right 
			ul = ll = ur = lr = null;
			// string str = "";
			int lastCol = sht.UsedRange.Columns.Count + sht.UsedRange.Column - 1;
			int lastRow = sht.UsedRange.Rows.Count + sht.UsedRange.Row - 1;

			foreach (Range chkCell in sht.UsedRange)
			{
				Debug.Print("cell:" + chkCell.Address);
				if (chkCell.Address == "$A$3")
					ul = null;
				// if we haven't found the ul corner yet
				if (ul == null)
				{
					// check for a multi-cell box first, then check for a border applied to
					// a contiguous "row" of cells
					// look for border around first, then look for all borders and other excel options
					if (IsTopLeftCornerBorder(chkCell.Parent, chkCell))
					{
						ul = chkCell;
						// check this row
						for (i = chkCell.Column + 1; i <= lastCol; ++i)
						{
							nextCell = sht.Cells[chkCell.Row, i];
							Debug.Print("nextCell:" + nextCell.Address);
							// make sure the next cell has the same borders, if not, that's a problem
							if (nextCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle == chkCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle)
							{
								if (IsTopRightCornerBorder(sht, nextCell))
								{
									ur = nextCell;
									break;
								}
							}
						}
					}
					else
					{
						ul = null;
					}

					// if we have an upper left and an upper right check for lower left
					if ((ul != null) && (ur != null))
					{
						// check left edge
						for (i = chkCell.Row + 1; i <= lastRow; ++i)
						{
							leftSideCell = sht.Cells[i, ul.Column];
							rightSideCell = sht.Cells[i, ur.Column];
							// make sure the next cell has the same borders, if not, that's a problem
							if (leftSideCell.Borders[XlBordersIndex.xlEdgeLeft].LineStyle == ul.Borders[XlBordersIndex.xlEdgeLeft].LineStyle
									&& rightSideCell.Borders[XlBordersIndex.xlEdgeRight].LineStyle == ur.Borders[XlBordersIndex.xlEdgeRight].LineStyle)
							{
								if (IsBottomLeftCornerBorder(sht, leftSideCell) && IsBottomRightCornerBorder(sht, rightSideCell))
								{
									ll = leftSideCell;
									lr = rightSideCell;
									++rangeSeqNum;

									Debug.Print("Found fill in: " + ul.Address + ":" + lr.Address);
									WriteStartObject();
									WritePropertyName("Range");
									WriteValue(ul.Address + ":" + lr.Address);
									WritePropertyName("Type");
									WriteValue("Border");
									Border border = leftSideCell.Borders[XlBordersIndex.xlEdgeLeft];
									XlLineStyle ls = (XlLineStyle)border.LineStyle;
									WritePropertyName("LineStyle");
									WriteValue(ls.ToString());
									WritePropertyName("Weight");
									Microsoft.Office.Interop.Excel.XlBorderWeight weight = (Microsoft.Office.Interop.Excel.XlBorderWeight)border.Weight;
									WriteValue(weight.ToString());
									WriteEndObject();
									ul = ur = ll = lr = null;
									break;
								}
							}
						}
					}
					else
					{
						ul = null;
						ur = null;
					}
				}
			}
		}
	
		private bool IsTopLeftCornerBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// see if this cell is the topleft corner of a border range
			// if it has a border at the top and left?
			XlLineStyle theLineStyle = (XlLineStyle) theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle;
			if (theLineStyle != XlLineStyle.xlLineStyleNone &&
					(XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeLeft].LineStyle == theLineStyle)
			{
				// now check to the right and to the left
				Range cellBelow = theSht.Cells[theCell.Row + 1, theCell.Column];
				Range cell2Right = theSht.Cells[theCell.Row, theCell.Column + 1];
				XlLineStyle cellAboveLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Row > 1)
					cellAboveLineStyle = (XlLineStyle)theSht.Cells[theCell.Row - 1, theCell.Column].Borders(XlBordersIndex.xlEdgeTop).LineStyle;
				XlLineStyle cell2LeftLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Column > 1)
					cell2LeftLineStyle = (XlLineStyle)theSht.Cells[theCell.Row, theCell.Column - 1].Borders(XlBordersIndex.xlEdgeTop).LineStyle;
				if ((XlLineStyle)cellBelow.Borders[XlBordersIndex.xlEdgeLeft].LineStyle == theLineStyle &&
						(XlLineStyle)cell2Right.Borders[XlBordersIndex.xlEdgeTop].LineStyle == theLineStyle)
				{
					if ((cellAboveLineStyle == XlLineStyle.xlLineStyleNone) && (cell2LeftLineStyle == XlLineStyle.xlLineStyleNone))
						results = true;
				}

			}
			return results;
		}
		private bool IsTopRightCornerBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// see if this cell is the topright corner of a border range
			// if it has a border at the top and right?
			XlLineStyle theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle;
			XlLineStyle rightLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeRight].LineStyle;
			//
			if (theLineStyle != XlLineStyle.xlLineStyleNone && rightLineStyle == theLineStyle)
			{
				Range cellBelow = theSht.Cells[theCell.Row + 1, theCell.Column];
				Range cell2Right = theSht.Cells[theCell.Row, theCell.Column + 1];
				// now check above and to the right 
				//       X
				//      - X
				//       |
				//
				XlLineStyle cellAboveLineStyle = (XlLineStyle)XlLineStyle.xlLineStyleNone;
				if (theCell.Row > 1)
					cellAboveLineStyle = (XlLineStyle)theSht.Cells[theCell.Row - 1, theCell.Column].Borders(XlBordersIndex.xlEdgeRight).LineStyle;
				XlLineStyle cell2LeftLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Column > 1)
					cell2LeftLineStyle = (XlLineStyle)theSht.Cells[theCell.Row, theCell.Column - 1].Borders(XlBordersIndex.xlEdgeTop).LineStyle;

				if ((XlLineStyle)cellBelow.Borders[XlBordersIndex.xlEdgeRight].LineStyle == theLineStyle
						&& (XlLineStyle)cell2Right.Borders[XlBordersIndex.xlEdgeTop].LineStyle == XlLineStyle.xlLineStyleNone
						&& cellAboveLineStyle == XlLineStyle.xlLineStyleNone
						&& cell2LeftLineStyle == theLineStyle)
				{
					results = true;
				}

			}
			return results;
		}
		private bool IsBottomRightCornerBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			if (theCell.Address == "$E$12")
				Debug.Print("gotit");

			// see if this cell is the bottomRight corner of a border range
			XlLineStyle theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle;
			if (theLineStyle != XlLineStyle.xlLineStyleNone &&
					(XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeRight].LineStyle == theLineStyle)
			{
				// now check to the left and above
				Range cellBelow = theSht.Cells[theCell.Row + 1, theCell.Column];
				Range cell2Right = theSht.Cells[theCell.Row, theCell.Column + 1];
				XlLineStyle cellAboveLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Row > 1)
					cellAboveLineStyle = (XlLineStyle)theSht.Cells[theCell.Row - 1, theCell.Column].Borders(XlBordersIndex.xlEdgeRight).LineStyle;
				XlLineStyle cell2LeftLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Column > 1)
					cell2LeftLineStyle = (XlLineStyle)theSht.Cells[theCell.Row, theCell.Column - 1].Borders(XlBordersIndex.xlEdgeBottom).LineStyle;
				if (cellAboveLineStyle == theLineStyle
						&& (XlLineStyle)cell2Right.Borders[XlBordersIndex.xlEdgeBottom].LineStyle == XlLineStyle.xlLineStyleNone
						&& (XlLineStyle)cellBelow.Borders[XlBordersIndex.xlEdgeRight].LineStyle == XlLineStyle.xlLineStyleNone 
						&& cell2LeftLineStyle == theLineStyle )
				{
					results = true;
				}

			}
			return results;
		}
		private bool IsBottomLeftCornerBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			if ( theCell.Address == "$A$12")
				Debug.Print ("gotit");
			// see if this cell is the bottomRight corner of a border range
			XlLineStyle theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle;
			if (theLineStyle != XlLineStyle.xlLineStyleNone &&
					(XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeLeft].LineStyle == theLineStyle)
			{
				// now check to the left and above
				Range cellBelow = theSht.Cells[theCell.Row + 1, theCell.Column];
				Range cell2Right = theSht.Cells[theCell.Row, theCell.Column + 1];
				XlLineStyle cellAboveLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Row > 1)
					cellAboveLineStyle = (XlLineStyle)theSht.Cells[theCell.Row - 1, theCell.Column].Borders(XlBordersIndex.xlEdgeLeft).LineStyle;
				XlLineStyle cell2LeftLineStyle = XlLineStyle.xlLineStyleNone;
				if (theCell.Column > 1)
					cell2LeftLineStyle = (XlLineStyle)theSht.Cells[theCell.Row, theCell.Column - 1].Borders(XlBordersIndex.xlEdgeBottom).LineStyle;
				// look for break in consecutive border, cell below should not have a left border
				if (cellAboveLineStyle == theLineStyle
						&& (XlLineStyle)cell2Right.Borders[XlBordersIndex.xlEdgeBottom].LineStyle == theLineStyle
						&& (XlLineStyle)cellBelow.Borders[XlBordersIndex.xlEdgeLeft].LineStyle == XlLineStyle.xlLineStyleNone
						&& cell2LeftLineStyle == XlLineStyle.xlLineStyleNone)
				{
					results = true;
				}
			}
			return results;
		}

		private bool IsLeftEndBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// if this is a top left or bottom left corner, it's not a left end
				//       X
				//      X--
				//       X
			if (IsTopLeftCornerBorder(theSht, theCell) || IsBottomLeftCornerBorder(theSht, theCell))
				return false;
			// if it has a border at the top and right?
			// see if this cell is the left end of a border range
			// we'll assume that the border is at least on the top or the bottom edge
			XlLineStyle theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle;
			if ( theLineStyle != XlLineStyle.xlLineStyleNone )	// check the top
				theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle;
			//
			Range cell2Right = theSht.Cells[theCell.Row, theCell.Column + 1];
			if (theLineStyle != XlLineStyle.xlLineStyleNone 
					&& theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle == cell2Right.Borders[XlBordersIndex.xlEdgeTop].LineStyle
					&& theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle == cell2Right.Borders[XlBordersIndex.xlEdgeBottom].LineStyle )
			{
				results = true;
			}
			return results;
		}
		private bool IsRightEndBorder(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// if this is a top left or bottom right corner, it's not a left end
			//       X
			//      --X
			//       X
			if (IsTopRightCornerBorder(theSht, theCell) || IsBottomRightCornerBorder(theSht, theCell))
				return false;
			// if it has a border at the top and right?
			// see if this cell is the left end of a border range
			// we'll assume that the border is at least on the top or the bottom edge
			XlLineStyle theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle;
			if (theLineStyle != XlLineStyle.xlLineStyleNone)	// check the top
				theLineStyle = (XlLineStyle)theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle;
			//
			if (theLineStyle != XlLineStyle.xlLineStyleNone && theCell.Column > 1 )
			{
				Range cell2Left = theSht.Cells[theCell.Row, theCell.Column - 1];
				if (theCell.Borders[XlBordersIndex.xlEdgeTop].LineStyle == cell2Left.Borders[XlBordersIndex.xlEdgeTop].LineStyle
						&& theCell.Borders[XlBordersIndex.xlEdgeBottom].LineStyle == cell2Left.Borders[XlBordersIndex.xlEdgeBottom].LineStyle)
					results = true;
			}
			return results;
		}


		private bool IsTopLeftCornerFill(Worksheet theSht, Range theCell)
		{
			bool results = false;
			int		noFillColor = (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone;
			if (theCell.Address == "$A$3")
				Debug.Print("gotit");
			// see if this cell is the topleft corner of a fill range, either fill color or pattern
			int	theCellCI = (int)theCell.Interior.ColorIndex;
			if (theCellCI != noFillColor)
			{
			    // now check to the right and to the left
			    int cellBelowCI = theSht.Cells[theCell.Row + 1, theCell.Column].Interior.ColorIndex;
				int cell2RightCI = theSht.Cells[theCell.Row, theCell.Column + 1].Interior.ColorIndex;
			    int cellAboveCI = noFillColor;
			    if (theCell.Row > 1)
			        cellAboveCI = theSht.Cells[theCell.Row - 1, theCell.Column].Interior.ColorIndex;
			    int	cell2LeftCI = noFillColor;
			    if (theCell.Column > 1)
			        cell2LeftCI = (int)theSht.Cells[theCell.Row, theCell.Column - 1].Interior.ColorIndex;

			    if (cellBelowCI == theCellCI && cell2RightCI == theCellCI && cellAboveCI == noFillColor && cell2LeftCI == noFillColor)
					results = true;
			}
			return results;
		}
		private bool IsTopRightCornerFill(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// see if this cell is the topright corner of a fill range
			int noFillColor = (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone;
			int theCellCI = (int)theCell.Interior.ColorIndex;
			if (theCellCI != noFillColor)
			{
				// now check to the right and to the left
				int cellBelowCI = theSht.Cells[theCell.Row + 1, theCell.Column].Interior.ColorIndex;
				int cell2RightCI = theSht.Cells[theCell.Row, theCell.Column + 1].Interior.ColorIndex;
				int cellAboveCI = noFillColor;
				if (theCell.Row > 1)
					cellAboveCI = theSht.Cells[theCell.Row - 1, theCell.Column].Interior.ColorIndex;
				int cell2LeftCI = noFillColor;
				if (theCell.Column > 1)
					cell2LeftCI = (int)theSht.Cells[theCell.Row, theCell.Column - 1].Interior.ColorIndex;

				if (cellBelowCI == theCellCI && cell2RightCI == noFillColor && cellAboveCI == noFillColor && cell2LeftCI == theCellCI)
					results = true;
			}
			return results;
		}
		private bool IsBottomRightCornerFill(Worksheet theSht, Range theCell)
		{
			bool results = false;

			// see if this cell is the bottomRight corner of a border range
			int noFillColor = (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone;
			int theCellCI = (int)theCell.Interior.ColorIndex;
			if (theCellCI != noFillColor)
			{
				// now check to the right and to the left
				int cellBelowCI = theSht.Cells[theCell.Row + 1, theCell.Column].Interior.ColorIndex;
				int cell2RightCI = theSht.Cells[theCell.Row, theCell.Column + 1].Interior.ColorIndex;
				int cellAboveCI = noFillColor;
				if (theCell.Row > 1)
					cellAboveCI = theSht.Cells[theCell.Row - 1, theCell.Column].Interior.ColorIndex;
				int cell2LeftCI = noFillColor;
				if (theCell.Column > 1)
					cell2LeftCI = (int)theSht.Cells[theCell.Row, theCell.Column - 1].Interior.ColorIndex;

				if (cellBelowCI == noFillColor && cell2RightCI == noFillColor && cellAboveCI == theCellCI && cell2LeftCI == theCellCI)
					results = true;
			}
			return results;
		}
		private bool IsBottomLeftCornerFill(Worksheet theSht, Range theCell)
		{
			bool results = false;
			// see if this cell is the bottomRight corner of a border range
			int noFillColor = (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone;
			int theCellCI = (int)theCell.Interior.ColorIndex;
			if (theCellCI != noFillColor)
			{
				// now check to the right and to the left
				int cellBelowCI = theSht.Cells[theCell.Row + 1, theCell.Column].Interior.ColorIndex;
				int cell2RightCI = theSht.Cells[theCell.Row, theCell.Column + 1].Interior.ColorIndex;
				int cellAboveCI = noFillColor;
				if (theCell.Row > 1)
					cellAboveCI = theSht.Cells[theCell.Row - 1, theCell.Column].Interior.ColorIndex;
				int cell2LeftCI = noFillColor;
				if (theCell.Column > 1)
					cell2LeftCI = (int)theSht.Cells[theCell.Row, theCell.Column - 1].Interior.ColorIndex;

				if (cellBelowCI == noFillColor && cell2RightCI == theCellCI && cellAboveCI == theCellCI && cell2LeftCI == noFillColor)
					results = true;
			}
			return results;
		}
#endregion
		private void XXX2Json(Interior interior)
		{
			WritePropertyName("XXX");
			WriteStartObject();
			WritePropertyName("RGB");
			WriteValue(RGB2String(Convert.ToInt64(interior.Color)));
			WriteEndObject();
		}
		// TODO: dump "Normal" style of the workbook
		// TODO: translate non-general styles to corresponding name as well as format string
		// Number formats
		//    Set chkCell = sht.Cells(7, 5)   ' Estimated cost, wrapped text
		//    Set chkCell = sht.Cells(13, 1)  ' date, 6/21/2011, Value2 40715, NumberFormat "m/d/yyyy", Text="6/21/2011", hasFormula=false
		//    Set chkCell = sht.Cells(14, 1)  ' Directions, Cambria, 18, bold, Dark Blue, Dark 2: Style:Builtin=true,Name=Title
		//    Set chkCell = sht.Cells(8, 2)   ' .5 as 1/2, Text=" 1/2", value2=0.5,NumberFormat="# ?/?",
		//    Set chkCell = sht.Cells(8, 5)   ' format price as currency, "$2.50", Text="$2.50", NumberFormat="$#,###0.00", ?wkbk.Styles("currency")
		//    <POSITIVE>;<NEGATIVE>;<ZERO>;<TEXT> = _($* #,##0.00_);_($* (#,##0.00);_($* "-"??_);_(@_)
		//    $* #,##0.00;_($* (#,##0.00);_($* "-"??_);_(@_)
		//    workbook has styles, check styles("normal").font and compare cell against that
		//    check cell style, see if it is Normal, or if it has been modified
		// .NumberFormat = "General"         // General
		// .NumberFormat = "0.00"            // Number
		// .NumberFormat = "$#,##0.00"       // Currency
		// .NumberFormat = "_($* #,##0.00_);_($* (#,##0.00);_($* ""-""??_);_(@_)"    // Accounting
		// .NumberFormat = "m/d/yyyy"                    // Short Date
		// .NumberFormat = "[$-F800]dddd, mmmm dd, yyyy" // Long date
		// .NumberFormat = "[$-F400]h:mm:ss AM/PM"   // Time
		// .NumberFormat = "0.00%"       // Percentage
		// .NumberFormat = "# ?/?"       // Fraction
		// .NumberFormat = "0.00E+00"    // Scientific
		// .NumberFormat = "@"           // Text


		private void Font2Json(Font font)
		{
			WritePropertyName("Font");
			WriteStartObject();
			WritePropertyName("Name");
			WriteValue(font.Name);
			WritePropertyName("Size");
			WriteValue(font.Size.ToString());
			WritePropertyName("Italic");
			WriteValue(font.Italic.ToString());
			WritePropertyName("Bold");
			WriteValue(font.Bold.ToString());
			WritePropertyName("FontStyle");
			WriteValue(font.FontStyle.ToString());
			WritePropertyName("Strikethrough");
			WriteValue(font.Strikethrough.ToString());
			WritePropertyName("Subscript");
			WriteValue(font.Subscript.ToString());
			WritePropertyName("Superscript");
			WriteValue(font.Superscript.ToString());
			WritePropertyName("Underline");
			Microsoft.Office.Interop.Excel.XlUnderlineStyle uls = (Microsoft.Office.Interop.Excel.XlUnderlineStyle)font.Underline;
			WriteValue(string.Format("{0:G}", uls));
			WritePropertyName("Color");
			WriteValue(RGB2String(Convert.ToInt64(font.Color)));
			// there may not be a background color for the font
			try
			{
				XlBackground bg = (XlBackground)font.Background;
				WritePropertyName("Background");
				WriteValue(string.Format("{0:G}", bg));
			}
			catch (Exception ex)
			{
				Debug.Print(ex.Message);
			}
			WriteEndObject();
		}
		private void NumberFormat2Json(Range theCell)
		{
			WritePropertyName("NumberFormat");
			// TODO: allow for specifying number of decimal places, symbol, separator, ... use regular expression
			WriteStartObject();
			string numberFormat = theCell.NumberFormat;
			string formatName = "General";
			if (numberFormat == "General")
				formatName = "General";
			else if (numberFormat == "0" || numberFormat == "0.0" || numberFormat == "0.00")
				formatName = "Number";
			else if (numberFormat.StartsWith("$#,#") || numberFormat.StartsWith("#,#")) // ( numberFormat == "$#,##0.00" )
				formatName = "Currency";
			else if (numberFormat.StartsWith("_($* #,#") || numberFormat.StartsWith("_(* #,#")) // "_($* #,##0.00_);_($* (#,##0.00);_($* \"\"-\"\"??_);_(@_)" )
				formatName = "Accounting";
			else if (numberFormat == "m/d/yyyy")
				formatName = "Short Date";
			else if (numberFormat == "[$-F800]dddd, mmmm dd, yyyy")
				formatName = "Long date";
			else if (numberFormat == "[$-F400]h:mm:ss AM/PM")
				formatName = "Time";
			else if (numberFormat.EndsWith("%")) // == "0.00%" )
				formatName = "Percentage";
			else if (numberFormat == "# ?/?")
				formatName = "Fraction";
			else if (numberFormat.EndsWith("E+00") || numberFormat == "0.00E+00")
				formatName = "Scientific";
			else if (numberFormat == "@")
				formatName = "Text";
			WritePropertyName("Name");
			WriteValue(formatName);
			WritePropertyName("Value");
			WriteValue(numberFormat);
			WriteEndObject();
		}
		private void CellFormat2Json(Range theCell)
		{
			WritePropertyName("Format");
			WriteStartObject();
			// look for various formatting settings
			WritePropertyName("Style");
			WriteStartObject();
				Style style = theCell.Style;
				string tmp = theCell.Style.Name;
				WritePropertyName("Name");
				WriteValue(tmp);
			WriteEndObject();
			Font2Json(theCell.Font);
			CellBorder2Json(theCell);
			CellFill2Json(theCell);
			// merged??
			Range mergeArea = null;
			if (theCell.MergeCells)
			{
				mergeArea = theCell.MergeArea;
				tmp = mergeArea.Address;
				tmp = mergeArea.Address.ToString();
				WritePropertyName("MergeArea");
				WriteValue(tmp);
			}
			WritePropertyName("WordWrap");
			WriteValue(theCell.WrapText.ToString());
			// alignment
			Microsoft.Office.Interop.Excel.XlHAlign halign = (Microsoft.Office.Interop.Excel.XlHAlign)theCell.HorizontalAlignment;
			WritePropertyName("HorizontalAlignment");
			tmp = halign.ToString();
			WriteValue(tmp);
			NumberFormat2Json(theCell);
			WriteEnd();
		}
		private void Color2Json(Interior interior)
		{
			// 12 theme colors
			// Text/Background - Dark 1
			// Text/Background - Light 1
			// Text/Background - Dark 2
			// Text/Background - Light 2
			// Accent 1
			// Accent 2
			// Accent 3
			// Accent 4
			// Accent 5
			// Accent 6
			// Hyperlink
			// Followed Hyperlink

			WritePropertyName("Color");
			WriteStartObject();
				WritePropertyName("RGB");
				WriteValue(RGB2String(Convert.ToInt64(interior.Color)));
				WritePropertyName("MSOThemeRGB");
				WriteValue(RGB2String(Convert.ToInt64(interior.ThemeColor)));
				WritePropertyName("MSOTheme");
				WriteValue(((XlThemeColor)interior.ThemeColor).ToString());
				WritePropertyName("MSO");
				XlRgbColor xlrc = (XlRgbColor)interior.Color;

				string tmp = xlrc.ToString();
				xlrc = (XlRgbColor)interior.ThemeColor;
				tmp = xlrc.ToString();
				xlrc = XlRgbColor.rgbDarkBlue;
				WriteValue(RGB2String(Convert.ToInt64(xlrc)));

				Microsoft.Office.Interop.Excel.XlColorIndex ci = (Microsoft.Office.Interop.Excel.XlColorIndex)interior.ColorIndex;
				XlThemeColor tc = (XlThemeColor)interior.ThemeColor;
				tmp = ci.ToString();
				tmp = tc.ToString();
				tmp = ((XlThemeColor)interior.Color).ToString();
				tmp = ((XlRgbColor)interior.Color).ToString();
				XlPattern pattern = (XlPattern)interior.Pattern;
				WritePropertyName("Pattern");
				WriteValue(pattern.ToString());

			WriteEndObject();
		}
		private void CellFill2Json(Range theCell)
		{
			// if the cell doesn't have any fill, just return
			if (theCell.Interior.ColorIndex == (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexNone
					|| theCell.Interior.ColorIndex == (int)Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic)
				return;
			WritePropertyName("Fill");
			WriteStartObject();
			// dump the color/themecolor
			Color2Json(theCell.Interior );			
			WriteEndObject();
		}
		public static string BorderTypes()
		{
			string borderTypes = "Excel Cell Border Types:\n";
			foreach (XlBordersIndex value in Enum.GetValues(typeof(XlBordersIndex)))
			{
				borderTypes += string.Format("   {0,4:D}: {0:G}\n", value);
			}
			return borderTypes;
		}
		private void CellBorder2Json(Range theCell)
		{
			// if there aren't any borders, forget it
			if ( theCell.Borders.Count == 0 )
				return;
			XlLineStyle theLineStyle = XlLineStyle.xlLineStyleNone;
			Microsoft.Office.Interop.Excel.XlBorderWeight theBorderWeight;
			Microsoft.Office.Interop.Excel.XlColorIndex theColorIndex;
			XlThemeColor theThemeColor;
			Border	border;
			WritePropertyName("Borders");
			WriteStartArray();
			foreach (XlBordersIndex borderIndex in Enum.GetValues(typeof(XlBordersIndex)))
			{
				border = theCell.Borders[borderIndex];
				theLineStyle = (XlLineStyle)border.LineStyle;
				theBorderWeight = (Microsoft.Office.Interop.Excel.XlBorderWeight) border.Weight;
				theColorIndex = (Microsoft.Office.Interop.Excel.XlColorIndex)border.ColorIndex;
				if (theLineStyle != XlLineStyle.xlLineStyleNone )
				{
					WriteStartObject();
					WritePropertyName(string.Format("{0:G}", borderIndex));
					WriteStartObject();
					WritePropertyName("LineStyle");
					WriteValue(string.Format("{0:G}", theLineStyle));
					WritePropertyName("Weight");
					WriteValue(string.Format("{0:G}", theBorderWeight));
					WritePropertyName("ColorIndex");
					WriteValue(string.Format("{0:G}", theColorIndex));
					WritePropertyName("RGBColor");
					WriteValue(RGB2String(Convert.ToInt64(border.Color)));
					try
					{
						theThemeColor = (Microsoft.Office.Interop.Excel.XlThemeColor)border.ThemeColor;
						WritePropertyName("ThemeColor");
						WriteValue(string.Format("{0:G}", theThemeColor));
					}
					catch (Exception ex)
					{
						// not going to do anything, just not sure how to avoid
						Debug.Print("ex:" + ex.Message);
					}
					WriteEnd();
					WriteEndObject();
				}
			}
			WriteEndArray();
		}
		private void Cell2Json(Range theCell)
		{
			// differentiate between formulas, value2, value and text and formats, at least figure out what default
			// values are, and aren't
			// 
			string val = "", value1 = "", value2 = "";
			if (theCell.Value != null)
			{
				val = theCell.Value.ToString();
				value1 = val;
			}
			else if (theCell.Value2 != null)
			{
				val = theCell.Value2.ToString();
				value2 = val;
			}
			string v = theCell.Address.ToString();
			//if ( v == "$D$1" || v == "$A$25" || v == "$B$25" || v == "$E$25" )
			//	v = v;
			//v = cell.AddressLocal.ToString();
			WriteStartObject();
			WritePropertyName(v);
				WriteStartObject();
				WritePropertyName("Name");
				WriteValue(theCell.Address.ToString() );
				WritePropertyName("Type");
				WriteValue("Cell");

				CellFormat2Json(theCell);
				WritePropertyName("Content");
				WriteStartObject();
				
				WritePropertyName("Value");
				WriteValue(val);
				WritePropertyName("Text");
				WriteValue(theCell.Text);
				// check for formula
				if (theCell.HasFormula)
				{
					WritePropertyName("Formula");
					val = theCell.Formula;
					WriteValue(val);
					WritePropertyName("FormulaR1C1");
					val = theCell.FormulaR1C1;
					WriteValue(val);
				}
				WriteEnd();
				// include comments
				Comment comment = theCell.Comment;
				if ( comment != null )
				{
					WritePropertyName("Comment");
					val = comment.Text();
					WriteValue(val);
				}
				// CellFormat2Json(theCell);
				WriteEndObject();
			WriteEndObject();
			Debug.Write(theCell.Row.ToString() + "," + theCell.Column.ToString() + ":" + val);
		}

		private void XXX2Json(Worksheet sht)
		{

		}

		private void Chart2Json(Chart chart)
		{
			WriteStartObject();
			WritePropertyName("Name");
			WriteValue(chart.Name);
			WritePropertyName("Type");
			WriteValue("Chart");
			WritePropertyName("Title");
			WriteValue(chart.ChartTitle.Text);
			WritePropertyName("Font");
			WriteStartObject();
			WritePropertyName("Size");
			WriteValue(chart.ChartTitle.Font.Size);
			WriteEndObject();

			WritePropertyName("Orientation");
			XlOrientation xlo = (XlOrientation)chart.ChartTitle.Orientation;

			Debug.WriteLine(xlo.ToString());
			Debug.WriteLine(((XlOrientation)chart.ChartTitle.Orientation).ToString());
			WriteValue(((XlOrientation)chart.ChartTitle.Orientation).ToString());
			//			Microsoft.Office.Interop.Excel.XlOrientation.xlDownward.ToString();
				Debug.WriteLine("chart to string:" + chart.ToString());
				string val = "";
				string val2 = "";
				Microsoft.Office.Interop.Excel.SeriesCollection seriesCollection = chart.SeriesCollection();
				int x = chart.SeriesCollection().count();
				foreach (Series series in seriesCollection)
				{
					Debug.WriteLine("series:" + series.ChartType.ToString());
				}
				WritePropertyName("Value");
				WriteValue(val);
				WritePropertyName("Value2");
				WriteValue(val2);
			WriteEndObject();
		}
		private string RGB2String(long rgb)
		{
			string strRGB = "";
			strRGB = (rgb % 256).ToString();
			rgb /= 256;
			strRGB += ", " + (rgb % 256).ToString();
			rgb /= 256;
			strRGB += ", " + (rgb % 256).ToString();
			return strRGB;			
		}
	}
//Sub CheckSheet()
//    Dim condFmtRule As FormatCondition
//    Dim sht As Worksheet
//    Set sht = ActiveSheet
//    Dim wkbk As Workbook
//    Set wkbk = ActiveWorkbook
//    Dim points As Integer
//    Dim chkCell As Range
//' 1) save the workbook as WizardWorks Order Form. (-1 pt)
//' 3a) in cell C4, enter the customer name, Kevin Kemper (-1 pt)
    
//    Debug.Print CheckRequired("2a) In the Documentation sheet, enter your name in cell B3 (-1 pt)", _
//        wkbk.Worksheets("Documentation").Cells(3, 2))
//    Debug.Print CheckRequired("2a) In the Documentation sheet, enter the date in cell B4  (-1 pt)", _
//        wkbk.Worksheets("Documentation").Cells(4, 2))
//    Dim results As String
    
//    results = CheckValue("3a) in cell C4, enter the customer name:", "Kevin Kemper", "(-1 pt)", sht.Cells(4, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3b) In cell C6, enter the order number:", "28314", "(-1 pt)", sht.Cells(6, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3c) In C9 enter the following Address1:", "315 Avalon Street", "(-1 pt)", sht.Cells(9, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3d) In C10 enter Address2:", "", "(-1 pt)", sht.Cells(10, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3e) In C11 enter City:", "Greenfield", "(-1 pt)", sht.Cells(11, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3f) In C12 enter State:", "IN", "(-1 pt)", sht.Cells(12, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("3g) In C13 enter Zip:", "46140", "(-1 pt)", sht.Cells(13, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckFormula("4) In cell C5, enter a function that displays the current date.", "=TODAY()", "(-2 pts)", sht.Cells(5, 3).formula)
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5a) In B20 enter Item:", "BF001", "(-.5 pts)", sht.Cells(20, 2))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5b) In C20 enter Name:", "Bucket of Fireworks", "(-.5 pts)", sht.Cells(20, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5c) In D20 enter Price:", "45.75", "(-.5 pts)", sht.Cells(20, 4))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5d) In E20 enter Qty:", "1", "(-.5 pts)", sht.Cells(20, 5))
//    If Len(results) > 0 Then Debug.Print results
    
//' 5e) In B21 enter Item: NAF (-0.5 pts)
//' 5f) In C21 enter Name: Nightair Fountain (-0.5 pts)
//' 5g) In D21 enter Price: $ 12.95 (-0.5 pts)
//' 5h) In E21 enter Qty: 4 (-0.5 pts)
//    results = CheckValue("5e) In B21 enter Item:", "NAF", "(-.5 pts)", sht.Cells(21, 2))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5f) In C21 enter Name:", "Nightair Fountain", "(-.5 pts)", sht.Cells(21, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5g) In D21 enter Price:", "12.95", "(-.5 pts)", sht.Cells(21, 4))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5h) In E21 enter Qty:", "4", "(-.5 pts)", sht.Cells(21, 5))
//    If Len(results) > 0 Then Debug.Print results
//' 5i) In B22 enter Item:  (-0.5 pts)
//' 5j) In C22 enter Name: Mountain Rockets ( Box 20) (-0.5 pts)
//' 5k) In D22 enter Price: $ 55.25 (-0.5 pts)
//' 5l) In E22 enter Qty: 2 , this amount gets changed to 1 in step 15. (-0.5 pts)
//    results = CheckValue("5i) In B22 enter Item:", "MR20B", "(-.5 pts)", sht.Cells(22, 2))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5j) In C22 enter Name:", "Mountain Rockets (Box 20)", "(-.5 pts)", sht.Cells(22, 3))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5k) In D22 enter Price:", "55.25", "(-.5 pts)", sht.Cells(22, 4))
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckValue("5l) In E22 enter Qty, if not 1, deduct:", "2", "(-.5 pts)", sht.Cells(22, 5))
//    If sht.Cells(22, 5) <> "1" Then
//        If Len(results) > 0 Then Debug.Print results
//    End If


//' 6) In cell C15, enter overnight to ship this order overnight. This value gets changed to standard in step 16. (-1 pt)
//    results = CheckValue("6) In cell C15, enter overnight to ship this order overnight, if not standard deduct:.", "overnight", "(-1 pt)", sht.Cells(15, 3))
//    If sht.Cells(15, 3) <> "standard" Then
//        If Len(results) > 0 Then Debug.Print results
//    End If

//    results = CheckFormula("7a) In cell F20, enter an IF function that tests whether the order quantity in cell E20 is greater than 0(zero). If it is, return the value of cell E20 multiplied by cell D20; otherwise, return no text by entering “”", _
//        Array( _
//            "=IF(E20>0,D20*E20,"""")", _
//            "=IF(E20>0,D20*E20, """")", _
//            "=IF(E20>0,E20*D20,"""")", _
//            "=IF(E20>0,E20*D20, """")", _
//            "=IF(E20>0,D20*E20,"" "")" _
//            ), "(-5 pts)", sht.Cells(20, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
//' 7b) AutoFill this formula into the range F21:F25 (-4 pts)
//    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F21", _
//            Array( _
//            "=IF(E21>0,D21*E21,"""")", _
//            "=IF(E21>0,D21*E21, """")", _
//            "=IF(E21>0,E21*D21,"""")", _
//            "=IF(E21>0,E21*D21, """")", _
//            "=IF(E21>0,D21*E21,"" "")" _
//            ), "(-1 pt)", sht.Cells(21, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F22", _
//            Array( _
//            "=IF(E22>0,D22*E22,"""")", _
//            "=IF(E22>0,D22*E22, """")", _
//            "=IF(E22>0,E22*D22,"""")", _
//            "=IF(E22>0,E22*D22, """")", _
//            "=IF(E22>0,D22*E22,"" "")" _
//            ), "(-1 pt)", sht.Cells(22, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F23", _
//            Array( _
//            "=IF(E23>0,D23*E23, """")", _
//            "=IF(E23>0,D23*E23,"""")", _
//            "=IF(E23>0,E23*D23,"""")", _
//            "=IF(E23>0,E23*D23, """")", _
//            "=IF(E23>0,D23*E23,"" "")" _
//            ), "(-1 pt)", sht.Cells(23, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F24", _
//            Array( _
//            "=IF(E24>0,D24*E24,"""")", _
//            "=IF(E24>0,D24*E24, """")", _
//            "=IF(E24>0,E24*D24,"""")", _
//            "=IF(E24>0,E24*D24, """")", _
//            "=IF(E24>0,D24*E24,"" "")" _
//            ), "(-1 pt)", sht.Cells(24, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
//    results = CheckFormula("7b) AutoFill this formula into the range F21:F25, F25", _
//            Array( _
//            "=IF(E25>0,D25*E25,"""")", _
//            "=IF(E25>0,D25*E25, """")", _
//            "=IF(E25>0,E25*D25,"""")", _
//            "=IF(E25>0,E25*D25, """")", _
//            "=IF(E25>0,D25*E25,"" "")" _
//            ), "(-1 pt)", sht.Cells(25, 6).formula)
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckFormula("8) In cell F27, calculate the sum of the values in the range F20:F25", "=SUM(F20:F25)", _
//        "(-2 pts)", sht.Cells(27, 6).formula)
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckFormula("9) In cell F28, enter an IF function that tests whether cell F27 is greater than 200. If it is, return the value of cell F27 multiplied by the discount percentage in cell F12; otherwise, return the value 0(zero)", _
//        "=IF(F27>200,F12*F27,0)", "(-5 pts)", sht.Cells(28, 6).formula)
//    If Len(results) > 0 Then
//        ' check alternate forms
//        results = CheckFormula("9) In cell F28, enter an IF function that tests whether cell F27 is greater than 200. If it is, return the value of cell F27 multiplied by the discount percentage in cell F12; otherwise, return the value 0(zero)", _
//                "=IF(F27>200,F27*F12,0)", "(-5 pts)", sht.Cells(28, 6).formula)
//        If Len(results) > 0 Then Debug.Print results
//    End If

//    results = CheckFormula("10) In cell F29, subtract the discount value in cell F28 from the subtotal value in cell F27", _
//        Array("=F27-F28", "=(F27-F28)"), "(-2 pts)", sht.Cells(29, 6).formula)
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckFormula("11) In cell F31, calculate the sales tax by multiplying the after discount value in cell F29 by the sales tax percentage, 0.05", _
//        Array("=F29*0.05", "=(F29*0.05)"), "(-2 pts)", sht.Cells(31, 6).formula)
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckFormula("12) In cell F32, determine the shipping charge by entering an IF function that tests whether cell C15 equals “standard”. If it does, return the value in cell F9; otherwise, return the value in cell F10.", _
//        "=IF(C15=""standard"",F9,F10)", "(-5 pts)", sht.Cells(32, 6).formula)
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckFormula("13) In cell G32, display the value of cell C15 ", "=C15", "(-2 pts)", sht.Cells(32, 7).formula)
//    If Len(results) > 0 Then Debug.Print results
    
//    results = CheckFormula("14) In cell F34, calculate the total of the after discount value, the sales tax, and the shipping fee", _
//            Array("=F29+F31+F32", "=SUM(F29,F31,F32)"), "(-2 pts)", sht.Cells(34, 6).formula)
//    If Len(results) > 0 Then Debug.Print results
    
//    results = CheckValue("15) Reduce the quantity of Mountain Rockets boxes from 2 to 1, and then verify that the discount is changed to 0 for the order", "1", "(-1 pt)", sht.Cells(22, 5))
//    If Len(results) > 0 Then Debug.Print results

//    results = CheckValue("16) Change the shipping option from overnight to standard, and then verify that the shipping fee is changed to the fee for standard shipping.", _
//        "standard", " (-1 pt)", sht.Cells(15, 3))
//    If Len(results) > 0 Then Debug.Print results
        
//    Set sht = ActiveWorkbook.Worksheets(1)
//End Sub

//Function CheckValue(msg As String, reqdValue As String, pts As String, value As Variant) As String
//    If Len(reqdValue) = 0 Then
//        If Len("" & value) > 0 Then
//            CheckValue = msg & reqdValue & ", found " & value & pts
//        End If
//    ElseIf Len("" & value) = 0 Then
//        CheckValue = msg & reqdValue & "**MISSING**, " & pts
//    ElseIf reqdValue <> value Then
//        CheckValue = msg & reqdValue & ", found " & value & pts
//    Else
//        CheckValue = ""
//    End If
//End Function
//Function CheckFormula(msg As String, reqdFormula As Variant, pts As String, formula As Variant) As String
//    Dim chkFormula As Variant
//    Dim f As Integer
//    If Len("" & formula) = 0 Then
//        CheckFormula = msg & " should be:" & reqdFormula & "**MISSING**, " & pts
//    ElseIf IsArray(reqdFormula) Then
//        'For f = 0 To UBound(reqdFormula)
//        '    chkFormula = reqdFormula(f)
//        'Next
//        For Each chkFormula In reqdFormula
//            If chkFormula = formula Then
//                CheckFormula = ""
//                Exit Function
//            End If
//            ' check for use of SUM in formula when not needed
//            If Left(chkFormula, 4) <> "=SUM" Then
//                If Left(formula, 4) = "=SUM" Then
//                    CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & _
//                        ", the SUM function should not be used when entering formulas unless cells are being SUM'ed, (-1 pt)"
//                    Exit Function
//                End If
//            End If
//        Next
//        CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & pts
//    ElseIf reqdFormula <> formula Then
//        CheckFormula = msg & " should be:" & reqdFormula & ", found " & formula & pts
//        If Left(reqdFormula, 4) <> "=SUM" Then
//            If Left(formula, 4) = "=SUM" Then
//                CheckFormula = msg & " should be:" & reqdFormula(0) & ", found " & formula & _
//                    ", the SUM function should not be used when entering formulas unless cells are being SUM'ed, (-1 pt)"
//                Exit Function
//            End If
//        End If
//    Else
//        CheckFormula = ""
//    End If
//End Function

//Function GetRGB(c As Long) As String
    
//    GetRGB = (c Mod 256)
//    c = c / 256
//    GetRGB = GetRGB & ", " & (c Mod 256)
//    c = c / 256
//    GetRGB = GetRGB & ", " & (c Mod 256)
//End Function
//Function CheckRequired(field As String, value As Variant) As String
//    If Len("" & value) = 0 Then
//        CheckRequired = "**MISSING**"
//    Else
//        CheckRequired = "" & value
//    End If
//    CheckRequired = field & ":" & CheckRequired
//End Function
//Function VerifyFgBg(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fg As String, bg As String, msg As String) As Boolean
//    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
//    VerifyFgBg = True
//    Dim chkBG As Boolean
//    Dim chkFG As Boolean
//    chkBG = True
//    chkFG = True
//'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
//    For chkRow = begRow To endRow
//        For chkCol = begCol To endCol
//            Set chkCell = sht.Cells(chkRow, chkCol)
//            With chkCell
//            If Hex(.Font.Color) <> fg Then
//                If chkFG Then
//                    Debug.Print msg
//                    Debug.Print "Cell: " & .Address & ", Bad fg(" & Hex(.Font.Color) & ") s/b " & fg
//                    chkFG = False
//                End If
//                VerifyFgBg = False
//            End If
//            If Hex(.Interior.Color) <> bg Then
//                If chkBG Then
//                    Debug.Print msg
//                    Debug.Print "Cell: " & .Address & ", Bad bg(" & Hex(.Interior.Color) & ") s/b " & bg
//                    chkBG = False
//                End If
//                VerifyFgBg = False
//            End If
//            End With
//        Next
//    Next

//End Function

//Function VerifyFontSize(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, fontSize As Integer, msg As String) As Boolean
//    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
//    VerifyFontSize = True
//'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
//    For chkRow = begRow To endRow
//        For chkCol = begCol To endCol
//            Set chkCell = sht.Cells(chkRow, chkCol)
//            With chkCell
//            If .Font.Size <> fontSize Then
//                If VerifyFontSize Then
//                    Debug.Print msg
//                    Debug.Print "Cell: " & .Address & ", Bad fontSize(" & .Font.Size & ") s/b " & fontSize
//                    VerifyFontSize = False
//                End If
//            End If
//            End With
//        Next
//    Next
//End Function
//Function VerifyBorderColor(sht As Worksheet, begRow As Integer, begCol As Integer, endRow As Integer, endCol As Integer, borderColor As String, msg As String) As Boolean
//    Dim chkCell As Range, chkRow As Integer, chkCol As Integer
//    VerifyBorderColor = True
//    Dim ba As Boolean
//    ba = True
//    Dim bg As Boolean
//    bg = True
//'3a) Select the range B3:V3, and then change the font style to white text on a black background (-2 pts)
//    For chkRow = begRow To endRow
//        For chkCol = begCol To endCol
//            Set chkCell = sht.Cells(chkRow, chkCol)
//            With chkCell
//'            Debug.Print .BorderAround
//            If Not .BorderAround Then
//                If ba Then
//                    Debug.Print msg & "Missing border for " & .Address
//                    ba = False
//                End If
//            End If
//            ' color index = 48
//            ' linestyle = 1
//            ' themecolor = 1
//            ' value = 1
//            ' weight = 2
//            ' total of 6 borders, first 4 are the outside borders
//            If Hex(.Borders.Color) <> borderColor Then
//                ' only check the first one
//                If VerifyBorderColor = True Then
//                ' see if we have a black border
//                    If bc Then
//                        Debug.Print msg
//                        If .Borders.Color = 0 Then  ' black border
//                            Debug.Print "Cell: " & .Address & ", has automatic/black border instead of: " & borderColor
//                        Else
//                            Debug.Print "Cell: " & .Address & ", Bad borderColor(" & Hex(.Borders.Color) & ") s/b " & borderColor
//                        End If
//                        bc = False
//                    End If
//                    VerifyBorderColor = False
//                End If
//            End If
//            End With
//        Next
//    Next
//End Function

//Function FindRuleRGB(sht As Worksheet, ruleRange As String, ruleExpr As String, theRGB As String, Optional altRGB As String = "") As String
//'18 with fill color equal to the RGB color value ( 99, 37, 35)
//    Dim condFmtRule As FormatCondition
//    FindRuleRGB = ""
//    For Each condFmtRule In sht.Cells.FormatConditions
//        With condFmtRule
//            Dim loc As Integer
//            'loc = InStr(1, ruleRange, "" & .AppliesTo.Address)
//            loc = InStr(1, "" & .AppliesTo.Address, ruleRange)
//            ' Debug.Print loc & "-" & .AppliesTo.Address & ", formula:[" & ruleExpr; "/" & .Formula1 & "]"
//            ' the rule can apply to multiple ranges
//            If (loc > 0) And (ruleExpr = .Formula1) Then
//                FindRuleRGB = GetRGB(.Interior.Color)
//                If FindRuleRGB <> theRGB Then
//                    ' oops, rule doesn't match, allow for alternate value
//                    If FindRuleRGB <> altRGB Then
//                        FindRuleRGB = "Rule RGB mismatch at: " & ruleRange & " expression: " & ruleExpr & _
//                            ", found rgb: " & FindRuleRGB & ", s/b: " & theRGB
//                        Debug.Print FindRuleRGB
//                        Exit Function
//                    Else
//                        FindRuleRGB = ""
//                        Exit Function
//                    End If
//                Else
//                    FindRuleRGB = ""
//                    Exit Function
//                End If
//            End If
//        End With
//    Next
//    FindRuleRGB = "Rule not found: " & ruleRange & " expression: " & ruleExpr & _
//        ", rgb: " & theRGB
//    Debug.Print FindRuleRGB
//End Function





}
