using G19.Models.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;

namespace G19.Filters {
    public class LidFilter : ActionFilterAttribute {
        private readonly ILidRepository _lidRepository;

        public LidFilter(ILidRepository lidRepository) {
            _lidRepository = lidRepository;
        }

        public override void OnActionExecuting(ActionExecutingContext context) {
            context.ActionArguments["lid"] = context.HttpContext.User.Identity.IsAuthenticated ? _lidRepository.GetByEmail(context.HttpContext.User.Identity.Name) : null;
            base.OnActionExecuting(context);
        }
    }
}