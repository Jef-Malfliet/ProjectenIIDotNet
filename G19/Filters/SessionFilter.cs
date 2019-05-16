using G19.Models.State_Pattern;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace G19.Filters {
    public class SessionFilter : ActionFilterAttribute {

        private SessionState _sessie;

        public override void OnActionExecuting(ActionExecutingContext context) {
            _sessie = ReadSessionObject(context.HttpContext);
            context.ActionArguments["sessie"] = _sessie;
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context) {
            WriteSessionObject(_sessie, context.HttpContext);
            base.OnActionExecuted(context);
        }

        private SessionState ReadSessionObject(HttpContext context) {
            _sessie =  context.Session.GetString("sessie") == null ?
                new SessionState() : JsonConvert.DeserializeObject<SessionState>(context.Session.GetString("sessie"));
            return _sessie;
        }

        private void WriteSessionObject(SessionState sessie, HttpContext context) {
            context.Session.SetString("sessie", JsonConvert.SerializeObject(sessie));
        }
    }
}

