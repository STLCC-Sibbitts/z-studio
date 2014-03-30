using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;
using ZLib;
using ZLib.ZRubric;
using Excel2Json;
using Grader;
using System.Reflection;
using System.Linq.Expressions;

namespace TestStuff
{
    public partial class frmPreferences : Form
    {
        private bool m_initializing;
        public frmPreferences()
        {
            InitializeComponent();
            m_initializing = true;
            chkPartialCredit.Checked = ZRubric.activePreferences.partialCredit.enabled;
            // initialize Rounding
            if (!ZRubric.activePreferences.rounding.enabled)
            {
                radRoundingNone.Checked = true;
            }
            else
            {
                RadioButton radButton = grpRounding.Controls["radRounding" + ZRubric.activePreferences.rounding.factor] as RadioButton;
                radButton.Checked = true;
            }
			// Loop through the groups on each tab
			foreach (Control groupControl in grpMultipliers.Controls)
			{
				if ( groupControl is GroupBox )
					LoadGroupSettings(groupControl as GroupBox);
			}
			// now check the content preferences
			// make this recursive, attach notes, if present to the node
//			tn.ToolTipText = "Assign notes text here, and display in notes box below";
			ZContentPreferences contentPreferences = ZRubric.activePreferences.content;
			ZThresholdPreference preference = contentPreferences.thresholdPreference("Create.Value.Text") as ZThresholdPreference;

			tvContentDefaultSceenarios.Nodes.Clear();
			TreeNode tn = tvContentDefaultSceenarios.Nodes.Add("Default Scenarios");
			AddContentPreferences(tn.Nodes, contentPreferences);
			tvContentDefaultSceenarios.ExpandAll();
			//foreach (JToken jTok in contentPreferences.Properties)
			//{
			//	string prefName = ZRubric.OName(jTok);
			//	object pref = contentPreferences.thresholdPreference(prefName);
			//	if (pref is ZThresholdPreference)
			//		preference = pref as ZThresholdPreference;
			//	else
			//	{

			//	}
			//	JToken hasEnabled = jTok.First.SelectToken("Enabled");

			//}
//			bool enabled = (bool)zPreference[chkDifficulties.Tag.ToString()].Value;
			//JToken tokPreference = ZRubric.activePreferences.SelectToken("[Difficulties].Enabled");	//chkDifficulties.Tag.ToString());
			//zPreference = new ZPreference(tokPreference);

			//for (int itemIndex = 0; itemIndex < clbEnablePreferences.Items.Count; ++itemIndex)
			//{
			//	string preferenceName = clbEnablePreferences.Items[itemIndex].ToString();
//			ZPreference zPreference = ZRubric.activePreferences[preferenceName];
			//	clbEnablePreferences.SetItemChecked(itemIndex, zPreference.enabled);
			//}
            m_initializing = false;
        }
		private void AddContentPreferences(TreeNodeCollection treeNodes, ZContentPreferences contentPreferences)
		{
			ZThresholdPreference preference = null;
			ZContentPreferences contentPref = null;
			TreeNode tn = null;
			if ( contentPreferences.Properties == null )
				return;
			foreach (JToken jTok in contentPreferences.Properties)
			{
				string prefName = ZRubric.OName(jTok);
				// skip over special cases
				if ( prefName == "Notes" || prefName == "ZUid" )
					continue;
				object pref = contentPreferences.thresholdPreference(prefName);
				tn = treeNodes.Add(prefName);
				tn.Tag = pref;
				if (pref is ZThresholdPreference)
				{
					preference = pref as ZThresholdPreference;
					tn.ToolTipText = preference.notes;
				}
				else if (pref is ZContentPreferences)
				{
					contentPref = pref as ZContentPreferences;
					tn.ToolTipText = contentPref.notes;
					AddContentPreferences(tn.Nodes, contentPref);
				}
				else
				{
					// what???
					object what = pref;
				}
			}

		}
		//private void clbEnablePreferences_SelectedIndexChanged(object sender, EventArgs e)
		//{
		//	string preference = clbEnablePreferences.SelectedItem.ToString();
		//	ZPreference zPreference = ZRubric.activePreferences[preference];

		//}

		//private void clbEnablePreferences_SelectedValueChanged(object sender, EventArgs e)
		//{
		//	string preference = clbEnablePreferences.SelectedItem.ToString();
		//	ZPreference zPreference = ZRubric.activePreferences[preference];
		//	zPreference.enabled = clbEnablePreferences.GetItemChecked(clbEnablePreferences.SelectedIndex);
		//}

        private void btnOK_Click(object sender, EventArgs e)
        {
			if (tabBasicPreferences.Focused)
			{
				SaveGroupSettings(grpDifficulties.Controls);
				SaveGroupSettings(grpDeductions.Controls);
				SaveGroupSettings(grpSkillLevels.Controls);
			}
			// make sure we've loaded something here.
			if ( (tabContentPreferences.Focused || tabsPreferences.SelectedTab == tabContentPreferences )&&  chkScenarioEnabled.Tag is JValue )
				SaveGroupSettings(grpDefaultScenario.Controls);

			ZRubric.SavePreferences();
        }

        private void radRounding_CheckedChanged(object sender, EventArgs e)
        {
            if (m_initializing) return;
            // update preferences
            RadioButton radButton = sender as RadioButton;
            if (radButton.Checked)
            {
                if ( radButton.Tag.ToString() == "0" )
                {
                    ZRubric.activePreferences.rounding.enabled = false;
                    ZRubric.activePreferences.rounding.factor = int.Parse(radButton.Tag.ToString());
                }
                else
                {
                    ZRubric.activePreferences.rounding.enabled = true;
                    ZRubric.activePreferences.rounding.factor = int.Parse( radButton.Tag.ToString() );
                }
            }
        }
		private void EnableGroupSettings(Control.ControlCollection controlCollection, bool isEnabled)
		{
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
				else if ( !(control is Label) )
					control.Enabled = isEnabled;
					
			}
		}
		private void LoadGroupSettings(Control.ControlCollection controlCollection, object zPreference)
		{
		
			object preferenceValue;
			//Debug.Print("Group control:" + groupControl.Name);
			foreach (Control control in controlCollection)
			{
				if (control.Tag == null)
					continue;
				ComboBox cboControl = null;
				if ( control.Name.StartsWith("cbo") )
					cboControl = control as ComboBox;
				// the tag will either be the name of the preference, or it will be the preference object itself
				string tag = "";
				preferenceValue = null;
				if (control.Tag is string)
				{
					tag = control.Tag.ToString();
					if (tag.Length == 0)
						continue;
					if ( zPreference is ZPreference )
						preferenceValue = (zPreference as ZPreference)[tag];
					else if (zPreference is ZThresholdPreference)
					{
//						tag = groupControl.Tag + "." + tag;
						if (tag == "Category" || tag == "Deduction")
							tag = tag.ToString();
						//preferenceValue = (zPreference as JToken).Value<JValue>(tag);
						preferenceValue = (zPreference as ZThresholdPreference).SelectToken(tag);
						// if the preference value is null, we have a content preference?
					}
					
					if (preferenceValue == null )
					{
						if (tag == "Category" || tag == "Deduction")
							tag = tag.ToString();
						preferenceValue = (controlCollection.Owner.Tag as JToken).Value<JValue>(tag);
					}
					control.Tag = preferenceValue;
				}
				else if (control.Tag is ZPreference)
				{
					preferenceValue = control.Tag as object;
				}
				Debug.Print("Text control:" + control.Name);
				// based on the type, grab a particular value
				if (control is CheckBox)
				{
					bool value = bool.Parse(preferenceValue.ToString());
					(control as CheckBox).Checked = value;
					EnableGroupSettings(controlCollection, value);
				}
				else if (control is MaskedTextBox)
				{
					MaskedTextBox mtb = (control as MaskedTextBox);
					// if this is a numeric mask, which it should be, convert accordingly
					if (mtb.Mask.Contains('0'))
					{
						double value = double.Parse(preferenceValue.ToString());
						mtb.Text = value.ToString(mtb.Mask.ToString());
					}
				}
				else if (control is TextBox)
				{
					(control as TextBox).Text = preferenceValue.ToString();
				}
				else if (control is RichTextBox)
				{
					(control as RichTextBox).Text = preferenceValue.ToString();
				}
				else if (control is ComboBox)
				{
					ComboBox cbo = control as ComboBox;
					cbo.SelectedItem = preferenceValue.ToString();
					cbo.Update();
					cbo.Refresh();
				}
				else if (control is NumericUpDown)
				{
					NumericUpDown nud = (control as NumericUpDown);
					(control as NumericUpDown).Value = int.Parse(preferenceValue.ToString());
					control.Text = preferenceValue.ToString();
					nud.Refresh();
				}
				else if (control is GroupBox)
				{
					LoadGroupSettings((control as GroupBox).Controls, zPreference);
				}
				else if (control is Panel)
				{
					LoadGroupSettings((control as Panel).Controls, preferenceValue);
				}
			}

		}
		private void LoadGroupSettings(GroupBox groupControl)
		{
			// go through each of the controls in this group
			ZPreference zPreference = ZRubric.activePreferences[groupControl.Tag.ToString()];
			LoadGroupSettings(groupControl.Controls, zPreference);
		}
		private void ResetGroupSettings(Control.ControlCollection controls)
		{
			Debug.Print("Group control:" + controls.Owner.Name);
			foreach (Control control in controls)
			{
				if (control.Name.StartsWith("cbo"))
				{
					ComboBox cbo = control as ComboBox;
				}
				if (control.Tag == null || control.Tag is string)
					continue;
				// the tag will either be the name of the preference, or it will be the preference object itself
				string tag = ZRubric.OName(control.Tag as JToken);
				if ( tag.Length == 0 )
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
			EnableGroupSettings(controls,true);
			if (!(controls.Owner.Tag is string))
			{
				string tag = ZRubric.OName(controls.Owner.Tag as JToken);
				controls.Owner.Tag = ZRubric.OName(controls.Owner.Tag as JToken);
			}
		}
		// TODO: add feacture to detect when options have changed or form is dirty
		private void SaveGroupSettings(Control.ControlCollection controls)
		{
			// go through each of the controls in this group
			JValue preferenceValue = null;
//			ZPreference zPreference = ZRubric.activePreferences[controls.Owner.Tag.ToString()];
			Debug.Print("Group control:" + controls.Owner.Name);
			foreach (Control control in controls)
			{
				if (control.Name.StartsWith("cbo"))
				{
					ComboBox cbo = control as ComboBox;
				}
				if (control.Tag == null)
					continue;
				// the tag will either be the name of the preference, or it will be the preference object itself
				preferenceValue = null;
				if (control.Tag is JValue)
				{
					preferenceValue = control.Tag as JValue;
				}
				Debug.Print("Text control:" + control.Name);
				// based on the type, grab a particular value
				if (control is CheckBox)
				{
					preferenceValue.Value = (control as CheckBox).Checked;
				}
				else if (control is MaskedTextBox)
				{
					MaskedTextBox mtb = (control as MaskedTextBox);
					// if this is a numeric mask, which it should be, convert accordingly
					if (mtb.Mask.Contains('0'))
					{
						double value = double.Parse(mtb.Text);
						preferenceValue.Value = value;
					}
					else
						preferenceValue.Value = mtb.Text;
				}
				else if (control is TextBox)
				{
					preferenceValue.Value = (control as TextBox).Text;
				}
				else if (control is RichTextBox)
				{
					preferenceValue.Value = (control as RichTextBox).Text;
				}
				else if (control is ComboBox)
				{
					ComboBox cbo = control as ComboBox;
					preferenceValue.Value = cbo.SelectedItem;
				}
				else if (control is NumericUpDown)
				{
					NumericUpDown nud = (control as NumericUpDown);
					// check the value, if it is a whole number, save as an int
					int	nudInt;
					if ( int.TryParse(nud.Value.ToString(), out nudInt) )
						preferenceValue.Value = nudInt;
					else
						preferenceValue.Value = nud.Value;
				}
				else if (control is GroupBox)
				{
					SaveGroupSettings((control as GroupBox).Controls);
				}
				else if (control is Panel)
				{
					SaveGroupSettings((control as Panel).Controls);
				}
			}
		}
		private void chkMultiplierOptions_CheckedChanged(object sender, EventArgs e)
		{
			CheckBox chkSettings = sender as CheckBox;
			EnableGroupSettings((chkSettings.Parent as GroupBox).Controls, chkSettings.Checked);
		}

		private void tvContentDefaultSceenarios_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{

		}

		private void tvContentDefaultSceenarios_AfterSelect(object sender, TreeViewEventArgs e)
		{
			txtScenarioPath.Text = e.Node.FullPath;
			if (e.Node.Tag is ZThresholdPreference)
			{
				ZThresholdPreference pref = e.Node.Tag as ZThresholdPreference;
				EnableGroupSettings(grpDefaultScenario.Controls, pref.enabled);
				LoadGroupSettings(grpDefaultScenario.Controls, e.Node.Tag);
			}
			else
			{
				ResetGroupSettings(grpDefaultScenario.Controls);
				EnableGroupSettings(grpDefaultScenario.Controls, false);
			}
		}

		private void tvContentDefaultSceenarios_BeforeSelect(object sender, TreeViewCancelEventArgs e)
		{
			// reset
			if (e.Node.Tag != null && (!(e.Node.Tag is string)))
			{
				// TODO: get more sophisticated and record to temp area and confirm commit
//				if (e.Node.Tag is ZThresholdPreference)
//					SaveGroupSettings(grpDefaultScenario.Controls);
				ResetGroupSettings(grpDefaultScenario.Controls);
			}
		}
    }
}
