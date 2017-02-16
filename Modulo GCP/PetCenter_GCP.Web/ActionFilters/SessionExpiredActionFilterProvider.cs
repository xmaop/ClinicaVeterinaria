using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PetCenter_GCP.Web.ActionFilters
{
    public class SessionExpiredActionFilterProvider : IFilterProvider
    {
        private IList<ControllerAction> actions = new List<ControllerAction>();

        public void Add(string controllerName, string actionName)
        {
            actions.Add(new ControllerAction()
            {
                ControllerName = controllerName,
                ActionName = actionName
            });
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (ControllerAction action in actions)
            {
                if ((action.ControllerName == actionDescriptor.ControllerDescriptor.ControllerName || action.ControllerName == "*")
                    && (action.ActionName == actionDescriptor.ActionName || action.ActionName == "*"))
                {
                    yield return new Filter(new SessionExpiredActionFilter(), FilterScope.First, null);
                    break;
                }
            }
            yield break;
        }
    }

    internal class ControllerAction
    {
        internal string ControllerName { get; set; }
        internal string ActionName { get; set; }
    }
}