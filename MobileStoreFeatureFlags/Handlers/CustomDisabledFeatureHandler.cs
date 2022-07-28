using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement.Mvc;

namespace MobileStoreFeatureFlags.Handlers
{
    public class DisabledFeaturesHandler : IDisabledFeaturesHandler
    {
        public Task HandleDisabledFeatures(IEnumerable<string> features, ActionExecutingContext context)
        {
            context.Result = new ContentResult
            {
                ContentType = "text/plain",
                Content = $"This feature is not available, please try again later - {String.Join(',',features)}",
                StatusCode = 404
            };

            return Task.CompletedTask;
        }
    }
}
