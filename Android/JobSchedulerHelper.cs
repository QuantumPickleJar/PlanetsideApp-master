using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.App.Job;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace PsApp.Droid
{
    /// <summary>
    /// Creates a JobInfo.Builder with an Android Context (like an Activity)
    /// </summary>
    public static class JobSchedulerHelper
    {
        public static JobInfo.Builder CreateJobBuilderUsingJobID<T>(this Context context, int jobId) where T : JobService
        {
            var javaClass = Java.Lang.Class.FromType(typeof(T));
            var componentName = new ComponentName(context, javaClass);
            return new JobInfo.Builder(jobId, componentName);
        }

    }
    
}