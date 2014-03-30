using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Text;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace ZLib.ZRubric
{
	public class ZTaskDeduction : ZObject<ZTaskDeduction>
	{
		public ZTaskDeduction(ZObject<ZTaskDeduction> obj) : base(obj) { }
		public ZTaskDeduction(JObject obj) : base(obj) { }
		public ZTaskDeduction() { }
		public ZTaskDeduction(ZTask task, ZScenario scenario)
			: base()
		{
			this.task = new ZTask(task);
			this.task.activeStep = task.activeStep;
			this.task.zScenarios.jArray.RemoveAll();	// we don't care about the other scenarios, only this one
			this.task.zScenarios.Add(scenario);			// not sure if I need to do both of these
			this.scenario = scenario;
			stepID = task.parentStep.key;
		}
		public ZTask task
		{
			get 
			{
				ZTask val = (ZTask)((JObject)GetObjectValue(ZTask.itemTag));
				if (val == null)
				{
					val = new ZTask();
					(m_jToken as JObject).Add(ZTask.itemTag, (JObject)val);
				}
				return val;
				//return (ZTask)get(MethodInfo.GetCurrentMethod()); 
			}
			set 
			{ 
				set(MethodInfo.GetCurrentMethod(), (JObject)value); 
				//stepID = value.activeStep.key;
			}
		}
		public string stepID
		{
			get { return (string)get(MethodInfo.GetCurrentMethod()); }
			set { set(MethodInfo.GetCurrentMethod(), (JValue)value); }
		}
		public ZScenario scenario
		{
			get
			{

				ZScenario val = task.zScenarios[0];		// (ZScenario)((JObject)GetObjectValue(ZScenario.itemTag));
				if (val == null)
				{
					val = new ZScenario();
					(m_jToken as JObject).Add(ZScenario.itemTag, (JObject)val);
				}
				return val;
				//return (ZScenario)get(MethodInfo.GetCurrentMethod()); 
			}
			set { set(MethodInfo.GetCurrentMethod(), (JObject)value); }
		}
		public double deduct 
		{ 
			get 
			{ 
                double value = ZRubric.activeProject.pts(task.mapping, scenario.deduction);
                return value;
			}
		}
        public double pointsDeducted
        {
            get { return (double)get(MethodInfo.GetCurrentMethod()); }
            set { set(MethodInfo.GetCurrentMethod(), (JValue)value); }
        }
    }
	public class ZTaskDeductions : ZObjects<ZTaskDeductions, ZTaskDeduction>
	{
		public ZTaskDeductions(JObject rubric) : base(rubric[ZTaskDeductions.itemTag] as JArray) { }
		public ZTaskDeductions(JArray jArray) : base(jArray) { }
		public ZTaskDeductions() : base(new JArray()) { }
        public override void Add(ZTaskDeduction taskDeduction)
        {
            base.Add(taskDeduction);
            // now update the allocations
            ZAllocation allocation = ZRubric.activeSubmission.allocations[taskDeduction.scenario.parentMapping.category];
            double deduction = taskDeduction.deduct;
            // update the deduction information with actual amount deducted, which is based on the
            // amount that has been deducted vs. the max allowed
            allocation.actual += deduction;
            if (allocation.max <= allocation.total)
                deduction = 0;
            else if (deduction > (allocation.max - allocation.total))
                deduction = allocation.max - allocation.total;

            allocation.total += deduction;
            taskDeduction.pointsDeducted = deduction;
            taskDeduction.scenario.deduction.pointsDeducted = deduction;
        }
	}

}

