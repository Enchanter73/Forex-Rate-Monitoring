using Log;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infastructure.Helpers
{
    public static class LogHelper
    {
        public static void Log(LogModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (string.IsNullOrWhiteSpace(model.Layer))
            {
                model.Layer = "ForexRateMonitoring.Data";
            }

            model.MachineName = Environment.MachineName;

            Logger.Redis(model);
        }
    }
}
